using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WallpaperAbyssApiV2
{
    /// <summary>
    /// How are the wallpapers sorted.
    /// </summary>
    public enum SortMethods
    {
        Newest,
        Rating,
        Views,
        Favorites
    }

    /// <summary>
    /// The operator to use with the resolution options. 
    /// </summary>
    public enum SizeOperators
    {
        /// <summary>
        /// Resolution should be no bigger than specified Height and Width. 
        /// </summary>
        Max,
        /// <summary>
        /// Resolution should be equal to specified Height and Width. 
        /// </summary>
        Equal,
        /// <summary>
        /// Resolution should be no smaller than specified Height and Width. 
        /// </summary>
        Min
    }
    
    /// <summary>
     /// The amount of info to request. 
     /// </summary>
    public enum InfoLevels
    {
        /// <summary>
        /// Requests only info about the image. Size, and urls. 
        /// </summary>
        Basic = 1,
        /// <summary>
        /// Includes basic as well as category and submitting user
        /// </summary>
        IncludeCategory = 2,
        /// <summary>
        /// Includes all other information as well as collection and group. 
        /// </summary>
        IncludeCategoryCollectionAndGroup = 3
    }

    public enum RequestErrors
    {
        [Description("An Unknown Error has occurred.")]
        Unknown,
        [Description("No error.")]
        NoError,
        [Description("No authentication key provided.")]
        auth_missing,
        [Description("Invalid authentication key.")]
        invalid_auth,
        [Description("No method name provided.")]
        method_missing,
        [Description("Failed to connect to database.")]
        db_connect,
        [Description("No wallpaper ID provided.")]
        id_missing,
        [Description("No search term provided.")]
        term_missing,
        [Description("Request has timed out.")]
        request_timeout
    }

    public enum LookupMethods
    {
        newest,
        highest_rated,
        by_views,
        by_favorites,
        wallpaper_count,
        category,
        category_count,
        category_list,
        collection,
        collection_count,
        collection_list,
        group,
        group_count,
        group_list,
        sub_category,
        sub_category_count,
        sub_category_list,
        featured,
        featured_count,
        popular,
        popular_count,
        tag,
        tag_count,
        user,
        user_count,
        search,
        wallpaper_info,
        random,
        query_count,
    }

}
