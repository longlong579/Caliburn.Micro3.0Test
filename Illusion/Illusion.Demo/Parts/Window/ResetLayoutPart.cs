using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.WindowPart, PreviousMenu = WorkbenchName.CloseDocumentsPart)]
	public class ResetLayoutPart : MenuPart
	{
		public ResetLayoutPart()
			: base(WorkbenchName.ResetLayoutPart)
		{
			Icon = "Icons.16x16.CopyIcon";
		}

		public override void Execute()
		{
			base.Execute();
		}
	}
}
