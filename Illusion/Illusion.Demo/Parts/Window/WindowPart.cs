using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	using Caliburn.Micro;
	[MenuPart(PreviousMenu = WorkbenchName.ToolPart)]
	public class WindowPart : MenuPart, IHandle<DockWindowLoadedMessage>
	{
		#region Field

		CollectionSyncer<IDockScreen, IMenuPart> syncer;
		CollectionMerger<IMenuPart> merger;
		
		#endregion
 
		#region Constructor
		
		public WindowPart()
			: base(WorkbenchName.WindowPart)
		{
			IoC.Get<IEventAggregator>().Subscribe(this);
		}
		
		#endregion

		#region IHandle<DockWindowLoadedMessage> Members

		public void Handle(DockWindowLoadedMessage message)
		{
			var manager = IoC.Get<IDockScreenManager>();
			syncer = new CollectionSyncer<IDockScreen, IMenuPart>(manager.Documents, (item) =>
			{
				return new MenuPart(item.Name, (i) =>
				{
					MenuPart menu = i as MenuPart;
					menu.IsChecked = !menu.IsChecked;
					if (menu.IsChecked)
					{
						IoC.Get<IDockScreenManager>()[menu.Name].Show();
					}
					else
					{
						IoC.Get<IDockScreenManager>()[menu.Name].Close();
					}
				}) { IsCheckable = true };
			});
			merger = new CollectionMerger<IMenuPart>((this as IObservableParent<IMenuPart>).Items, syncer.SyncCollection);
		}

		#endregion
	}
}
