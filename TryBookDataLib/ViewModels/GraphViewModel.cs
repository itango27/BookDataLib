using BookDataLib.Graph;
using BookDataLib.Model;
using HelperLib.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using TryBookDataLib.Layout;

namespace TryBookDataLib.ViewModels;

public class GraphViewModel : INotifyPropertyChanged
{
    private ObservableCollection<GraphNodeViewModel> nodes;
    private ObservableCollection<GraphEdgeViewModel> edges;

    public ObservableCollection<GraphNodeViewModel> Nodes 
    { 
        get => nodes; 
        set 
        {
            nodes = value;
            OnPropertyChanged();
        } 
    }
    public ObservableCollection<GraphEdgeViewModel> Edges 
    { 
        get => edges;
        set
        {
            edges = value;
            OnPropertyChanged();
        }
    }

    public GraphViewModel()
    {
    }

    public void InitializeVRelsView()
    {
        var vrels = VRelsView.Read();
        var nodesEdges = VRelsView.BuildGraphFromVRels(vrels);

        // Wrap GraphNodes
        var nodeViewModels = nodesEdges.Nodes
            .Select(n => new GraphNodeViewModel(n))
            .ToDictionary(vm => vm.Model); // Allows lookup by DTO

        Nodes = new ObservableCollection<GraphNodeViewModel>(nodeViewModels.Values);

        // Wrap GraphEdges using wrapped nodes
        var edgeViewModels = nodesEdges.Edges
            .Select(e => new GraphEdgeViewModel(
                e,
                nodeViewModels[e.Source],
                nodeViewModels[e.Target]
            ));

        Edges = new ObservableCollection<GraphEdgeViewModel>(edgeViewModels);
    }

    public void InitilizeStateModel()
    {
        // Load your JSON
        var model = StateModelBuilder.Load(@"T:\ChatGPT\Final Novel State Model.json");
        var builder = new StateModelBuilder();
        builder.Build(model);
        var adapter = new StateModelGraphAdapter();
        adapter.Convert(model);

        var layoutEngine = ServiceLocator.Get<ForceDirectedLayoutEngine>();
        layoutEngine.ApplyLayout(Nodes, Edges);
    }

    public void StartLayout(Size actualSize)
    {
        var layoutEngine = ServiceLocator.Get<ForceDirectedLayoutEngine>();

        layoutEngine.SetCanvasSize(actualSize.Width, actualSize.Height);  // Match your Canvas actual size
        layoutEngine.ApplyLayout(Nodes, Edges);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
