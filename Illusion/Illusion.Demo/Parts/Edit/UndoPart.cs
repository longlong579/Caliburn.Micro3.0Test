using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart)]
    [MenuPart(BaseMenu = WorkbenchName.EditPart)]
	public class UndoPart : MenuToolPart
	{
        public UndoPart()
			: base(WorkbenchName.UndoPart)
		{
			Icon = "Resources/Images/Undo.png";
			InputGestureText = "Ctrl+Z";
		}
	}
}
