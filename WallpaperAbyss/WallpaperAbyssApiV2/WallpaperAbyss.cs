using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WallpaperAbyssApiV2
{
    public class WallpaperAbyss
    {
        public static string LastResult { get; private set; }

        private const string baseUrl = "https://wall.alphacoders.com/api2.0/get.php?auth=";

        /// <summary>
        /// If an error occurs while getting wallpapers and some have already been retreived then return what has been received. 
        /// To signify there was an error the last element will be null. 
        /// </summary>
        public static bool ReturnPartialResults;

        /// <summary>
        /// The API key issued by https://wall.alphacoders.com/api.php 
        /// A valid key is needed to make requests from the API.
        /// </summary>
        public static string AuthenticationKey { get; set; }

        public static async Task<List<Wallpaper>> QueryAsync(LookupMethods method, InfoLevels level = InfoLevels.Basic, int maxResults = 30, int width = 0, int height = 0, SizeOperators op = SizeOperators.Equal, int id = 0, string searchterm = null, SortMethods sortBy = SortMethods.Newest)
        {
            List<string> urls = BuildUrls(method, level, maxResults, width, height, op, id, searchterm, sortBy);

            return await Task.Run(() => {
                try { return Get(urls);
                } catch (Exception) { return null; }
                
            });
        }
        
        public static async Task<List<Wallpaper>> QueryAsync(LookupMethods method, InfoLevels level = InfoLevels.Basic, int maxResults = 30, Size? size = null, SizeOperators op = SizeOperators.Equal, int id = 0, string searchterm = null, SortMethods sortBy = SortMethods.Newest)
        {
            int width = 0;
            int height = 0;

            SizeToParams(size, out width, out height);

            List<string> urls = BuildUrls(method, level, maxResults, width, height, op, id, searchterm, sortBy);

            return await Task.Run(() => {
                try
                {
                    return Get(urls);
                }
                catch (Exception) { return null; }

            });
        }

        //public static List<Wallpaper> Newest(InfoLevels level = InfoLevels.Basic, SizeOperators op = SizeOperators.Equal, Size? size = null, int maxResults = 30)
        //{
        //    int width = 0;
        //    int height = 0;

        //    if (size != null && size.HasValue)
        //    {
        //        width = size.Value.Width;
        //        height = size.Value.Height;
        //    }

        //    List<string> urls = BuildUrls(LookupMethods.newest, level, maxResults, width, height, op, 0, SortMethods.Newest);

        //    return Get(urls);
        //}

        //public static List<Wallpaper> Newest(InfoLevels level = InfoLevels.Basic, SizeOperators op = SizeOperators.Equal, int width = 0, int height = 0, int maxResults = 30)
        //{
        //    List<string> urls = BuildUrls(LookupMethods.newest, level, maxResults, width, height, op, 0, SortMethods.Newest);

        //    return Get(urls);
        //}

        #region Counts

        public static ApiCounts GetCounts()
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.query_count));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Counts;

            return null;
        }

        public static int GetWallpaperCount(Size size, SizeOperators op)
        {
            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetWallpaperCount(width, height, op);
        }

        public static int GetWallpaperCount(int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.wallpaper_count, InfoLevels.Basic, 1, width, height, op));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetCategoryCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetCategoryCount(id, width, height, op);
        }

        public static int GetCategoryCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.category_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetCollectionCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetCollectionCount(id, width, height, op);
        }

        public static int GetCollectionCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.collection_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetGroupCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetGroupCount(id, width, height, op);
        }

        public static int GetGroupCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.group_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetSubCategoryCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetSubCategoryCount(id, width, height, op);
        }

        public static int GetSubCategoryCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.sub_category_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetFeaturedCount(Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetFeaturedCount(width, height, op);
        }

        public static int GetFeaturedCount(int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.featured_count, InfoLevels.Basic, 1, width, height, op));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetPopularCount(Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetPopularCount(width, height, op);
        }

        public static int GetPopularCount(int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.popular_count, InfoLevels.Basic, 1, width, height, op));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetTagCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetTagCount(id, width, height, op);
        }

        public static int GetTagCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.tag_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        public static int GetUserCount(int id, Size size, SizeOperators op)
        {

            int width = 0;
            int height = 0;
            SizeToParams(size, out width, out height);
            return GetUserCount(id, width, height, op);
        }

        public static int GetUserCount(int id, int width, int height, SizeOperators op)
        {
            WallpaperResponse response = Get(buildUrl(LookupMethods.user_count, InfoLevels.Basic, 1, width, height, op, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Counts == null)
                LastResult = response.ErrorMessage;
            else
                return response.Count;

            return -1;
        }

        #endregion

        #region Collection Lists

        private static List<CollectionListItem> Categories {get;set;} = new List<CollectionListItem>();
        private static List<CollectionListItem> Collections { get;  set; } = new List<CollectionListItem>();

        private static Dictionary<int, List<CollectionListItem>> SubCategories { get;  set; } = new Dictionary<int, List<CollectionListItem>>();
        private static Dictionary<int, List<CollectionListItem>> Groups { get;  set; } = new Dictionary<int, List<CollectionListItem>>();

        //public static List<CollectionListItem> Categories { get; private set; }
        //public static List<CollectionListItem> Categories { get; private set; }

        public static List<CollectionListItem> GetCategoryList(bool forceRefresh = false)
        {
            if (Categories.Count > 0 && !forceRefresh)
                return Categories;

            WallpaperResponse response = Get(buildUrl(LookupMethods.category_list));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Categories == null)
                LastResult = response.ErrorMessage;
            else
                return Categories = new List<CollectionListItem>(response.Categories);

            return new List<CollectionListItem>();
        }

        public static List<CollectionListItem> GetCollectionList(bool forceRefresh = false)
        {
            if (Collections.Count > 0 && !forceRefresh)
                return Collections;

            WallpaperResponse response = Get(buildUrl(LookupMethods.collection_list));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Collections == null)
                LastResult = response.ErrorMessage;
            else
                return Collections = new List<CollectionListItem>(response.Collections);

            return new List<CollectionListItem>();
        }

        public static List<CollectionListItem> GetGroupList(int id, bool forceRefresh = false)
        {
            if (Groups.ContainsKey(id) && !forceRefresh)
                return Groups[id];

            WallpaperResponse response = Get(buildUrl(LookupMethods.group_list, InfoLevels.Basic, 1, 0, 0, SizeOperators.Equal, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.Groups == null)
                LastResult = response.ErrorMessage;
            else
            {
                if (Groups.ContainsKey(id))
                    Groups[id] = new List<CollectionListItem>(response.Groups);
                else
                    Groups.Add(id, new List<CollectionListItem>(response.Groups));

                return Groups[id];
            }
            return new List<CollectionListItem>();
        }

        public static List<CollectionListItem> GetSubCategoryList(int id, bool forceRefresh = false)
        {
            if (SubCategories.ContainsKey(id) && !forceRefresh)
                return SubCategories[id];

            WallpaperResponse response = Get(buildUrl(LookupMethods.sub_category_list, InfoLevels.Basic, 1, 0, 0, SizeOperators.Equal, id));

            if (response == null)
                LastResult = "An unknown error has occured!";
            else if (response.SubCategories == null)
                LastResult = response.ErrorMessage;
            else
            {
                if (SubCategories.ContainsKey(id))
                    SubCategories[id] = new List<CollectionListItem>(response.SubCategories);
                else
                    SubCategories.Add(id, new List<CollectionListItem>(response.SubCategories));

                return SubCategories[id];
            }
            return new List<CollectionListItem>();
        }


        #endregion

        private static List<Wallpaper> Get(List<string> urls)
        {
            List<Wallpaper> toReturn = new List<Wallpaper>();

            foreach (string url in urls)
            {
                List<Wallpaper> returnedwallpapers;
                bool isLastPage;
                RequestErrors result = Get(url, out returnedwallpapers, out isLastPage);

                if (result != RequestErrors.NoError)
                {
                    if (ReturnPartialResults && toReturn.Count > 0 || (returnedwallpapers != null && returnedwallpapers.Count > 0))
                    {
                        if (returnedwallpapers != null)
                            toReturn.AddRange(returnedwallpapers);
                        return toReturn;
                    }
                    throw new IOException(ErrorDescriptions.GetDescription(result));
                }

                toReturn.AddRange(returnedwallpapers);
                if (isLastPage)
                    break;
                
            }

            return toReturn;
        }

        private static int MaxResultsToPages(int maxResults)
        {
            return (int)Math.Round(maxResults / 30.0, MidpointRounding.AwayFromZero);
        }

        private static List<string> BuildUrls(LookupMethods method, InfoLevels infolevel = InfoLevels.Basic, int maxResults = 30, int width = 0, int height = 0, SizeOperators op = SizeOperators.Equal, int id = 0, string query = null, SortMethods sortby = SortMethods.Newest)
        {
            int maxPages = MaxResultsToPages(maxResults);

            List<string> toReturn = new List<string>();

            for (int i = 0; i < maxPages; i++)
                toReturn.Add(buildUrl(method, infolevel, i, width, height, op, id, query, sortby));

            return toReturn;
        }

        private static string buildUrl(LookupMethods method, InfoLevels infolevel = InfoLevels.Basic, int page = 1, int width = 0, int height = 0, SizeOperators op = SizeOperators.Equal, int id = 0, string query = null, SortMethods sortby = SortMethods.Newest )
        {
            if (AuthenticationKey == null || AuthenticationKey.Trim().Length == 0)
                throw new NullReferenceException("API Key cannot be null!");

            if (page < 1)
                page = 1;

            if ((width <= 0 && height > 0)|| (width > 0 && height <= 0))
                throw new ArgumentException("Both Width and height must be specified!");

            string toReturn = baseUrl + AuthenticationKey + "&method=" + method;

            switch (infolevel)
            {
                //case InfoLevels.Basic:
                case InfoLevels.IncludeCategory:
                    toReturn += "&info_level=2";
                    break;
                case InfoLevels.IncludeCategoryCollectionAndGroup:
                    toReturn += "&info_level=3";
                    break;
            }

            if (page > 1)
                toReturn += "&page=" + page;

            if (width > 0)
            {
                toReturn += "&width=" + width + "&height=" + height;
            }

            switch (op)
            {
                //case SizeOperators.Equal:
                case SizeOperators.Max:
                    toReturn += "&operator=max";
                    break;
                case SizeOperators.Min:
                    toReturn += "&operator=min";
                        break;
            }

            if (id > 0)
                toReturn += "&id=" + id;

            if (method == LookupMethods.search || method == LookupMethods.user)
            {
                toReturn += "&term=" + Uri.EscapeDataString( query);
            }

            switch (sortby)
            {
                case SortMethods.Favorites:
                    toReturn += "&sort=favorites";
                    break;
                case SortMethods.Newest:
                    toReturn += "&sort=newest";
                    break;
                case SortMethods.Rating:
                    toReturn += "&sort=rating";
                    break;
                case SortMethods.Views:
                    toReturn += "&sort=views";
                    break;
            }

            return toReturn += "&check_last=1";
        }

        private static RequestErrors Get(String requestUrl, out List<Wallpaper> returnedwallpapers, out bool isLastPage)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 10);
            Stream stream;

            Task<Stream> task = new Task<Stream>(() => { return new System.Net.WebClient().OpenRead(requestUrl); });

            task.Start();

            bool timedout = false;
            DateTime start = DateTime.Now;

            while(!task.IsCompleted)
            {
                Task.Delay(100);

                if (DateTime.Now - start >  timeout)
                {
                    timedout = true;
                    break;
                }
            }

            if (!timedout)
            {
                stream = task.Result;
                // task completed within timeout
            }
            else
            {
                isLastPage = true;
                returnedwallpapers = null;
                return RequestErrors.request_timeout;
                // timeout logic
            }

            //Stream stream = new System.Net.WebClient().OpenRead(requestUrl);

            var serializer = new DataContractJsonSerializer(typeof(WallpaperResponse));
            WallpaperResponse deserializedResult = (WallpaperResponse)serializer.ReadObject(stream);


            if (deserializedResult.Success == true)
            {
                LastResult = "OK";

                returnedwallpapers = new List<Wallpaper>(deserializedResult.Wallpapers);
                isLastPage = deserializedResult.IsLast;
                return RequestErrors.NoError;
            }

            LastResult = deserializedResult.ErrorMessage;

            isLastPage = true;
            returnedwallpapers = null;
            return ErrorDescriptions.ToEnum(deserializedResult.ErrorMessage);
        }

        private static WallpaperResponse Get(String requestUrl)
        {
            Stream stream = new System.Net.WebClient().OpenRead(requestUrl);

            var serializer = new DataContractJsonSerializer(typeof(WallpaperResponse));
            WallpaperResponse deserializedResult = (WallpaperResponse)serializer.ReadObject(stream);

            if (deserializedResult.Success == true)
                LastResult = "OK";
            else
                LastResult = deserializedResult.ErrorMessage;

            return deserializedResult;
        }
        
        private static void SizeToParams(Size? size, out int width, out int height)
        {
            if (size != null && size.HasValue)
            {
                width = size.Value.Width;
                height = size.Value.Height;
            }
            else
            {
                width = 0;
                height = 0;
            }
        }

    }
}
