// ViewModels/LayoutSettingsViewModel.cs
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TryBookDataLib.ViewModels;

public class LayoutSettingsViewModel : INotifyPropertyChanged
{
    private double _repulsion = 10000;
    private double _attraction = 0.1;
    private double _damping = 0.5;
    private double _timeStep = 0.1;

    public double Repulsion
    {
        get => _repulsion;
        set { _repulsion = value; OnPropertyChanged(); }
    }

    public double Attraction
    {
        get => _attraction;
        set { _attraction = value; OnPropertyChanged(); }
    }

    public double Damping
    {
        get => _damping;
        set { _damping = value; OnPropertyChanged(); }
    }

    public double TimeStep
    {
        get => _timeStep;
        set { _timeStep = value; OnPropertyChanged(); }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
