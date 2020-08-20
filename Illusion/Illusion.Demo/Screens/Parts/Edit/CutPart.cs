using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar = WorkbenchName.RedoPart)]
	[MenuPart(BaseMenu = WorkbenchName.EditPart, PreviousMenu = WorkbenchName.RedoPart)]
	public class CutPart : MenuToolPart
	{
		public CutPart()
			: base(WorkbenchName.CutPart)
		{
			Icon = "Icons.16x16.CutIcon";
			InputGestureText = "Ctrl+X";
		}
	}
}
