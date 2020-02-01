using System.Runtime.Serialization;

namespace WallpaperAbyssApiV2
{
    [DataContract]
    public class WallpaperResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "error")]
        public string ErrorMessage { get; set; }

        [DataMember(Name = "is_last")]
        public bool IsLast { get; set; }

        [DataMember(Name = "wallpapers")]
        public Wallpaper[] Wallpapers { get; set; }

        [DataMember(Name = "wallpaper")]
        public Wallpaper Wallpaper { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "counts")]
        public ApiCounts Counts { get; set; }

        [DataMember(Name = "tags")]
        public Tag[] Tags { get; set; }

        [DataMember(Name = "total_match")]
        public int TotalMached { get; set; }

        [DataMember(Name = "categories")]
        public CollectionListItem[] Categories { get; set; }

        [DataMember(Name = "collections")]
        public CollectionListItem[] Collections { get; set; }

        [DataMember(Name = "groups")]
        public CollectionListItem[] Groups { get; set; }

        [DataMember(Name = "sub-categories")]
        public CollectionListItem[] SubCategories { get; set; }

        

            

    }
}
