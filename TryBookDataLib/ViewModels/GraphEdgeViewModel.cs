using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookDataLib.Model;

namespace TryBookDataLib.ViewModels
{
    public class GraphEdgeViewModel : INotifyPropertyChanged
    {
        public GraphEdge Model { get; }

        private GraphNodeViewModel source;
        public GraphNodeViewModel Source
        {
            get => source;
            set
            {
                source = value;
                OnPropertyChanged();
            }
        }

        private GraphNodeViewModel target;
        public GraphNodeViewModel Target
        {
            get => target;
            set
            {
                target = value;
                OnPropertyChanged();
            }
        }

        public GraphEdgeViewModel(GraphEdge model, GraphNodeViewModel source, GraphNodeViewModel target)
        {
            Model = model;
            Source = source;
            Target = target;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
