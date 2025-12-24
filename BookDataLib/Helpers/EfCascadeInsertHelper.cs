using BookDataLib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BookDataLib.Helpers;

public static class EfCascadeInsertHelper
{
    public static void nsert(DbContext context)
    {
        var scene = new Scene { Id = 1, Title = "Climax" };
        var thread = new NarrativeThread { Id = 1, Name = "Hero's Journey" };
        var evt = new BookEvent { Id = 1, Description = "The battle", SceneId = 1, ThreadId = 1 };

        var allData = new Dictionary<Type, IEnumerable<object>>
        {
            [typeof(Scene)] = new[] { scene },
            [typeof(NarrativeThread)] = new[] { thread },
            [typeof(BookEvent)] = new[] { evt },
        };

        EfCascadeInsertHelper.InsertEntitiesInDependencyOrderAsync(context, allData).RunSynchronously();
    }

    public static async Task InsertEntitiesInDependencyOrderAsync(
        DbContext dbContext,
        Dictionary<Type, IEnumerable<object>> entitiesByType)
    {
        var insertionOrder = EfDependencyOrderHelper.GetEntityInsertionOrder(dbContext);

        foreach (var type in insertionOrder)
        {
            if (!entitiesByType.TryGetValue(type, out var entities)) continue;

            //var dbSet = dbContext.Set(type);
            var dbSet = (IQueryable<object>)dbContext.GetType()
                        .GetMethod("Set", Type.EmptyTypes)!
                        .MakeGenericMethod(type)
                        .Invoke(dbContext, null)!;

            //foreach (var entity in entities)
            //    dbSet.Add(entity);
            var addMethod = dbContext.GetType().GetMethod("Add");
            foreach (var entity in entities)
            {
                addMethod!.Invoke(dbContext, new[] { entity });
            }

            await dbContext.SaveChangesAsync();
        }
    }

}
