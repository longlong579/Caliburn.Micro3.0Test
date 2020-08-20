using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;
using System.ComponentModel.Composition;
using System.IO;

using AvalonDock;
using Illusion;
using System.Windows.Threading;

namespace Illusion
{
	using Caliburn.Micro;
	public interface IDockWindowManager : IWindowManager
	{
		void Initialize();

		void Show(IDockScreen screen, DockType? dockType = null, bool activate = true, DockSide dockSide = DockSide.Left);

		void Hide(IDockScreen screen);

		void Close(IDockScreen screen);

		void Save(string path);

		void Restore(string path);
	}

	public class AvalonDockWindowManager : WindowManager, IDockWindowManager
	{
		#region Field

		bool isLoaded = false;
		DockingManager _dockingManager;

		#endregion

		#region Constructor

		public AvalonDockWindowManager()
		{ }

		#endregion

		#region Method

		private DockableContent GetDockable(IDockScreen rootModel)
		{

			//see if the dockable exists
			var existingView = _dockingManager.DockableContents.FirstOrDefault(dc => dc.DataContext == rootModel);
			if (existingView != null)
				return existingView;

			var view = ViewLocator.LocateForModel(rootModel, null, DockType.DockableContent);
			ViewModelBinder.Bind(rootModel, view, DockType.DockableContent);

			return view as DockableContent;
		}

		private DocumentContent GetDocument(IDockScreen rootModel)
		{
			//see if the dockable exists
			var existingView = _dockingManager.Documents.FirstOrDefault(dc => dc.DataContext == rootModel);
			if (existingView != null)
				return existingView;

			var view = ViewLocator.LocateForModel(rootModel, null, DockType.Document);
			ViewModelBinder.Bind(rootModel, view, DockType.Document);

			return view as DocumentContent;
		}

		private DockingManager GetDockingManager(Window window = null)
		{
			var dockingManager = _dockingManager;

			if (dockingManager == null && Application.Current.MainWindow != null)
			{
				dockingManager = Application.Current.MainWindow.FindChild<DockingManager>();
				_dockingManager = dockingManager;
			}

			if (dockingManager == null)
				throw new InvalidOperationException("Unable to retrieve a DockingManager ");

			return dockingManager;
		}

		private static AnchorStyle GetAnchorStyle(DockSide side)
		{
			switch (side)
			{
				case DockSide.None: return AnchorStyle.None;
				case DockSide.Left: return AnchorStyle.Left;
				case DockSide.Top: return AnchorStyle.Top;
				case DockSide.Right: return AnchorStyle.Right;
				case DockSide.Bottom: return AnchorStyle.Bottom;
				default: return AnchorStyle.Left;
			}
		}

		#endregion

		#region IDockWindowManager Members

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize()
		{
			_dockingManager = GetDockingManager();
			isLoaded = true;
		}

		public bool IsLoaded
		{
			get
			{
				return isLoaded;
			}
		}

		public void Show(IDockScreen screen, DockType? dockType, bool activate, DockSide dockSide)
		{
			DockType type = dockType.HasValue ? dockType.Value : screen.Type;
			switch (type)
			{
				case DockType.Document:
					ShowDocumentWindow(screen, activate);
					break;
				case DockType.DockableContent:
					ShowDockedWindow(screen, activate, dockSide);
					break;
				case DockType.Floating:
					ShowFloatingWindow(screen, activate);
					break;
				default:
					break;
			}
		}

		public void Hide(IDockScreen screen)
		{
			var content = this[screen.Name];
			if (content != null)
			{
				content.Hide();
			}
		}

		public void Close(IDockScreen screen)
		{
			var content = this[screen.Name];
			if (content != null)
			{
				content.Close();
			}
		}

		private void ShowDockedWindow(IDockScreen viewModel,
									 bool activate = true,
									 DockSide dockSide = DockSide.Left)
		{
			var dockableContent = GetDockable(viewModel);
			dockableContent.Show(_dockingManager, GetAnchorStyle(dockSide));
			if (activate)
				dockableContent.Activate();
			else
				dockableContent.ToggleAutoHide();
		}

		private void ShowFloatingWindow(IDockScreen viewModel, bool activate = true)
		{
			var dockableContent = GetDockable(viewModel);
			dockableContent.ShowAsFloatingWindow(_dockingManager, true);
			if (activate)
				dockableContent.Activate();
		}

		private void ShowDocumentWindow(IDockScreen viewModel, bool activate = true)
		{
			var document = GetDocument(viewModel);
			document.Show(_dockingManager);
			if (activate)
				document.Activate();
		}

		string layoutPath = string.Empty;

		public void Save(string path)
		{
			//_dockingManager.SaveLayout(path);
		}

		public void Restore(string path)
		{
			if (!File.Exists(path))
			{
				return;
			}

			layoutPath = path;
			if (_dockingManager != null)
			{
				_dockingManager.Loaded += new RoutedEventHandler(manager_Loaded);
			}

		}

		void manager_Loaded(object sender, RoutedEventArgs e)
		{
			DockingManager manager = sender as DockingManager;
			manager.Loaded -= manager_Loaded;
			manager.RestoreLayout(layoutPath);
		}

		internal ManagedContent this[string name]
		{
			get
			{
				if (!IsLoaded)
				{
					throw new Exception("The DockWindowManager has not been initialize, please init it first.");
				}

				var dockContent = _dockingManager.DockableContents.FirstOrDefault(item => string.Equals(item.Name, name));
				if (dockContent == null)
				{
					return _dockingManager.Documents.FirstOrDefault(item => string.Equals(item.Name, name));
				}
				return dockContent;
			}
		}

		#endregion
	}

	public class DockWindowLoadedMessage : IMessage
	{

	}
}
