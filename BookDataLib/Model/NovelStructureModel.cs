using System.Windows;

namespace BookDataLib.Model
{
    public class Novel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Plot> Plots { get; set; }
        public ICollection<Theme> Themes { get; set; }
        public ICollection<Character> Characters { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
        public ICollection<Motif> Motifs { get; set; }
        public ICollection<Style> Styles { get; set; }
        public ICollection<FramingDevice> FramingDevices { get; set; }
        public ICollection<Layer> Layers { get; set; }

        public Novel()
        {
            Plots = new List<Plot>();   
            Themes = new List<Theme>(); 
            Characters = new List<Character>(); 
            Chapters = new List<Chapter>(); 
            Motifs = new List<Motif>();
            Styles = new List<Style>();
            FramingDevices = new List<FramingDevice>(); 
            Layers = new List<Layer>(); 
        }
    }

    public class Plot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NarrativeStructureId { get; set; }
        public NarrativeStructure NarrativeStructure { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }
        public ICollection<NarrativeThread> Threads { get; set; }
    }

    public class NarrativeStructure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Plot> Plots { get; set; }
    }

    public class NarrativeThread
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlotId { get; set; }
        public Plot Plot { get; set; }
        public ICollection<BookEvent> Events { get; set; }
    }

    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }
        public int PointOfViewId { get; set; }
        public PointOfView PointOfView { get; set; }
        public ICollection<Scene> Scenes { get; set; }
    }

    public class Scene
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }

        public int SettingId { get; set; }
        public Setting Setting { get; set; }

        public int ToneId { get; set; }
        public Tone Tone { get; set; }

        public PacingInfo PacingInfo { get; set; }

        public ICollection<Dialogue> Dialogues { get; set; }
        public ICollection<BookEvent> Events { get; set; }
        public ICollection<MotifScene> MotifScenes { get; set; }
        public ICollection<LayerScene> LayerScenes { get; set; }
    }

    public class BookEvent
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int ThreadId { get; set; }
        public NarrativeThread Thread { get; set; }

        public int SceneId { get; set; }
        public Scene Scene { get; set; }

        public ICollection<Conflict> Conflicts { get; set; }
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CharacterArc> CharacterArcs { get; set; }
        public ICollection<Dialogue> Dialogues { get; set; }
        public ICollection<Scene> Scenes { get; set; }
    }

    public class CharacterArc
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CharacterId { get; set; }
        public Character Character { get; set; }

        public ICollection<ArcStage> ArcStages { get; set; }
    }

    public class ArcStage
    {
        public int Id { get; set; }
        public string StageName { get; set; }
        public string Description { get; set; }
        public int CharacterArcId { get; set; }
        public CharacterArc CharacterArc { get; set; }
    }

    public class Dialogue
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CharacterId { get; set; }
        public Character Character { get; set; }
        public int SceneId { get; set; }
        public Scene Scene { get; set; }
    }

    public class Conflict
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int EventId { get; set; }
        public BookEvent Event { get; set; }
    }

    public class Setting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LocationHierarchy { get; set; }
        public ICollection<Scene> Scenes { get; set; }
    }

    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }
        public ICollection<Motif> Motifs { get; set; }
    }

    public class Motif
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public ICollection<MotifScene> MotifScenes { get; set; }
    }

    public class PointOfView
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string NarratorDescription { get; set; }
        public ICollection<Chapter> Chapters { get; set; }
    }

    public class Tone
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<Scene> Scenes { get; set; }
    }

    public class Style
    {
        public int Id { get; set; }
        public string VoiceDescription { get; set; }
        public string LanguageNotes { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }
    }

    public class FramingDevice
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public int NovelId { get; set; }
        public Novel Novel { get; set; }
    }

    public class Layer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int NovelId { get; set; }
        public Novel Novel { get; set; }

        public ICollection<LayerScene> LayerScenes { get; set; }
    }

    public class PacingInfo
    {
        public int Id { get; set; }
        public int SceneId { get; set; }
        public Scene Scene { get; set; }
        public int WordCount { get; set; }
        public double TensionLevel { get; set; }
        public double Tempo { get; set; }
    }
    public class MotifScene
    {
        public int MotifId { get; set; }
        public Motif Motif { get; set; }

        public int SceneId { get; set; }
        public Scene Scene { get; set; }
    }

    public class LayerScene
    {
        public int LayerId { get; set; }
        public Layer Layer { get; set; }

        public int SceneId { get; set; }
        public Scene Scene { get; set; }
    }


    public class GraphNode
    {
        public GraphNode()
        {
        }

        public GraphNode(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public GraphNode(string name, Point position)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Position = position;
            X = position.X;
            Y = position.Y;
        }

        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public Point Position { get; set; }
    }

    public class GraphEdge
    {
        public GraphEdge()
        {
        }

        public GraphEdge(GraphNode source, GraphNode target)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public GraphNode Source { get; set; }
        public GraphNode Target { get; set; }
    }

}
