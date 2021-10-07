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

using NVD.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Windows.Forms;

namespace ACLKeeper
{
	partial class ConsoleForm : Form
	{
		private readonly string exePath, dbPath, exeName;
		private Catalogue catalogue;

		public ConsoleForm()
		{
			InitializeComponent();
		}

		public ConsoleForm(string exePath, string dbPath, string exeName) : this()
		{
			this.exePath = exePath;
			this.dbPath = dbPath;
			this.exeName = exeName;
		}

		private void consoleForm_Load(object sender, EventArgs e)
		{
			consoleTabControl.TabPages.Remove(sqlTabPage);

			loadSettings();

			bool admin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

			elevateButton.Enabled = elevateButton.Visible = !admin;
			installButton.Enabled = installButton.Visible = removeButton.Enabled = removeButton.Visible = startButton.Enabled = startButton.Visible = stopButton.Enabled = stopButton.Visible = admin;

			ToolTip toolTip = new ToolTip();

			toolTip.SetToolTip(loopTextBox, "Delay between iterations, in minutes; recommended: 1-15");
			toolTip.SetToolTip(loglevelTextBox, "Minimum prioriry of messages to log: 1 - debug, 2 - information, 3 - errors, 4 - exceptions; recommended: 3-4");
			toolTip.SetToolTip(bypassACLCheckBox, "Grant service the privileges necessary to access files and folders disregard current ACLs; recommended, if service is running under local system account");

			toolTip.SetToolTip(saveButton, "Save settings to database");
			toolTip.SetToolTip(elevateButton, "Restart application as administrator to get access to service management functions");
			toolTip.SetToolTip(installButton, "Install ACLKeeper service");
			toolTip.SetToolTip(removeButton, "Remove ACLKeeper service");
			toolTip.SetToolTip(startButton, "Start ACLKeeper service");
			toolTip.SetToolTip(stopButton, "Stop ACLKeeper service");

			toolTip.SetToolTip(refreshButton, "Refresh log");
			toolTip.SetToolTip(exportLogButton, "Export log to CSV file");
			toolTip.SetToolTip(clearLogButton, "Clear log");
		}

		private void loadSettings()
		{
			SQLite db = new SQLite("Data Source = \"" + dbPath + "\"");

			loopTextBox.Text = db.Get<string>("SELECT value FROM settings WHERE name = 'loop';");
			loglevelTextBox.Text = db.Get<string>("SELECT value FROM settings WHERE name = 'loglevel';");
			bypassACLCheckBox.Checked = !db.Get<string>("SELECT value FROM settings WHERE name = 'bypassacl';").Equals("0", StringComparison.InvariantCultureIgnoreCase);

			catalogue = new Catalogue();
			catalogue.Load(db);
			updateCatalogueGridView();
		}

		private bool saveSettings()
		{
			SQLite db = new SQLite("Data Source = \"" + dbPath + "\"");

			db.Execute("UPDATE settings SET value = @1 WHERE name = @0;", "loop", loopTextBox.Text);
			db.Execute("UPDATE settings SET value = @1 WHERE name = @0;", "loglevel", loglevelTextBox.Text);
			db.Execute("UPDATE settings SET value = @1 WHERE name = @0;", "bypassacl", bypassACLCheckBox.Checked ? "1" : "0");

			return catalogue.Save(db);
		}

		private void updateCatalogueGridView()
		{
			catalogueGridView.Rows.Clear();

			catalogue.Decompose((item, path, monitoring, refresh_time, refresh_dow) =>
			{
				DataGridViewRow row = catalogueGridView.Rows[catalogueGridView.Rows.Add()];
				
				row.Tag = item;
				row.Cells[0].Value = path;
				((DataGridViewCheckBoxCell)row.Cells[1]).Value = monitoring;
				var (h, m) = Helper.SplitMinutes(refresh_time);
				row.Cells[2].Value = h.ToString("00") + ":" + m.ToString("00");
				row.Cells[3].Value = Helper.DowToString(refresh_dow);
			});
		}

		private void refreshLog()
		{
			logDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
			SQLite db = new SQLite("Data Source = \"" + dbPath + "\"");
			DataTable table = db.GetDataTable("SELECT * FROM logs;");
			logDataGridView.DataSource = table;
			logDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
		}

		private void saveButton_Click(object sender, EventArgs e)
		{
			if(saveSettings())
				MessageBox.Show("Settings saved. Service should be restarted for the new settings to become effective.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Failed to save settings!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void installButton_Click(object sender, EventArgs e)
		{
			if(ServiceManager.ServiceInstall(exeName, ServiceManagerNative.SERVICE_START.SERVICE_AUTO_START, exePath + " -run"))
				MessageBox.Show("Service installed.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Service installation failed!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			if(ServiceManager.ServiceUninstall(exeName))
				MessageBox.Show("Service removed.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Service removal failed!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void startButton_Click(object sender, EventArgs e)
		{
			if(ServiceManager.ServiceStart(exeName))
				MessageBox.Show("Service started.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Failed to start service!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			if(ServiceManager.ServiceStop(exeName))
				MessageBox.Show("Service stopped.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			else
				MessageBox.Show("Failed to stop service!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void refreshButton_Click(object sender, EventArgs e)
		{
			refreshLog();
		}

		private void clearLogButton_Click(object sender, EventArgs e)
		{
			SQLite db = new SQLite("Data Source = \"" + dbPath + "\"");
			db.Execute("DELETE FROM logs;");

			refreshLog();
		}

		private void exportLogButton_Click(object sender, EventArgs e)
		{
			SaveFileDialog sfd = new SaveFileDialog
			{
				Filter = "CSV files (*.csv)|*.csv",
				DefaultExt = "csv",
				AddExtension = true,
				CheckPathExists = true,
				FilterIndex = 0
			};
			if (sfd.ShowDialog() == DialogResult.OK)
			{
				StreamWriter sw = new StreamWriter(sfd.FileName);
				string separator = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

				DataTable table = (DataTable)logDataGridView.DataSource;

				List<string> columns = new List<string>();
				foreach (DataColumn column in table.Columns)
				{
					columns.Add(column.ColumnName);
				}
				sw.WriteLine(string.Join(separator, columns));

				int ncolumns = table.Columns.Count;
				foreach(DataRow row in table.Rows)
				{
					sw.WriteLine(string.Join(separator, row.ItemArray.Select(i => i.ToString())));
				}

				sw.Close();

				MessageBox.Show("Log exported.", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void consoleTabControl_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == logTabPage)
				refreshLog();
		}

		private void elevateButton_Click(object sender, EventArgs e)
		{
			try
			{
				Process proc = new Process();
				proc.StartInfo.FileName = exePath;
				proc.StartInfo.UseShellExecute = true;
				proc.StartInfo.Verb = "runas";
				proc.Start();
				Close();
			}
			catch { }
		}

		private void consoleTabControl_DoubleClick(object sender, EventArgs e)
		{
			if(ModifierKeys.HasFlag(Keys.Control) && !consoleTabControl.TabPages.Contains(sqlTabPage))
				consoleTabControl.TabPages.Add(sqlTabPage);
		}

		private void execButton_Click(object sender, EventArgs e)
		{
			try
			{
				SQLite db = new SQLite("Data Source = \"" + dbPath + "\"");
				if (queryTextBox.Text.ToLowerInvariant().StartsWith("select"))
				{
					resultDataGridView.DataSource = null;
					DataTable table = db.GetDataTable(queryTextBox.Text);
					resultDataGridView.DataSource = table;
				}
				else
				{
					resultDataGridView.DataSource = null;
					db.Execute(queryTextBox.Text);
					DataTable table = new DataTable();
					table.Columns.Add("Message");
					table.Rows.Add("Done.");
					resultDataGridView.DataSource = table;
				}
			}
			catch
			{
				resultDataGridView.DataSource = null;
				DataTable table = new DataTable();
				table.Columns.Add("Message");
				table.Rows.Add("Failure!");
				resultDataGridView.DataSource = table;
			}
		}

		private void queryTextBox_TextChanged(object sender, EventArgs e)
		{
			execButton.Enabled = queryTextBox.Text.Length > 0;
		}

		private void addToolStripButton_Click(object sender, EventArgs e)
		{
			CatalogueItem item = new CatalogueItem();
			CatalogueItemForm cif = new CatalogueItemForm(catalogue, item);
			if (cif.ShowDialog() == DialogResult.OK)
			{
				catalogue.Add(item);
				updateCatalogueGridView();
			}
		}

		private void removeToolStripButton_Click(object sender, EventArgs e)
		{
			if(catalogueGridView.CurrentRow != null)
			{
				catalogue.Remove((CatalogueItem)catalogueGridView.CurrentRow.Tag);
				updateCatalogueGridView();
			}
		}

		private void editToolStripButton_Click(object sender, EventArgs e)
		{
			editCurrentItem();
		}

		private void editCurrentItem()
		{
			if (catalogueGridView.CurrentRow != null)
			{
				CatalogueItemForm cif = new CatalogueItemForm(catalogue, (CatalogueItem)catalogueGridView.CurrentRow.Tag);
				if (cif.ShowDialog() == DialogResult.OK)
				{
					updateCatalogueGridView();
				}
			}
		}

		private void catalogueGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			editCurrentItem();
		}

		private void loopTextBox_TextChanged(object sender, EventArgs e)
		{
			if (loopTextBox.Text.Equals("about:aclkeeper", StringComparison.InvariantCultureIgnoreCase))
			{
				MessageBox.Show(Assembly.GetExecutingAssembly().GetName().Name + "\r\n" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + "\r\n" + ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute))).Copyright, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
		}
	}
}
