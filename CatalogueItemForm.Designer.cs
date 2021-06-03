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
	partial class CatalogueItemForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatalogueItemForm));
			this.actionsGroupBox = new System.Windows.Forms.GroupBox();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.paramsGroupBox = new System.Windows.Forms.GroupBox();
			this.sunCheckBox = new System.Windows.Forms.CheckBox();
			this.satCheckBox = new System.Windows.Forms.CheckBox();
			this.friCheckBox = new System.Windows.Forms.CheckBox();
			this.thuCheckBox = new System.Windows.Forms.CheckBox();
			this.wedCheckBox = new System.Windows.Forms.CheckBox();
			this.tueCheckBox = new System.Windows.Forms.CheckBox();
			this.monCheckBox = new System.Windows.Forms.CheckBox();
			this.monitoringLabel = new System.Windows.Forms.Label();
			this.refreshLabel = new System.Windows.Forms.Label();
			this.monitoringCheckBox = new System.Windows.Forms.CheckBox();
			this.refreshPicker = new System.Windows.Forms.DateTimePicker();
			this.pathButton = new System.Windows.Forms.Button();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.pathLabel = new System.Windows.Forms.Label();
			this.actionsGroupBox.SuspendLayout();
			this.paramsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// actionsGroupBox
			// 
			this.actionsGroupBox.Controls.Add(this.cancelButton);
			this.actionsGroupBox.Controls.Add(this.okButton);
			this.actionsGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.actionsGroupBox.Location = new System.Drawing.Point(0, 96);
			this.actionsGroupBox.Name = "actionsGroupBox";
			this.actionsGroupBox.Size = new System.Drawing.Size(599, 52);
			this.actionsGroupBox.TabIndex = 1;
			this.actionsGroupBox.TabStop = false;
			this.actionsGroupBox.Text = "Actions:";
			// 
			// cancelButton
			// 
			this.cancelButton.Location = new System.Drawing.Point(93, 19);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 1;
			this.cancelButton.Text = "&Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Location = new System.Drawing.Point(12, 19);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 0;
			this.okButton.Text = "&OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// paramsGroupBox
			// 
			this.paramsGroupBox.Controls.Add(this.sunCheckBox);
			this.paramsGroupBox.Controls.Add(this.satCheckBox);
			this.paramsGroupBox.Controls.Add(this.friCheckBox);
			this.paramsGroupBox.Controls.Add(this.thuCheckBox);
			this.paramsGroupBox.Controls.Add(this.wedCheckBox);
			this.paramsGroupBox.Controls.Add(this.tueCheckBox);
			this.paramsGroupBox.Controls.Add(this.monCheckBox);
			this.paramsGroupBox.Controls.Add(this.monitoringLabel);
			this.paramsGroupBox.Controls.Add(this.refreshLabel);
			this.paramsGroupBox.Controls.Add(this.monitoringCheckBox);
			this.paramsGroupBox.Controls.Add(this.refreshPicker);
			this.paramsGroupBox.Controls.Add(this.pathButton);
			this.paramsGroupBox.Controls.Add(this.pathTextBox);
			this.paramsGroupBox.Controls.Add(this.pathLabel);
			this.paramsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.paramsGroupBox.Location = new System.Drawing.Point(0, 0);
			this.paramsGroupBox.Name = "paramsGroupBox";
			this.paramsGroupBox.Size = new System.Drawing.Size(599, 96);
			this.paramsGroupBox.TabIndex = 2;
			this.paramsGroupBox.TabStop = false;
			this.paramsGroupBox.Text = "Parameters:";
			// 
			// sunCheckBox
			// 
			this.sunCheckBox.AutoSize = true;
			this.sunCheckBox.Location = new System.Drawing.Point(503, 67);
			this.sunCheckBox.Name = "sunCheckBox";
			this.sunCheckBox.Size = new System.Drawing.Size(45, 17);
			this.sunCheckBox.TabIndex = 16;
			this.sunCheckBox.Text = "Sun";
			this.sunCheckBox.UseVisualStyleBackColor = true;
			this.sunCheckBox.CheckedChanged += new System.EventHandler(this.sunCheckBox_CheckedChanged);
			// 
			// satCheckBox
			// 
			this.satCheckBox.AutoSize = true;
			this.satCheckBox.Location = new System.Drawing.Point(450, 67);
			this.satCheckBox.Name = "satCheckBox";
			this.satCheckBox.Size = new System.Drawing.Size(42, 17);
			this.satCheckBox.TabIndex = 15;
			this.satCheckBox.Text = "Sat";
			this.satCheckBox.UseVisualStyleBackColor = true;
			this.satCheckBox.CheckedChanged += new System.EventHandler(this.satCheckBox_CheckedChanged);
			// 
			// friCheckBox
			// 
			this.friCheckBox.AutoSize = true;
			this.friCheckBox.Location = new System.Drawing.Point(397, 67);
			this.friCheckBox.Name = "friCheckBox";
			this.friCheckBox.Size = new System.Drawing.Size(37, 17);
			this.friCheckBox.TabIndex = 14;
			this.friCheckBox.Text = "Fri";
			this.friCheckBox.UseVisualStyleBackColor = true;
			this.friCheckBox.CheckedChanged += new System.EventHandler(this.friCheckBox_CheckedChanged);
			// 
			// thuCheckBox
			// 
			this.thuCheckBox.AutoSize = true;
			this.thuCheckBox.Location = new System.Drawing.Point(344, 67);
			this.thuCheckBox.Name = "thuCheckBox";
			this.thuCheckBox.Size = new System.Drawing.Size(45, 17);
			this.thuCheckBox.TabIndex = 13;
			this.thuCheckBox.Text = "Thu";
			this.thuCheckBox.UseVisualStyleBackColor = true;
			this.thuCheckBox.CheckedChanged += new System.EventHandler(this.thuCheckBox_CheckedChanged);
			// 
			// wedCheckBox
			// 
			this.wedCheckBox.AutoSize = true;
			this.wedCheckBox.Location = new System.Drawing.Point(291, 67);
			this.wedCheckBox.Name = "wedCheckBox";
			this.wedCheckBox.Size = new System.Drawing.Size(49, 17);
			this.wedCheckBox.TabIndex = 12;
			this.wedCheckBox.Text = "Wed";
			this.wedCheckBox.UseVisualStyleBackColor = true;
			this.wedCheckBox.CheckedChanged += new System.EventHandler(this.wedCheckBox_CheckedChanged);
			// 
			// tueCheckBox
			// 
			this.tueCheckBox.AutoSize = true;
			this.tueCheckBox.Location = new System.Drawing.Point(238, 67);
			this.tueCheckBox.Name = "tueCheckBox";
			this.tueCheckBox.Size = new System.Drawing.Size(45, 17);
			this.tueCheckBox.TabIndex = 11;
			this.tueCheckBox.Text = "Tue";
			this.tueCheckBox.UseVisualStyleBackColor = true;
			this.tueCheckBox.CheckedChanged += new System.EventHandler(this.tueCheckBox_CheckedChanged);
			// 
			// monCheckBox
			// 
			this.monCheckBox.AutoSize = true;
			this.monCheckBox.Location = new System.Drawing.Point(185, 67);
			this.monCheckBox.Name = "monCheckBox";
			this.monCheckBox.Size = new System.Drawing.Size(47, 17);
			this.monCheckBox.TabIndex = 10;
			this.monCheckBox.Text = "Mon";
			this.monCheckBox.UseVisualStyleBackColor = true;
			this.monCheckBox.CheckedChanged += new System.EventHandler(this.monCheckBox_CheckedChanged);
			// 
			// monitoringLabel
			// 
			this.monitoringLabel.AutoSize = true;
			this.monitoringLabel.Location = new System.Drawing.Point(6, 45);
			this.monitoringLabel.Name = "monitoringLabel";
			this.monitoringLabel.Size = new System.Drawing.Size(59, 13);
			this.monitoringLabel.TabIndex = 9;
			this.monitoringLabel.Text = "Monitoring:";
			// 
			// refreshLabel
			// 
			this.refreshLabel.AutoSize = true;
			this.refreshLabel.Location = new System.Drawing.Point(6, 71);
			this.refreshLabel.Name = "refreshLabel";
			this.refreshLabel.Size = new System.Drawing.Size(96, 13);
			this.refreshLabel.TabIndex = 8;
			this.refreshLabel.Text = "Scheduled refresh:";
			// 
			// monitoringCheckBox
			// 
			this.monitoringCheckBox.AutoSize = true;
			this.monitoringCheckBox.Location = new System.Drawing.Point(112, 44);
			this.monitoringCheckBox.Name = "monitoringCheckBox";
			this.monitoringCheckBox.Size = new System.Drawing.Size(64, 17);
			this.monitoringCheckBox.TabIndex = 7;
			this.monitoringCheckBox.Text = "enabled";
			this.monitoringCheckBox.UseVisualStyleBackColor = true;
			// 
			// refreshPicker
			// 
			this.refreshPicker.CustomFormat = "HH:mm";
			this.refreshPicker.Enabled = false;
			this.refreshPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.refreshPicker.Location = new System.Drawing.Point(111, 65);
			this.refreshPicker.Name = "refreshPicker";
			this.refreshPicker.ShowUpDown = true;
			this.refreshPicker.Size = new System.Drawing.Size(68, 20);
			this.refreshPicker.TabIndex = 4;
			// 
			// pathButton
			// 
			this.pathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pathButton.Location = new System.Drawing.Point(550, 17);
			this.pathButton.Name = "pathButton";
			this.pathButton.Size = new System.Drawing.Size(37, 23);
			this.pathButton.TabIndex = 2;
			this.pathButton.Text = "...";
			this.pathButton.UseVisualStyleBackColor = true;
			this.pathButton.Click += new System.EventHandler(this.pathButton_Click);
			// 
			// pathTextBox
			// 
			this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pathTextBox.Location = new System.Drawing.Point(112, 19);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.Size = new System.Drawing.Size(432, 20);
			this.pathTextBox.TabIndex = 1;
			// 
			// pathLabel
			// 
			this.pathLabel.AutoSize = true;
			this.pathLabel.Location = new System.Drawing.Point(6, 22);
			this.pathLabel.Name = "pathLabel";
			this.pathLabel.Size = new System.Drawing.Size(32, 13);
			this.pathLabel.TabIndex = 0;
			this.pathLabel.Text = "Path:";
			// 
			// CatalogueItemForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(599, 148);
			this.Controls.Add(this.paramsGroupBox);
			this.Controls.Add(this.actionsGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CatalogueItemForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Catalogue item";
			this.Load += new System.EventHandler(this.CatalogueItemForm_Load);
			this.actionsGroupBox.ResumeLayout(false);
			this.paramsGroupBox.ResumeLayout(false);
			this.paramsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox actionsGroupBox;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.GroupBox paramsGroupBox;
		private System.Windows.Forms.Button pathButton;
		private System.Windows.Forms.TextBox pathTextBox;
		private System.Windows.Forms.Label pathLabel;
		private System.Windows.Forms.DateTimePicker refreshPicker;
		private System.Windows.Forms.CheckBox monitoringCheckBox;
		private System.Windows.Forms.CheckBox sunCheckBox;
		private System.Windows.Forms.CheckBox satCheckBox;
		private System.Windows.Forms.CheckBox friCheckBox;
		private System.Windows.Forms.CheckBox thuCheckBox;
		private System.Windows.Forms.CheckBox wedCheckBox;
		private System.Windows.Forms.CheckBox tueCheckBox;
		private System.Windows.Forms.CheckBox monCheckBox;
		private System.Windows.Forms.Label monitoringLabel;
		private System.Windows.Forms.Label refreshLabel;
	}
}