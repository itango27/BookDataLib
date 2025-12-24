
using System.IO;
using System.Text.Json;

namespace BookDataLib.Graph
{
    public class GraphNode
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Stage { get; set; }
        public string Element { get; set; }
        public string State { get; set; }
    }

    public class GraphEdge
    {
        public int Source { get; set; }
        public int Target { get; set; }
    }
    public class StateModelBuilder
    {
        public List<GraphNode> Nodes { get; private set; }
        public List<GraphEdge> Edges { get; private set; }

        private int _nodeIdCounter = 0;
        private Dictionary<string, GraphNode> _nodeLookup;

        public StateModelBuilder()
        {
            Nodes = new List<GraphNode>();
            Edges = new List<GraphEdge>();
            _nodeLookup = new Dictionary<string, GraphNode>();
        }

        public class MainStateModel
        {
            public string? Name { get; set; }
            public List<Stage>? States { get; set; }
            public List<StageTransition>? Transitions { get; set; }
        }
        public class Stage
        {
            public string? Name { get; set; }
            public List<Element>? Elements { get; set; }
        }
        public class Element
        {
            public string? Name { get; set; }
            public List<string>? States { get; set; }
            public List<ElementTransition>? Transitions { get; set; }
        }
        public class ElementTransition
        {
            public string? From { get; set; }
            public string? To { get; set; }
        }
        public class StageTransition
        {
            public string? Source { get; set; }
            public string? Target { get; set; }
        }

        public static MainStateModel Load(string jsonFilePath)
        {
            var json = File.ReadAllText(jsonFilePath);
            var document = JsonDocument.Parse(json);
            var root = document.RootElement;

            if (root.TryGetProperty("mainStateModel", out var modelElement))
            {
                var modelJson = modelElement.GetRawText();
                var model = JsonSerializer.Deserialize<MainStateModel>(modelJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return model!;
            }

            throw new InvalidOperationException("mainStateModel not found in root JSON.");
        }

        public void Build(MainStateModel model)
        {
            foreach (var stage in model.States)
            {
                foreach (var element in stage.Elements)
                {
                    // Create nodes for all states within this element
                    for (int i = 0; i < element.States.Count; i++)
                    {
                        string stateLabel = $"{stage.Name}:{element.Name}:{element.States[i]}";
                        var node = new GraphNode
                        {
                            Id = _nodeIdCounter++,
                            Label = stateLabel,
                            Stage = stage.Name,
                            Element = element.Name,
                            State = element.States[i]
                        };
                        Nodes.Add(node);
                        _nodeLookup[stateLabel] = node;
                    }

                    // Create edges for transitions within this element
                    foreach (var transition in element.Transitions)
                    {
                        string fromKey = $"{stage.Name}:{element.Name}:{transition.From}";
                        string toKey = $"{stage.Name}:{element.Name}:{transition.To}";

                        if (_nodeLookup.TryGetValue(fromKey, out var fromNode) &&
                            _nodeLookup.TryGetValue(toKey, out var toNode))
                        {
                            Edges.Add(new GraphEdge { Source = fromNode.Id, Target = toNode.Id });
                        }
                    }
                }
            }

            // Handle cross-stage transitions
            foreach (var t in model.Transitions)
            {
                var fromStage = t.Source;
                var toStage = t.Target;

                var fromNodes = Nodes.Where(n => n.Stage == fromStage).ToList();
                var toNodes = Nodes.Where(n => n.Stage == toStage).ToList();

                // Connect all elements' last state in the fromStage to all first states in the toStage
                var fromTerminalNodes = fromNodes
                    .GroupBy(n => n.Element)
                    .Select(g => g.OrderByDescending(n => g.ToList().IndexOf(n)).Last());

                var toInitialNodes = toNodes
                    .GroupBy(n => n.Element)
                    .Select(g => g.OrderBy(n => g.ToList().IndexOf(n)).First());

                foreach (var fromNode in fromTerminalNodes)
                {
                    foreach (var toNode in toInitialNodes)
                    {
                        Edges.Add(new GraphEdge { Source = fromNode.Id, Target = toNode.Id });
                    }
                }
            }
        }
    }
}
