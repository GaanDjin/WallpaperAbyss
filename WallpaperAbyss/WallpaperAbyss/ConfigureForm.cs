using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperAbyssSettings;

namespace WallpaperAbyss
{
    /// <summary>
    /// Loads and saves changes to the configuration file
    /// </summary>
    public partial class ConfigureForm : Form
    {
        public ConfigureForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Load the form and set up Tool Tips
        /// TODO: Break out Tool Tip text for multi-lingual support
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            if (LoadSettings())
            {
                Populate();
            }

            /// TODO: Not Ready yet!
            tabCtrlSections.TabPages.Remove(tabBGService);

            lblAPIKey.Links.Add(0, lblAPIKey.Text.Length, "https://wall.alphacoders.com/api.php");

            //toolTip1.SetToolTip(, "My button1");
            toolTipHolder.SetToolTip(this.txtAPIKey,"The API key obtained from Wallpaper Abyss");
                toolTipHolder.SetToolTip(this.txtMinSec, "The minumum time (in seconds) to switch the wallpaper image.\nThe wallpaper will change at a random interval between these two numbers.");
            toolTipHolder.SetToolTip(this.txtMaxSec, "The maximum time (in seconds) to switch the wallpaper image.\nThe wallpaper will change at a random interval between these two numbers.");
            toolTipHolder.SetToolTip(this.txtCacheSize, "The maximum size in MB to use for downloading images.");
            toolTipHolder.SetToolTip(this.btnClear, "Clears all images that have been downloaded.");
            toolTipHolder.SetToolTip(this.txtWorkingDir, "The Directory to store images that have been downloaded.\nWarning! Any file that isn't the settings file or an active wallpaper will be deleted from this Directory.");
            toolTipHolder.SetToolTip(this.txtMaxQueries, "The maximum number of queries the screensaver will make per month to Wallpaper Abyss.\nThis should be kept at leat 10% lower than what you want to use as querying for api usage also takes up 1 query and this program will continue to query for usage.");
            toolTipHolder.SetToolTip(this.cboImageLayout, @"The Layout images should be displayed.\n
None: Keep the original image size\n
Center: Center the image on the screen\n
Stretch: Strech the image (and change the aspect ratio to fit 100% of the screen\n
Tile: Keep the original image size and if there is space then tile the image\n
Zoom: Make the picture fit the window but keep the same aspect ratio");
            toolTipHolder.SetToolTip(this.chkShowBottomBar, "Show the bottom bar w/ about and information on the current wallpaper");
            toolTipHolder.SetToolTip(this.cboLookupMethods, "What is the critera for finding wallpapers.");
            toolTipHolder.SetToolTip(this.cboSortMethods, "How should the results be sorted.");
            toolTipHolder.SetToolTip(this.cboSizeOperators, @"Should the wallpapers match a certain size.\nWhen this is set the Width & Height must also be set.\n
Equal: Wallpepers should only be the specified size\n
Max: Wallpepers must be no bigger than the specified size\n
Min: The wallpapers must be larger than the specified size");
            toolTipHolder.SetToolTip(this.txtWidth, "The specified Width of the wallpaper. If this is set then the Height must be larger than 0 as well or it will be ignored.");
            toolTipHolder.SetToolTip(this.txtHeight, "The specified Height of the wallpaper. If this is set then the Width must be larger than 0 as well or it will be ignored.");
            toolTipHolder.SetToolTip(this.btnAddSearchMethod, "Add a new search");
            toolTipHolder.SetToolTip(this.btnRemoveSearchMethod, "Remove the selected search");

        }

        /// <summary>
        /// Go to the API web page on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblAPIKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        #region Init Settings

        /// <summary>
        /// Load config file from disk.
        /// If the settings file doesn't exist attempt to create it.
        /// </summary>
        /// <returns>false only if the config file does not exist and we can't create it</returns>
        public bool LoadSettings()
        {
            string commonAppData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            if (!Directory.Exists(commonAppData + "\\FromThe.Blue\\WallpaperAbyss"))
                Directory.CreateDirectory(commonAppData + "\\FromThe.Blue\\WallpaperAbyss");


            string settingsFile = commonAppData + "\\FromThe.Blue\\WallpaperAbyss\\Settings.json";
            Settings.SavePath = (settingsFile);

            if (!File.Exists(settingsFile) || Settings.Instance == null)
            {
                Settings s = new Settings();
                s.Save(settingsFile);

                if (Settings.Instance == null)
                {
                    MessageBox.Show("Failed to create settings file in " + settingsFile + "!");
                    return false;
                }
                else
                    return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Populate window fields with config settings.
        /// </summary>
        public void Populate()
        {
            if (Settings.Instance == null)
                return;

            if (Settings.Instance.APIKey != null && Settings.Instance.APIKey.Trim().Length > 0)
            {
                WallpaperAbyssApiV2.WallpaperAbyss.AuthenticationKey = Settings.Instance.APIKey;
                WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList();
                WallpaperAbyssApiV2.WallpaperAbyss.GetCollectionList();
            }

            SetupManagedWiFiTable();

            SetupSearchMethods();

            //
            txtAPIKey.Text = Settings.Instance.APIKey;

            /// <summary>
            /// Minimum number of seconds between image change
            /// </summary>
            txtMinSec.Value = Settings.Instance.UpdateMin;

            /// <summary>
            /// Maximum number of seconds between image change
            /// </summary>
            txtMaxSec.Value = Settings.Instance.UpdateMax;

            /// <summary>
            /// The maximum size of images to keep cached in MB.
            /// </summary>
            txtCacheSize.Value = Settings.Instance.MaxCacheSize;

            /// <summary>
            /// Directory to cache images in.
            /// </summary>
            txtWorkingDir.Text = workingDirDialog.SelectedPath = Settings.Instance.CacheDirectory;

            /// <summary>
            /// The maximum query count to perform.
            /// </summary>
            txtMaxQueries.Value = Settings.Instance.MaxQueries;

            /// <summary>
            /// Show the image details and powered by in the bottom.
            /// </summary>
            chkShowBottomBar.Checked = Settings.Instance.ShowDescription;

            /// <summary>
            /// How should the image be displayed if it doesn't fit the window. 
            /// </summary>
            switch (Settings.Instance.Layout)
            {
                case ImageLayout.Center:
                    cboImageLayout.SelectedItem = "Center";
                    break;
                case ImageLayout.None:
                    cboImageLayout.SelectedItem = "None";
                    break;
                case ImageLayout.Stretch:
                    cboImageLayout.SelectedItem = "Stretch";
                    break;
                case ImageLayout.Tile:
                    cboImageLayout.SelectedItem = "Tile";
                    break;
                case ImageLayout.Zoom:
                    cboImageLayout.SelectedItem = "Zoom";
                    break;
            }
        }

        /// <summary>
        /// When a search method (search list) is clicked update the frames to show the appropriate fields for the search type 
        /// </summary>
        /// <param name="index">Index of the settings searches</param>
        private void SetupSearchMethodName(int index)
        {
            if (index < 0 || index >= Settings.Instance.Searches.Count)
                return;

            SearchMethods sm = Settings.Instance.Searches[index];

            WallpaperAbyssApiV2.CollectionListItem item = null;

            switch (sm.Method)
            {
                case WallpaperAbyssApiV2.LookupMethods.search:
                    lstSearches.Items[index] = (sm.Method + " " + sm.SearchTerm);
                    break;
                case WallpaperAbyssApiV2.LookupMethods.category:
                    if (Settings.Instance.APIKey != null)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Id == sm.Id));
                    if (item != null)
                        lstSearches.Items[index] = (sm.Method + " " + item.Name);
                    else
                        lstSearches.Items[index] = (sm.Method);
                    break;
                case WallpaperAbyssApiV2.LookupMethods.collection:
                    if (Settings.Instance.APIKey != null)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCollectionList().Find((x => x.Id == sm.Id));
                    if (item != null)
                        lstSearches.Items[index] = (sm.Method + " " + item.Name);
                    else
                        lstSearches.Items[index] = (sm.Method);
                    break;
                case WallpaperAbyssApiV2.LookupMethods.group:
                    if (Settings.Instance.APIKey != null)
                    {
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetGroupList(sm.Id).Find((x => x.Id == sm.SubId));
                        if (item != null)
                            lstSearches.Items[index] = (sm.Method + " " + item.Name);
                        else
                            lstSearches.Items[index] = (sm.Method + " " + sm.Id + " " + sm.SubId);
                    }
                    else
                        lstSearches.Items[index] = (sm.Method + " " + sm.Id + " " + sm.SubId);
                    break;
                case WallpaperAbyssApiV2.LookupMethods.sub_category:
                    if (Settings.Instance.APIKey != null)
                    {
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetSubCategoryList(sm.Id).Find((x => x.Id == sm.SubId));
                        if (item != null)
                            lstSearches.Items[index] = (sm.Method + " " + item.Name);
                        else
                            lstSearches.Items[index] = (sm.Method + " " + sm.Id + " " + sm.SubId);
                    }
                    else
                        lstSearches.Items[index] = (sm.Method + " " + sm.Id + " " + sm.SubId);
                    break;
                default:
                    lstSearches.Items[index] = (sm.Method);
                    break;
            }
        }

        /// <summary>
        /// Initialize the searches list from from what's in the config file.
        /// </summary>
        private void SetupSearchMethods()
        {
            lstSearches.Items.Clear();

            /// <summary>
            /// The list of search methods to use. 
            /// </summary>
            foreach (SearchMethods sm in Settings.Instance.Searches)
            {
                WallpaperAbyssApiV2.CollectionListItem item = null;

                switch (sm.Method)
                {
                    case WallpaperAbyssApiV2.LookupMethods.search:
                        lstSearches.Items.Add(sm.Method + " " + sm.SearchTerm);
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.category:
                        if (Settings.Instance.APIKey != null)
                            item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Id == sm.Id));
                        if (item != null)
                            lstSearches.Items.Add(sm.Method + " " + item.Name);
                        else
                            lstSearches.Items.Add(sm.Method);
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.collection:
                        if (Settings.Instance.APIKey != null)
                            item = WallpaperAbyssApiV2.WallpaperAbyss.GetCollectionList().Find((x => x.Id == sm.Id));
                        if (item != null)
                            lstSearches.Items.Add(sm.Method + " " + item.Name);
                        else
                            lstSearches.Items.Add(sm.Method);
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.group:
                        if (Settings.Instance.APIKey != null)
                        {
                            item = WallpaperAbyssApiV2.WallpaperAbyss.GetGroupList(sm.Id).Find((x => x.Id == sm.SubId));
                            if (item != null)
                                lstSearches.Items.Add(sm.Method + " " + item.Name);
                            else
                                lstSearches.Items.Add(sm.Method + " " + sm.Id + " " + sm.SubId);
                        }
                        else
                            lstSearches.Items.Add(sm.Method + " " + sm.Id + " " + sm.SubId);
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.sub_category:
                        if (Settings.Instance.APIKey != null)
                        {
                            item = WallpaperAbyssApiV2.WallpaperAbyss.GetSubCategoryList(sm.Id).Find((x => x.Id == sm.SubId));
                            if (item != null)
                                lstSearches.Items.Add(sm.Method + " " + item.Name);
                            else
                                lstSearches.Items.Add(sm.Method + " " + sm.Id + " " + sm.SubId);
                        }
                        else
                            lstSearches.Items.Add(sm.Method + " " + sm.Id + " " + sm.SubId);
                        break;
                    default:
                        lstSearches.Items.Add(sm.Method);
                        break;
                }

                //sm.Height;
                //sm.Width;
                //sm.Id;
                //sm.Method;
                //sm.SizeOp;
                //sm.SortBy;
                //sm.SearchTerm;
            }
            lstSearches.SelectedIndex = 0;
        }

        /// <summary>
        /// List WiFi names that should be treated as managed (limited use)
        /// </summary>
        private void SetupManagedWiFiTable()
        {
            dataGridViewMeteredWifi.Columns.Clear();
            DataTable DTable = new DataTable();
            BindingSource SBind = new BindingSource();

            DTable.Columns.Add(new DataColumn("Wireless Name"));
            //dataGridViewMeteredWifi.Columns[0].DataPropertyName = dataGridViewMeteredWifi.Columns[0].Name;

            foreach (string wifi in Settings.Instance.MeteredSSIDs)
            {
                DataRow row = DTable.NewRow();

                row["Wireless Name"] = wifi;

                DTable.Rows.Add(row);
            }

            SBind.DataSource = DTable;
            dataGridViewMeteredWifi.DataSource = SBind;
            dataGridViewMeteredWifi.Refresh();
        }

        #endregion

        #region Buttons

        private void btnSelectWorkingDir_Click(object sender, EventArgs e)
        {
            workingDirDialog.ShowNewFolderButton = true;
            if (workingDirDialog.ShowDialog() == DialogResult.OK)
            {
                txtWorkingDir.Text = workingDirDialog.SelectedPath;
            }
        }

        private void btnAddSearchMethod_Click(object sender, EventArgs e)
        {
            Settings.Instance.Searches.Add(new SearchMethods());
            lstSearches.Items.Add(WallpaperAbyssApiV2.LookupMethods.newest);

            cboLookupMethods.SelectedIndex = 0;
            cboSortMethods.SelectedIndex = 0;
            cboSizeOperators.SelectedIndex = 0;
            txtWidth.Value = 0;
            txtHeight.Value = 0;

            lstSearches.SelectedIndex = lstSearches.Items.Count - 1;
        }

        private void btnRemoveSearchMethod_Click(object sender, EventArgs e)
        {
            if (lstSearches.SelectedIndex < 0)
                return;

            if (lstSearches.Items.Count <= 1)
                return;

            if (lstSearches.SelectedIndex < Settings.Instance.Searches.Count)
                Settings.Instance.Searches.RemoveAt(lstSearches.SelectedIndex);
            if (lstSearches.SelectedIndex < lstSearches.Items.Count)
                lstSearches.Items.RemoveAt(lstSearches.SelectedIndex);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BindingSource table = ((BindingSource)dataGridViewMeteredWifi.DataSource);
            Settings.Instance.MeteredSSIDs.Clear();

            foreach (DataRowView row in table)
            {
                if (!row.Row.IsNull(0))
                    Settings.Instance.MeteredSSIDs.Add(row.Row[0].ToString());
            }

            Settings.Instance.CacheDirectory = txtWorkingDir.Text;
            //Settings.Instance.Layout = cboImageLayout.sel;
            //Settings.Instance.MaxCacheSize;
            //Settings.Instance.MaxQueries;
            //Settings.Instance.ShowDescription;
            //Settings.Instance.UpdateMax;
            //Settings.Instance.UpdateMin;


            Settings.Instance.Save(Settings.SavePath);
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplyMatch_Click(object sender, EventArgs e)
        {
            UpdateCollectionList();
            SetupSearchMethodName(lstSearches.SelectedIndex);
        }

        /// <summary>
        /// Install Desktop Background Slideshow Service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInstallService_Click(object sender, EventArgs e)
        {
            String path = Application.ExecutablePath;

            FileInfo f = new FileInfo(path);

            path = f.Directory.FullName + "\\WallpaperAbyssDesktopBackgroundService.exe";

            if (!File.Exists(path))
            {
                MessageBox.Show("Failed to install Service! " + " Cannot find " + path);
            }
            else if (!ServiceManager.InstallService(path, "Wallpaper Abyss Desktop Changer", "Wallpaper Abyss Desktop Changer"))
            {
                int errCode = ServiceManager.GetLastError();
                MessageBox.Show("Failed to install Service! " + errCode);//.ToString("X2")
            }
            else
                MessageBox.Show("Service installed.");
        }

        /// <summary>
        /// Remove Desktop Background Slideshow Service
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveService_Click(object sender, EventArgs e)
        {
            if (!ServiceManager.UnInstallService("Wallpaper Abyss Desktop Changer"))
            {
                int errCode = ServiceManager.GetLastError();
                MessageBox.Show("Failed to remove Service! " + errCode.ToString("X2"));
            }
            else
                MessageBox.Show("Service removed.");
        }

        /// <summary>
        /// Clear Cached results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Settings.Instance.CacheDirectory);

            foreach (string file in files)
            {
                if (file.EndsWith(".json"))
                    continue;

                File.Delete(file);
            }

            Settings.Instance.CachedResults.Clear();

            MessageBox.Show("Cache Cleared");
        }

        #endregion 

        #region Values Changed

        private void txtAPIKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.APIKey = txtAPIKey.Text;
            WallpaperAbyssApiV2.WallpaperAbyss.AuthenticationKey = Settings.Instance.APIKey;
        }

        private void chkShowBottomBar_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.ShowDescription = chkShowBottomBar.Checked;
        }

        private void txtMinSec_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.UpdateMin = (int)txtMinSec.Value;
        }

        private void txtMaxSec_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.UpdateMax = (int)txtMaxSec.Value;
        }

        private void txtCacheSize_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.MaxCacheSize = (int)txtCacheSize.Value;
        }

        private void txtWorkingDir_TextChanged(object sender, EventArgs e)
        {
            //Settings.Instance.CacheDirectory
        }

        private void txtWorkingDir_Leave(object sender, EventArgs e)
        {
            Settings.Instance.CacheDirectory = txtWorkingDir.Text;
        }

        private void txtMaxQueries_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.MaxQueries = (int)txtMaxQueries.Value;
        }


        private void cboImageLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboImageLayout.SelectedItem.ToString().ToLower())
            {
                case "center":
                    Settings.Instance.Layout = ImageLayout.Center;
                    break;
                case "none":
                    Settings.Instance.Layout = ImageLayout.None;
                    break;
                case "stretch":
                    Settings.Instance.Layout = ImageLayout.Stretch;
                    break;
                case "title":
                    Settings.Instance.Layout = ImageLayout.Tile;
                    break;
                case "zoom":
                    Settings.Instance.Layout = ImageLayout.Zoom;
                    break;
            }
        }


        #endregion

        private void lstSearches_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Settings.Instance.Searches.Count > lstSearches.SelectedIndex && lstSearches.SelectedIndex >= 0)
            {
                SearchMethods sm = Settings.Instance.Searches[lstSearches.SelectedIndex];

                switch (sm.Method)
                {
                    default:
                    case WallpaperAbyssApiV2.LookupMethods.newest:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Newest");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.highest_rated:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Highest Rated");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.by_views:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("By Views");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.by_favorites:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("By Favorites");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.featured:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Featured");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.popular:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Popular");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.random:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Random");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.category:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Category");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.collection:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Collection");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.search:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Search");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.user:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("User");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.sub_category:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Sub Category");
                        break;
                    case WallpaperAbyssApiV2.LookupMethods.group:
                        cboLookupMethods.SelectedIndex = cboLookupMethods.Items.IndexOf("Group");
                        break;
                }

                cboSizeOperators.SelectedIndex = cboSizeOperators.Items.IndexOf(sm.SizeOp.ToString());
                cboSortMethods.SelectedIndex = cboSortMethods.Items.IndexOf(sm.SortBy.ToString());

                txtWidth.Value = sm.Width;
                txtHeight.Value = sm.Height;

                //if (sm.Id > 0)
                //    UpdateCollectionList(sm.Id);
            }
        }

        private void cboLookupMethods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLookupMethods.SelectedIndex < 0)
            {
                pnlSearch.Visible = false;
                lstCollectionLabels.Visible = false;
                lstSubCollection.Visible = false;
                return;
            }

            List<WallpaperAbyssApiV2.CollectionListItem> items = null;

            string selectedSearchMethod = cboLookupMethods.Items[cboLookupMethods.SelectedIndex].ToString().ToLower();

            switch (selectedSearchMethod)
            {
                default:
                case "newest":
                case "highest rated":
                case "by views":
                case "by favorites":
                case "featured":
                case "popular":
                case "random":
                    pnlSearch.Visible = false;
                    lstCollectionLabels.Visible = false;
                    lstSubCollection.Visible = false;
                    break;
                case "category":
                case "collection":
                    pnlSearch.Visible = false;
                    lstCollectionLabels.Visible = true;
                    lstSubCollection.Visible = false;
                    break;
                case "search":
                case "user":
                    pnlSearch.Visible = true;
                    lstCollectionLabels.Visible = false;
                    lstSubCollection.Visible = false;
                    break;
                case "sub category": //Needs Collection
                case "group": //Needs Collection
                    pnlSearch.Visible = false;
                    lstCollectionLabels.Visible = true;
                    lstSubCollection.Visible = true;
                    break;
            }

            switch (cboLookupMethods.SelectedItem.ToString().ToLower())
            {
                case "category":
                case "sub category": //Needs Collection
                case "group": //Needs Collection
                    items = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList();
                    break;
                case "collection":
                    items = WallpaperAbyssApiV2.WallpaperAbyss.GetCollectionList();
                    break;
            }

            if (items != null)
            {
                SearchMethods sm = Settings.Instance.Searches[lstSearches.SelectedIndex];
                bool found = false;
                lstCollectionLabels.Items.Clear();
                foreach (WallpaperAbyssApiV2.CollectionListItem subItem in items)
                {
                    lstCollectionLabels.Items.Add(subItem.Name);

                    if (sm.Id == subItem.Id)
                    {
                        lstCollectionLabels.SelectedIndex = lstCollectionLabels.Items.Count - 1;
                        found = true;
                    }
                }
                if (!found && lstCollectionLabels.Items.Count > 0)
                    lstCollectionLabels.SelectedIndex = 0;
            }
        }

        private void lstCollectionLabels_SelectedIndexChanged(object sender, EventArgs e)
        {
            WallpaperAbyssApiV2.CollectionListItem item;
            item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.Items[lstCollectionLabels.SelectedIndex].ToString(), StringComparison.InvariantCultureIgnoreCase)));

            List<WallpaperAbyssApiV2.CollectionListItem> subitems = null; // = WallpaperAbyssApiV2.WallpaperAbyss.GetSubCategoryList(item.Id);
            switch (cboLookupMethods.SelectedItem.ToString().ToLower())
            {
                case "sub category": //Needs Collection
                    subitems = WallpaperAbyssApiV2.WallpaperAbyss.GetSubCategoryList(item.Id);
                    break;
                case "group": //Needs Collection
                    subitems = WallpaperAbyssApiV2.WallpaperAbyss.GetGroupList(item.Id);
                    break;
            }

            if (subitems != null)
            {
                SearchMethods sm = Settings.Instance.Searches[lstSearches.SelectedIndex];
                bool found = false;
                lstSubCollection.Items.Clear();

                foreach (WallpaperAbyssApiV2.CollectionListItem subItem in subitems)
                {
                    lstSubCollection.Items.Add(subItem.Name);

                    if (sm.SubId == subItem.Id)
                    {
                        lstSubCollection.SelectedIndex = lstSubCollection.Items.Count - 1;
                        found = true;
                    }
                }
                if (!found && lstSubCollection.Items.Count > 0)
                    lstSubCollection.SelectedIndex = 0;
            }

        }

        /// <summary>
        /// Called when Apply button is clicked (btnApplyMatch_Click)
        /// Applies search method changes
        /// </summary>
        private void UpdateCollectionList()
        {
            WallpaperAbyssApiV2.CollectionListItem item = null;

            if (lstCollectionLabels.Items.Count == 0)
            {
                cboLookupMethods_SelectedIndexChanged(null, null);

                //cboLookupMethods.SelectedIndex = 0;
                if (lstCollectionLabels.Items.Count > 0)
                    lstCollectionLabels.SelectedItem = lstCollectionLabels.Items[0];
            }
            SearchMethods search = new SearchMethods();
            search.Height = (int)txtHeight.Value;
            search.Width = (int)txtWidth.Value;

            switch (cboSortMethods.SelectedItem.ToString().ToLower())
            {
                case "newest":
                    search.SortBy = WallpaperAbyssApiV2.SortMethods.Newest;
                    break;
                case "rating":
                    search.SortBy = WallpaperAbyssApiV2.SortMethods.Rating;
                    break;
                case "views":
                    search.SortBy = WallpaperAbyssApiV2.SortMethods.Views;
                    break;
                case "favorites":
                    search.SortBy = WallpaperAbyssApiV2.SortMethods.Favorites;
                    break;
            }

            switch (cboSizeOperators.SelectedItem.ToString().ToLower())
            {
                case "equal":
                    search.SizeOp = WallpaperAbyssApiV2.SizeOperators.Equal;
                    break;
                case "max":
                    search.SizeOp = WallpaperAbyssApiV2.SizeOperators.Max;
                    break;
                case "min":
                    search.SizeOp = WallpaperAbyssApiV2.SizeOperators.Min;
                    break;
            }

            switch (cboLookupMethods.SelectedItem.ToString().ToLower())
            {
                default:
                case "newest":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.newest;

                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;
                    break;
                case "highest rated":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.highest_rated;
                    break;
                case "by views":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.by_views;
                    break;
                case "by favorites":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.by_favorites;
                    break;
                case "featured":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.featured;
                    break;
                case "popular":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.popular;
                    break;
                case "random":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.random;
                    break;
                case "category":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.category;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;
                    break;
                case "collection":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.collection;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;
                    break;
                case "search":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.search;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;
                    search.SearchTerm = txtSearchTerm.Text;
                    break;
                case "user":
                    search.Method = WallpaperAbyssApiV2.LookupMethods.user;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;
                    search.SearchTerm = txtSearchTerm.Text;
                    break;
                case "sub category": //Needs Collection
                    search.Method = WallpaperAbyssApiV2.LookupMethods.sub_category;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;

                    if (lstSubCollection.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetSubCategoryList(item.Id).Find((x => x.Name.Equals(lstSubCollection.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.SubId = item.Id;
                    break;
                case "group": //Needs Collection
                    search.Method = WallpaperAbyssApiV2.LookupMethods.group;
                    if (lstCollectionLabels.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetCategoryList().Find((x => x.Name.Equals(lstCollectionLabels.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.Id = item.Id;

                    if (lstSubCollection.Items.Count > 0)
                        item = WallpaperAbyssApiV2.WallpaperAbyss.GetGroupList(item.Id).Find((x => x.Name.Equals(lstSubCollection.SelectedItem.ToString(), StringComparison.InvariantCultureIgnoreCase)));
                    if (item != null)
                        search.SubId = item.Id;
                    break;
            }

            Settings.Instance.Searches[lstSearches.SelectedIndex] = search;

            //SetupSearchMethods();
            //SetupSearchMethod(lstSearches.SelectedIndex);
        }

    }
}
