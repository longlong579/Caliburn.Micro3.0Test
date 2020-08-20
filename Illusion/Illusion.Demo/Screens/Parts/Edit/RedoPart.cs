using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar=WorkbenchName.UndoPart)]
	[MenuPart(BaseMenu = WorkbenchName.EditPart)]
	public class RedoPart : MenuToolPart
	{
        public RedoPart()
			: base(WorkbenchName.RedoPart)
		{
			Icon = "Resources/Images/Redo.png";
			InputGestureText = "Ctrl+Y";
		}
	}
}
