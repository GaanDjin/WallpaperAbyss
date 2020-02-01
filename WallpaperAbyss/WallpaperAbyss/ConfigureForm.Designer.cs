namespace WallpaperAbyss
{
    partial class ConfigureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigureForm));
            this.tabCtrlSections = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnSelectWorkingDir = new System.Windows.Forms.Button();
            this.cboImageLayout = new System.Windows.Forms.ComboBox();
            this.chkShowBottomBar = new System.Windows.Forms.CheckBox();
            this.txtMaxSec = new System.Windows.Forms.NumericUpDown();
            this.txtMaxQueries = new System.Windows.Forms.NumericUpDown();
            this.txtCacheSize = new System.Windows.Forms.NumericUpDown();
            this.lblRndMin = new System.Windows.Forms.Label();
            this.lblRndAnd = new System.Windows.Forms.Label();
            this.lblPerMonth = new System.Windows.Forms.Label();
            this.lblResizeImages = new System.Windows.Forms.Label();
            this.lblMaxQueries = new System.Windows.Forms.Label();
            this.lblCacheDirMB = new System.Windows.Forms.Label();
            this.lblCcheSize = new System.Windows.Forms.Label();
            this.txtMinSec = new System.Windows.Forms.NumericUpDown();
            this.lblWorkingDir = new System.Windows.Forms.Label();
            this.lblRndSwitchBetween = new System.Windows.Forms.Label();
            this.txtWorkingDir = new System.Windows.Forms.TextBox();
            this.txtAPIKey = new System.Windows.Forms.TextBox();
            this.lblAPIKey = new System.Windows.Forms.LinkLabel();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.grpSearchOptions = new System.Windows.Forms.GroupBox();
            this.pnlCollectionSupCollection = new System.Windows.Forms.Panel();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearchTerm = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.lstSubCollection = new System.Windows.Forms.ListBox();
            this.lstCollectionLabels = new System.Windows.Forms.ListBox();
            this.pnlSearchOptions = new System.Windows.Forms.Panel();
            this.btnApplyMatch = new System.Windows.Forms.Button();
            this.txtHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.NumericUpDown();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblImageSizeOptions = new System.Windows.Forms.Label();
            this.lblSortBy = new System.Windows.Forms.Label();
            this.lblSearchBy = new System.Windows.Forms.Label();
            this.cboLookupMethods = new System.Windows.Forms.ComboBox();
            this.cboSizeOperators = new System.Windows.Forms.ComboBox();
            this.cboSortMethods = new System.Windows.Forms.ComboBox();
            this.pnlSearchMethods = new System.Windows.Forms.Panel();
            this.lstSearches = new System.Windows.Forms.ListBox();
            this.pnlAddRemoveSearchMethods = new System.Windows.Forms.Panel();
            this.btnRemoveSearchMethod = new System.Windows.Forms.Button();
            this.btnAddSearchMethod = new System.Windows.Forms.Button();
            this.lblSearchMethodsUsed = new System.Windows.Forms.Label();
            this.tabMeteredWifi = new System.Windows.Forms.TabPage();
            this.dataGridViewMeteredWifi = new System.Windows.Forms.DataGridView();
            this.WirelessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblMeteredWifiInfo = new System.Windows.Forms.Label();
            this.tabBGService = new System.Windows.Forms.TabPage();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.btnRemoveService = new System.Windows.Forms.Button();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.rtfAbout = new System.Windows.Forms.RichTextBox();
            this.pnlSaveClose = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.workingDirDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTipHolder = new System.Windows.Forms.ToolTip(this.components);
            this.tabCtrlSections.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxQueries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCacheSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSec)).BeginInit();
            this.tabCategories.SuspendLayout();
            this.grpSearchOptions.SuspendLayout();
            this.pnlCollectionSupCollection.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlSearchOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            this.pnlSearchMethods.SuspendLayout();
            this.pnlAddRemoveSearchMethods.SuspendLayout();
            this.tabMeteredWifi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeteredWifi)).BeginInit();
            this.tabBGService.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.pnlSaveClose.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCtrlSections
            // 
            this.tabCtrlSections.Controls.Add(this.tabGeneral);
            this.tabCtrlSections.Controls.Add(this.tabCategories);
            this.tabCtrlSections.Controls.Add(this.tabMeteredWifi);
            this.tabCtrlSections.Controls.Add(this.tabBGService);
            this.tabCtrlSections.Controls.Add(this.tabAbout);
            this.tabCtrlSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlSections.Location = new System.Drawing.Point(0, 0);
            this.tabCtrlSections.Margin = new System.Windows.Forms.Padding(4);
            this.tabCtrlSections.Name = "tabCtrlSections";
            this.tabCtrlSections.SelectedIndex = 0;
            this.tabCtrlSections.Size = new System.Drawing.Size(1293, 519);
            this.tabCtrlSections.TabIndex = 5;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.btnClear);
            this.tabGeneral.Controls.Add(this.btnSelectWorkingDir);
            this.tabGeneral.Controls.Add(this.cboImageLayout);
            this.tabGeneral.Controls.Add(this.chkShowBottomBar);
            this.tabGeneral.Controls.Add(this.txtMaxSec);
            this.tabGeneral.Controls.Add(this.txtMaxQueries);
            this.tabGeneral.Controls.Add(this.txtCacheSize);
            this.tabGeneral.Controls.Add(this.lblRndMin);
            this.tabGeneral.Controls.Add(this.lblRndAnd);
            this.tabGeneral.Controls.Add(this.lblPerMonth);
            this.tabGeneral.Controls.Add(this.lblResizeImages);
            this.tabGeneral.Controls.Add(this.lblMaxQueries);
            this.tabGeneral.Controls.Add(this.lblCacheDirMB);
            this.tabGeneral.Controls.Add(this.lblCcheSize);
            this.tabGeneral.Controls.Add(this.txtMinSec);
            this.tabGeneral.Controls.Add(this.lblWorkingDir);
            this.tabGeneral.Controls.Add(this.lblRndSwitchBetween);
            this.tabGeneral.Controls.Add(this.txtWorkingDir);
            this.tabGeneral.Controls.Add(this.txtAPIKey);
            this.tabGeneral.Controls.Add(this.lblAPIKey);
            this.tabGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.tabGeneral.Size = new System.Drawing.Size(1285, 490);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(483, 68);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 28);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSelectWorkingDir
            // 
            this.btnSelectWorkingDir.Location = new System.Drawing.Point(697, 103);
            this.btnSelectWorkingDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectWorkingDir.Name = "btnSelectWorkingDir";
            this.btnSelectWorkingDir.Size = new System.Drawing.Size(33, 25);
            this.btnSelectWorkingDir.TabIndex = 16;
            this.btnSelectWorkingDir.Text = "...";
            this.btnSelectWorkingDir.UseVisualStyleBackColor = true;
            this.btnSelectWorkingDir.Click += new System.EventHandler(this.btnSelectWorkingDir_Click);
            // 
            // cboImageLayout
            // 
            this.cboImageLayout.FormattingEnabled = true;
            this.cboImageLayout.Items.AddRange(new object[] {
            "None",
            "Center",
            "Stretch",
            "Tile",
            "Zoom"});
            this.cboImageLayout.Location = new System.Drawing.Point(273, 167);
            this.cboImageLayout.Margin = new System.Windows.Forms.Padding(4);
            this.cboImageLayout.Name = "cboImageLayout";
            this.cboImageLayout.Size = new System.Drawing.Size(160, 24);
            this.cboImageLayout.TabIndex = 15;
            this.cboImageLayout.Text = "None";
            this.cboImageLayout.SelectedIndexChanged += new System.EventHandler(this.cboImageLayout_SelectedIndexChanged);
            // 
            // chkShowBottomBar
            // 
            this.chkShowBottomBar.AutoSize = true;
            this.chkShowBottomBar.Location = new System.Drawing.Point(273, 201);
            this.chkShowBottomBar.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowBottomBar.Name = "chkShowBottomBar";
            this.chkShowBottomBar.Size = new System.Drawing.Size(187, 21);
            this.chkShowBottomBar.TabIndex = 14;
            this.chkShowBottomBar.Text = "Show Bottom Description";
            this.chkShowBottomBar.UseVisualStyleBackColor = true;
            this.chkShowBottomBar.CheckedChanged += new System.EventHandler(this.chkShowBottomBar_CheckedChanged);
            // 
            // txtMaxSec
            // 
            this.txtMaxSec.Location = new System.Drawing.Point(483, 39);
            this.txtMaxSec.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxSec.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.txtMaxSec.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtMaxSec.Name = "txtMaxSec";
            this.txtMaxSec.Size = new System.Drawing.Size(80, 22);
            this.txtMaxSec.TabIndex = 11;
            this.txtMaxSec.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtMaxSec.ValueChanged += new System.EventHandler(this.txtMaxSec_ValueChanged);
            // 
            // txtMaxQueries
            // 
            this.txtMaxQueries.Location = new System.Drawing.Point(273, 135);
            this.txtMaxQueries.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxQueries.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtMaxQueries.Name = "txtMaxQueries";
            this.txtMaxQueries.Size = new System.Drawing.Size(160, 22);
            this.txtMaxQueries.TabIndex = 12;
            this.txtMaxQueries.ValueChanged += new System.EventHandler(this.txtMaxQueries_ValueChanged);
            // 
            // txtCacheSize
            // 
            this.txtCacheSize.Location = new System.Drawing.Point(273, 71);
            this.txtCacheSize.Margin = new System.Windows.Forms.Padding(4);
            this.txtCacheSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.txtCacheSize.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtCacheSize.Name = "txtCacheSize";
            this.txtCacheSize.Size = new System.Drawing.Size(160, 22);
            this.txtCacheSize.TabIndex = 12;
            this.txtCacheSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtCacheSize.ValueChanged += new System.EventHandler(this.txtCacheSize_ValueChanged);
            // 
            // lblRndMin
            // 
            this.lblRndMin.AutoSize = true;
            this.lblRndMin.Location = new System.Drawing.Point(571, 42);
            this.lblRndMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRndMin.Name = "lblRndMin";
            this.lblRndMin.Size = new System.Drawing.Size(61, 17);
            this.lblRndMin.TabIndex = 7;
            this.lblRndMin.Text = "seconds";
            // 
            // lblRndAnd
            // 
            this.lblRndAnd.AutoSize = true;
            this.lblRndAnd.Location = new System.Drawing.Point(441, 42);
            this.lblRndAnd.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRndAnd.Name = "lblRndAnd";
            this.lblRndAnd.Size = new System.Drawing.Size(32, 17);
            this.lblRndAnd.TabIndex = 8;
            this.lblRndAnd.Text = "and";
            // 
            // lblPerMonth
            // 
            this.lblPerMonth.AutoSize = true;
            this.lblPerMonth.Location = new System.Drawing.Point(441, 138);
            this.lblPerMonth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPerMonth.Name = "lblPerMonth";
            this.lblPerMonth.Size = new System.Drawing.Size(72, 17);
            this.lblPerMonth.TabIndex = 9;
            this.lblPerMonth.Text = "per month";
            // 
            // lblResizeImages
            // 
            this.lblResizeImages.AutoSize = true;
            this.lblResizeImages.Location = new System.Drawing.Point(164, 171);
            this.lblResizeImages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResizeImages.Name = "lblResizeImages";
            this.lblResizeImages.Size = new System.Drawing.Size(100, 17);
            this.lblResizeImages.TabIndex = 9;
            this.lblResizeImages.Text = "Resize Images";
            // 
            // lblMaxQueries
            // 
            this.lblMaxQueries.AutoSize = true;
            this.lblMaxQueries.Location = new System.Drawing.Point(177, 138);
            this.lblMaxQueries.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxQueries.Name = "lblMaxQueries";
            this.lblMaxQueries.Size = new System.Drawing.Size(87, 17);
            this.lblMaxQueries.TabIndex = 9;
            this.lblMaxQueries.Text = "Max Queries";
            // 
            // lblCacheDirMB
            // 
            this.lblCacheDirMB.AutoSize = true;
            this.lblCacheDirMB.Location = new System.Drawing.Point(441, 74);
            this.lblCacheDirMB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCacheDirMB.Name = "lblCacheDirMB";
            this.lblCacheDirMB.Size = new System.Drawing.Size(28, 17);
            this.lblCacheDirMB.TabIndex = 9;
            this.lblCacheDirMB.Text = "MB";
            // 
            // lblCcheSize
            // 
            this.lblCcheSize.AutoSize = true;
            this.lblCcheSize.Location = new System.Drawing.Point(184, 74);
            this.lblCcheSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCcheSize.Name = "lblCcheSize";
            this.lblCcheSize.Size = new System.Drawing.Size(79, 17);
            this.lblCcheSize.TabIndex = 9;
            this.lblCcheSize.Text = "Cache Size";
            // 
            // txtMinSec
            // 
            this.txtMinSec.Location = new System.Drawing.Point(353, 39);
            this.txtMinSec.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinSec.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMinSec.Name = "txtMinSec";
            this.txtMinSec.Size = new System.Drawing.Size(80, 22);
            this.txtMinSec.TabIndex = 13;
            this.txtMinSec.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtMinSec.ValueChanged += new System.EventHandler(this.txtMinSec_ValueChanged);
            // 
            // lblWorkingDir
            // 
            this.lblWorkingDir.AutoSize = true;
            this.lblWorkingDir.Location = new System.Drawing.Point(143, 107);
            this.lblWorkingDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkingDir.Name = "lblWorkingDir";
            this.lblWorkingDir.Size = new System.Drawing.Size(121, 17);
            this.lblWorkingDir.TabIndex = 10;
            this.lblWorkingDir.Text = "Working Directory";
            // 
            // lblRndSwitchBetween
            // 
            this.lblRndSwitchBetween.AutoSize = true;
            this.lblRndSwitchBetween.Location = new System.Drawing.Point(115, 42);
            this.lblRndSwitchBetween.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRndSwitchBetween.Name = "lblRndSwitchBetween";
            this.lblRndSwitchBetween.Size = new System.Drawing.Size(231, 17);
            this.lblRndSwitchBetween.TabIndex = 10;
            this.lblRndSwitchBetween.Text = "Radndomly switch images between ";
            // 
            // txtWorkingDir
            // 
            this.txtWorkingDir.Location = new System.Drawing.Point(273, 103);
            this.txtWorkingDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtWorkingDir.Name = "txtWorkingDir";
            this.txtWorkingDir.Size = new System.Drawing.Size(415, 22);
            this.txtWorkingDir.TabIndex = 6;
            this.txtWorkingDir.TextChanged += new System.EventHandler(this.txtWorkingDir_TextChanged);
            this.txtWorkingDir.Leave += new System.EventHandler(this.txtWorkingDir_Leave);
            // 
            // txtAPIKey
            // 
            this.txtAPIKey.Location = new System.Drawing.Point(119, 7);
            this.txtAPIKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtAPIKey.Name = "txtAPIKey";
            this.txtAPIKey.Size = new System.Drawing.Size(569, 22);
            this.txtAPIKey.TabIndex = 6;
            this.txtAPIKey.TextChanged += new System.EventHandler(this.txtAPIKey_TextChanged);
            // 
            // lblAPIKey
            // 
            this.lblAPIKey.AutoSize = true;
            this.lblAPIKey.Location = new System.Drawing.Point(47, 11);
            this.lblAPIKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAPIKey.Name = "lblAPIKey";
            this.lblAPIKey.Size = new System.Drawing.Size(61, 17);
            this.lblAPIKey.TabIndex = 5;
            this.lblAPIKey.TabStop = true;
            this.lblAPIKey.Text = "API Key:";
            this.lblAPIKey.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblAPIKey_LinkClicked);
            // 
            // tabCategories
            // 
            this.tabCategories.Controls.Add(this.grpSearchOptions);
            this.tabCategories.Controls.Add(this.pnlSearchMethods);
            this.tabCategories.Location = new System.Drawing.Point(4, 25);
            this.tabCategories.Margin = new System.Windows.Forms.Padding(4);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Padding = new System.Windows.Forms.Padding(4);
            this.tabCategories.Size = new System.Drawing.Size(1285, 490);
            this.tabCategories.TabIndex = 1;
            this.tabCategories.Text = "Categories";
            this.tabCategories.UseVisualStyleBackColor = true;
            // 
            // grpSearchOptions
            // 
            this.grpSearchOptions.Controls.Add(this.pnlCollectionSupCollection);
            this.grpSearchOptions.Controls.Add(this.pnlSearchOptions);
            this.grpSearchOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearchOptions.Location = new System.Drawing.Point(271, 4);
            this.grpSearchOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpSearchOptions.Name = "grpSearchOptions";
            this.grpSearchOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpSearchOptions.Size = new System.Drawing.Size(1010, 482);
            this.grpSearchOptions.TabIndex = 0;
            this.grpSearchOptions.TabStop = false;
            this.grpSearchOptions.Text = "Search Options";
            // 
            // pnlCollectionSupCollection
            // 
            this.pnlCollectionSupCollection.Controls.Add(this.pnlSearch);
            this.pnlCollectionSupCollection.Controls.Add(this.lstSubCollection);
            this.pnlCollectionSupCollection.Controls.Add(this.lstCollectionLabels);
            this.pnlCollectionSupCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCollectionSupCollection.Location = new System.Drawing.Point(353, 19);
            this.pnlCollectionSupCollection.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlCollectionSupCollection.Name = "pnlCollectionSupCollection";
            this.pnlCollectionSupCollection.Size = new System.Drawing.Size(653, 459);
            this.pnlCollectionSupCollection.TabIndex = 17;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.lblSearchTerm);
            this.pnlSearch.Controls.Add(this.txtSearchTerm);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSearch.Location = new System.Drawing.Point(650, 0);
            this.pnlSearch.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(3, 459);
            this.pnlSearch.TabIndex = 4;
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.AutoSize = true;
            this.lblSearchTerm.Location = new System.Drawing.Point(7, 21);
            this.lblSearchTerm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(152, 17);
            this.lblSearchTerm.TabIndex = 4;
            this.lblSearchTerm.Text = "Search term to look for";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Location = new System.Drawing.Point(11, 41);
            this.txtSearchTerm.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(344, 22);
            this.txtSearchTerm.TabIndex = 3;
            // 
            // lstSubCollection
            // 
            this.lstSubCollection.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstSubCollection.FormattingEnabled = true;
            this.lstSubCollection.ItemHeight = 16;
            this.lstSubCollection.Location = new System.Drawing.Point(325, 0);
            this.lstSubCollection.Margin = new System.Windows.Forms.Padding(4);
            this.lstSubCollection.Name = "lstSubCollection";
            this.lstSubCollection.Size = new System.Drawing.Size(325, 459);
            this.lstSubCollection.TabIndex = 4;
            // 
            // lstCollectionLabels
            // 
            this.lstCollectionLabels.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstCollectionLabels.FormattingEnabled = true;
            this.lstCollectionLabels.ItemHeight = 16;
            this.lstCollectionLabels.Location = new System.Drawing.Point(0, 0);
            this.lstCollectionLabels.Margin = new System.Windows.Forms.Padding(4);
            this.lstCollectionLabels.Name = "lstCollectionLabels";
            this.lstCollectionLabels.Size = new System.Drawing.Size(325, 459);
            this.lstCollectionLabels.TabIndex = 5;
            this.lstCollectionLabels.SelectedIndexChanged += new System.EventHandler(this.lstCollectionLabels_SelectedIndexChanged);
            // 
            // pnlSearchOptions
            // 
            this.pnlSearchOptions.Controls.Add(this.btnApplyMatch);
            this.pnlSearchOptions.Controls.Add(this.txtHeight);
            this.pnlSearchOptions.Controls.Add(this.lblHeight);
            this.pnlSearchOptions.Controls.Add(this.txtWidth);
            this.pnlSearchOptions.Controls.Add(this.lblWidth);
            this.pnlSearchOptions.Controls.Add(this.lblImageSizeOptions);
            this.pnlSearchOptions.Controls.Add(this.lblSortBy);
            this.pnlSearchOptions.Controls.Add(this.lblSearchBy);
            this.pnlSearchOptions.Controls.Add(this.cboLookupMethods);
            this.pnlSearchOptions.Controls.Add(this.cboSizeOperators);
            this.pnlSearchOptions.Controls.Add(this.cboSortMethods);
            this.pnlSearchOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearchOptions.Location = new System.Drawing.Point(4, 19);
            this.pnlSearchOptions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlSearchOptions.Name = "pnlSearchOptions";
            this.pnlSearchOptions.Size = new System.Drawing.Size(349, 459);
            this.pnlSearchOptions.TabIndex = 18;
            // 
            // btnApplyMatch
            // 
            this.btnApplyMatch.Location = new System.Drawing.Point(148, 176);
            this.btnApplyMatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApplyMatch.Name = "btnApplyMatch";
            this.btnApplyMatch.Size = new System.Drawing.Size(75, 25);
            this.btnApplyMatch.TabIndex = 27;
            this.btnApplyMatch.Text = "Apply";
            this.btnApplyMatch.UseVisualStyleBackColor = true;
            this.btnApplyMatch.Click += new System.EventHandler(this.btnApplyMatch_Click);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(187, 135);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(160, 22);
            this.txtHeight.TabIndex = 26;
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(117, 138);
            this.lblHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(53, 17);
            this.lblHeight.TabIndex = 25;
            this.lblHeight.Text = "Height:";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(187, 103);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(160, 22);
            this.txtWidth.TabIndex = 24;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(125, 106);
            this.lblWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(48, 17);
            this.lblWidth.TabIndex = 23;
            this.lblWidth.Text = "Width:";
            // 
            // lblImageSizeOptions
            // 
            this.lblImageSizeOptions.AutoSize = true;
            this.lblImageSizeOptions.Location = new System.Drawing.Point(5, 74);
            this.lblImageSizeOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageSizeOptions.Name = "lblImageSizeOptions";
            this.lblImageSizeOptions.Size = new System.Drawing.Size(171, 17);
            this.lblImageSizeOptions.TabIndex = 20;
            this.lblImageSizeOptions.Text = "Only show images of size:";
            // 
            // lblSortBy
            // 
            this.lblSortBy.AutoSize = true;
            this.lblSortBy.Location = new System.Drawing.Point(117, 41);
            this.lblSortBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSortBy.Name = "lblSortBy";
            this.lblSortBy.Size = new System.Drawing.Size(58, 17);
            this.lblSortBy.TabIndex = 21;
            this.lblSortBy.Text = "Sort By:";
            // 
            // lblSearchBy
            // 
            this.lblSearchBy.AutoSize = true;
            this.lblSearchBy.Location = new System.Drawing.Point(99, 7);
            this.lblSearchBy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchBy.Name = "lblSearchBy";
            this.lblSearchBy.Size = new System.Drawing.Size(77, 17);
            this.lblSearchBy.TabIndex = 22;
            this.lblSearchBy.Text = "Search By:";
            // 
            // cboLookupMethods
            // 
            this.cboLookupMethods.FormattingEnabled = true;
            this.cboLookupMethods.Items.AddRange(new object[] {
            "Newest",
            "Highest Rated",
            "By Views",
            "By Favorites",
            "Category",
            "Collection",
            "Group",
            "Sub Category",
            "Featured",
            "Popular",
            "User",
            "Search",
            "Random"});
            this.cboLookupMethods.Location = new System.Drawing.Point(185, 4);
            this.cboLookupMethods.Margin = new System.Windows.Forms.Padding(4);
            this.cboLookupMethods.Name = "cboLookupMethods";
            this.cboLookupMethods.Size = new System.Drawing.Size(160, 24);
            this.cboLookupMethods.TabIndex = 17;
            this.cboLookupMethods.Text = "Newest";
            this.cboLookupMethods.SelectedIndexChanged += new System.EventHandler(this.cboLookupMethods_SelectedIndexChanged);
            // 
            // cboSizeOperators
            // 
            this.cboSizeOperators.FormattingEnabled = true;
            this.cboSizeOperators.Items.AddRange(new object[] {
            "Equal",
            "Max",
            "Min"});
            this.cboSizeOperators.Location = new System.Drawing.Point(185, 71);
            this.cboSizeOperators.Margin = new System.Windows.Forms.Padding(4);
            this.cboSizeOperators.Name = "cboSizeOperators";
            this.cboSizeOperators.Size = new System.Drawing.Size(160, 24);
            this.cboSizeOperators.TabIndex = 18;
            this.cboSizeOperators.Text = "Equal";
            // 
            // cboSortMethods
            // 
            this.cboSortMethods.FormattingEnabled = true;
            this.cboSortMethods.Items.AddRange(new object[] {
            "Newest",
            "Rating",
            "Views",
            "Favorites"});
            this.cboSortMethods.Location = new System.Drawing.Point(185, 37);
            this.cboSortMethods.Margin = new System.Windows.Forms.Padding(4);
            this.cboSortMethods.Name = "cboSortMethods";
            this.cboSortMethods.Size = new System.Drawing.Size(160, 24);
            this.cboSortMethods.TabIndex = 19;
            this.cboSortMethods.Text = "Newest";
            // 
            // pnlSearchMethods
            // 
            this.pnlSearchMethods.Controls.Add(this.lstSearches);
            this.pnlSearchMethods.Controls.Add(this.pnlAddRemoveSearchMethods);
            this.pnlSearchMethods.Controls.Add(this.lblSearchMethodsUsed);
            this.pnlSearchMethods.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSearchMethods.Location = new System.Drawing.Point(4, 4);
            this.pnlSearchMethods.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearchMethods.Name = "pnlSearchMethods";
            this.pnlSearchMethods.Size = new System.Drawing.Size(267, 482);
            this.pnlSearchMethods.TabIndex = 2;
            // 
            // lstSearches
            // 
            this.lstSearches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSearches.FormattingEnabled = true;
            this.lstSearches.ItemHeight = 16;
            this.lstSearches.Location = new System.Drawing.Point(0, 17);
            this.lstSearches.Margin = new System.Windows.Forms.Padding(4);
            this.lstSearches.Name = "lstSearches";
            this.lstSearches.Size = new System.Drawing.Size(267, 419);
            this.lstSearches.TabIndex = 0;
            this.lstSearches.SelectedIndexChanged += new System.EventHandler(this.lstSearches_SelectedIndexChanged);
            // 
            // pnlAddRemoveSearchMethods
            // 
            this.pnlAddRemoveSearchMethods.Controls.Add(this.btnRemoveSearchMethod);
            this.pnlAddRemoveSearchMethods.Controls.Add(this.btnAddSearchMethod);
            this.pnlAddRemoveSearchMethods.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAddRemoveSearchMethods.Location = new System.Drawing.Point(0, 436);
            this.pnlAddRemoveSearchMethods.Margin = new System.Windows.Forms.Padding(4);
            this.pnlAddRemoveSearchMethods.Name = "pnlAddRemoveSearchMethods";
            this.pnlAddRemoveSearchMethods.Size = new System.Drawing.Size(267, 46);
            this.pnlAddRemoveSearchMethods.TabIndex = 4;
            // 
            // btnRemoveSearchMethod
            // 
            this.btnRemoveSearchMethod.Location = new System.Drawing.Point(120, 7);
            this.btnRemoveSearchMethod.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveSearchMethod.Name = "btnRemoveSearchMethod";
            this.btnRemoveSearchMethod.Size = new System.Drawing.Size(31, 28);
            this.btnRemoveSearchMethod.TabIndex = 4;
            this.btnRemoveSearchMethod.Text = "-";
            this.btnRemoveSearchMethod.UseVisualStyleBackColor = true;
            this.btnRemoveSearchMethod.Click += new System.EventHandler(this.btnRemoveSearchMethod_Click);
            // 
            // btnAddSearchMethod
            // 
            this.btnAddSearchMethod.Location = new System.Drawing.Point(81, 7);
            this.btnAddSearchMethod.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddSearchMethod.Name = "btnAddSearchMethod";
            this.btnAddSearchMethod.Size = new System.Drawing.Size(31, 28);
            this.btnAddSearchMethod.TabIndex = 4;
            this.btnAddSearchMethod.Text = "+";
            this.btnAddSearchMethod.UseVisualStyleBackColor = true;
            this.btnAddSearchMethod.Click += new System.EventHandler(this.btnAddSearchMethod_Click);
            // 
            // lblSearchMethodsUsed
            // 
            this.lblSearchMethodsUsed.AutoSize = true;
            this.lblSearchMethodsUsed.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSearchMethodsUsed.Location = new System.Drawing.Point(0, 0);
            this.lblSearchMethodsUsed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchMethodsUsed.Name = "lblSearchMethodsUsed";
            this.lblSearchMethodsUsed.Size = new System.Drawing.Size(148, 17);
            this.lblSearchMethodsUsed.TabIndex = 2;
            this.lblSearchMethodsUsed.Text = "Search Methods Used";
            // 
            // tabMeteredWifi
            // 
            this.tabMeteredWifi.Controls.Add(this.dataGridViewMeteredWifi);
            this.tabMeteredWifi.Controls.Add(this.lblMeteredWifiInfo);
            this.tabMeteredWifi.Location = new System.Drawing.Point(4, 25);
            this.tabMeteredWifi.Margin = new System.Windows.Forms.Padding(4);
            this.tabMeteredWifi.Name = "tabMeteredWifi";
            this.tabMeteredWifi.Size = new System.Drawing.Size(1285, 490);
            this.tabMeteredWifi.TabIndex = 2;
            this.tabMeteredWifi.Text = "Metered WiFi";
            this.tabMeteredWifi.UseVisualStyleBackColor = true;
            // 
            // dataGridViewMeteredWifi
            // 
            this.dataGridViewMeteredWifi.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridViewMeteredWifi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMeteredWifi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.WirelessName});
            this.dataGridViewMeteredWifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewMeteredWifi.Location = new System.Drawing.Point(0, 43);
            this.dataGridViewMeteredWifi.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewMeteredWifi.Name = "dataGridViewMeteredWifi";
            this.dataGridViewMeteredWifi.RowHeadersWidth = 51;
            this.dataGridViewMeteredWifi.Size = new System.Drawing.Size(1285, 447);
            this.dataGridViewMeteredWifi.TabIndex = 0;
            // 
            // WirelessName
            // 
            this.WirelessName.HeaderText = "Wireless Name";
            this.WirelessName.MinimumWidth = 6;
            this.WirelessName.Name = "WirelessName";
            this.WirelessName.Width = 125;
            // 
            // lblMeteredWifiInfo
            // 
            this.lblMeteredWifiInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMeteredWifiInfo.Location = new System.Drawing.Point(0, 0);
            this.lblMeteredWifiInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMeteredWifiInfo.Name = "lblMeteredWifiInfo";
            this.lblMeteredWifiInfo.Size = new System.Drawing.Size(1285, 43);
            this.lblMeteredWifiInfo.TabIndex = 1;
            this.lblMeteredWifiInfo.Text = resources.GetString("lblMeteredWifiInfo.Text");
            this.lblMeteredWifiInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabBGService
            // 
            this.tabBGService.Controls.Add(this.lblInstructions);
            this.tabBGService.Controls.Add(this.btnRemoveService);
            this.tabBGService.Controls.Add(this.btnInstallService);
            this.tabBGService.Location = new System.Drawing.Point(4, 25);
            this.tabBGService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabBGService.Name = "tabBGService";
            this.tabBGService.Size = new System.Drawing.Size(1285, 490);
            this.tabBGService.TabIndex = 3;
            this.tabBGService.Text = "Background Service";
            this.tabBGService.UseVisualStyleBackColor = true;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(371, 49);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(928, 102);
            this.lblInstructions.TabIndex = 2;
            this.lblInstructions.Text = resources.GetString("lblInstructions.Text");
            // 
            // btnRemoveService
            // 
            this.btnRemoveService.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRemoveService.Location = new System.Drawing.Point(629, 217);
            this.btnRemoveService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRemoveService.Name = "btnRemoveService";
            this.btnRemoveService.Size = new System.Drawing.Size(120, 25);
            this.btnRemoveService.TabIndex = 1;
            this.btnRemoveService.Text = "Remove Service";
            this.btnRemoveService.UseVisualStyleBackColor = true;
            this.btnRemoveService.Click += new System.EventHandler(this.btnRemoveService_Click);
            // 
            // btnInstallService
            // 
            this.btnInstallService.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnInstallService.Location = new System.Drawing.Point(503, 217);
            this.btnInstallService.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnInstallService.Name = "btnInstallService";
            this.btnInstallService.Size = new System.Drawing.Size(120, 25);
            this.btnInstallService.TabIndex = 0;
            this.btnInstallService.Text = "Install Service";
            this.btnInstallService.UseVisualStyleBackColor = true;
            this.btnInstallService.Click += new System.EventHandler(this.btnInstallService_Click);
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.rtfAbout);
            this.tabAbout.Location = new System.Drawing.Point(4, 25);
            this.tabAbout.Margin = new System.Windows.Forms.Padding(4);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(4);
            this.tabAbout.Size = new System.Drawing.Size(1285, 490);
            this.tabAbout.TabIndex = 4;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // rtfAbout
            // 
            this.rtfAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtfAbout.Location = new System.Drawing.Point(4, 4);
            this.rtfAbout.Margin = new System.Windows.Forms.Padding(4);
            this.rtfAbout.Name = "rtfAbout";
            this.rtfAbout.ReadOnly = true;
            this.rtfAbout.Size = new System.Drawing.Size(1277, 482);
            this.rtfAbout.TabIndex = 0;
            this.rtfAbout.Text = resources.GetString("rtfAbout.Text");
            // 
            // pnlSaveClose
            // 
            this.pnlSaveClose.Controls.Add(this.btnClose);
            this.pnlSaveClose.Controls.Add(this.btnSave);
            this.pnlSaveClose.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSaveClose.Location = new System.Drawing.Point(0, 519);
            this.pnlSaveClose.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSaveClose.Name = "pnlSaveClose";
            this.pnlSaveClose.Size = new System.Drawing.Size(1293, 42);
            this.pnlSaveClose.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClose.Location = new System.Drawing.Point(635, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSave.Location = new System.Drawing.Point(527, 7);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ConfigureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 561);
            this.Controls.Add(this.tabCtrlSections);
            this.Controls.Add(this.pnlSaveClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1309, 598);
            this.Name = "ConfigureForm";
            this.Text = "Wallpeper Abyss Screensavr Configuration";
            this.Load += new System.EventHandler(this.ConfigureForm_Load);
            this.tabCtrlSections.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxQueries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCacheSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinSec)).EndInit();
            this.tabCategories.ResumeLayout(false);
            this.grpSearchOptions.ResumeLayout(false);
            this.pnlCollectionSupCollection.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlSearchOptions.ResumeLayout(false);
            this.pnlSearchOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            this.pnlSearchMethods.ResumeLayout(false);
            this.pnlSearchMethods.PerformLayout();
            this.pnlAddRemoveSearchMethods.ResumeLayout(false);
            this.tabMeteredWifi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeteredWifi)).EndInit();
            this.tabBGService.ResumeLayout(false);
            this.tabBGService.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.pnlSaveClose.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrlSections;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.NumericUpDown txtMaxSec;
        private System.Windows.Forms.NumericUpDown txtCacheSize;
        private System.Windows.Forms.Label lblRndMin;
        private System.Windows.Forms.Label lblRndAnd;
        private System.Windows.Forms.Label lblCcheSize;
        private System.Windows.Forms.NumericUpDown txtMinSec;
        private System.Windows.Forms.Label lblRndSwitchBetween;
        private System.Windows.Forms.TextBox txtAPIKey;
        private System.Windows.Forms.LinkLabel lblAPIKey;
        private System.Windows.Forms.TabPage tabCategories;
        private System.Windows.Forms.TabPage tabMeteredWifi;
        private System.Windows.Forms.DataGridView dataGridViewMeteredWifi;
        private System.Windows.Forms.DataGridViewTextBoxColumn WirelessName;
        private System.Windows.Forms.Panel pnlSaveClose;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkShowBottomBar;
        private System.Windows.Forms.Label lblCacheDirMB;
        private System.Windows.Forms.Label lblWorkingDir;
        private System.Windows.Forms.TextBox txtWorkingDir;
        private System.Windows.Forms.NumericUpDown txtMaxQueries;
        private System.Windows.Forms.Label lblPerMonth;
        private System.Windows.Forms.Label lblMaxQueries;
        private System.Windows.Forms.ComboBox cboImageLayout;
        private System.Windows.Forms.Label lblResizeImages;
        private System.Windows.Forms.Button btnSelectWorkingDir;
        private System.Windows.Forms.FolderBrowserDialog workingDirDialog;
        private System.Windows.Forms.GroupBox grpSearchOptions;
        private System.Windows.Forms.Panel pnlSearchMethods;
        private System.Windows.Forms.ListBox lstSearches;
        private System.Windows.Forms.Panel pnlAddRemoveSearchMethods;
        private System.Windows.Forms.Button btnRemoveSearchMethod;
        private System.Windows.Forms.Button btnAddSearchMethod;
        private System.Windows.Forms.Label lblSearchMethodsUsed;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.TextBox txtSearchTerm;
        private System.Windows.Forms.Label lblSearchTerm;
        private System.Windows.Forms.Panel pnlCollectionSupCollection;
        private System.Windows.Forms.ListBox lstSubCollection;
        private System.Windows.Forms.ListBox lstCollectionLabels;
        private System.Windows.Forms.Panel pnlSearchOptions;
        private System.Windows.Forms.NumericUpDown txtHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.NumericUpDown txtWidth;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblImageSizeOptions;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.Label lblSearchBy;
        private System.Windows.Forms.ComboBox cboLookupMethods;
        private System.Windows.Forms.ComboBox cboSizeOperators;
        private System.Windows.Forms.ComboBox cboSortMethods;
        private System.Windows.Forms.Button btnApplyMatch;
        private System.Windows.Forms.TabPage tabBGService;
        private System.Windows.Forms.Button btnRemoveService;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ToolTip toolTipHolder;
        private System.Windows.Forms.Label lblMeteredWifiInfo;
        private System.Windows.Forms.TabPage tabAbout;
        private System.Windows.Forms.RichTextBox rtfAbout;
    }
}