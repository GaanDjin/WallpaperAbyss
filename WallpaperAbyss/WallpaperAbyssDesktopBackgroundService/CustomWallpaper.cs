using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperAbyssDesktopBackgroundService
{
    /// <summary>
    /// https://stackoverflow.com/questions/1540337/how-to-set-multiple-desktop-backgrounds-dual-monitor
    /// 
    /// Controls the ability to set the wallpaper image for each screen.
    /// </summary>
    public class CustomWallpaper
    {
        const int SetDeskWallpaper = 20;
        const int UpdateIniFile = 0x01;
        const int SendWinIniChange = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        
        /// <summary>
        /// The (0,0) point for the primary monitor
        /// </summary>
        Point primaryMonitorPoint = new Point(0, 0);

        /// <summary>
        /// The default background image file
        /// </summary>
        public const string defaultBackgroundFile = @"C:\Users\Public\Pictures\DefaultBackground.bmp";

        /// <summary>
        /// A Dictionary holding an image for each screen. Or asingle image under the key "all" for a single image or single screen use.
        /// </summary>
        Dictionary<string, Image> images = new Dictionary<string, Image>();

        /// <summary>
        /// A list of each screen number as they may not be sequential depending on display order
        /// </summary>
        List<string> screenos = new List<string>();

        /// <summary>
        /// Index of the last screen updated
        /// </summary>
        int index = 0;

        /// <summary>
        /// Initializes screen information and attempts to get the last wallpaper used
        /// </summary>
        public CustomWallpaper()
        {
            //figure out where the main monitor is in relation to the virtualScreenBitmap
            foreach (Screen scr in Screen.AllScreens)
            {
                images.Add(scr.DeviceName, null);
                screenos.Add(scr.DeviceName);
                if (scr.Bounds.Left < primaryMonitorPoint.X)
                    primaryMonitorPoint.X = scr.Bounds.Left;
                if (scr.Bounds.Top < primaryMonitorPoint.Y)
                    primaryMonitorPoint.Y = scr.Bounds.Top;
            }
            primaryMonitorPoint.X *= -1;
            primaryMonitorPoint.Y *= -1;

            //Image for multiple screens
            images.Add("all", null);

            //set Images in Dictionary in case there are previous Images
            if (File.Exists(defaultBackgroundFile))
            {
                using (var old = new Bitmap(defaultBackgroundFile))
                {
                    foreach (Screen scr in Screen.AllScreens)
                    {
                        Rectangle rectangle = new Rectangle(primaryMonitorPoint.X + scr.Bounds.Left, primaryMonitorPoint.Y + scr.Bounds.Top, scr.Bounds.Width, scr.Bounds.Height);
                        if (old.Width >= (rectangle.X + rectangle.Width) &&
                            old.Height >= (rectangle.Y + rectangle.Height))
                            images[scr.DeviceName] = (Bitmap)old.Clone(rectangle, old.PixelFormat);
                    }
                }
            }
        }
        
        /// <summary>
        /// Sets the background wallpaper on a screen to be the images located under file.
        /// The screen is incremented to 
        /// </summary>
        /// <param name="file">The path of the image to set as the background</param>
        public void setAlternatingWalls(string file)
        {
            images[screenos[index]] = Image.FromFile(file);
            index++;
            if (index == screenos.Count)
                index = 0;

            CreateBackgroundImage(Method.multiple);
            GC.Collect();
        }

        /// <summary>
        /// Sets the background wallpaper on the specified screen
        /// </summary>
        /// <param name="screen">The screen to set</param>
        /// <param name="file">The path of the image to set as the background</param>
        public void setWallforScreen(Screen screen, string file)
        {
            if (Screen.AllScreens.Length == 1)
            {
                setAlternatingWalls(file);
            }
            else
            {
                images[screen.DeviceName] = Image.FromFile(file);
                CreateBackgroundImage(Method.multiple);
                GC.Collect();
            }
        }

        /// <summary>
        /// Sets the background wallpaper on all screens to be the images located under file.
        /// </summary>
        /// <param name="file">The path of the image to set as the background</param>
        public void setWallforAllScreen(string file)
        {
            images["all"] = Image.FromFile(file);
            CreateBackgroundImage(Method.single);
            GC.Collect();
        }
        
        /// <summary>
        /// Method to use when creating the background
        /// </summary>
        private enum Method
        {
            /// <summary>
            /// Multiple images
            /// </summary>
            multiple,
            /// <summary>
            /// A single image
            /// </summary>
            single
        }

        /// <summary>
        /// Generate a new background image, save it, and then update the registry to change the desktop background.
        /// </summary>
        /// <param name="method">Multipule or single images to be used</param>
        private void CreateBackgroundImage(Method method)
        {

            using (var virtualScreenBitmap = new Bitmap((int)System.Windows.Forms.SystemInformation.VirtualScreen.Width, (int)System.Windows.Forms.SystemInformation.VirtualScreen.Height))
            {
                using (var virtualScreenGraphic = Graphics.FromImage(virtualScreenBitmap))
                {

                    switch (method)
                    {
                        // alternated Screen Images
                        case Method.multiple:
                            foreach (var screen in System.Windows.Forms.Screen.AllScreens)
                            {
                                // gets the image which we want to place in virtualScreenGraphic
                                var image = (images.ContainsKey(screen.DeviceName)) ? images[screen.DeviceName] : null;

                                //sets the position and size where the images will go
                                Rectangle rectangle = new Rectangle(primaryMonitorPoint.X + screen.Bounds.Left, primaryMonitorPoint.Y + screen.Bounds.Top, screen.Bounds.Width, screen.Bounds.Height);

                                // produce a image for the screen and fill it with the desired image... centered
                                var monitorBitmap = new Bitmap(rectangle.Width, rectangle.Height);
                                if (image != null)
                                    DrawImageCentered(Graphics.FromImage(monitorBitmap), image, rectangle);

                                //draws the picture at the right place in virtualScreenGraphic
                                virtualScreenGraphic.DrawImage(monitorBitmap, rectangle);
                            }
                            break;

                        //Single screen Image
                        case Method.single:
                            // gets the image which we want to place in virtualScreenGraphic
                            var image2 = images["all"];

                            //sets the position and size where the images will go
                            Rectangle rectangle2 = new Rectangle(0, 0, virtualScreenBitmap.Width, virtualScreenBitmap.Height);

                            // fill with the desired image... centered                            
                            if (image2 != null)
                                DrawImageCentered(virtualScreenGraphic, image2, rectangle2);

                            //draws the picture at the right place in virtualScreenGraphic
                            virtualScreenGraphic.DrawImage(virtualScreenBitmap, rectangle2);
                            break;
                    }

                    if (File.Exists(defaultBackgroundFile))
                        File.Delete(defaultBackgroundFile);

                    virtualScreenBitmap.Save(defaultBackgroundFile, ImageFormat.Bmp);
                }
            }

            /*
Picture Position	WallpaperStyle	TileWallpaper
Stretched	2	0
Centered	1	0
Tiled	1	1
Fit*	6	0
Fill*	10	0*/

            //key.SetValue(@"WallpaperStyle", 0.ToString());
            //key.SetValue(@"TileWallpaper", 1.ToString());
            int result = SystemParametersInfo(SetDeskWallpaper, 0, defaultBackgroundFile, UpdateIniFile | SendWinIniChange);

           RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallPaper", defaultBackgroundFile);
            key.SetValue(@"TileWallpaper", 0.ToString());
            switch (WallpaperAbyssSettings.Settings.Instance.Layout)
            {
                case ImageLayout.Center: key.SetValue(@"WallpaperStyle", 1.ToString()); break;
                case ImageLayout.None: key.SetValue(@"WallpaperStyle", 0.ToString()); break;
                case ImageLayout.Stretch: key.SetValue(@"WallpaperStyle", 2.ToString()); break;
                case ImageLayout.Tile: key.SetValue(@"WallpaperStyle", 1.ToString()); key.SetValue(@"TileWallpaper", 1.ToString()); break;
                case ImageLayout.Zoom: key.SetValue(@"WallpaperStyle", 10.ToString()); break;
            }

         }

        /// <summary>
        /// Draw an image centered inside the specified Rectangle
        /// </summary>
        /// <param name="g">Graphics target to draw on</param>
        /// <param name="img">The image to draw</param>
        /// <param name="monitorRect">The Rectangle area to draw the image in (centered within this rect)</param>
        private void DrawImageCentered(Graphics g, Image img, Rectangle monitorRect)
        {
            double ratiodev = (1.0 * monitorRect.Width / monitorRect.Height) - (1.0 * img.Width / img.Height);
            if (((1.0 * monitorRect.Width / monitorRect.Height > 1) && ratiodev > -0.25 && ratiodev < 0.25))
            {
                img = getsnappedIMG(img, monitorRect);
            }


            float heightRatio = (float)monitorRect.Height / (float)img.Height;
            float widthRatio = (float)monitorRect.Width / (float)img.Width;
            int height = monitorRect.Height;
            int width = monitorRect.Width;
            int x = 0;
            int y = 0;

            if (heightRatio < widthRatio)
            {
                width = (int)((float)img.Width * heightRatio);
                height = (int)((float)img.Height * heightRatio);
                x = (int)((float)(monitorRect.Width - width) / 2f);
            }
            else
            {
                width = (int)((float)img.Width * widthRatio);
                height = (int)((float)img.Height * widthRatio);
                y = (int)((float)(monitorRect.Height - height) / 2f);
            }
            Rectangle rect = new Rectangle(x, y, width, height);
            g.DrawImage(img, rect);
        }

        /// <summary>
        /// Clones an area of the specified image
        /// </summary>
        /// <param name="img">Image to copy</param>
        /// <param name="monitorRect">Area of image to copy</param>
        /// <returns>The copied area of the image.</returns>
        private Image getsnappedIMG(Image img, Rectangle monitorRect)
        {
            double ratiodev = (1.0 * monitorRect.Width / monitorRect.Height) - (1.0 * img.Width / img.Height);
            int height = img.Height;
            int width = img.Width;

            Rectangle rect;
            if (ratiodev < 0)
            {
                rect = new Rectangle(0, 0, (int)((1.0 * monitorRect.Width / monitorRect.Height) * height), height);
                rect.X = (width - rect.Width) / 2;
            }
            else
            {
                rect = new Rectangle(0, 0, width, (int)(1.0 * width / (1.0 * monitorRect.Width / monitorRect.Height)));
                rect.Y = (height - rect.Height) / 2;
            }


            var img2 = (Bitmap)img;
            return (Bitmap)img2.Clone(rect, img.PixelFormat);

        }
    }
}
