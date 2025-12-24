using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace PlotSketchApp
{
    public class PlotSketch
    {
        public string Title { get; set; }
        public string StoryGoal { get; set; }
        public string RomanceThread_Optional { get; set; }
        public string SubplotThreads1 { get; set; }
        public string Additional { get; set; }
        public string PlotTension { get; set; }
        public string RomanticSexualTension { get; set; }
        public string Release { get; set; }
        public string Downtime { get; set; }
        public string BlackMoment { get; set; }
        public string Resolution { get; set; }
        public string AfterEffectsOfResolution { get; set; }

        public override string ToString()
        {
            return $"{Title} (Plot)";
        }
    }

    public partial class MainWindow : Window
    {
        private List<PlotSketch> sketches = new List<PlotSketch>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPlotSketchButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTextBox.Text))
            {
                MessageBox.Show("Please enter a title for the plot sketch.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PlotSketch sketch = new PlotSketch
            {
                Title = TitleTextBox.Text,
                StoryGoal = StoryGoalTextBox.Text,
                RomanceThread_Optional = RomanceThreadTextBox.Text,
                SubplotThreads1 = SubplotThreadsTextBox.Text,
                Additional = AdditionalTextBox.Text,
                PlotTension = PlotTensionTextBox.Text,
                RomanticSexualTension = RomanticSexualTensionTextBox.Text,
                Release = ReleaseTextBox.Text,
                Downtime = DowntimeTextBox.Text,
                BlackMoment = BlackMomentTextBox.Text,
                Resolution = ResolutionTextBox.Text,
                AfterEffectsOfResolution = AfterEffectsTextBox.Text
            };

            sketches.Add(sketch);
            SketchesListBox.Items.Add(sketch);

            ClearFields();
        }

        private void UpdatePlotSketchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                var sketch = sketches[SketchesListBox.SelectedIndex];

                sketch.Title = TitleTextBox.Text;
                sketch.StoryGoal = StoryGoalTextBox.Text;
                sketch.RomanceThread_Optional = RomanceThreadTextBox.Text;
                sketch.SubplotThreads1 = SubplotThreadsTextBox.Text;
                sketch.Additional = AdditionalTextBox.Text;
                sketch.PlotTension = PlotTensionTextBox.Text;
                sketch.RomanticSexualTension = RomanticSexualTensionTextBox.Text;
                sketch.Release = ReleaseTextBox.Text;
                sketch.Downtime = DowntimeTextBox.Text;
                sketch.BlackMoment = BlackMomentTextBox.Text;
                sketch.Resolution = ResolutionTextBox.Text;
                sketch.AfterEffectsOfResolution = AfterEffectsTextBox.Text;

                SketchesListBox.Items[SketchesListBox.SelectedIndex] = null;
                SketchesListBox.Items[SketchesListBox.SelectedIndex] = sketch;
            }
        }

        private void DeletePlotSketchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                sketches.RemoveAt(SketchesListBox.SelectedIndex);
                SketchesListBox.Items.RemoveAt(SketchesListBox.SelectedIndex);
                ClearFields();
            }
        }

        private void PlotSketchListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                var sketch = sketches[SketchesListBox.SelectedIndex];

                TitleTextBox.Text = sketch.Title;
                StoryGoalTextBox.Text = sketch.StoryGoal;
                RomanceThreadTextBox.Text = sketch.RomanceThread_Optional;
                SubplotThreadsTextBox.Text = sketch.SubplotThreads1;
                AdditionalTextBox.Text = sketch.Additional;
                PlotTensionTextBox.Text = sketch.PlotTension;
                RomanticSexualTensionTextBox.Text = sketch.RomanticSexualTension;
                ReleaseTextBox.Text = sketch.Release;
                DowntimeTextBox.Text = sketch.Downtime;
                BlackMomentTextBox.Text = sketch.BlackMoment;
                ResolutionTextBox.Text = sketch.Resolution;
                AfterEffectsTextBox.Text = sketch.AfterEffectsOfResolution;
            }
        }

        private void ClearFields()
        {
            TitleTextBox.Clear();
            StoryGoalTextBox.Clear();
            RomanceThreadTextBox.Clear();
            SubplotThreadsTextBox.Clear();
            AdditionalTextBox.Clear();
            PlotTensionTextBox.Clear();
            RomanticSexualTensionTextBox.Clear();
            ReleaseTextBox.Clear();
            DowntimeTextBox.Clear();
            BlackMomentTextBox.Clear();
            ResolutionTextBox.Clear();
            AfterEffectsTextBox.Clear();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            var inPath = $@"T:\\Projects\\WriteBook\\Ideas\\PlotSketches.json";

            if (File.Exists(inPath))
            {
                var json = File.ReadAllText(inPath);
                sketches = JsonSerializer.Deserialize<List<PlotSketch>>(json);

                foreach (var sketch in sketches)
                {
                    SketchesListBox.Items.Add(sketch);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            var outPath = $@"T:\\Projects\\WriteBook\\Ideas\\PlotSketches.json";

            var json = JsonSerializer.Serialize(sketches, new JsonSerializerOptions { WriteIndented = true });
            if (true)
            {

            }
            File.WriteAllText(outPath, json);
        }
    }
}
