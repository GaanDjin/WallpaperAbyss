using ManagedWiFi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperAbyssApiV2;
using WallpaperAbyssSettings;
//using Windows.Networking.Connectivity;

namespace WallpaperAbyss
{
    /// <summary>
    /// The main window that displays Images as a screensaver.
    /// 
    /// https://stackoverflow.com/questions/2272019/how-to-display-a-windows-form-in-full-screen-on-top-of-the-taskbarFormBorderStyle = FormBorderStyle.None WindowState = FormWindowState.Maximized
    /// 
    /// </summary>
    public partial class MainForm : Form
    {
        #region Screensaver Settings Preview API's

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        /// <summary>
        /// A history of slides being displayed this session. 
        /// Allows the user to go back and forth if they saw a pick they liked.
        /// </summary>
        List<CachedSearchResults> Playlist = new List<CachedSearchResults>();

        /// <summary>
        /// The current position of the slideshow
        /// </summary>
        int PlaylistIndex = 0;

        /// <summary>
        /// Holds running tasks for image downloads
        /// </summary>
        List<Task> tasks = new List<Task>();

        /// <summary>
        /// Used to cancel running tasks as needed
        /// </summary>
        CancellationTokenSource ctn;

        /// <summary>
        /// True if the cleanup routine is running.
        /// </summary>
        bool cleanupRunning = false;

        /// <summary>
        /// true if changes to the settings have been made (cache updated)
        /// </summary>
        bool changesMade = false;

        /// <summary>
        /// Handles the number of results to fetch from the API. 
        /// Each request will get more results. This is done so that each search method will be run in a round robin style and 
        /// images from each search will be added.
        /// </summary>
        Dictionary<SearchMethods, int> dupCount = new Dictionary<SearchMethods, int>();

        /// <summary>
        /// true if we're running inside the Screensaver Settings Preview window.
        /// </summary>
        public bool IsPreviewMode { get; private set; }

        /// <summary>
        /// start off OriginalLoction with an X and Y of int.MaxValue, because
        /// it is impossible for the cursor to be at that position. That way, we
        /// know if this variable has been set yet.
        /// </summary>
        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);

        /// <summary>
        /// This constructor is passed the bounds this form is to show in.
        /// It is used when in normal mode
        /// </summary>
        /// <param name="Bounds">The bounds of the screen</param>
        /// <param name="ScreenNo">The screen number</param>
        public MainForm(Rectangle Bounds, int ScreenNo)
        {
            InitializeComponent();
            this.Bounds = Bounds;
            ShowOnMonitor(ScreenNo);
            //hide the cursor
            Cursor.Hide();
            LoadSettings();
        }

        /// <summary>
        /// This constructor is the handle to the select screen saver dialog preview window
        /// It is used when in preview mode (/p)
        /// </summary>
        /// <param name="PreviewHandle">The parent preview window</param>
        public MainForm(IntPtr PreviewHandle)
        {
            InitializeComponent();

            //set the preview window as the parent of this window
            SetParent(this.Handle, PreviewHandle);

            //make this a child window, so when the select screensaver 
            //dialog closes, this will also close
            SetWindowLong(this.Handle, -16,
                  new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            //set our window's size to the size of our window's new parent
            Rectangle ParentRect;
            GetClientRect(PreviewHandle, out ParentRect);
            this.Size = ParentRect.Size;

            //set our location at (0, 0)
            this.Location = new Point(0, 0);

            IsPreviewMode = true;
            LoadSettings();
        }

        /// <summary>
        /// Fill the specified screen 
        /// </summary>
        /// <param name="ScreenNo">Number of screen to display on</param>
        public void ShowOnMonitor(int ScreenNo)
        {
            Screen[] sc;
            sc = Screen.AllScreens;
            this.Left = sc[ScreenNo].Bounds.Width;
            this.Top = sc[ScreenNo].Bounds.Height;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = sc[ScreenNo].Bounds.Location;
            Point p = new Point(sc[ScreenNo].Bounds.Location.X, sc[ScreenNo].Bounds.Location.Y);
            this.Location = p;
        }

        /// <summary>
        /// Load the config file (and cache) from disk 
        /// </summary>
        /// <returns></returns>
        public bool LoadSettings()
        {
            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (!Directory.Exists(commonAppData + "\\FromThe.Blue\\WallpaperAbyss"))
                Directory.CreateDirectory(commonAppData + "\\FromThe.Blue\\WallpaperAbyss");


            string settingsFile = commonAppData + "\\FromThe.Blue\\WallpaperAbyss\\Settings.json";
            Settings.SavePath = (settingsFile);

            if (!File.Exists(settingsFile) || Settings.Instance == null)
            {
                lblNotConfigured.Visible = true;
                return false;
            }
            else
            {
                lblNotConfigured.Visible = false;

                WallpaperAbyssApiV2.WallpaperAbyss.AuthenticationKey = Settings.Instance.APIKey;

                //CreateFileWatcher(Settings.SavePath);
                
                return true;
            }
        }

        /// <summary>
        /// When the config file has changed we should reload our settings. 
        /// Removed: It's a screensaver. Settings shouldn't change while the computer is idle!
        /// </summary>
        /// <param name="path">The path of the config file Directory.</param>
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
                watcher.Filter = "*.json";
            }
            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
           // watcher.Filter = "*.json";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Triggered when a file is changed
        /// Removed: It's a screensaver. Settings shouldn't change while the computer is idle!
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            
            //if (ctn != null)
            //{
            //    ctn.Cancel();
            //}
            //tmrUpdate.Enabled = false;
            //tmrCleanup.Enabled = false;

            //Settings.Reload();

            //tmrUpdate.Enabled = true;
            //tmrCleanup.Enabled = true;

        }

        //private static void OnRenamed(object source, RenamedEventArgs e)
        //{
        //    // Specify what is done when a file is renamed.
        //    Console.WriteLine("File: {0} renamed to {1}", e.OldFullPath, e.FullPath);
        //}

        #region User Input

            /// <summary>
            /// When a key is pressed:
            /// If its the Left Arrow go back one picture (up to the first picture added to the slideshow)
            /// If its the Right Arrow go forward one picture (up to the last picture added to the slideshow)
            /// Any other key tells us to quit
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e">The key pressed</param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                if (PlaylistIndex > 1) //1 based index. Just go with it.
                    PlaylistIndex--;
                SetImage();
                return;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (PlaylistIndex < Playlist.Count)
                    PlaylistIndex++;
                SetImage();
                return;
            }

            //** take this if statement out if your not doing a preview
            if (!IsPreviewMode) //disable exit functions for preview
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// On click then quit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Click(object sender, EventArgs e)
        {
            //** take this if statement out if your not doing a preview
            if (!IsPreviewMode) //disable exit functions for preview
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// If the mouse has moved quit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //** take this if statement out if your not doing a preview
            if (!IsPreviewMode) //disable exit functions for preview
            {
                //see if originallocation has been set
                if (OriginalLocation.X == int.MaxValue &
                    OriginalLocation.Y == int.MaxValue)
                {
                    OriginalLocation = e.Location;
                }
                //see if the mouse has moved more than 20 pixels 
                //in any direction. If it has, close the application.
                if (Math.Abs(e.X - OriginalLocation.X) > 20 |
                    Math.Abs(e.Y - OriginalLocation.Y) > 20)
                {
                    Application.Exit();
                }
            }
        }

        #endregion

        /// <summary>
        /// Used to invoke called to the picturebox
        /// </summary>
        delegate void SetImageDelegate();

        /// <summary>
        /// Add an image to the slideshow.
        /// If the playlist pointer isn't at the end the image will be added but the picture displayed will be the next one in
        /// the slideshow after the current image displayed and not the one being added.
        /// </summary>
        /// <param name="result">The image to add</param>
        private void SetImage(CachedSearchResults result)
        {
            Playlist.Add(result);
            PlaylistIndex++;
            SetImage();
        }

        /// <summary>
        /// Increment the slideshow pointer and display the next image.
        /// Also trim to 250 slides if needed.
        /// </summary>
        private void SetImage()
        {
            if (lblImageInfo.InvokeRequired)
            {
                Invoke(new SetImageDelegate(SetImage));
                return;
            }

            try
            {
                //Only keep 250 images in the slideshow and kick out the oldest.
                if (Playlist.Count > 250)
                {
                    Playlist.RemoveAt(0);
                    if (PlaylistIndex > 0)
                        PlaylistIndex--;
                }

                if (PlaylistIndex > Playlist.Count)
                    PlaylistIndex--;

                CachedSearchResults result = Playlist[PlaylistIndex-1];

                this.BackgroundImage = Image.FromFile(result.Path);

                lblImageInfo.Text = result.Wallpaper.name + Environment.NewLine + "ID: " + result.Wallpaper.id;

                if (result.Wallpaper.sub_category != null && result.Wallpaper.sub_category.Trim().Length > 0)
                    lblImageInfo.Text += Environment.NewLine + "Category: " + result.Wallpaper.category;

                if (result.Wallpaper.sub_category != null && result.Wallpaper.sub_category.Trim().Length > 0)
                    lblImageInfo.Text += " Sub: " + result.Wallpaper.sub_category;
            }
            catch (Exception ex) {
                Console.WriteLine("MainForm.SetImage: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Fallback to throw up an image even though it's not properly cached.
        /// </summary>
        /// <param name="path">Path to image.</param>
        private void SetImage(string path)
        {
            try
            {
                this.BackgroundImage = Image.FromFile(path);
                lblImageInfo.Text = path;
            }
            catch (Exception) { }
}

        /// <summary>
        /// Timer function to handle Cache and displaying next image.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (Settings.Instance == null)
            {
                if (!LoadSettings())
                {
                    lblNotConfigured.Visible = true;

                    if (this.Height - lblNotConfigured.Height > 0 && this.Width - lblNotConfigured.Width > 0)
                    {
                        //lblNotConfigured should move around. 
                        lblNotConfigured.Left = ThreadSafeRandom.ThisThreadsRandom.Next(0, this.Width - lblNotConfigured.Width);
                        lblNotConfigured.Top = ThreadSafeRandom.ThisThreadsRandom.Next(0, this.Height - lblNotConfigured.Height);
                    }
                    return;
                }
            }
            else if (lblNotConfigured.Visible)
            {
                lblNotConfigured.Visible = false;
            }

            if (!Settings.Instance.ShowDescription)
                pnlDescription.Visible = false;

            if (PlaylistIndex < Playlist.Count)
            {
                SetImage();
            }
            try
            {
                //Handle stale cache items only if not on a metered WiFi connection. 
                //Not actually tested this yet!
                if (!MeteredConnection.IsMetered(Settings.Instance.MeteredSSIDs))
                {
                    if (Settings.Instance.LastUsage < Settings.Instance.MaxQueries * 0.9 && tasks.Count == 0) //Don't wipe out if usage is too high. 
                        Settings.Instance.PurgeCachedResults(new TimeSpan(1, 0, 0));

                    if (Settings.Instance.CachedResults.Count == 0 || UnderThreshold())
                        GetMoreResults();

                }

                if (Settings.Instance.CachedResults.Count > 0)
                {
                    int rand = ThreadSafeRandom.ThisThreadsRandom.Next(0, Settings.Instance.CachedResults.Count - 1);

                    //Remove Cache item if the image isn't on the file system. (Assume removed externally)
                    while (Settings.Instance.CachedResults.Count > 0 && !File.Exists(Settings.Instance.CachedResults[rand].Path))
                    {
                        Settings.Instance.RemoveCachedResult(Settings.Instance.CachedResults[rand]);
                        if (Settings.Instance.CachedResults.Count > 0)
                            rand = ThreadSafeRandom.ThisThreadsRandom.Next(0, Settings.Instance.CachedResults.Count - 1);
                    }

                    if (Settings.Instance.CachedResults.Count > 0)
                    {
                        rand = ThreadSafeRandom.ThisThreadsRandom.Next(0, Settings.Instance.CachedResults.Count - 1);
                        SetImage(Settings.Instance.CachedResults[rand]);
                    }
                    else
                    {
                        //Fallback if there are images but cache is empty for some reason?

                        List<string> files = new List<string>();
                        files.AddRange(Directory.GetFiles(Settings.Instance.CacheDirectory, "*.jpg"));
                        files.AddRange(Directory.GetFiles(Settings.Instance.CacheDirectory, "*.png"));

                        if (files != null && files.Count > 0)
                        {
                            rand = ThreadSafeRandom.ThisThreadsRandom.Next(0, files.Count - 1);
                            SetImage(files[rand]);
                        }
                    }

                    this.BackgroundImageLayout = Settings.Instance.Layout;
                }

            }
            catch (Exception) { }

            if (Settings.Instance.UpdateMin != Settings.Instance.UpdateMax)
                tmrUpdate.Interval = ThreadSafeRandom.ThisThreadsRandom.Next(Settings.Instance.UpdateMin * 1000, Settings.Instance.UpdateMax * 1000);
            else
                tmrUpdate.Interval = Settings.Instance.UpdateMin * 1000;

            //if (lblNotConfigured.Text.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
            //    lblNotConfigured.Visible = false;
        }

        /// <summary>
        /// Kill and Tasks that are currently running. 
        /// </summary>
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
                    try
                    {
                        Settings.Instance.Save(Settings.SavePath);
                        changesMade = false;
                    }
                    catch (Exception) { }
                }

            cleanupRunning = false;
        }

        /// <summary>
        /// Query the wallpaper Abyss for more entries and then download them. 
        /// The first image is downloaded right away if the cache is empty and following images are added to a 
        /// task to avoid thread blocking the main window.
        /// </summary>
        private async void GetMoreResults()
        {
            if (cleanupRunning || tasks.Count > 0)
                return;

            if (ctn == null)
                ctn = new CancellationTokenSource();

            if (Settings.Instance.Searches.Count == 0)
            {
                lblNotConfigured.Text = "No searches are found in Settings! Have you forgotten to add some?";
                lblNotConfigured.Visible = true;
                return;
            }

            Settings.Instance.LastFetch = DateTime.Now;

            foreach (SearchMethods meth in Settings.Instance.Searches)
            {
                if (!dupCount.ContainsKey(meth))
                    dupCount.Add(meth, 0);

                try
                {
                    int queycount = 30;

                    int size = Settings.Instance.CacheSize();

                    //Each successive query will request more results until the cache fills up.
                    if (size < Settings.Instance.MaxCacheSize * 0.9)
                        queycount *= (dupCount[meth] / 30) + 1;

                    int id = meth.SubId > 0 ? meth.SubId : meth.Id;

                    List<Wallpaper> results = await WallpaperAbyssApiV2.WallpaperAbyss.QueryAsync(meth.Method, InfoLevels.IncludeCategory, queycount, meth.Width, meth.Height, meth.SizeOp, id, meth.SearchTerm, meth.SortBy);

                    if (results != null)
                    {
                        foreach (Wallpaper result in results)
                        {
                            string path = Settings.Instance.CacheDirectory + "\\" + result.id + Path.GetExtension(result.url_image);
                            if (File.Exists(path)) //Duplicate entry from another search. Skip it.
                            {
                                dupCount[meth]++;
                                // wallpapers.Remove(result);
                            }
                            else
                            {
                                if (Settings.Instance.CachedResults.Count == 0)
                                {
                                    //Get an image up there right now.
                                    //For some reason Settings.Instance.CachedResults is empty until all threads have completed?
                                    AddImageToCache(result);

                                }
                                else
                                {
                                    Task taskA = new Task(() =>
                                    {
                                        AddImageToCache(result);
                                    }, ctn.Token);
                                    // Start the task.
                                    taskA.Start();

                                    tasks.Add(taskA);
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("MainForm.GetMoreResults: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
                }
            }
            changesMade = true;
        }

        /// <summary>
        /// Used to asyncronously download images.
        /// </summary>
        /// <param name="result">The search result to download</param>
        private void AddImageToCache(Wallpaper result)
        {
            try
            {
                string path0 = Settings.Instance.CacheDirectory + "\\" + result.id + Path.GetExtension(result.url_image);


                result.Image.Save(path0);
                Thread.Sleep(500); //Needed because without it GDI+ may/will throw an exception. 
                result.ClearCachedImages(); //Once saved clear the image to reduce memory.
                Settings.Instance.AddCachedResults(result, path0);

                //If nothing is being shown put it up there right away!
                if (this.BackgroundImage == null && Settings.Instance.CachedResults.Count > 0)
                {
                    int rand = ThreadSafeRandom.ThisThreadsRandom.Next(0, Settings.Instance.CachedResults.Count - 1);
                    SetImage(Settings.Instance.CachedResults[rand]);
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.AddImageToCache: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        /// <summary>
        /// Garbage collection 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrCleanup_Tick(object sender, EventArgs e)
        {
            if (Settings.Instance != null)
            {

                if (MeteredConnection.IsMetered(Settings.Instance.MeteredSSIDs))
                {
                    return;
                }

                if (Settings.Instance.LastUsage > Settings.Instance.MaxQueries * 0.95) //Don't wipe out if usage is too high. 
                    return;

            }

            if (!cleanupRunning)
            {
                try{ 
                CleanupTasks();
                //if (tasks.Count == 0) 
                    //CleanupFiles();
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// Remove orphan images from disk
        /// </summary>
        private void CleanupFiles()
        {
            string[] files = Directory.GetFiles(Settings.Instance.CacheDirectory);

            foreach(string file in files)
            {
                if (file.EndsWith(".json"))
                    continue;

                bool inCache = false;
                foreach( CachedSearchResults result in Settings.Instance.CachedResults)
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

        /// <summary>
        /// Checks the Wallpaper Abyss API calls and how many have been made. 
        /// Ensures we don't go over API the threshold.
        /// </summary>
        /// <returns>true if still under the max queries for the month.</returns>
        private bool UnderThreshold()
        {
            try
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


                int size = Settings.Instance.CacheSize();


                if (size > Settings.Instance.MaxCacheSize * 0.9)
                    return false;

                itemAge = DateTime.UtcNow - Settings.Instance.LastFetch;
                age = new TimeSpan(0, 0, 10, 0);

                if (itemAge < age && size > Settings.Instance.MaxCacheSize * 0.3)
                {
                    return false;
                }

            }
            catch (Exception) { }

            return true;
        }

        /// <summary>
        /// Once the form is loaded run the update routine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            //Call the image lookup and set timer right away.
            tmrUpdate_Tick(sender, e);

            this.FormClosed += MainForm_FormClosed;
        }

        /// <summary>
        /// On close cleanup any running tasks and save config file (and cache)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ctn != null)
            {
                ctn.Cancel();
            }

            try {

                CleanupFiles();
                Settings.Instance.Save(Settings.SavePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine("MainForm.FormClosed: " + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
