using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;
using Illusion.Demo.Views;

namespace Illusion.Demo
{
	[MenuPart(BaseMenu = WorkbenchName.ToolPart)]
	public class AddInManagerPart : MenuPart
	{
		public AddInManagerPart()
			: base(WorkbenchName.AddInManagerPart)
		{
		}

		public override void Execute()
		{

		}
	}
}
