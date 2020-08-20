using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar = WorkbenchName.EnglishPart)]
	[MenuPart(BaseMenu = WorkbenchName.ToolPart)]
	public class LanguagePart : MenuToolPart
	{
		public LanguagePart()
			: base(WorkbenchName.LanguagePart)
		{

		}
	}
}
