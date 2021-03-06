﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar = WorkbenchName.EnglishPart)]
	[MenuPart(BaseMenu = WorkbenchName.LanguagePart, PreviousMenu = WorkbenchName.EnglishPart)]
	public class ChinesePart : MenuToolPart
	{
		public ChinesePart()
			: base(WorkbenchName.ChinesePart)
		{
			IsCheckable = true;
		}

		public override void Execute()
		{
			IoC.Get<IResourceService>().ChangeLanguage("zh-cn");
			IoC.Get<IConfigurationService>()[this].SaveProperty<MenuToolPart>(this, i => i.IsChecked);
		}

		public override void OnAttached()
		{
			IoC.Get<IConfigurationService>()[this].LoadProperty<MenuToolPart>(this, i => i.IsChecked);
		}
	}
}
