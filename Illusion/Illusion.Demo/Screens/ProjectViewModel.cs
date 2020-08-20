using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using Illusion;

namespace Illusion.Demo.ViewModels
{
    [DockScreen(Type = DockType.DockableContent, Side = DockSide.Right)]
    public class ProjectViewModel : AvalonDockScreen
	{
		public ProjectViewModel()
			: base(WorkbenchName.ProjectScreen)
		{
			Icon = "Icons.16x16.SolutionIcon";
		}
	}
}
