using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Illusion;

namespace Illusion.AddIn1
{
    [DockScreen(Type = DockType.DockableContent, Side = DockSide.Bottom)]
    public class FindResultViewModel : AvalonDockScreen
	{
		public FindResultViewModel()
			: base(WorkbenchName.FindResultScreen)
		{
			Icon = "Icons.16x16.FindIcon";
		}
	}
}
