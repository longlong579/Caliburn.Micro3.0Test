using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.HelpPart)]
	public class AboutPart : MenuPart
	{
		public AboutPart()
			: base(WorkbenchName.AboutPart)
		{
			Icon = "Icons.16x16.AboutIcon";
		}
	}
}
