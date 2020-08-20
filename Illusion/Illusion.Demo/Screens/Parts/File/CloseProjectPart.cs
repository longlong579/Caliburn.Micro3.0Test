using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.FilePart, PreviousMenu = WorkbenchName.OpenProjectPart)]
	public class CloseProjectPart : MenuToolPart
	{
        public CloseProjectPart()
			: base(WorkbenchName.CloseProjectPart)
		{
			Icon = "Icons.16x16.CloseFileIcon";
		}
	}
}
