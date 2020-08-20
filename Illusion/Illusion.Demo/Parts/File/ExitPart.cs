using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.FilePart, PreviousMenu=WorkbenchName.SavePart)]
	public class ExitPart : MenuToolPart
	{
		public ExitPart()
			: base(WorkbenchName.ExitPart)
		{
			Icon = "Icons.16x16.ExitIcon";
		}
	}
}
