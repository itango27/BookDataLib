using Azure.Core.GeoJson;
using BookDataLib.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using TryBookDataLib.ViewModels;


namespace TryBookDataLib.Layout;

public class ForceDirectedLayoutEngine
{
    private const double RepulsionConstant = 50000;  // Stronger repulsion
    private const double AttractionConstant = 0.0005;  // Weaker attraction
    private const double Damping = 0.85;              // Less damping
    private const double TimeStep = 0.5;              // Bigger step for movement

    private ObservableCollection<GraphNodeViewModel> _nodes;
    private ObservableCollection<GraphEdgeViewModel> _edges;
    private  Dictionary<GraphNodeViewModel, Vector> _velocities = new();

    private double _canvasWidth = 800;  // Set based on your actual canvas size
    private double _canvasHeight = 600;

    public ForceDirectedLayoutEngine()
    {
    }

    public void SetCanvasSize(double width, double height)
    {
        _canvasWidth = width;
        _canvasHeight = height;
    }

    public void ApplyLayout(ObservableCollection<GraphNodeViewModel> nodes, ObservableCollection<GraphEdgeViewModel> edges)
    {
        _nodes = nodes;
        _edges = edges;

        foreach (var node in _nodes)
            _velocities[node] = new Vector(0, 0);

        var timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds(100) // ~60 FPS
        };

        int iteration = 0;
        int maxIterations = 100;

        timer.Tick += (s, e) =>
        {
            if (iteration++ >= maxIterations)
            {
                ((DispatcherTimer)s).Stop();
                return;
            }

            Step();
        };

        InitializeRandomPositions(800, 600); // You can also pass actual canvas size here
        timer.Start();
    }

    public void InitializeRandomPositions(double width, double height)
    {
        var rand = new Random();
        foreach (var node in _nodes)
        {
            node.Position = new Point(rand.NextDouble() * width, rand.NextDouble() * height);
        }
    }

    public void Step()
    {
        var forces = new Dictionary<GraphNodeViewModel, Vector>();

        foreach (var node in _nodes)
            forces[node] = new Vector(0, 0);

        // Repulsive forces
        for (int i = 0; i < _nodes.Count; i++)
        {
            for (int j = i + 1; j < _nodes.Count; j++)
            {
                var a = _nodes[i];
                var b = _nodes[j];
                var delta = a.Position - b.Position;
                var distance = Math.Max(1, delta.Length);
                var force = RepulsionConstant / (distance * distance);
                var direction = delta / distance;

                forces[a] += direction * force;
                forces[b] -= direction * force;
            }
        }

        // Attractive forces
        foreach (var edge in _edges)
        {
            var delta = edge.Source.Position - edge.Target.Position;
            var distance = Math.Max(1, delta.Length);
            var force = AttractionConstant * (distance * distance);
            var direction = delta / distance;

            forces[edge.Source] -= direction * force;
            forces[edge.Target] += direction * force;
        }

        // 3. Edge crossing penalty forces (add this new section)
        foreach (var edge1 in _edges)
        {
            foreach (var edge2 in _edges)
            {
                if (edge1 == edge2 ||
                    edge1.Source == edge2.Source || edge1.Source == edge2.Target ||
                    edge1.Target == edge2.Source || edge1.Target == edge2.Target)
                {
                    continue;
                }
                if (DoEdgesIntersect(edge1, edge2))
                {
                    double penalty = 0.1;

                    var delta1 = edge1.Source.Position - edge1.Target.Position;
                    var direction1 = delta1 / Math.Max(delta1.Length, 1);

                    var delta2 = edge2.Source.Position - edge2.Target.Position;
                    var direction2 = delta2 / Math.Max(delta2.Length, 1);

                    forces[edge1.Source] += direction1 * penalty;
                    forces[edge1.Target] -= direction1 * penalty;

                    forces[edge2.Source] += direction2 * penalty;
                    forces[edge2.Target] -= direction2 * penalty;
                }
            }
        }

        // Apply forces
        foreach (var node in _nodes)
        {
            var acceleration = forces[node];
            _velocities[node] = (_velocities[node] + acceleration * TimeStep) * Damping;

            var newPosition = node.Position + _velocities[node] * TimeStep;

            // Clamp to canvas
            newPosition.X = Math.Max(0, Math.Min(_canvasWidth - 80, newPosition.X));   // 80 = node width
            newPosition.Y = Math.Max(0, Math.Min(_canvasHeight - 30, newPosition.Y));  // 30 = node height

            node.Position = newPosition;
        }
    }

    private bool DoEdgesIntersect(GraphEdgeViewModel e1, GraphEdgeViewModel e2)
    {
        var p1 = e1.Source.Position;
        var q1 = e1.Target.Position;
        var p2 = e2.Source.Position;
        var q2 = e2.Target.Position;

        bool ccw(Point a, Point b, Point c) => (c.Y - a.Y) * (b.X - a.X) > (b.Y - a.Y) * (c.X - a.X);

        return ccw(p1, p2, q2) != ccw(q1, p2, q2) && ccw(p1, q1, p2) != ccw(p1, q1, q2);
    }
}

