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

using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ACLKeeper
{
	internal partial class CatalogueItemForm : Form
	{
		private readonly Catalogue catalogue;
		private readonly CatalogueItem item;

		public CatalogueItemForm()
		{
			InitializeComponent();
		}

		public CatalogueItemForm(Catalogue catalogue, CatalogueItem item) : this()
		{
			this.catalogue = catalogue;
			this.item = item;
		}

		private void CatalogueItemForm_Load(object sender, EventArgs e)
		{
			item.Decompose((dummy, path, monitoring, refresh_time, refresh_dow) =>
			{
				pathTextBox.Text = path;
				monitoringCheckBox.Checked = monitoring;
				var (h, m) = Helper.SplitMinutes(refresh_time);
				refreshPicker.Value = DateTime.Now.Date.AddHours(h).AddMinutes(m);

				sunCheckBox.Checked = (refresh_dow & 1) != 0;
				monCheckBox.Checked = (refresh_dow & 2) != 0;
				tueCheckBox.Checked = (refresh_dow & 4) != 0;
				wedCheckBox.Checked = (refresh_dow & 8) != 0;
				thuCheckBox.Checked = (refresh_dow & 16) != 0;
				friCheckBox.Checked = (refresh_dow & 32) != 0;
				satCheckBox.Checked = (refresh_dow & 64) != 0;
			});
		}


		private void updateItem()
		{
			long refresh_dow = 0;
			if (sunCheckBox.Checked) refresh_dow |= 1;
			if (monCheckBox.Checked) refresh_dow |= 2;
			if (tueCheckBox.Checked) refresh_dow |= 4;
			if (wedCheckBox.Checked) refresh_dow |= 8;
			if (thuCheckBox.Checked) refresh_dow |= 16;
			if (friCheckBox.Checked) refresh_dow |= 32;
			if (satCheckBox.Checked) refresh_dow |= 64;

			item.Update(pathTextBox.Text, monitoringCheckBox.Checked, refreshPicker.Value.Hour * 60 + refreshPicker.Value.Minute, refresh_dow);
		}

		private void updateRefreshTime()
		{
			refreshPicker.Enabled = sunCheckBox.Checked || monCheckBox.Checked || tueCheckBox.Checked || wedCheckBox.Checked || thuCheckBox.Checked || friCheckBox.Checked || satCheckBox.Checked;

			if (!refreshPicker.Enabled)
			{
				refreshPicker.Value = DateTime.Now.Date;
			}
		}

		private void pathButton_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fbd = new FolderBrowserDialog
			{
				RootFolder = Environment.SpecialFolder.MyComputer
			};

			if (fbd.ShowDialog() == DialogResult.OK)
			{
				pathTextBox.Text = fbd.SelectedPath;
			}
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			if (pathTextBox.Text.Length == 0)
			{
				MessageBox.Show("Path cannot be empty!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			updateItem();

			if(!catalogue.ItemCanBeAdded(item))
			{
				MessageBox.Show("Path with the same root already registered!", Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void monCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void tueCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void wedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void thuCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void friCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void satCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}

		private void sunCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			updateRefreshTime();
		}
	}
}
