using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.ToolPart)]
	public class ThemePart : MenuPart
	{
		public ThemePart()
			: base(WorkbenchName.ThemePart)
		{

		}
	}
}
