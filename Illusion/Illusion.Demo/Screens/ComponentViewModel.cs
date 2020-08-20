using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Illusion;

namespace Illusion.Demo.ViewModels
{
    [DockScreen(Type = DockType.DockableContent, Side = DockSide.Left)]
    public class ComponentViewModel : AvalonDockScreen
    {
        public ComponentViewModel()
            : base(WorkbenchName.ComponentScreen)
        {
            Icon = "Icons.16x16.ToolBarIcon";
        }
    }
}
