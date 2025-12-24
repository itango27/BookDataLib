using BookDataLib.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace TryBookDataLib.ViewModels
{
    public class GraphNodeViewModel : INotifyPropertyChanged
    {
        public GraphNode Model { get; }

        public string Name => Model.Name;

        public Point Position
        {
            get => Model.Position;
            set
            {
                if (Model.Position != value)
                {
                    Model.Position = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PositionX));
                    OnPropertyChanged(nameof(PositionY));
                }
            }
        }

        public double PositionX => Position.X;
        public double PositionY => Position.Y;

        public GraphNodeViewModel(GraphNode model)
        {
            Model = model;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
