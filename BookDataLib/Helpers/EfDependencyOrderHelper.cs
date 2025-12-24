using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace BookDataLib.Helpers;

public static class EfDependencyOrderHelper
{
    public static void ShowDeletionOrder(DbContext dbContext)
    {
        var deletionOrder = EfDependencyOrderHelper.GetEntityDeletionOrder(dbContext);
        Console.WriteLine("Safe deletion order (children to parents):");

        foreach (var type in deletionOrder)
        {
            Console.WriteLine(type.Name);
        }
    }
    public static void ShowInsertionOrder(DbContext dbContext)
    {
        var insertOrder = EfDependencyOrderHelper.GetEntityInsertionOrder(dbContext);
        Console.WriteLine("Safe insertion order (parents to children):");
        foreach (var type in insertOrder)
            Console.WriteLine(type.Name);
    }

    public static List<Type> GetEntityInsertionOrder(DbContext context)
    {
        var model = context.Model;
        var graph = new Dictionary<Type, List<Type>>();

        // Build graph: Parent → Children
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

                // Principal → Dependent
                graph[principal].Add(clrType);
            }
        }

        return TopologicalSort(graph);
    }

    public static List<Type> GetEntityDeletionOrder(DbContext context)
    {
        var model = context.Model;
        var graph = new Dictionary<Type, List<Type>>();

        // Build graph: Child → Parent
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

                // Dependent → Principal
                graph[clrType].Add(principal);
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
