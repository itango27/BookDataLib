using static BookDataLib.Graph.StateModelBuilder;

namespace BookDataLib.Model
{
    public class StateModelGraphAdapter
    {
        public List<GraphNode> Nodes { get; } = new();
        public List<GraphEdge> Edges { get; } = new();

        private readonly Dictionary<string, GraphNode> nodeLookup = new();

        public void Convert(MainStateModel model)
        {
            foreach (var stage in model.States)
            {
                var stageNode = CreateNode($"Stage:{stage.Name}");
                AddNode(stageNode);

                foreach (var element in stage.Elements)
                {
                    var elementNode = CreateNode($"Element:{stage.Name}:{element.Name}");
                    AddNode(elementNode);
                    AddEdge(stageNode, elementNode);

                    foreach (var state in element.States)
                    {
                        var stateNode = CreateNode($"State:{stage.Name}:{element.Name}:{state}");
                        AddNode(stateNode);
                        AddEdge(elementNode, stateNode);
                    }

                    foreach (var transition in element.Transitions)
                    {
                        var fromNode = CreateNode($"State:{stage.Name}:{element.Name}:{transition.From}");
                        var toNode = CreateNode($"State:{stage.Name}:{element.Name}:{transition.To}");
                        AddEdge(fromNode, toNode);
                    }
                }
            }

            foreach (var transition in model.Transitions)
            {
                var fromNode = CreateNode($"Stage:{transition.Source}");
                var toNode = CreateNode($"Stage:{transition.Target}");
                AddEdge(fromNode, toNode);
            }
        }

        private GraphNode CreateNode(string name)
        {
            if (nodeLookup.TryGetValue(name, out var node))
                return node;

            node = new GraphNode(name);
            nodeLookup[name] = node;
            return node;
        }

        private void AddNode(GraphNode node)
        {
            if (!Nodes.Contains(node))
                Nodes.Add(node);
        }

        private void AddEdge(GraphNode from, GraphNode to)
        {
            Edges.Add(new GraphEdge(from, to));
        }
    }
}
