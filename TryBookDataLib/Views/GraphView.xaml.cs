using BookDataLib.Model;
using HelperLib.Controllers;
using HelperLib.Services;
using HelperLib.ViewModels;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TryBookDataLib.Layout;
using TryBookDataLib.ViewModels;

namespace TryBookDataLib.Views
{
    /// <summary>
    /// Interaction logic for GraphView.xaml
    /// </summary>
    public partial class GraphView : UserControl
    {
        GraphViewModel vm = default;

        private bool _isDragging = false;
        private Point _mouseDownPosition;
        private Point _lastMousePosition;
        private GraphNodeViewModel _draggedNode;
        private const double DragThreshold = 1.0; // Pixels

        public GraphView()
        {
            InitializeComponent();

            Loaded += GraphView_Loaded;
            SizeChanged += GraphView_SizeChanged;

        }

        private void Node_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            _draggedNode = border?.DataContext as GraphNodeViewModel;

            if (_draggedNode != null)
            {
                _mouseDownPosition = e.GetPosition(GraphCanvas);
                _lastMousePosition = _mouseDownPosition;
                _isDragging = true;
                border.CaptureMouse();
                e.Handled = true;
            }
        }

        private void Node_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && _draggedNode != null && e.LeftButton == MouseButtonState.Pressed)
            {
                var currentPosition = e.GetPosition(GraphCanvas);
                var offset = currentPosition - _lastMousePosition;

                _draggedNode.Position = new Point(
                    _draggedNode.Position.X + offset.X,
                    _draggedNode.Position.Y + offset.Y);

                _lastMousePosition = currentPosition;
            }
        }

        private void Node_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            border?.ReleaseMouseCapture();

            if (_isDragging)
            {
                var releasePosition = e.GetPosition(GraphCanvas);
                var totalDragDistance = (releasePosition - _mouseDownPosition).Length;

                if (totalDragDistance < DragThreshold)
                {
                    // Click detected
                    if (_draggedNode != null)
                    {
                        ShowNodeDialog(_draggedNode);
                    }
                }

                _isDragging = false;
                _draggedNode = null;
            }
        }
        private void ShowNodeDialog(GraphNodeViewModel node)
        {
            var className = node.Name.Substring(0, node.Name.Length - 1);
            var controller = ServiceLocator.Get<Controller>();
            var found = controller.MakeDynamicDataInstance(className);

            var vvm = controller.WireUPDynamicVvm(found, false);

            dynamic vm = vvm.vm;

            //vm.Item = found;

            var win = CreateWin(vvm.vw as UserControl);

            var Vm_RequestOk = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                //GenericViewModel<Data> sendervm = sender as GenericViewModel<Data>;

                if (sender is GenericViewModel<Novel>)
                {
                    var novel = (sender as GenericViewModel<Novel>).Item as Novel;
                }

                Type genType = vvm.vmType.GetGenericArguments().Single();
                string genTypeName = vvm.vmType.GetGenericArguments().Single().Name;
                win.Close();
            });
            (vvm.vm as WorkspaceViewModel).RequestOk += Vm_RequestOk;

            var Vm_RequestClose = new RoutedEventHandler((object sender, RoutedEventArgs e) =>
            {
                win.Close();
            });
            (vvm.vm as WorkspaceViewModel).RequestClose += Vm_RequestClose;

            win.Content = vvm.vw;
            bool? winres = win.ShowDialog();

            (vvm.vm as WorkspaceViewModel).RequestOk -= Vm_RequestOk;
            (vvm.vm as WorkspaceViewModel).RequestClose -= Vm_RequestClose;
        }
        private Window CreateWin(UserControl view)
        {
            Window win = new Window();
            win = new Window();
            win.Content = view;
            win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win.SizeToContent = SizeToContent.WidthAndHeight;
            return win;
        }
        private void CenterWindowOnScreen(Window win, Size newsize)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = newsize.Width;
            double windowHeight = newsize.Height;
            win.Left = (screenWidth / 2) - (windowWidth / 2);
            win.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void GraphView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            vm = ServiceLocator.Get<GraphViewModel>();
            var layoutengine = ServiceLocator.Get<ForceDirectedLayoutEngine>();

            if (layoutengine != null)
            {
                layoutengine.SetCanvasSize(GraphCanvas.ActualWidth, GraphCanvas.ActualHeight);
            }
        }
        private void GraphView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            vm = ServiceLocator.Get<GraphViewModel>();
            vm.Initialize();

            DataContext = vm;

            var actualSize = new Size(GraphCanvas.ActualWidth, GraphCanvas.ActualHeight);

            vm.StartLayout(actualSize);
        }
    }
}
