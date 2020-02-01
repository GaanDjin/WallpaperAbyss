using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace WallpaperAbyssApiV2
{
    public class Wallpaper
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool featured { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string file_type { get; set; }
        public int file_size { get; set; } //Octets
        public string url_image { get; set; }
        public string url_thumb { get; set; }
        public string url_page { get; set; }
        public string category { get; set; }
        public int category_id { get; set; }
        public string sub_category { get; set; }
        public int sub_category_id { get; set; }
        public string user_name { get; set; }
        public int user_id { get; set; }
        public string collection { get; set; }
        public int collection_id { get; set; }
        public string group { get; set; }
        public int group_id { get; set; }

        public List<Tag> Tags { get; set; } = null;

        private Image img;

        [XmlIgnore]
        [ScriptIgnore]
        public Image Image
        {
            get
            {
                if (img != null)
                    return img;

                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(new Uri(url_image));
                MemoryStream ms = new MemoryStream(bytes);
                img = System.Drawing.Image.FromStream(ms);

                return img;
            }
        }
        
        private Image thumb;

        [XmlIgnore]
        [ScriptIgnore]
        public Image Thumb
        {
            get
            {
                if (thumb != null)
                    return thumb;

                WebClient wc = new WebClient();
                byte[] bytes = wc.DownloadData(new Uri(url_image));
                MemoryStream ms = new MemoryStream(bytes);
                thumb = System.Drawing.Image.FromStream(ms);

                return thumb;
            }
        }


        public void ClearCachedImages()
        {
            img = null;
            thumb = null;
        }
    }

    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
