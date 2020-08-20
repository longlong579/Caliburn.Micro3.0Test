using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, NextToolBar=WorkbenchName.UndoPart)]
	[MenuPart(BaseMenu = WorkbenchName.FilePart)]
	public class CreateProjectPart : MenuToolPart
	{
        public CreateProjectPart()
			: base(WorkbenchName.CreateProjectPart)
		{
			Icon = "Icons.16x16.NewProjectIcon";
		}
	}
}
