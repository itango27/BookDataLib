using HelperLib.Helpers;
using HelperLib.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDataLib.Model
{
    public partial class VRelsView
    {
        public string foreign_key_name { get; set; }
        public string referring_table { get; set; }
        public string referring_column { get; set; }
        public string referenced_table { get; set; }
        public string referenced_column { get; set; }

        public VRelsView() { }

        public static List<VRelsView> Read()
        {
            var sql = $"SELECT * from VRelsTreeView";

            var dbContext = ServiceLocator.Get<BookDataDbContext>();

            return dbContext.VRelsViews.ToList();
        }

        public static (List<GraphNode> Nodes, List<GraphEdge> Edges) BuildGraphFromVRels(List<VRelsView> vrels)
        {
            // Step 1: Unique nodes by name
            var nodeDict = vrels
                .SelectMany(v => new[] { v.referring_table, v.referenced_table })
                .Distinct()
                .ToDictionary(name => name, name => new GraphNode(name));

            // Step 2: Create edges using GraphNode instances
            var edges = vrels
                .Select(v =>
                    new GraphEdge(
                        nodeDict[v.referring_table],
                        nodeDict[v.referenced_table]
                    )
                ).ToList();

            var nodes = nodeDict.Values.ToList();

            return (nodes, edges);
        } 

        public static string GenerateVRelsPathQuery(string fromTable, string toTable, int maxDepth = 5)
        {
            return $@"
        WITH FKPaths AS (
            SELECT
                CAST(referring_table + ' → ' + referenced_table AS VARCHAR(MAX)) AS Path,
                referring_table,
                referenced_table,
                1 AS Depth
            FROM VRelsView
            WHERE referring_table = '{fromTable}'

            UNION ALL

            SELECT
                CAST(fp.Path + ' → ' + v.referenced_table AS VARCHAR(MAX)) AS Path,
                fp.referring_table,
                v.referenced_table,
                fp.Depth + 1
            FROM FKPaths fp
            JOIN VRelsView v ON fp.referenced_table = v.referring_table
            WHERE fp.Depth < {maxDepth}
        )
        SELECT DISTINCT Path
        FROM FKPaths
        WHERE referenced_table = '{toTable}'
    ";
        }

        public static void CreateRelsTreeView()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("CREATE VIEW VRelsTreeView as");
            sbSql.Append(" with Rels(referenced_table, referring_table, level) as (");
            sbSql.Append(" select referenced_table, referring_table, 0 as level from VRelsView a1");
            sbSql.Append(" union all");
            sbSql.Append(" select a.referenced_table, a.referring_table, level + 1 as level from VRelsView a inner join Rels b on a.referring_table = b.referenced_table");
            sbSql.Append(" ) select * from Rels");

            ExecRawSql(sbSql.ToString());
        }

        public static void CreateVRelsView()
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append("CREATE VIEW VRelsView as");
            sbSql.Append(" SELECT fk1.name as foreign_key_name, SCHEMA_NAME(t1.schema_id) as referring_schema, t1.name AS referring_table, c1.name AS referring_column, SCHEMA_NAME(t2.schema_id) as referenced_schema, t2.name AS referenced_table, c2.name AS referenced_column, COUNT(*) AS Ct");
            sbSql.Append(" FROM sys.foreign_keys fk1, sys.foreign_key_columns fkc, sys.columns c1, sys.columns c2, sys.tables t1, sys.tables t2");
            sbSql.Append(" WHERE t1.object_id = c1.object_id");
            sbSql.Append(" AND t2.object_id = c2.object_id");
            sbSql.Append(" AND fk1.parent_object_id = t1.object_id");
            sbSql.Append(" AND fk1.referenced_object_id = t2.object_id");
            sbSql.Append(" AND fk1.object_id = fkc.constraint_object_id");
            sbSql.Append(" AND c1.column_id = fkc.parent_column_id");
            sbSql.Append(" AND c2.column_id = fkc.referenced_column_id");
            sbSql.Append(" GROUP BY fk1.name, SCHEMA_NAME(t1.schema_id), t1.name, c1.name, SCHEMA_NAME(t2.schema_id), t2.name, c2.name");
            //sbSql.Append(" ORDER BY fk1.name");

            ExecRawSql(sbSql.ToString());
        }


        public static int ExecRawSql(string sql)
        {
            int rowsAffected = 0;
            Logger.Log(String.Format("ExecRawSql Start"));
            try
            {
                var dbContext = ServiceLocator.Get<BookDataDbContext>();

                // NB For DDL statements, such as CREATE TABLE or ALTER TABLE, the return value is 0.
                rowsAffected = dbContext.Database.ExecuteSqlRaw(sql);
            }
            catch (Exception ex)
            {
                Logger.Log(String.Format("ExecRawSql exception: {0}", ex.Message));
            }
            finally
            {
                Logger.Log(String.Format("ExecRawSql: rowsRead: {0}", rowsAffected));
            }
            Logger.Log(String.Format("ExecRawSql end"));
            return rowsAffected;
        }

    }
}
