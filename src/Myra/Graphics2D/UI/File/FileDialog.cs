﻿using System;
using System.Collections.Generic;
using System.IO;
using static Myra.Graphics2D.UI.Grid;

#if !XENKO
using Microsoft.Xna.Framework;
#else
using Xenko.Core.Mathematics;
#endif

namespace Myra.Graphics2D.UI.File
{
	public partial class FileDialog : Dialog
	{
		private const int ImageTextSpacing = 4;

		private static readonly string[] Folders =
		{
			"Desktop", "Downloads"
		};

		private readonly List<string> _paths = new List<string>();
		private readonly List<string> _history = new List<string>();
		private int _historyPosition;
		private readonly FileDialogMode _mode;
		private bool _firstRender = true;

		public string Folder
		{
			get { return _textFieldPath.Text; }

			set
			{
				if (value == _textFieldPath.Text)
				{
					return;
				}

				SetFolder(value, true);
			}
		}

		/// <summary>
		/// File filter that is used as 2nd parameter for Directory.EnumerateFiles call
		/// </summary>
		public string Filter
		{
			get; set;
		}

		internal string FileName
		{
			get { return _textFieldFileName.Text; }

			set
			{
				_textFieldFileName.Text = value;
			}
		}

		public string FilePath
		{
			get
			{
				return Path.Combine(Folder, FileName);
			}

			set
			{
				Folder = Path.GetDirectoryName(value);
				FileName = Path.GetFileName(value);

				if (!string.IsNullOrEmpty(FileName))
				{
					foreach (var widget in _gridFiles.Widgets)
					{
						var asTextBlock = widget as TextBlock;

						if (asTextBlock == null)
						{
							continue;
						}

						if (asTextBlock.Text == FileName)
						{
							_gridFiles.SelectedRowIndex = asTextBlock.GridRow;
							break;
						}
					}
				}
			}
		}

		public bool AutoAddFilterExtension { get; set; }

		public FileDialog(FileDialogMode mode)
		{
			_mode = mode;

			BuildUI();

			switch (mode)
			{
				case FileDialogMode.OpenFile:
					Title = "Open File...";
					break;
				case FileDialogMode.SaveFile:
					Title = "Save File...";
					break;
				case FileDialogMode.ChooseFolder:
					Title = "Choose Folder...";
					break;
			}

			AutoAddFilterExtension = true;

			if (mode == FileDialogMode.ChooseFolder)
			{
				_textBlockFileName.Visible = false;
				_textFieldFileName.Visible = false;
			}

			_splitPane.SetSplitterPosition(0, 0.3f);

			_buttonBack.Background = null;
			_buttonForward.Background = null;
			_buttonParent.Background = null;

			_listBoxPlaces.Background = null;

			_buttonBack.Image = DefaultAssets.UISpritesheet["icon-arrow-left"];
			_buttonForward.Image = DefaultAssets.UISpritesheet["icon-arrow-right"];
			_buttonParent.Image = DefaultAssets.UISpritesheet["icon-folder-parent"];

			var homePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
							Environment.OSVersion.Platform == PlatformID.MacOSX)
				? Environment.GetEnvironmentVariable("HOME")
				: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

			var iconFolder = DefaultAssets.UISpritesheet["icon-folder"];

			var places = new List<string>
			{
				homePath
			};

			foreach (var f in Folders)
			{
				places.Add(Path.Combine(homePath, f));
			}

			foreach (var p in places)
			{
				if (!Directory.Exists(p))
				{
					continue;
				}

				_listBoxPlaces.Items.Add(new ListItem(Path.GetFileName(p), null, p)
				{
					Image = iconFolder,
					ImageTextSpacing = ImageTextSpacing
				});
			}

			if (_listBoxPlaces.Items.Count > 0)
			{
				SetFolder((string)_listBoxPlaces.Items[0].Tag, false);
			}

			_listBoxPlaces.Items.Add(new ListItem
			{
				IsSeparator = true
			});

			var drives = DriveInfo.GetDrives();

			var iconDrive = DefaultAssets.UISpritesheet["icon-drive"];
			foreach (var d in drives)
			{
				if (d.DriveType == DriveType.Ram)
				{
					continue;
				}

				try
				{
					var s = d.RootDirectory.Name;

					if (!string.IsNullOrEmpty(d.VolumeLabel) && d.VolumeLabel != d.RootDirectory.Name)
					{
						s += " (" + d.VolumeLabel + ")";
					}

					_listBoxPlaces.Items.Add(new ListItem(s, null, d.RootDirectory.Name)
					{
						Image = iconDrive,
						ImageTextSpacing = ImageTextSpacing
					});
				}
				catch (Exception)
				{
				}
			}

			_listBoxPlaces.SelectedIndexChanged += OnPlacesSelectedIndexChanged;

			_gridFiles.SelectionBackground = DefaultAssets.UISpritesheet["tree-selection"];
			_gridFiles.SelectionHoverBackground = DefaultAssets.UISpritesheet["button-over"];
			_gridFiles.SelectedIndexChanged += OnGridFilesSelectedIndexChanged;
			_gridFiles.MouseDoubleClick += OnGridFilesDoubleClick;

			_buttonParent.Click += OnButtonParent;

			_textFieldFileName.TextChanged += (s, a) => UpdateEnabled();

			_buttonBack.Click += OnButtonBack;
			_buttonForward.Click += OnButtonForward;

			UpdateEnabled();
		}

		private void UpdateEnabled()
		{
			var enabled = false;
			switch (_mode)
			{
				case FileDialogMode.OpenFile:
					enabled = !string.IsNullOrEmpty(FileName) && System.IO.File.Exists(FilePath);
					break;
				case FileDialogMode.SaveFile:
					enabled = !string.IsNullOrEmpty(FileName);
					break;
				case FileDialogMode.ChooseFolder:
					enabled = !string.IsNullOrEmpty(Folder);
					break;
			}

			ButtonOk.Enabled = enabled;
		}

		private void OnButtonParent(object sender, EventArgs args)
		{
			if (string.IsNullOrEmpty(Folder))
			{
				return;
			}

			var parentFolder = Path.GetDirectoryName(Folder);

			Folder = parentFolder;
		}

		private void OnButtonBack(object sender, EventArgs args)
		{
			if (_historyPosition <= 0)
			{
				return;
			}

			--_historyPosition;
			if (_historyPosition >= 0 && _historyPosition < _history.Count)
			{
				SetFolder(_history[_historyPosition], false);
			}
		}

		private void OnButtonForward(object sender, EventArgs args)
		{
			if (_historyPosition >= _history.Count - 1)
			{
				return;
			}

			++_historyPosition;
			if (_historyPosition >= 0 && _historyPosition < _history.Count)
			{
				SetFolder(_history[_historyPosition], false);
			}
		}

		private void SetFolder(string value, bool storeInHistory)
		{
			if (!Directory.Exists(value))
			{
				return;
			}

			_textFieldPath.Text = value;
			UpdateFolder();
			UpdateEnabled();

			if (!storeInHistory)
			{
				return;
			}

			while (_history.Count > 0 && _historyPosition < _history.Count - 1)
			{
				_history.RemoveAt(_history.Count - 1);
			}

			_history.Add(Folder);

			_historyPosition = _history.Count - 1;
		}

		private void OnGridFilesDoubleClick(object sender, EventArgs args)
		{
			if (_gridFiles.SelectedRowIndex == null)
			{
				return;
			}

			var path = _paths[_gridFiles.SelectedRowIndex.Value];

			if (!Directory.Exists(path))
			{
				return;
			}

			_listBoxPlaces.SelectedIndex = null;
			Folder = path;
		}

		private void OnGridFilesSelectedIndexChanged(object sender, EventArgs args)
		{
			if (_gridFiles.SelectedRowIndex == null)
			{
				return;
			}

			_listBoxPlaces.SelectedIndex = null;

			var path = _paths[_gridFiles.SelectedRowIndex.Value];

			if (!System.IO.File.Exists(path))
			{
				return;
			}

			var fi = new FileInfo(path);

			var choose = (fi.Attributes.HasFlag(FileAttributes.Directory) && _mode == FileDialogMode.ChooseFolder) ||
						 (!fi.Attributes.HasFlag(FileAttributes.Directory) && _mode != FileDialogMode.ChooseFolder);

			if (choose)
			{
				FileName = Path.GetFileName(path);
			}
		}

		private void OnPlacesSelectedIndexChanged(object sender, EventArgs args)
		{
			if (_listBoxPlaces.SelectedIndex == null)
			{
				return;
			}

			var path = (string)_listBoxPlaces.Items[_listBoxPlaces.SelectedIndex.Value].Tag;
			Folder = path;
		}

		private void UpdateFolder()
		{
			_gridFiles.RowsProportions.Clear();
			_gridFiles.Widgets.Clear();
			_paths.Clear();

			_scrollPane.ScrollPosition = Point.Zero;

			var path = _textFieldPath.Text;
			var folders = Directory.EnumerateDirectories(path);

			var iconFolder = DefaultAssets.UISpritesheet["icon-folder"];

			var gridY = 0;
			foreach (var f in folders)
			{
				var fileInfo = new FileInfo(f);
				if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
				{
					continue;
				}

				var prop = new Proportion();

				_gridFiles.RowsProportions.Add(prop);

				var image = new Image
				{
					Renderable = iconFolder,
					GridRow = gridY,
					HorizontalAlignment = HorizontalAlignment.Center,
					VerticalAlignment = VerticalAlignment.Center
				};

				_gridFiles.Widgets.Add(image);

				var name = new TextBlock
				{
					Text = Path.GetFileName(f),
					GridColumn = 1,
					GridRow = gridY
				};

				_gridFiles.Widgets.Add(name);

				_paths.Add(f);

				++gridY;
			}

			if (_mode == FileDialogMode.ChooseFolder)
			{
				return;
			}

			IEnumerable<string> files;

			if (string.IsNullOrEmpty(Filter))
			{
				files = Directory.EnumerateFiles(path);
			}
			else
			{
				var parts = Filter.Split('|');
				var result = new List<string>();

				foreach (var part in parts)
				{
					result.AddRange(Directory.EnumerateFiles(path, part));
				}

				files = result;
			}

			foreach (var f in files)
			{
				var fileInfo = new FileInfo(f);
				if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
				{
					continue;
				}

				var prop = new Proportion();

				_gridFiles.RowsProportions.Add(prop);

				var name = new TextBlock
				{
					Text = Path.GetFileName(f),
					GridColumn = 1,
					GridRow = gridY
				};

				_gridFiles.Widgets.Add(name);

				_paths.Add(f);

				++gridY;
			}
		}

		public override void InternalRender(RenderContext context)
		{
			base.InternalRender(context);

			if (_firstRender)
			{
				Desktop.FocusedWidget = _gridFiles;
				_firstRender = false;
			}
		}

		protected override bool CanCloseByOk()
		{
			if (_mode != FileDialogMode.SaveFile)
			{
				return true;
			}

			var fileName = FileName;

			if (AutoAddFilterExtension && !string.IsNullOrEmpty(Filter))
			{
				var idx = Filter.LastIndexOf('.');
				if (idx != -1)
				{
					var ext = Filter.Substring(idx);

					if (!fileName.EndsWith(ext))
					{
						fileName += ext;
					}
				}
			}

			if (System.IO.File.Exists(Path.Combine(Folder, fileName)))
			{
				var dlg = CreateMessageBox("Confirm Replace",
					string.Format("File named '{0}' already exists. Do you want to replace it?", fileName));

				dlg.Closed += (s, a) =>
				{
					if (!dlg.Result)
					{
						return;
					}

					FileName = fileName;

					Result = true;
					Close();
				};

				dlg.ShowModal(Desktop);
			}
			else
			{
				FileName = fileName;

				Result = true;
				Close();
			}

			return false;
		}
	}
}