using BookDataLib.Model;
using HelperLib.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Forms.Layout;
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

    public void Initialize()
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