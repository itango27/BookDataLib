using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookDataLib.Graph.StateModelBuilder;

namespace BookDataLib.Model
{
    public class StateModelToGraphConverter
    {
        public List<GraphNode> Nodes { get; } = new();
        public List<GraphEdge> Edges { get; } = new();

        public void Convert(MainStateModel model)
        {
            if (model.States == null)
                return;

            var nodeMap = new Dictionary<string, GraphNode>();

            foreach (var state in model.States)
            {
                var node = new GraphNode(state.Name ?? "Unnamed");
                Nodes.Add(node);
                nodeMap[state.Name!] = node;
            }

            if (model.Transitions != null)
            {
                foreach (StageTransition transition in model.Transitions)
                {
                    if (transition.Source != null && transition.Target != null &&
                        nodeMap.TryGetValue(transition.Source, out var source) &&
                        nodeMap.TryGetValue(transition.Target, out var target))
                    {
                        Edges.Add(new GraphEdge(source, target));
                    }
                }
            }
        }
    }
}
