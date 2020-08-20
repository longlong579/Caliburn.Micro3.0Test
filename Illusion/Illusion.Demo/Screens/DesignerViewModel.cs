using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

using Illusion;

namespace Illusion.Demo.ViewModels
{
	using Caliburn.Micro;
	[DockScreen(Type = DockType.Document)]
    public class DesignerViewModel : AvalonDockScreen, IDockDocumentScreen
	{
		public DesignerViewModel()
			: base(WorkbenchName.DesignerScreen)
		{
		}
	}
}
