using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookData.Services.Views
{
    public class CharacterSketch
    {
        public string Title { get; set; }
        public string CharacterName { get; set; }
        public string Nickname { get; set; }
        public string BirthDatePlace { get; set; }
        public string CharacterRole { get; set; }
        public string PhysicalDescriptions { get; set; }
        public string Age { get; set; }
        public string Race { get; set; }
        public string EyeColour { get; set; }
        public string HairColourStyle { get; set; }
        public string BuildHeightWeight { get; set; }
        public string SkinTone { get; set; }
        public string StyleOfDress { get; set; }
        public string CharacteristicsAndMannerisms { get; set; }
        public string PersonalityTraits { get; set; }
        public string Background { get; set; }
        public string InternalConflicts { get; set; }
        public string ExternalConflicts { get; set; }
        public string OccupationEducation { get; set; }
        public string MiscellaneousNotes { get; set; }

        public override string ToString() => $"{CharacterName} - {Title}";
    }

    /// <summary>
    /// Interaction logic for CharacterSketchWindow.xaml
    /// </summary>
    public partial class CharacterSketchWindow : Window
    {
        static CharacterSketchWindow instance = default;

        public static CharacterSketchWindow Create()
        {
            if (instance == null)
            {
                instance = new CharacterSketchWindow();
            }
            else
            {
                return null;
            }
                return instance;
        }


        private List<CharacterSketch> sketches = new();
        private string storagePath = @"T:\\Projects\\WriteBook\\Ideas\\CharacterSketches.json";


        public CharacterSketchWindow()
        {
            InitializeComponent();
        }

        private void AddSketchButton_Click(object sender, RoutedEventArgs e)
        {
            var sketch = CreateSketchFromInputs();
            sketches.Add(sketch);
            SketchesListBox.Items.Add(sketch);
            ClearInputs();
        }

        private void UpdateSketchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                var updatedSketch = CreateSketchFromInputs();
                sketches[SketchesListBox.SelectedIndex] = updatedSketch;
                SketchesListBox.Items[SketchesListBox.SelectedIndex] = updatedSketch;
            }
        }

        private void DeleteSketchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                sketches.RemoveAt(SketchesListBox.SelectedIndex);
                SketchesListBox.Items.RemoveAt(SketchesListBox.SelectedIndex);
                ClearInputs();
            }
        }

        private void SketchesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SketchesListBox.SelectedIndex >= 0)
            {
                var selected = sketches[SketchesListBox.SelectedIndex];
                PopulateInputs(selected);
            }
        }

        private CharacterSketch CreateSketchFromInputs()
        {
            return new CharacterSketch
            {
                Title = TitleTextBox.Text,
                CharacterName = CharacterNameTextBox.Text,
                Nickname = NicknameTextBox.Text,
                BirthDatePlace = BirthDatePlaceTextBox.Text,
                CharacterRole = CharacterRoleTextBox.Text,
                PhysicalDescriptions = PhysicalDescriptionsTextBox.Text,
                Age = AgeTextBox.Text,
                Race = RaceTextBox.Text,
                EyeColour = EyeColourTextBox.Text,
                HairColourStyle = HairColourStyleTextBox.Text,
                BuildHeightWeight = BuildHeightWeightTextBox.Text,
                SkinTone = SkinToneTextBox.Text,
                StyleOfDress = StyleOfDressTextBox.Text,
                CharacteristicsAndMannerisms = CharacteristicsAndMannerismsTextBox.Text,
                PersonalityTraits = PersonalityTraitsTextBox.Text,
                Background = BackgroundTextBox.Text,
                InternalConflicts = InternalConflictsTextBox.Text,
                ExternalConflicts = ExternalConflictsTextBox.Text,
                OccupationEducation = OccupationEducationTextBox.Text,
                MiscellaneousNotes = MiscellaneousNotesTextBox.Text
            };
        }

        private void PopulateInputs(CharacterSketch sketch)
        {
            TitleTextBox.Text = sketch.Title;
            CharacterNameTextBox.Text = sketch.CharacterName;
            NicknameTextBox.Text = sketch.Nickname;
            BirthDatePlaceTextBox.Text = sketch.BirthDatePlace;
            CharacterRoleTextBox.Text = sketch.CharacterRole;
            PhysicalDescriptionsTextBox.Text = sketch.PhysicalDescriptions;
            AgeTextBox.Text = sketch.Age;
            RaceTextBox.Text = sketch.Race;
            EyeColourTextBox.Text = sketch.EyeColour;
            HairColourStyleTextBox.Text = sketch.HairColourStyle;
            BuildHeightWeightTextBox.Text = sketch.BuildHeightWeight;
            SkinToneTextBox.Text = sketch.SkinTone;
            StyleOfDressTextBox.Text = sketch.StyleOfDress;
            CharacteristicsAndMannerismsTextBox.Text = sketch.CharacteristicsAndMannerisms;
            PersonalityTraitsTextBox.Text = sketch.PersonalityTraits;
            BackgroundTextBox.Text = sketch.Background;
            InternalConflictsTextBox.Text = sketch.InternalConflicts;
            ExternalConflictsTextBox.Text = sketch.ExternalConflicts;
            OccupationEducationTextBox.Text = sketch.OccupationEducation;
            MiscellaneousNotesTextBox.Text = sketch.MiscellaneousNotes;
        }

        private void ClearInputs()
        {
            TitleTextBox.Clear();
            CharacterNameTextBox.Clear();
            NicknameTextBox.Clear();
            BirthDatePlaceTextBox.Clear();
            CharacterRoleTextBox.Clear();
            PhysicalDescriptionsTextBox.Clear();
            AgeTextBox.Clear();
            RaceTextBox.Clear();
            EyeColourTextBox.Clear();
            HairColourStyleTextBox.Clear();
            BuildHeightWeightTextBox.Clear();
            SkinToneTextBox.Clear();
            StyleOfDressTextBox.Clear();
            CharacteristicsAndMannerismsTextBox.Clear();
            PersonalityTraitsTextBox.Clear();
            BackgroundTextBox.Clear();
            InternalConflictsTextBox.Clear();
            ExternalConflictsTextBox.Clear();
            OccupationEducationTextBox.Clear();
            MiscellaneousNotesTextBox.Clear();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (File.Exists(storagePath))
            {
                string json = File.ReadAllText(storagePath);
                sketches = JsonSerializer.Deserialize<List<CharacterSketch>>(json);
                foreach (var sketch in sketches)
                {
                    SketchesListBox.Items.Add(sketch);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            string json = JsonSerializer.Serialize(sketches, new JsonSerializerOptions { WriteIndented = true });
            if (json != null && json.Length > 2)
            {
                File.WriteAllText(storagePath, json);
            }

            instance = null;
        }
    }
}
