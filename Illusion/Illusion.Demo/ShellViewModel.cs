namespace Illusion.Demo
{
	using System;
	using System.IO;
	using System.Linq;
	using System.ComponentModel.Composition;
	using Illusion.Demo.ViewModels;
	using System.Windows;
	using Illusion;
	using Caliburn.Micro;
	[Export(typeof(IShell))]
	public class ShellViewModel : Screen, IShell, IConfigurationTarget, ILocalizableDisplay
    {
		public ShellViewModel()
		{
			this.AutoUpdate(true);
		}

        //导入对应的ViewMode ,View会自动根据名称绑定
        //[Import]
        //public IDockScreenManager DockRegion { get; set; }

        //[Import]
        //public IPartManager<IMenuPart> MenuRegion { get; set; }

        [Import]
        public IPartManager<IToolBarPart> ToolBarRegion { get; set; }

        [Import]
        public IStatusBarService StatusBarRegion { get; set; }

        #region IGuardClose Members

        //Called when deactivating
        protected override void OnDeactivate(bool close)
		{
			base.OnDeactivate(close);

			if (close)
			{
				//Save Layout
				Window window = GetView() as Window;

				IConfigurationService service = IoC.Get<IConfigurationService>();
				service[this].SetProperty("Width", window.ActualWidth).SetProperty("Height", window.ActualHeight).SetProperty("WindowState", window.WindowState);

				IoC.Get<IDockWindowManager>().Save(@"Configuration\layout.xml");
			}
		}

		protected override void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);

			//Restore layout
			Window window = GetView() as Window;
			IConfigurationService service = IoC.Get<IConfigurationService>();
			service[this].LoadProperty<Window>(window, w => w.Width, w => w.Height, w => w.WindowState);
		}

		#endregion

		#region IConfigurationTarget Members

		private PropertyObserver _monitor = new PropertyObserver();
		public PropertyObserver Monitor
		{
			get { return _monitor; }
		}

        #endregion

        #region IHaveDisplayName Members

        public string Name
        {
            get { return WorkbenchName.Workbench; }
        }

        #endregion

        #region IHandle<LanguageChangedMessage> Members

        public void Handle(LanguageChangedMessage message)
		{
			this.UpdateDisplayName();
		}

		#endregion
	}
}
