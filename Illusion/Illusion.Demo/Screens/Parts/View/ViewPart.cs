using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(PreviousMenu = WorkbenchName.EditPart, NextMenu = WorkbenchName.ToolPart)]
	public class ViewPart : MenuPart
	{
		CollectionSyncer<IDockScreen, IMenuPart> syncer;
		CollectionMerger<IMenuPart> merger;

		public ViewPart()
			: base(WorkbenchName.ViewPart)
		{

		}

		public override void OnAttached()
		{
			syncer = new CollectionSyncer<IDockScreen, IMenuPart>(IoC.Get<IDockScreenManager>().Screens, (item) =>
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
				}) { DisplayName = item.DisplayName, IsCheckable = true, IsChecked = true };
			});

			merger = new CollectionMerger<IMenuPart>((this as IObservableParent<IMenuPart>).Items, syncer.SyncCollection);
		}
	}
}
