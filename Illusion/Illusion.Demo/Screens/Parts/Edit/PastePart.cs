using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar = WorkbenchName.CopyPart)]
	[MenuPart(BaseMenu = WorkbenchName.EditPart, PreviousMenu = WorkbenchName.CopyPart)]
	public class PastePart : MenuToolPart
	{
		public PastePart()
			: base(WorkbenchName.PastePart)
		{
			Icon = "Icons.16x16.PasteIcon";
			InputGestureText = "Ctrl+V";
		}
	}
}
