using BookDataLib.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace BookDataLib.Helpers;

public static class EfCascadeDeleteHelper
{
    public static void DeleteAll(DbContext context)
    {
        EfCascadeDeleteHelper.DeleteAllEntitiesInDependencyOrderAsync(context).RunSynchronously();
    }

    public static void DeleteFiltered(DbContext context)
    {
        int targetSceneId = 42;

        EfCascadeDeleteHelper.DeleteAllEntitiesInDependencyOrderAsync(context, (type, queryable) =>
        {
            if (type == typeof(BookEvent))
                return queryable.Cast<BookEvent>().Where(e => e.SceneId == targetSceneId);
            if (type == typeof(MotifScene))
                return queryable.Cast<MotifScene>().Where(ms => ms.SceneId == targetSceneId);
            if (type == typeof(LayerScene))
                return queryable.Cast<LayerScene>().Where(ls => ls.SceneId == targetSceneId);
            if (type == typeof(Scene))
                return queryable.Cast<Scene>().Where(s => s.Id == targetSceneId);
            return queryable;
        }).RunSynchronously();
    }

    public static async Task DeleteAllEntitiesInDependencyOrderAsync(
        DbContext dbContext,
        Func<Type, IQueryable<object>, IQueryable<object>>? filterOverride = null)
    {
        var deletionOrder = GetEntityDeletionOrder(dbContext);

        foreach (var type in deletionOrder)
        {
            //var set = dbContext.Set(type);
            var dbSet = (IQueryable<object>)dbContext.GetType()
            .GetMethod("Set", Type.EmptyTypes)!
            .MakeGenericMethod(type)
            .Invoke(dbContext, null)!;

            IQueryable<object> query = dbSet;

            // Optional: allow caller to override/filter per type
            if (filterOverride != null)
                query = filterOverride(type, query);

            var toDelete = await query.ToListAsync();

            if (toDelete.Any())
            {
                dbContext.RemoveRange(toDelete);
                await dbContext.SaveChangesAsync();
            }
        }
    }

    public static List<Type> GetEntityDeletionOrder(DbContext context)
    {
        var model = context.Model;
        var graph = new Dictionary<Type, List<Type>>();

        foreach (var entityType in model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;
            if (!graph.ContainsKey(clrType))
                graph[clrType] = new List<Type>();

            foreach (var fk in entityType.GetForeignKeys())
            {
                var principal = fk.PrincipalEntityType.ClrType;
                if (!graph.ContainsKey(principal))
                    graph[principal] = new List<Type>();

                graph[clrType].Add(principal); // Dependent → Principal
            }
        }

        return TopologicalSort(graph);
    }

    private static List<Type> TopologicalSort(Dictionary<Type, List<Type>> graph)
    {
        var result = new List<Type>();
        var visited = new Dictionary<Type, bool>();

        void Visit(Type node)
        {
            if (visited.TryGetValue(node, out var inProcess))
            {
                if (inProcess)
                    throw new InvalidOperationException($"Cycle detected at {node.Name}");
                return;
            }

            visited[node] = true;

            foreach (var neighbor in graph[node])
                Visit(neighbor);

            visited[node] = false;
            if (!result.Contains(node))
                result.Add(node);
        }

        foreach (var node in graph.Keys)
            Visit(node);

        return result;
    }
}
