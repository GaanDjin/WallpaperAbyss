using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WallpaperAbyssSettings;
using ManagedWiFi;
using System.Threading;
using WallpaperAbyssApiV2;
using System.Windows.Forms;

namespace WallpaperAbyssDesktopBackgroundService
{
    /// <summary>
    /// TODO: Currently a WIP and not nearly ready for use!
    /// 
    /// Service that performas the same actions and changes the destop background rather than a screensaver.
    /// </summary>
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer tmrUpdate;
        System.Timers.Timer tmrCleanup;


        CustomWallpaper bgSetter;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (!LoadSettings())
            {
                this.ExitCode = 2;
                this.Stop();
            }
            else
            {
                if (File.Exists(CustomWallpaper.defaultBackgroundFile))
                    File.Delete(CustomWallpaper.defaultBackgroundFile);

                bgSetter = new CustomWallpaper();

                tmrUpdate = new System.Timers.Timer(1000);
                tmrCleanup = new System.Timers.Timer(1000);

                tmrUpdate.Elapsed += tmrUpdate_Tick;
                tmrCleanup.Elapsed += tmrCleanup_Tick;

                tmrUpdate.Start();
                tmrCleanup.Start();

                tmrUpdate_Tick(null, null);
            }
        }

        protected override void OnStop()
        {
            if (ctn != null)
            {
                ctn.Cancel();
            }

            Settings.Instance.Save(Settings.SavePath);
        }
        
        public bool LoadSettings()
        {
            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (!Directory.Exists(commonAppData + "\\FromThe.Blue\\WallpaperAbyss"))
                Directory.CreateDirectory(commonAppData + "\\FromThe.Blue\\WallpaperAbyss");


            string settingsFile = commonAppData + "\\FromThe.Blue\\WallpaperAbyss\\Settings.json";
            Settings.SavePath = (settingsFile);

            if (!File.Exists(settingsFile) || Settings.Instance == null)
            {
                return false;
            }
            else
            {
                WallpaperAbyssApiV2.WallpaperAbyss.AuthenticationKey = Settings.Instance.APIKey;

                CreateFileWatcher(Settings.SavePath);

                return true;
            }
        }

        public void CreateFileWatcher(string path)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();

            FileInfo fi = new FileInfo(path);
            if ((fi.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                watcher.Path = fi.Directory.FullName;
                watcher.Filter = fi.Name;
            }
            else
            {
                // Create a new FileSystemWatcher and set its properties.
                watcher.Path = path;
            }
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);

            if (ctn != null)
            {
                ctn.Cancel();
            }
            tmrUpdate.Enabled = false;
            tmrCleanup.Enabled = false;

            Settings.Reload();

            tmrUpdate.Enabled = true;
            tmrCleanup.Enabled = true;

        }

        //private static void OnRenamed(object source, RenamedEventArgs e)
        //{
        //    // Specify what is done when a file is renamed.
        //    Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        //}
        
        private void SetImage(Screen screen, CachedSearchResults result)
        {
            bgSetter.setWallforScreen(screen, result.Path);



            //this.BackgroundImageLayout = Settings.Instance.Layout;
            //if (!Settings.Instance.ShowDescription)
            //    pnlDescription.Visible = false;

            //this.BackgroundImage = Image.FromFile(result.Path);

            ////CollectionListItem cat =  WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find(x => x.Id == result.Wallpaper.id);


            //label3.Text = result.Wallpaper.name + Environment.NewLine + "ID: " + result.Wallpaper.id;

            //if (result.Wallpaper.sub_category != null && result.Wallpaper.sub_category.Trim().Length > 0)
            //    label3.Text += Environment.NewLine + "Category: " + result.Wallpaper.category;

            //if (result.Wallpaper.sub_category != null && result.Wallpaper.sub_category.Trim().Length > 0)
            //    label3.Text += " Sub: " + result.Wallpaper.sub_category;
        }

        private void SetImage(Screen screen, string path)
        {
            bgSetter.setWallforScreen(screen, path);

            //this.BackgroundImageLayout = Settings.Instance.Layout;
            //if (!Settings.Instance.ShowDescription)
            //    pnlDescription.Visible = false;

            //this.BackgroundImage = Image.FromFile(path);
            //label3.Text = path;
        }

        bool updateRunning = false;

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (updateRunning)
                return;
            updateRunning = true;

            Random r = new Random();

            if (Settings.Instance == null)
            {
                if (!LoadSettings())
                {
                    updateRunning = false;
                    return;
                }
            }
            
            if (!MeteredConnection.IsMetered(Settings.Instance.MeteredSSIDs))
            {
                Settings.Instance.PurgeCachedResults(new TimeSpan(1, 0, 0));

                if (Settings.Instance.CachedResults.Count == 0 || UnderThreshold())
                    GetMoreResults();

            }

            if (Settings.Instance.CachedResults.Count > 0)
            {
                int rand = r.Next(0, Settings.Instance.CachedResults.Count - 1);

                while (Settings.Instance.CachedResults.Count > 0 && !File.Exists(Settings.Instance.CachedResults[rand].Path))
                {
                    Settings.Instance.RemoveCachedResult(Settings.Instance.CachedResults[rand]);
                    if (Settings.Instance.CachedResults.Count > 0)
                        rand = r.Next(0, Settings.Instance.CachedResults.Count - 1);
                }

                if (Settings.Instance.CachedResults.Count > 0)
                {
                    
                    foreach (Screen screen in Screen.AllScreens)
                    {
                        rand = r.Next(0, Settings.Instance.CachedResults.Count - 1);
                        SetImage(screen, Settings.Instance.CachedResults[rand]);
                    }
                }
                else
                {
                    List<string> files = new List<string>();
                    files.AddRange(Directory.GetFiles(Settings.Instance.CacheDirectory, "*.jpg"));
                    files.AddRange(Directory.GetFiles(Settings.Instance.CacheDirectory, "*.png"));

                    if (files != null && files.Count > 0)
                    {
                        foreach (Screen screen in Screen.AllScreens)
                        {
                            rand = r.Next(0, files.Count - 1);
                            SetImage(screen, files[rand]);
                        }
                    }
                }

            }

            if (Settings.Instance.UpdateMin != Settings.Instance.UpdateMax)
                tmrUpdate.Interval = r.Next(Settings.Instance.UpdateMin * 1000, Settings.Instance.UpdateMax * 1000);
            else
                tmrUpdate.Interval = Settings.Instance.UpdateMin * 1000;

            updateRunning = false;
        }

        List<Task> tasks = new List<Task>();
        CancellationTokenSource ctn;

        bool cleanupRunning = false;
        bool changesMade = false;

        private void CleanupTasks()
        {
            cleanupRunning = true;
            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status != TaskStatus.Running && tasks[i].Status != TaskStatus.WaitingToRun)
                {
                    tasks.RemoveAt(i);
                    i--;
                }
            }

            if (tasks.Count == 0 && changesMade)
                lock (Settings.Instance)
                {
                    Settings.Instance.Save(Settings.SavePath);
                    changesMade = false;
                }

            cleanupRunning = false;
        }

        private async void GetMoreResults()
        {
            if (cleanupRunning || tasks.Count > 0)
                return;

            if (ctn == null)
                ctn = new CancellationTokenSource();

            Random r = new Random();

            SearchMethods meth = Settings.Instance.Searches[r.Next(0, Settings.Instance.Searches.Count - 1)];

            int id = meth.SubId > 0 ? meth.SubId : meth.Id;

            Settings.Instance.LastFetch = DateTime.Now;
            List<Wallpaper> wallpapers = await WallpaperAbyssApiV2.WallpaperAbyss.QueryAsync(meth.Method, InfoLevels.Basic, 30, meth.Width, meth.Height, meth.SizeOp, id, meth.SearchTerm, meth.SortBy);

            if (wallpapers.Count == 0)
            {
                //label1.Text = WallpaperAbyssApiV2.WallpaperAbyss.LastResult;
                return;
            }
            string path0 = Settings.Instance.CacheDirectory + "\\" + wallpapers[0].id + Path.GetExtension(wallpapers[0].url_image);
            wallpapers[0].Image.Save(path0);
            Settings.Instance.AddCachedResults(wallpapers[0], path0);

            wallpapers.RemoveAt(0);

            foreach (Wallpaper paper in wallpapers)
            {
                Task taskA = new Task(() => {
                    try
                    {
                        string path = Settings.Instance.CacheDirectory + "\\" + paper.id + Path.GetExtension(paper.url_image);
                        paper.Image.Save(path);
                        Settings.Instance.AddCachedResults(paper, path);
                    }
                    catch (Exception) { }
                }, ctn.Token);
                // Start the task.
                taskA.Start();

                tasks.Add(taskA);
            }

            changesMade = true;
        }

        private void tmrCleanup_Tick(object sender, EventArgs e)
        {
            if (!cleanupRunning)
            {
                CleanupTasks();
                if (tasks.Count == 0)
                    CleanupFiles();
            }
        }

        private void CleanupFiles()
        {
            string[] files = Directory.GetFiles(Settings.Instance.CacheDirectory);

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                    continue;

                bool inCache = false;
                foreach (CachedSearchResults result in Settings.Instance.CachedResults)
                {
                    if (result.Path.Equals(file, StringComparison.InvariantCultureIgnoreCase))
                    {
                        inCache = true;
                        break;
                    }
                }

                if (!inCache)
                    File.Delete(file);
            }
        }

        private bool UnderThreshold()
        {
            TimeSpan itemAge = DateTime.UtcNow - Settings.Instance.LastUsageCheck;
            TimeSpan age = new TimeSpan(1, 0, 0, 0);

            if (itemAge > age || Settings.Instance.LastUsageCheck == DateTime.MinValue)
            {
                ApiCounts counts = WallpaperAbyssApiV2.WallpaperAbyss.GetCounts();

                Settings.Instance.LastUsage = counts.Count;
                Settings.Instance.LastUsageCheck = DateTime.Now;
            }

            if (Settings.Instance.LastUsage > Settings.Instance.MaxQueries * 0.9)
                return false;

            string[] files = Directory.GetFiles(Settings.Instance.CacheDirectory);
            long size = 0;

            foreach (string file in files)
                size += new FileInfo(file).Length;

            if (size / 1048576 > Settings.Instance.MaxCacheSize * 0.9)
                return false;

            itemAge = DateTime.UtcNow - Settings.Instance.LastFetch;
            age = new TimeSpan(0, 0, 10, 0);

            if (itemAge < age)
            {

                return false;
            }



            return true;
        }
    }
}
