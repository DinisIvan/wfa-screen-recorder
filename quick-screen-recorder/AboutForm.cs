﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace quick_screen_recorder
{
	partial class AboutForm : Form
	{
		private bool darkMode;

		public AboutForm(bool darkMode)
		{
			if (darkMode)
			{
				this.HandleCreated += new EventHandler(ThemeManager.formHandleCreated);
			}

			this.darkMode = darkMode;

			InitializeComponent();

			string fullVer = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			int lastDotIndex = fullVer.LastIndexOf('.');
			versionLabel.Text = String.Format("Version: {0}", fullVer.Substring(0, lastDotIndex));

			if (IntPtr.Size == 4)
			{
				versionLabel.Text += " (x32)";
			}
			else if (IntPtr.Size == 8)
			{
				versionLabel.Text += " (x64)";
			}

			if (darkMode)
			{
				this.BackColor = ThemeManager.DarkBackColor;
				this.ForeColor = Color.White;

				infoGroup.Paint += ThemeManager.PaintDarkGroupBox;
				pagesGroup.Paint += ThemeManager.PaintDarkGroupBox;

				updatesLink.LinkColor = ThemeManager.AccentColor;
				developerLink.LinkColor = ThemeManager.AccentColor;
				projectLink.LinkColor = ThemeManager.AccentColor;
				licenseLink.LinkColor = ThemeManager.AccentColor;
				issuesLink.LinkColor = ThemeManager.AccentColor;
			}
		}

		private void developerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/ModuleArt/");
		}

		private void projectLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/ModuleArt/quick-picture-viewer/");
		}

		private void issuesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/ModuleArt/quick-picture-viewer/issues/");
		}

		private void updatesLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MainForm parent = (MainForm)this.Owner;
			parent.checkForUpdates(true);
			this.Close();
		}

		private void AboutForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				this.Close();
			}
		}

		private void licenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://github.com/ModuleArt/quick-picture-viewer/blob/master/LICENSE.md/");
		}

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

		[DllImport("User32.dll")]
		private static extern IntPtr GetWindowDC(IntPtr hWnd);
	}
}
