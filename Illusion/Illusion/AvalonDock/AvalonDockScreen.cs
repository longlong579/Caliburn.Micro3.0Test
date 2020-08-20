using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

using Illusion;
using AvalonDock;
using System.ComponentModel;
using System.Windows;

namespace Illusion
{
	using Caliburn.Micro;
	public class AvalonDockScreen : DockScreenBase, IWeakEventListener
	{
		PropertyObserver<AvalonDockScreen> observer;

		public AvalonDockScreen(string name)
			: base(name)
		{
			CacheViews = false;

			observer = new PropertyObserver<AvalonDockScreen>(this);
			observer.RegisterHandler(n => n.Type, screen =>
				{
					var manager = IoC.Get<IDockScreenManager>();
					if (screen.Type == DockType.Document)
					{
						if (!manager.Documents.Contains(this))
						{
							manager.Documents.Add(this);
						}
					}
					else if (manager.Documents.Contains(this))
					{
						manager.Documents.Remove(this);
					}
				});
		}

		public override object GetView(object context = null)
		{
			if (context == null)
			{
				return base.GetView(context);
			}
			else
			{
				//1. Create corresponding ManagedContent. 
				DockType type = (DockType)context;
				ManagedContent mc;
				if (type == DockType.Document)
				{
					mc = new DocumentContent();
					Type = DockType.Document;
				}
				else
				{
					mc = new DockableContent();
					(mc as DockableContent).HideOnClose = true;

					StateChangedEventManager.AddListener(mc as DockableContent, this);
				}
				mc.Content = base.GetView(null);
				mc.Name = Name;
				mc.IsCloseable = true;

				if (!ConventionManager.HasBinding(mc, ManagedContent.TitleProperty))
					mc.SetBinding(ManagedContent.TitleProperty, "DisplayName");

				if (!ConventionManager.HasBinding(mc, ManagedContent.IconProperty))
				{
					Binding bind = new Binding("Icon") { Converter = ImgaeSourceResourceConverter.Default };
					mc.SetBinding(ManagedContent.IconProperty, bind);
				}


				//2. Create Conductor to manage life cycle
				new ManagedContentConductor(this, mc);

				return mc;
			}
		}

		public override void Show()
		{
			IoC.Get<IDockWindowManager>().Show(this);
		}

		public override void Close()
		{
			IoC.Get<IDockWindowManager>().Close(this);
		}

		#region Private DockableContentConductor Class

		/// <summary>
		///   The managed content conductor, used to allow for interaction between view and view model.
		/// </summary>
		private class ManagedContentConductor
		{
			#region Fields
			/// <summary>
			///   The view.
			/// </summary>
			private readonly ManagedContent _view;

			/// <summary>
			///   The view model.
			/// </summary>
			private readonly object _viewModel;

			/// <summary>
			///   The flag used to identify the view as closing.
			/// </summary>
			private bool _isClosing;

			/// <summary>
			///   The flag used to determine if the view requested deactivation.
			/// </summary>
			private bool _isDeactivatingFromView;

			/// <summary>
			///   The flag used to determine if the view model requested deactivation.
			/// </summary>
			private bool _isDeactivatingFromViewModel;
			#endregion

			/// <summary>
			///   Initializes a new instance of the <see cref = "ManagedContentConductor" /> class.
			/// </summary>
			/// <param name = "viewModel">The view model.</param>
			/// <param name = "view">The view.</param>
			public ManagedContentConductor(object viewModel, ManagedContent view)
			{
				_viewModel = viewModel;
				_view = view;

				var activatable = viewModel as IActivate;
				if (activatable != null)
					activatable.Activate();

				var deactivatable = viewModel as IDeactivate;
				if (deactivatable != null)
				{
					view.Closed += OnClosed;
					deactivatable.Deactivated += OnDeactivated;
				}

				var guard = viewModel as IGuardClose;
				if (guard != null)
					view.Closing += OnClosing;
			}

			/// <summary>
			///   Called when the view has been closed.
			/// </summary>
			/// <param name = "sender">The sender.</param>
			/// <param name = "e">The <see cref = "System.EventArgs" /> instance containing the event data.</param>
			private void OnClosed(object sender, EventArgs e)
			{
				_view.Closed -= OnClosed;
				_view.Closing -= OnClosing;

				if (_isDeactivatingFromViewModel)
					return;

				var deactivatable = (IDeactivate)_viewModel;

				_isDeactivatingFromView = true;
				deactivatable.Deactivate(true);
				_isDeactivatingFromView = false;
			}

			/// <summary>
			///   Called when the view has been deactivated.
			/// </summary>
			/// <param name = "sender">The sender.</param>
			/// <param name = "e">The <see cref = "Caliburn.Micro.DeactivationEventArgs" /> instance containing the event data.</param>
			private void OnDeactivated(object sender, DeactivationEventArgs e)
			{
				((IDeactivate)_viewModel).Deactivated -= OnDeactivated;

				if (!e.WasClosed || _isDeactivatingFromView)
					return;

				_isDeactivatingFromViewModel = true;
				_isClosing = true;
				_view.Close();
				_isClosing = false;
				_isDeactivatingFromViewModel = false;
			}

			/// <summary>
			///   Called when the view is about to be closed.
			/// </summary>
			/// <param name = "sender">The sender.</param>
			/// <param name = "e">The <see cref = "System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
			private void OnClosing(object sender, CancelEventArgs e)
			{
				var guard = (IGuardClose)_viewModel;

				if (_isClosing)
				{
					_isClosing = false;
					return;
				}

				bool runningAsync = false, shouldEnd = false;

				bool async = runningAsync;
				guard.CanClose(canClose =>
				{
					if (async && canClose)
					{
						_isClosing = true;
						_view.Close();
					}
					else
						e.Cancel = !canClose;

					shouldEnd = true;
				});

				if (shouldEnd)
					return;

				runningAsync = e.Cancel = true;
			}
		}

		#endregion

		#region IWeakEventListener Members

		public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
		{
			if (managerType == typeof(StateChangedEventManager))
			{
				DockableContent conent = sender as DockableContent;
				if (conent != null)
				{
					DockType type = Type;
					switch (conent.State)
					{
						case DockableContentState.FloatingWindow:
							type = DockType.Floating;
							break;
						case DockableContentState.Document:
							type = DockType.Document;
							break;
						default:
							type = DockType.DockableContent;
							break;
					}
					Type = type;
				}
				return true;
			}
			return false;
		}

		#endregion
	}

	public class StateChangedEventManager : WeakEventManager
	{
		public static void AddListener(DockableContent source, IWeakEventListener listener)
		{
			StateChangedEventManager.CurrentManager.ProtectedAddListener(source, listener);
		}

		public static void RemoveListener(DockableContent source, IWeakEventListener listener)
		{
			StateChangedEventManager.CurrentManager.ProtectedRemoveListener(source, listener);
		}

		private static StateChangedEventManager CurrentManager
		{
			get
			{
				Type managerType = typeof(StateChangedEventManager);
				StateChangedEventManager manager = (StateChangedEventManager)WeakEventManager.GetCurrentManager(managerType);
				if (manager == null)
				{
					manager = new StateChangedEventManager();
					WeakEventManager.SetCurrentManager(managerType, manager);
				}

				return manager;
			}
		}

		protected override void StartListening(object source)
		{
			DockableContent content = source as DockableContent;
			content.StateChanged += OnStateChanged;
		}

		void OnStateChanged(object sender, RoutedEventArgs e)
		{
			base.DeliverEvent(sender, e);
		}

		protected override void StopListening(object source)
		{
			DockableContent content = source as DockableContent;
			content.StateChanged -= OnStateChanged;
		}
	}
}
