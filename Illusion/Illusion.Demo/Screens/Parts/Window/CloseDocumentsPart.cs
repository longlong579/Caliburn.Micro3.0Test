using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.WindowPart)]
	public class CloseDocumentsPart : MenuPart
	{
		public CloseDocumentsPart()
			: base(WorkbenchName.CloseDocumentsPart)
		{
			Icon = "Icons.16x16.CopyIcon";
		}

		public override void Execute()
		{
			var manager = IoC.Get<IDockScreenManager>();
			manager.Documents.Apply(item => item.Close());
		}
	}
}
