using BookDataLib.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.IO;

namespace BookDataLib
{
    public interface IBookDataDbContextFactory
    {
        BookDataDbContext CreateDbContext(string[]? args = null);
    }

    public interface IBookDataDbContext
    {
    }

    public class BookDataDbContext : DbContext, IBookDataDbContext
    {
        public DbSet<Novel> Novels { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<NarrativeStructure> NarrativeStructures { get; set; }
        public DbSet<NarrativeThread> NarrativeThreads { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Scene> Scenes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Conflict> Conflicts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterArc> CharacterArcs { get; set; }
        public DbSet<ArcStage> ArcStages { get; set; }
        public DbSet<Dialogue> Dialogues { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Motif> Motifs { get; set; }
        public DbSet<PointOfView> PointsOfView { get; set; }
        public DbSet<Tone> Tones { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<FramingDevice> FramingDevices { get; set; }
        public DbSet<Layer> Layers { get; set; }
        public DbSet<PacingInfo> PacingInfos { get; set; }
        public DbSet<MotifScene> MotifScenes { get; set; }
        public DbSet<LayerScene> LayerScenes { get; set; }

        // Views
        public DbSet<VRelsView> VRelsViews { get; set; }

        public BookDataDbContext(DbContextOptions<BookDataDbContext> options)
            : base(options)
        {
        }

        //T:\SOURCE\chatgpt\Net8\BookDataDbContextNavigator>dotnet ef migrations add InitialCreate --project BookDataLib
        //T:\SOURCE\chatgpt\Net8\BookDataDbContextNavigator>dotnet ef database update --project BookDataLib

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Plot>()
                .HasOne(p => p.Novel)
                .WithMany(n => n.Plots)
                .HasForeignKey(p => p.NovelId);

            modelBuilder.Entity<Theme>()
                .HasOne(t => t.Novel)
                .WithMany(n => n.Themes)
                .HasForeignKey(t => t.NovelId);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.Novel)
                .WithMany(n => n.Chapters)
                .HasForeignKey(c => c.NovelId);

            modelBuilder.Entity<Style>()
                .HasOne(s => s.Novel)
                .WithMany(n => n.Styles)
                .HasForeignKey(s => s.NovelId);

            modelBuilder.Entity<FramingDevice>()
                .HasOne(fd => fd.Novel)
                .WithMany(n => n.FramingDevices)
                .HasForeignKey(fd => fd.NovelId);

            modelBuilder.Entity<Layer>()
                .HasOne(l => l.Novel)
                .WithMany(n => n.Layers)
                .HasForeignKey(l => l.NovelId);

            modelBuilder.Entity<NarrativeThread>()
                .HasOne(nt => nt.Plot)
                .WithMany(p => p.Threads)
                .HasForeignKey(nt => nt.PlotId);

            modelBuilder.Entity<Plot>()
                .HasOne(p => p.NarrativeStructure)
                .WithMany(ns => ns.Plots)
                .HasForeignKey(p => p.NarrativeStructureId);

            modelBuilder.Entity<Scene>()
                .HasOne(s => s.Chapter)
                .WithMany(c => c.Scenes)
                .HasForeignKey(s => s.ChapterId);

            modelBuilder.Entity<Scene>()
                .HasOne(s => s.Setting)
                .WithMany(set => set.Scenes)
                .HasForeignKey(s => s.SettingId);

            modelBuilder.Entity<Scene>()
                .HasOne(s => s.Tone)
                .WithMany(t => t.Scenes)
                .HasForeignKey(s => s.ToneId);

            modelBuilder.Entity<PacingInfo>()
                .HasOne(p => p.Scene)
                .WithOne(s => s.PacingInfo)
                .HasForeignKey<PacingInfo>(p => p.SceneId);

            modelBuilder.Entity<Dialogue>()
                .HasOne(d => d.Scene)
                .WithMany(s => s.Dialogues)
                .HasForeignKey(d => d.SceneId);

            modelBuilder.Entity<Dialogue>()
                .HasOne(d => d.Character)
                .WithMany(c => c.Dialogues)
                .HasForeignKey(d => d.CharacterId);

            modelBuilder.Entity<CharacterArc>()
                .HasOne(ca => ca.Character)
                .WithMany(c => c.CharacterArcs)
                .HasForeignKey(ca => ca.CharacterId);

            modelBuilder.Entity<ArcStage>()
                .HasOne(asg => asg.CharacterArc)
                .WithMany(ca => ca.ArcStages)
                .HasForeignKey(asg => asg.CharacterArcId);

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Thread)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.ThreadId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction

            modelBuilder.Entity<Event>()
                .HasOne(e => e.Scene)
                .WithMany(s => s.Events)
                .HasForeignKey(e => e.SceneId)
                .OnDelete(DeleteBehavior.Restrict); // or .NoAction

            modelBuilder.Entity<Conflict>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Conflicts)
                .HasForeignKey(c => c.EventId);

            modelBuilder.Entity<Chapter>()
                .HasOne(c => c.PointOfView)
                .WithMany(p => p.Chapters)
                .HasForeignKey(c => c.PointOfViewId);

            modelBuilder.Entity<MotifScene>()
                .HasKey(ms => new { ms.MotifId, ms.SceneId });

            modelBuilder.Entity<MotifScene>()
                .HasOne(ms => ms.Motif)
                .WithMany(m => m.MotifScenes)
                .HasForeignKey(ms => ms.MotifId);

            modelBuilder.Entity<MotifScene>()
                .HasOne(ms => ms.Scene)
                .WithMany(s => s.MotifScenes)
                .HasForeignKey(ms => ms.SceneId);

            modelBuilder.Entity<LayerScene>()
                .HasKey(ls => new { ls.LayerId, ls.SceneId });

            modelBuilder.Entity<LayerScene>()
                .HasOne(ls => ls.Layer)
                .WithMany(l => l.LayerScenes)
                .HasForeignKey(ls => ls.LayerId);

            modelBuilder.Entity<LayerScene>()
                .HasOne(ls => ls.Scene)
                .WithMany(s => s.LayerScenes)
                .HasForeignKey(ls => ls.SceneId);

            // Views

            modelBuilder.Entity<VRelsView>().ToView("VRelsView");
            modelBuilder.Entity<VRelsView>().HasNoKey(); // Optional: if the view doesn't have a primary key
        }
    }


    public class BookDataDbContextFactory : IDesignTimeDbContextFactory<BookDataDbContext>, IBookDataDbContextFactory
    {
        public BookDataDbContextFactory()
        {
        }

        public BookDataDbContext CreateDbContext(string[]? args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookDataDbContext>();

            var dbDir = PathResolver.GetAppDataDataDirectory("BookDataLib");
            var dbPath = Path.Combine(dbDir, "BookDataLibDb.mdf");

            optionsBuilder.UseSqlServer(
                $"Server=(localdb)\\MSSQLLocalDB;" +
                $"AttachDbFilename={dbPath};" +
                $"Database=BookDataLibDb;" +
                $"Trusted_Connection=True;");

            return new BookDataDbContext(optionsBuilder.Options);
        }
    }
    public static class PathResolver
    {
        public static string GetAppDataDataDirectory(string projectName)
        {
            var baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDataPath = Path.Combine(baseDir, "IndiaTango", projectName, "Data");

            Directory.CreateDirectory(appDataPath); // ensure it exists
            return appDataPath;
        }
    }


    // Server=(localdb)\MSSQLLocalDB; Database=BookDataLibDb; AttachDbFilename=C:\Users\Indio\AppData\Roaming\IndiaTango\BookDataLibDb\Data\BookDataLibDb.mdf; Trusted_Connection=True;
}