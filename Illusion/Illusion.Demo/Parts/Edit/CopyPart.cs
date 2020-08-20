using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;
using Caliburn.Micro;
namespace Illusion.Demo.Parts
{
	[ToolBarPart(BaseToolBar = WorkbenchName.MainToolBarPart, PreviousToolBar = WorkbenchName.CutPart)]
	[MenuPart(BaseMenu = WorkbenchName.EditPart, PreviousMenu = WorkbenchName.CutPart)]
	public class CopyPart : MenuToolPart
	{
		public CopyPart()
			: base(WorkbenchName.CopyPart)
		{
			Icon = "Icons.16x16.CopyIcon";
			InputGestureText = "Ctrl+C";
		}

		public override void Execute()
		{
			IStatusBarService statusBar = IoC.Get<IStatusBarService>();
			statusBar.ShowMessage("Copy");
		}
	}
}
