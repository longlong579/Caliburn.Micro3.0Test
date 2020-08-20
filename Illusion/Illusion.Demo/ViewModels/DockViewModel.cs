using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using Illusion;

namespace Illusion.Demo.ViewModels
{
    using Caliburn.Micro;
    [Export(typeof(IDockScreenManager))]
    public class DockViewModel : DockScreenManager
    {
        public DockViewModel()
            : base()
        {

        }

        /// <summary>
        ///   Called when an attached view's Loaded event fires.
        /// </summary>
        /// <param name = "view">The displayed view.</param>
        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            var dockWindowManager = IoC.Get<IDockWindowManager>();
            dockWindowManager.Initialize();

            foreach (var screen in Screens)
            {
                if (screen is IDockDocumentScreen)
                {
                    dockWindowManager.Show(screen, DockType.Document);
                }
                else
                {
                    dockWindowManager.Show(screen, DockType.DockableContent, true, screen.Side);
                }
            }

            dockWindowManager.Restore(@"Configuration\layout.xml");

            IoC.Get<IEventAggregator>().Publish(new DockWindowLoadedMessage());
        }
    }
}
