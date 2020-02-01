using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WallpaperAbyssApiV2;

namespace WallpaperAbyssSettings
{
    /// <summary>
    /// Settings class used to store all the settings needed for the Screensaver and cached images
    /// </summary>
    public class Settings : AppSettings<Settings>
    {
        /// <summary>
        /// The API Key used to connect to Wallpaper Abyss
        /// </summary>
        public string APIKey { get; set; } = "";

        /// <summary>
        /// A list of SSIDs that the app should not download images on. 
        /// Useful, for instance, if you are on a laptop and tethered to your data plan.
        /// </summary>
        public List<string> MeteredSSIDs { get; set; } = new List<string>();

        /// <summary>
        /// The list of search methods to use. 
        /// </summary>
        public List<SearchMethods> Searches { get; set; } = new List<SearchMethods>();

        /// <summary>
        /// Minimum number of seconds between image change
        /// </summary>
        public int UpdateMin { get; set; } = 30;

        /// <summary>
        /// Maximum number of seconds between image change
        /// </summary>
        public int UpdateMax { get; set; } = 300;

        /// <summary>
        /// The maximum size of images to keep cached in MB.
        /// </summary>
        public int MaxCacheSize { get; set; } = 256;

        /// <summary>
        /// Directory to cache images in.
        /// </summary>
        public string CacheDirectory { get; set; }

        /// <summary>
        /// The maximum query count to perform.
        /// </summary>
        public int MaxQueries { get; set; } = 140000;

        /// <summary>
        /// Show the image details and powered by in the bottom.
        /// </summary>
        public bool ShowDescription { get; set; } = true;

        /// <summary>
        /// Saved results. 
        /// </summary>
        public List<CachedSearchResults> CachedResults { get; set; } = new List<CachedSearchResults>();

        /// <summary>
        /// Last time the quota limit was checked
        /// </summary>
        public DateTime LastUsageCheck { get; set; }

        /// <summary>
        /// What the API Quota usage is at.
        /// </summary>
        public int LastUsage { get; set; }

        /// <summary>
        /// Last time Images were searched for
        /// </summary>
        public DateTime LastFetch { get; set; }

        /// <summary>
        /// How should the image be displayed if it doesn't fit the window. 
        /// </summary>
        public System.Windows.Forms.ImageLayout Layout { get; set; }

        /// <summary>
        /// Delete all images and the metadata (cache) for all images over the specified age
        /// </summary>
        /// <param name="age">The max age to keep. (Ie any items older than this will be deleted)</param>
        public void PurgeCachedResults(TimeSpan age)
        {
            lock (threadLock)
            {
                for (int i = 0; i < CachedResults.Count * 0.3; i++)
                {
                    TimeSpan itemAge = DateTime.Now - CachedResults[i].ResultDate;

                    if (itemAge >  age)
                    {
                        try
                        {
                            File.Delete(CachedResults[i].Path);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Settings.PurgeCachedResults: " + Environment.NewLine + e.Message + Environment.NewLine + e.StackTrace);
                        }
                        CachedResults.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        /// <summary>
        /// How much space does the cache take up?
        /// </summary>
        /// <returns>The size in MB of the cached images.</returns>
        public int CacheSize()
        {
            string[] files = Directory.GetFiles(Settings.Instance.CacheDirectory);
            long size = 0;

            foreach (string file in files)
                size += new FileInfo(file).Length;

            return (int)(size / 1048576);
        }

        /// <summary>
        /// Delegate to <see cref="AddCachedResults(Wallpaper, string)"/> for sub-tasks.
        /// </summary>
        /// <param name="wallpaper">Wallpaper to add</param>
        /// <param name="savedpath">The full path to the image file</param>
        public delegate void AddCachedResultsDelegate(Wallpaper wallpaper, string savedpath);

        /// <summary>
        /// Adds a wallpaper to the cache
        /// </summary>
        /// <param name="wallpaper">Wallpaper to add</param>
        /// <param name="savedpath">The full path to the image file</param>
        public void AddCachedResults(Wallpaper wallpaper, string savedpath)
        {
            if (wallpaper == null || !File.Exists(savedpath))
                return;

            if (CacheSize() > Settings.Instance.MaxCacheSize)
                return;


            lock (threadLock)
            {
                CachedSearchResults item;
                for(int i = 0; i < CachedResults.Count; i++)
                {
                    item = CachedResults[i];

                    //If the wallpaper is already in the cache just refresh its age. 
                    //That way duplicates are avoided and 
                    if (item.Wallpaper.id == wallpaper.id)
                    {
                        item.ResultDate = DateTime.Now;
                        return;
                    }
                }

                CachedSearchResults newitem = new CachedSearchResults();
                newitem.Path = savedpath;
                newitem.ResultDate = DateTime.Now;
                newitem.Wallpaper = wallpaper;

                CachedResults.Add(newitem);
            }
        }
 
        /// <summary>
        /// Remove a cached image from the cache
        /// </summary>
        /// <param name="result">The item to remove</param>
        public void RemoveCachedResult(CachedSearchResults result)
        {
            lock (threadLock)
            {
                CachedResults.Remove(result);
            }
        }

        /// <summary>
        /// The path to the settings file. 
        /// Usually: C:\\ProgramData\\FromThe.Blue\\WallpaperAbyss\\Settings.json
        /// </summary>
        public static string SavePath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\FromThe.Blue\\WallpaperAbyss\\Settings.json";
        
        /// <summary>
        /// Static instance of this settings
        /// </summary>
        private static Settings instance;

        /// <summary>
        /// Used for adding and removing items from the cache in a threadsafe manner
        /// </summary>
        internal static object threadLock = new Object();


        /// <summary>
        /// Re-reads the config file and loads read settings into Instance
        /// </summary>
        public static bool Reload()
        {
            try
            {
                lock (threadLock)
                {
                    instance = Settings.Load(SavePath);
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }

        /// <summary>
        /// Gets the static instance of the settings. 
        /// If the static instance is null we try to initialize it. 
        /// If that failes return null signifying that no settings are set up!
        /// </summary>
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (threadLock)
                    {
                        if (instance == null)
                        {
                            if (!File.Exists(SavePath))
                                return instance;

                            instance = Settings.Load(SavePath);
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Create a default settings with one search item and the default cache folder points to: 
        /// C:\\ProgramData\\FromThe.Blue\\WallpaperAbyss\\
        /// </summary>
        public Settings()
        {
            CacheDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\FromThe.Blue\\WallpaperAbyss";

            SearchMethods search = new SearchMethods();
            search.Method = LookupMethods.newest;

            Searches.Add(search);
        }
    }

    /// <summary>
    /// Stores information about the search info in settings. Used as a reference when searching the API.
    /// </summary>
    public class SearchMethods
    {
        /// <summary>
        /// Lookup Method
        /// </summary>
        public LookupMethods Method { get; set; } = LookupMethods.newest;

        /// <summary>
        /// ID of the image
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Used for the Category Sub ID
        /// </summary>
        public int SubId { get; set; }

        /// <summary>
        /// How results should be sorted
        /// </summary>
        public SortMethods SortBy { get; set; } = SortMethods.Favorites;

        /// <summary>
        /// Width of the wallpaper.
        /// 0 for any
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height of the wallpaper.
        /// 0 for any
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// The type of resolution filter, <= will be used with 'max', = with 'equal' and >= with 'min'.
        /// </summary>
        public SizeOperators SizeOp { get; set; } = SizeOperators.Equal;

        /// <summary>
        /// Term to match when searching
        /// </summary>
        public string SearchTerm { get; set; } = "";
    }

    /// <summary>
    /// A cached search result. 
    /// </summary>
    public class CachedSearchResults
    {
        /// <summary>
        /// Date and time the result was issued
        /// </summary>
        public DateTime ResultDate { get; set; }
        /// <summary>
        /// The wallpaper info from the API
        /// </summary>
        public Wallpaper Wallpaper { get; set; }
        /// <summary>
        /// The physical path on disk to the image
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The size of the image. In bytes.
        /// </summary>
        public int Size { get; set; }
    }

    /// <summary>
    /// Helper class to save and load <see cref="Settings"/> in JSON format
    /// </summary>
    /// <typeparam name="T">type T must be an instantiable object</typeparam>
    public class AppSettings<T> where T : new()
    {
        /// <summary>
        /// Default file save name
        /// </summary>
        private const string DEFAULT_FILENAME = "settings.json";

        /// <summary>
        /// Save to file in JSON format. This method is to be used by a sub-class.
        /// If the target exists it will be overwritten.
        /// </summary>
        /// <param name="fileName">Path of save file</param>
        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        /// <summary>
        /// Save object T to file in JSON format.
        /// If the target exists it will be overwritten.
        /// </summary>
        /// <param name="pSettings">Object to save</param>
        /// <param name="fileName">Path of save file</param>
        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(pSettings));
        }

        /// <summary>
        /// Attemps to load an object from a saved JSON file.
        /// </summary>
        /// <param name="fileName">Path of file to load</param>
        /// <returns>The instantiated object or default using new T();</returns>
        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
                t = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));
            return t;
        }
    }
}
