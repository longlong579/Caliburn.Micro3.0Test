using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar=WorkbenchName.RedoPart)]
	[MenuPart(BaseMenu = WorkbenchName.LanguagePart)]
	public class EnglishPart : MenuToolPart
	{
		public EnglishPart()
			: base(WorkbenchName.EnglishPart)
		{
			IsCheckable = true;
		}

        public override void Execute()
        {
			IoC.Get<IResourceService>().ChangeLanguage("en-us");
			IoC.Get<IConfigurationService>()[this].SaveProperty<MenuToolPart>(this, i => i.IsChecked);
		}

		public override void OnAttached()
		{
			IoC.Get<IConfigurationService>()[this].LoadProperty<MenuToolPart>(this, i => i.IsChecked);
		}
	}
}
