//NVD ACLKeeper
//Copyright © 2021, Nikolay Dudkin

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.
//You should have received a copy of the GNU General Public License
//along with this program.If not, see<https://www.gnu.org/licenses/>

namespace ACLKeeper
{
	partial class ConsoleForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleForm));
			this.consoleTabControl = new System.Windows.Forms.TabControl();
			this.managementTabPage = new System.Windows.Forms.TabPage();
			this.catalogueGroupBox = new System.Windows.Forms.GroupBox();
			this.catalogueGridView = new System.Windows.Forms.DataGridView();
			this.PathColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MonitoringColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.RefreshTimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RefreshDowColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.catalogueToolStrip = new System.Windows.Forms.ToolStrip();
			this.addToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.removeToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.settingsGroupBox = new System.Windows.Forms.GroupBox();
			this.bypassACLCheckBox = new System.Windows.Forms.CheckBox();
			this.loglevelTextBox = new System.Windows.Forms.TextBox();
			this.loglevelLabel = new System.Windows.Forms.Label();
			this.loopTextBox = new System.Windows.Forms.TextBox();
			this.loopLabel = new System.Windows.Forms.Label();
			this.actionsGroupBox = new System.Windows.Forms.GroupBox();
			this.elevateButton = new System.Windows.Forms.Button();
			this.stopButton = new System.Windows.Forms.Button();
			this.startButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.installButton = new System.Windows.Forms.Button();
			this.saveButton = new System.Windows.Forms.Button();
			this.logTabPage = new System.Windows.Forms.TabPage();
			this.logDataGridView = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.clearLogButton = new System.Windows.Forms.Button();
			this.exportLogButton = new System.Windows.Forms.Button();
			this.refreshButton = new System.Windows.Forms.Button();
			this.sqlTabPage = new System.Windows.Forms.TabPage();
			this.resultPanel = new System.Windows.Forms.Panel();
			this.resultDataGridView = new System.Windows.Forms.DataGridView();
			this.execPanel = new System.Windows.Forms.Panel();
			this.execButton = new System.Windows.Forms.Button();
			this.querySplitter = new System.Windows.Forms.Splitter();
			this.queryPanel = new System.Windows.Forms.Panel();
			this.queryTextBox = new System.Windows.Forms.TextBox();
			this.consoleTabControl.SuspendLayout();
			this.managementTabPage.SuspendLayout();
			this.catalogueGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.catalogueGridView)).BeginInit();
			this.catalogueToolStrip.SuspendLayout();
			this.settingsGroupBox.SuspendLayout();
			this.actionsGroupBox.SuspendLayout();
			this.logTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logDataGridView)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.sqlTabPage.SuspendLayout();
			this.resultPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
			this.execPanel.SuspendLayout();
			this.queryPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// consoleTabControl
			// 
			this.consoleTabControl.Controls.Add(this.managementTabPage);
			this.consoleTabControl.Controls.Add(this.logTabPage);
			this.consoleTabControl.Controls.Add(this.sqlTabPage);
			this.consoleTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.consoleTabControl.Location = new System.Drawing.Point(0, 0);
			this.consoleTabControl.Name = "consoleTabControl";
			this.consoleTabControl.SelectedIndex = 0;
			this.consoleTabControl.Size = new System.Drawing.Size(665, 416);
			this.consoleTabControl.TabIndex = 2;
			this.consoleTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.consoleTabControl_Selected);
			this.consoleTabControl.DoubleClick += new System.EventHandler(this.consoleTabControl_DoubleClick);
			// 
			// managementTabPage
			// 
			this.managementTabPage.BackColor = System.Drawing.SystemColors.Control;
			this.managementTabPage.Controls.Add(this.catalogueGroupBox);
			this.managementTabPage.Controls.Add(this.settingsGroupBox);
			this.managementTabPage.Controls.Add(this.actionsGroupBox);
			this.managementTabPage.Location = new System.Drawing.Point(4, 22);
			this.managementTabPage.Name = "managementTabPage";
			this.managementTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.managementTabPage.Size = new System.Drawing.Size(657, 390);
			this.managementTabPage.TabIndex = 0;
			this.managementTabPage.Text = "Management";
			// 
			// catalogueGroupBox
			// 
			this.catalogueGroupBox.Controls.Add(this.catalogueGridView);
			this.catalogueGroupBox.Controls.Add(this.catalogueToolStrip);
			this.catalogueGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.catalogueGroupBox.Location = new System.Drawing.Point(3, 100);
			this.catalogueGroupBox.Name = "catalogueGroupBox";
			this.catalogueGroupBox.Size = new System.Drawing.Size(651, 237);
			this.catalogueGroupBox.TabIndex = 2;
			this.catalogueGroupBox.TabStop = false;
			this.catalogueGroupBox.Text = "Catalogue:";
			// 
			// catalogueGridView
			// 
			this.catalogueGridView.AllowUserToAddRows = false;
			this.catalogueGridView.AllowUserToDeleteRows = false;
			this.catalogueGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.catalogueGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.catalogueGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PathColumn,
            this.MonitoringColumn,
            this.RefreshTimeColumn,
            this.RefreshDowColumn});
			this.catalogueGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.catalogueGridView.Location = new System.Drawing.Point(3, 41);
			this.catalogueGridView.MultiSelect = false;
			this.catalogueGridView.Name = "catalogueGridView";
			this.catalogueGridView.ReadOnly = true;
			this.catalogueGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.catalogueGridView.Size = new System.Drawing.Size(645, 193);
			this.catalogueGridView.TabIndex = 1;
			this.catalogueGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.catalogueGridView_CellDoubleClick);
			// 
			// PathColumn
			// 
			this.PathColumn.HeaderText = "Path";
			this.PathColumn.Name = "PathColumn";
			this.PathColumn.ReadOnly = true;
			this.PathColumn.Width = 54;
			// 
			// MonitoringColumn
			// 
			this.MonitoringColumn.HeaderText = "Monitioring";
			this.MonitoringColumn.Name = "MonitoringColumn";
			this.MonitoringColumn.ReadOnly = true;
			this.MonitoringColumn.Width = 64;
			// 
			// RefreshTimeColumn
			// 
			this.RefreshTimeColumn.HeaderText = "Refresh time";
			this.RefreshTimeColumn.Name = "RefreshTimeColumn";
			this.RefreshTimeColumn.ReadOnly = true;
			this.RefreshTimeColumn.Width = 91;
			// 
			// RefreshDowColumn
			// 
			this.RefreshDowColumn.HeaderText = "Refresh days";
			this.RefreshDowColumn.Name = "RefreshDowColumn";
			this.RefreshDowColumn.ReadOnly = true;
			this.RefreshDowColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.RefreshDowColumn.Width = 94;
			// 
			// catalogueToolStrip
			// 
			this.catalogueToolStrip.BackColor = System.Drawing.Color.Transparent;
			this.catalogueToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.catalogueToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripButton,
            this.removeToolStripButton,
            this.editToolStripButton});
			this.catalogueToolStrip.Location = new System.Drawing.Point(3, 16);
			this.catalogueToolStrip.Name = "catalogueToolStrip";
			this.catalogueToolStrip.Size = new System.Drawing.Size(645, 25);
			this.catalogueToolStrip.TabIndex = 0;
			this.catalogueToolStrip.Text = "catalogueToolStrip";
			// 
			// addToolStripButton
			// 
			this.addToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.addToolStripButton.Image = global::ACLKeeper.Properties.Resources.navigate_plus;
			this.addToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.addToolStripButton.Name = "addToolStripButton";
			this.addToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.addToolStripButton.Text = "Add";
			this.addToolStripButton.Click += new System.EventHandler(this.addToolStripButton_Click);
			// 
			// removeToolStripButton
			// 
			this.removeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.removeToolStripButton.Image = global::ACLKeeper.Properties.Resources.navigate_minus;
			this.removeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.removeToolStripButton.Name = "removeToolStripButton";
			this.removeToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.removeToolStripButton.Text = "Remove";
			this.removeToolStripButton.Click += new System.EventHandler(this.removeToolStripButton_Click);
			// 
			// editToolStripButton
			// 
			this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.editToolStripButton.Image = global::ACLKeeper.Properties.Resources.pencil;
			this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.editToolStripButton.Name = "editToolStripButton";
			this.editToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.editToolStripButton.Text = "Edit";
			this.editToolStripButton.Click += new System.EventHandler(this.editToolStripButton_Click);
			// 
			// settingsGroupBox
			// 
			this.settingsGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.settingsGroupBox.Controls.Add(this.bypassACLCheckBox);
			this.settingsGroupBox.Controls.Add(this.loglevelTextBox);
			this.settingsGroupBox.Controls.Add(this.loglevelLabel);
			this.settingsGroupBox.Controls.Add(this.loopTextBox);
			this.settingsGroupBox.Controls.Add(this.loopLabel);
			this.settingsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.settingsGroupBox.Location = new System.Drawing.Point(3, 3);
			this.settingsGroupBox.Name = "settingsGroupBox";
			this.settingsGroupBox.Size = new System.Drawing.Size(651, 97);
			this.settingsGroupBox.TabIndex = 1;
			this.settingsGroupBox.TabStop = false;
			this.settingsGroupBox.Text = "Settings:";
			// 
			// bypassACLCheckBox
			// 
			this.bypassACLCheckBox.AutoSize = true;
			this.bypassACLCheckBox.Location = new System.Drawing.Point(62, 71);
			this.bypassACLCheckBox.Name = "bypassACLCheckBox";
			this.bypassACLCheckBox.Size = new System.Drawing.Size(155, 17);
			this.bypassACLCheckBox.TabIndex = 24;
			this.bypassACLCheckBox.Text = "Service should bypass ACL";
			this.bypassACLCheckBox.UseVisualStyleBackColor = true;
			// 
			// loglevelTextBox
			// 
			this.loglevelTextBox.Location = new System.Drawing.Point(62, 45);
			this.loglevelTextBox.Name = "loglevelTextBox";
			this.loglevelTextBox.Size = new System.Drawing.Size(155, 20);
			this.loglevelTextBox.TabIndex = 23;
			// 
			// loglevelLabel
			// 
			this.loglevelLabel.AutoSize = true;
			this.loglevelLabel.Location = new System.Drawing.Point(6, 48);
			this.loglevelLabel.Name = "loglevelLabel";
			this.loglevelLabel.Size = new System.Drawing.Size(53, 13);
			this.loglevelLabel.TabIndex = 22;
			this.loglevelLabel.Text = "Log level:";
			// 
			// loopTextBox
			// 
			this.loopTextBox.BackColor = System.Drawing.SystemColors.Window;
			this.loopTextBox.Location = new System.Drawing.Point(62, 19);
			this.loopTextBox.Name = "loopTextBox";
			this.loopTextBox.Size = new System.Drawing.Size(155, 20);
			this.loopTextBox.TabIndex = 19;
			this.loopTextBox.TextChanged += new System.EventHandler(this.loopTextBox_TextChanged);
			// 
			// loopLabel
			// 
			this.loopLabel.AutoSize = true;
			this.loopLabel.Location = new System.Drawing.Point(6, 22);
			this.loopLabel.Name = "loopLabel";
			this.loopLabel.Size = new System.Drawing.Size(37, 13);
			this.loopLabel.TabIndex = 18;
			this.loopLabel.Text = "Delay:";
			// 
			// actionsGroupBox
			// 
			this.actionsGroupBox.BackColor = System.Drawing.Color.Transparent;
			this.actionsGroupBox.Controls.Add(this.elevateButton);
			this.actionsGroupBox.Controls.Add(this.stopButton);
			this.actionsGroupBox.Controls.Add(this.startButton);
			this.actionsGroupBox.Controls.Add(this.removeButton);
			this.actionsGroupBox.Controls.Add(this.installButton);
			this.actionsGroupBox.Controls.Add(this.saveButton);
			this.actionsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.actionsGroupBox.Location = new System.Drawing.Point(3, 337);
			this.actionsGroupBox.Name = "actionsGroupBox";
			this.actionsGroupBox.Size = new System.Drawing.Size(651, 50);
			this.actionsGroupBox.TabIndex = 3;
			this.actionsGroupBox.TabStop = false;
			this.actionsGroupBox.Text = "Actions:";
			// 
			// elevateButton
			// 
			this.elevateButton.Image = global::ACLKeeper.Properties.Resources.shield;
			this.elevateButton.Location = new System.Drawing.Point(122, 19);
			this.elevateButton.Name = "elevateButton";
			this.elevateButton.Size = new System.Drawing.Size(110, 23);
			this.elevateButton.TabIndex = 5;
			this.elevateButton.Text = "Restart as admin";
			this.elevateButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.elevateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.elevateButton.UseVisualStyleBackColor = true;
			this.elevateButton.Click += new System.EventHandler(this.elevateButton_Click);
			// 
			// stopButton
			// 
			this.stopButton.Image = global::ACLKeeper.Properties.Resources.media_stop;
			this.stopButton.Location = new System.Drawing.Point(469, 18);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(110, 23);
			this.stopButton.TabIndex = 4;
			this.stopButton.Text = "Stop service";
			this.stopButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.stopButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.stopButton.UseVisualStyleBackColor = true;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// startButton
			// 
			this.startButton.Image = global::ACLKeeper.Properties.Resources.media_play;
			this.startButton.Location = new System.Drawing.Point(353, 19);
			this.startButton.Name = "startButton";
			this.startButton.Size = new System.Drawing.Size(110, 23);
			this.startButton.TabIndex = 3;
			this.startButton.Text = "Start service";
			this.startButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.startButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.startButton.UseVisualStyleBackColor = true;
			this.startButton.Click += new System.EventHandler(this.startButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Image = global::ACLKeeper.Properties.Resources.uninstall;
			this.removeButton.Location = new System.Drawing.Point(237, 19);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(110, 23);
			this.removeButton.TabIndex = 2;
			this.removeButton.Text = "Remove service";
			this.removeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.removeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// installButton
			// 
			this.installButton.Image = global::ACLKeeper.Properties.Resources.install;
			this.installButton.Location = new System.Drawing.Point(121, 19);
			this.installButton.Name = "installButton";
			this.installButton.Size = new System.Drawing.Size(110, 23);
			this.installButton.TabIndex = 1;
			this.installButton.Text = "Install service";
			this.installButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.installButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.installButton.UseVisualStyleBackColor = true;
			this.installButton.Click += new System.EventHandler(this.installButton_Click);
			// 
			// saveButton
			// 
			this.saveButton.Image = global::ACLKeeper.Properties.Resources.floppy_disk;
			this.saveButton.Location = new System.Drawing.Point(6, 19);
			this.saveButton.Name = "saveButton";
			this.saveButton.Size = new System.Drawing.Size(110, 23);
			this.saveButton.TabIndex = 0;
			this.saveButton.Text = "Save";
			this.saveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.saveButton.UseVisualStyleBackColor = true;
			this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
			// 
			// logTabPage
			// 
			this.logTabPage.Controls.Add(this.logDataGridView);
			this.logTabPage.Controls.Add(this.groupBox1);
			this.logTabPage.Location = new System.Drawing.Point(4, 22);
			this.logTabPage.Name = "logTabPage";
			this.logTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.logTabPage.Size = new System.Drawing.Size(657, 390);
			this.logTabPage.TabIndex = 1;
			this.logTabPage.Text = "Log";
			this.logTabPage.UseVisualStyleBackColor = true;
			// 
			// logDataGridView
			// 
			this.logDataGridView.AllowUserToAddRows = false;
			this.logDataGridView.AllowUserToDeleteRows = false;
			this.logDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.logDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.logDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logDataGridView.Location = new System.Drawing.Point(3, 3);
			this.logDataGridView.Name = "logDataGridView";
			this.logDataGridView.ReadOnly = true;
			this.logDataGridView.RowHeadersVisible = false;
			this.logDataGridView.Size = new System.Drawing.Size(651, 334);
			this.logDataGridView.StandardTab = true;
			this.logDataGridView.TabIndex = 27;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.clearLogButton);
			this.groupBox1.Controls.Add(this.exportLogButton);
			this.groupBox1.Controls.Add(this.refreshButton);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(3, 337);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(651, 50);
			this.groupBox1.TabIndex = 26;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Actions:";
			// 
			// clearLogButton
			// 
			this.clearLogButton.Image = global::ACLKeeper.Properties.Resources.erase;
			this.clearLogButton.Location = new System.Drawing.Point(238, 19);
			this.clearLogButton.Name = "clearLogButton";
			this.clearLogButton.Size = new System.Drawing.Size(110, 23);
			this.clearLogButton.TabIndex = 2;
			this.clearLogButton.Text = "Clear";
			this.clearLogButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.clearLogButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.clearLogButton.UseVisualStyleBackColor = true;
			this.clearLogButton.Click += new System.EventHandler(this.clearLogButton_Click);
			// 
			// exportLogButton
			// 
			this.exportLogButton.Image = global::ACLKeeper.Properties.Resources.spreadsheed_data;
			this.exportLogButton.Location = new System.Drawing.Point(122, 19);
			this.exportLogButton.Name = "exportLogButton";
			this.exportLogButton.Size = new System.Drawing.Size(110, 23);
			this.exportLogButton.TabIndex = 1;
			this.exportLogButton.Text = "Export";
			this.exportLogButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.exportLogButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.exportLogButton.UseVisualStyleBackColor = true;
			this.exportLogButton.Click += new System.EventHandler(this.exportLogButton_Click);
			// 
			// refreshButton
			// 
			this.refreshButton.Image = global::ACLKeeper.Properties.Resources.refresh;
			this.refreshButton.Location = new System.Drawing.Point(6, 19);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(110, 23);
			this.refreshButton.TabIndex = 0;
			this.refreshButton.Text = "Refresh";
			this.refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
			// 
			// sqlTabPage
			// 
			this.sqlTabPage.Controls.Add(this.resultPanel);
			this.sqlTabPage.Controls.Add(this.execPanel);
			this.sqlTabPage.Controls.Add(this.querySplitter);
			this.sqlTabPage.Controls.Add(this.queryPanel);
			this.sqlTabPage.Location = new System.Drawing.Point(4, 22);
			this.sqlTabPage.Name = "sqlTabPage";
			this.sqlTabPage.Size = new System.Drawing.Size(657, 390);
			this.sqlTabPage.TabIndex = 2;
			this.sqlTabPage.Text = "SQL";
			this.sqlTabPage.UseVisualStyleBackColor = true;
			// 
			// resultPanel
			// 
			this.resultPanel.Controls.Add(this.resultDataGridView);
			this.resultPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultPanel.Location = new System.Drawing.Point(0, 141);
			this.resultPanel.Name = "resultPanel";
			this.resultPanel.Size = new System.Drawing.Size(657, 249);
			this.resultPanel.TabIndex = 5;
			// 
			// resultDataGridView
			// 
			this.resultDataGridView.AllowUserToAddRows = false;
			this.resultDataGridView.AllowUserToDeleteRows = false;
			this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.resultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.resultDataGridView.Location = new System.Drawing.Point(0, 0);
			this.resultDataGridView.Name = "resultDataGridView";
			this.resultDataGridView.ReadOnly = true;
			this.resultDataGridView.RowHeadersVisible = false;
			this.resultDataGridView.Size = new System.Drawing.Size(657, 249);
			this.resultDataGridView.StandardTab = true;
			this.resultDataGridView.TabIndex = 28;
			// 
			// execPanel
			// 
			this.execPanel.Controls.Add(this.execButton);
			this.execPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.execPanel.Location = new System.Drawing.Point(0, 103);
			this.execPanel.Name = "execPanel";
			this.execPanel.Size = new System.Drawing.Size(657, 38);
			this.execPanel.TabIndex = 4;
			// 
			// execButton
			// 
			this.execButton.Enabled = false;
			this.execButton.Image = global::ACLKeeper.Properties.Resources.media_play;
			this.execButton.Location = new System.Drawing.Point(8, 6);
			this.execButton.Name = "execButton";
			this.execButton.Size = new System.Drawing.Size(110, 23);
			this.execButton.TabIndex = 4;
			this.execButton.Text = "Execute";
			this.execButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.execButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.execButton.UseVisualStyleBackColor = true;
			this.execButton.Click += new System.EventHandler(this.execButton_Click);
			// 
			// querySplitter
			// 
			this.querySplitter.Dock = System.Windows.Forms.DockStyle.Top;
			this.querySplitter.Location = new System.Drawing.Point(0, 100);
			this.querySplitter.Name = "querySplitter";
			this.querySplitter.Size = new System.Drawing.Size(657, 3);
			this.querySplitter.TabIndex = 2;
			this.querySplitter.TabStop = false;
			// 
			// queryPanel
			// 
			this.queryPanel.Controls.Add(this.queryTextBox);
			this.queryPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.queryPanel.Location = new System.Drawing.Point(0, 0);
			this.queryPanel.Name = "queryPanel";
			this.queryPanel.Size = new System.Drawing.Size(657, 100);
			this.queryPanel.TabIndex = 0;
			// 
			// queryTextBox
			// 
			this.queryTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queryTextBox.Location = new System.Drawing.Point(0, 0);
			this.queryTextBox.Multiline = true;
			this.queryTextBox.Name = "queryTextBox";
			this.queryTextBox.Size = new System.Drawing.Size(657, 100);
			this.queryTextBox.TabIndex = 0;
			this.queryTextBox.TextChanged += new System.EventHandler(this.queryTextBox_TextChanged);
			// 
			// ConsoleForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(665, 416);
			this.Controls.Add(this.consoleTabControl);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConsoleForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Management console";
			this.Load += new System.EventHandler(this.consoleForm_Load);
			this.consoleTabControl.ResumeLayout(false);
			this.managementTabPage.ResumeLayout(false);
			this.catalogueGroupBox.ResumeLayout(false);
			this.catalogueGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.catalogueGridView)).EndInit();
			this.catalogueToolStrip.ResumeLayout(false);
			this.catalogueToolStrip.PerformLayout();
			this.settingsGroupBox.ResumeLayout(false);
			this.settingsGroupBox.PerformLayout();
			this.actionsGroupBox.ResumeLayout(false);
			this.logTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.logDataGridView)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.sqlTabPage.ResumeLayout(false);
			this.resultPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
			this.execPanel.ResumeLayout(false);
			this.queryPanel.ResumeLayout(false);
			this.queryPanel.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl consoleTabControl;
		private System.Windows.Forms.TabPage managementTabPage;
		private System.Windows.Forms.GroupBox settingsGroupBox;
		private System.Windows.Forms.TextBox loglevelTextBox;
		private System.Windows.Forms.Label loglevelLabel;
		private System.Windows.Forms.TextBox loopTextBox;
		private System.Windows.Forms.Label loopLabel;
		private System.Windows.Forms.TabPage logTabPage;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.DataGridView logDataGridView;
		private System.Windows.Forms.Button exportLogButton;
		private System.Windows.Forms.Button clearLogButton;
		private System.Windows.Forms.GroupBox actionsGroupBox;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button installButton;
		private System.Windows.Forms.Button saveButton;
		private System.Windows.Forms.Button elevateButton;
		private System.Windows.Forms.TabPage sqlTabPage;
		private System.Windows.Forms.Panel queryPanel;
		private System.Windows.Forms.TextBox queryTextBox;
		private System.Windows.Forms.Splitter querySplitter;
		private System.Windows.Forms.Panel resultPanel;
		private System.Windows.Forms.DataGridView resultDataGridView;
		private System.Windows.Forms.Panel execPanel;
		private System.Windows.Forms.Button execButton;
		private System.Windows.Forms.GroupBox catalogueGroupBox;
		private System.Windows.Forms.ToolStrip catalogueToolStrip;
		private System.Windows.Forms.ToolStripButton addToolStripButton;
		private System.Windows.Forms.ToolStripButton removeToolStripButton;
		private System.Windows.Forms.ToolStripButton editToolStripButton;
		private System.Windows.Forms.DataGridView catalogueGridView;
		private System.Windows.Forms.CheckBox bypassACLCheckBox;
		private System.Windows.Forms.DataGridViewTextBoxColumn PathColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn MonitoringColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn RefreshTimeColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn RefreshDowColumn;
	}
}