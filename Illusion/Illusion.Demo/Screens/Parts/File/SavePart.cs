using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, NextToolBar = WorkbenchName.CloseProjectPart)]
	[MenuPart(BaseMenu = WorkbenchName.FilePart, PreviousMenu = WorkbenchName.CloseProjectPart)]
	public class SavePart : MenuToolPart
	{
		public SavePart()
			: base(WorkbenchName.SavePart)
		{
			Icon = "Resources/Images/SaveAll.png";
			InputGestureText = "Ctrl+S";
		}
	}
}
