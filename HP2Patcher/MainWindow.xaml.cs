using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Threading;
using System.Windows;

namespace HP2Patcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string rootPath;
        private string temp;
        private string tempPath;
        private string gamePath;
        private string gameExe;
        private string noCDexe;
        private string noCDgamePath;
        private string diskPath;
        private string actors;
        private string actorsPath;
        private string actorsDest;
        private string AI;
        private string AIPath;
        private string AIDest;
        private string audio;
        private string audioPath;
        private string audioDest;
        private string Cars;
        private string CarsPath;
        private string CarsDest;
        private string movies;
        private string moviesPath;
        private string moviesDest;
        private string Particle;
        private string ParticlePath;
        private string ParticleDest;
        private string tracks;
        private string tracksPath;
        private string tracksDest;

        public MainWindow()
        {
            InitializeComponent();

            rootPath = Directory.GetCurrentDirectory();
            temp = Path.GetTempPath();
            tempPath = Path.Combine(temp, "HP2Patcher");
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DelTemp();
        }

        private void DelTemp()
        {
            if (Directory.Exists(tempPath))
            {
                Directory.Delete(tempPath, true);
            }
        }


        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/KilLo445/HP2Patcher");
        }

        private void Patch_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Hot Pursuit 2 install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                gameExe = Path.Combine(dialog.SelectedPath, "NFSHP2.exe");

                if (File.Exists(gameExe))
                {
                    gamePath = Path.Combine(dialog.SelectedPath);
                    actorsPath = Path.Combine(dialog.SelectedPath, "actors");
                    AIPath = Path.Combine(dialog.SelectedPath, "AI");
                    audioPath = Path.Combine(dialog.SelectedPath, "audio");
                    CarsPath = Path.Combine(dialog.SelectedPath, "Cars");
                    moviesPath = Path.Combine(dialog.SelectedPath, "movies");
                    ParticlePath = Path.Combine(dialog.SelectedPath, "Particle");
                    tracksPath = Path.Combine(dialog.SelectedPath, "tracks");

                    var dialog2 = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                    dialog2.Description = "Please select your Need for Speed: Hot Pursuit 2 disk.";
                    dialog2.UseDescriptionForTitle = true;
                    if (dialog2.ShowDialog(this).GetValueOrDefault())
                    {
                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("Please note that the patcher may freeze during the patch, this is normal, do not close the window.", "HP2Patcher");

                        Directory.CreateDirectory(tempPath);

                        Directory.CreateDirectory(actorsPath);
                        Directory.CreateDirectory(AIPath);
                        Directory.CreateDirectory(audioPath);
                        Directory.CreateDirectory(CarsPath);
                        Directory.CreateDirectory(moviesPath);
                        Directory.CreateDirectory(ParticlePath);
                        Directory.CreateDirectory(tracksPath);

                        diskPath = Path.Combine(dialog2.SelectedPath);
                        actors = Path.Combine(dialog2.SelectedPath, "actors");
                        actorsDest = Path.Combine(tempPath, "actors.zip");
                        AI = Path.Combine(dialog2.SelectedPath, "AI");
                        AIDest = Path.Combine(tempPath, "AI.zip");
                        audio = Path.Combine(dialog2.SelectedPath, "audio");
                        audioDest = Path.Combine(tempPath, "audio.zip");
                        Cars = Path.Combine(dialog2.SelectedPath, "Cars");
                        CarsDest = Path.Combine(tempPath, "Cars.zip");
                        movies = Path.Combine(dialog2.SelectedPath, "movies");
                        moviesDest = Path.Combine(tempPath, "movies.zip");
                        Particle = Path.Combine(dialog2.SelectedPath, "Particle");
                        ParticleDest = Path.Combine(tempPath, "Particle.zip");
                        tracks = Path.Combine(dialog2.SelectedPath, "tracks");
                        tracksDest = Path.Combine(tempPath, "tracks.zip");

                        ZipFile.CreateFromDirectory(actors, actorsDest);
                        ZipFile.CreateFromDirectory(AI, AIDest);
                        ZipFile.CreateFromDirectory(audio, audioDest);
                        ZipFile.CreateFromDirectory(Cars, CarsDest);
                        ZipFile.CreateFromDirectory(movies, moviesDest);
                        ZipFile.CreateFromDirectory(Particle, ParticleDest);
                        ZipFile.CreateFromDirectory(tracks, tracksDest);

                        using (ZipArchive source = ZipFile.Open(actorsDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(actorsPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(AIDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(AIPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(audioDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(audioPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(CarsDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(CarsPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(moviesDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(moviesPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(ParticleDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(ParticlePath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }
                        using (ZipArchive source = ZipFile.Open(tracksDest, ZipArchiveMode.Read, null))
                        {
                            foreach (ZipArchiveEntry entry in source.Entries)
                            {
                                string fullPath = Path.GetFullPath(Path.Combine(tracksPath, entry.FullName));

                                if (Path.GetFileName(fullPath).Length != 0)
                                {
                                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
                                    entry.ExtractToFile(fullPath, true);
                                }
                            }
                        }

                        DelTemp();

                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("Patch complete!", "Need for Speed: Hot Pursuit 2");

                        using (TaskDialog dialog3 = new TaskDialog())
                        {
                            dialog3.WindowTitle = "Widescreen Fix";
                            dialog3.MainInstruction = "Would you like to install a Widescreen Fix?";
                            dialog3.Content = "Would you like to open the download link to the Hot Pursuit 2 Widescreen Fix? See details for installation.";
                            dialog3.ExpandedInformation = "To install the fix, download your resolution and overwrite the files in your Hot Pursuit 2 install location.";
                            dialog3.Footer = "Widescreen Fix by <a href=\"https://www.youtube.com/channel/UCgSunVswSNC4e-Xac1aoz3g\">AuToMaNiAk005</a>.";
                            dialog3.FooterIcon = TaskDialogIcon.Information;
                            dialog3.EnableHyperlinks = true;
                            TaskDialogButton yesButton = new TaskDialogButton("Yes");
                            TaskDialogButton noButton = new TaskDialogButton("No");
                            dialog3.Buttons.Add(yesButton);
                            dialog3.Buttons.Add(noButton);
                            dialog3.HyperlinkClicked += new EventHandler<HyperlinkClickedEventArgs>(TaskDialog_HyperLinkClicked);
                            TaskDialogButton button = dialog3.ShowDialog(this);
                            if (button == yesButton)
                                Process.Start("https://github.com/KilLo445/NFSPatcher/blob/main/patcher/hp2/README.md#widescreenfix");
                            else if (button == noButton)
                            {

                            }
                        }
                    }
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("Please select the location with NFSHP2.exe", "Need for Speed: Hot Pursuit 2");
                }
            }
        }
        private void TaskDialog_HyperLinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Href);
        }

        private void NoCD_Click(object sender, RoutedEventArgs e)
        {
            noCDexe = Path.Combine(tempPath, "NFSHP2.exe");

            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            dialog.Description = "Please select your Need for Speed: Hot Pursuit 2 install folder.";
            dialog.UseDescriptionForTitle = true;
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                gamePath = Path.Combine(dialog.SelectedPath);
                gameExe = Path.Combine(dialog.SelectedPath, "NFSHP2.exe");

                if (File.Exists(gameExe))
                {
                    Directory.CreateDirectory(tempPath);

                    WebClient webClient = new WebClient();

                    webClient.DownloadFile(new Uri("https://github.com/KilLo445/NFSPatcher/raw/main/patcher/hp2/NoCD/NFSHP2.exe"), noCDexe);

                    noCDgamePath = Path.Combine((dialog.SelectedPath), "NFSHP2.exe");

                    try
                    {
                        File.Delete(noCDgamePath);
                        File.Move(noCDexe, noCDgamePath);

                        DelTemp();

                        SystemSounds.Exclamation.Play();
                        MessageBox.Show("NoCD Patch installed!", "Need for Speed: Hot Pursuit 2");
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else
                {
                    SystemSounds.Exclamation.Play();
                    MessageBox.Show("Please select the location with NFSHP2.exe", "Need for Speed: Hot Pursuit 2");
                }
            }
        }
    }
}
