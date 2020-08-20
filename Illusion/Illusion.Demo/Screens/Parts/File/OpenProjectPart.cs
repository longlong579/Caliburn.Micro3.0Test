using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.FilePart, PreviousMenu = WorkbenchName.CreateProjectPart)]
	public class OpenProjectPart : MenuToolPart
	{
		public OpenProjectPart()
			: base(WorkbenchName.OpenProjectPart)
		{
			Icon = "Icons.16x16.OpenFileIcon";
		}
	}
}
