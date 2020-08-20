using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(PreviousMenu = WorkbenchName.EditPart)]
	public class ToolPart : MenuPart
	{
		public ToolPart()
			: base(WorkbenchName.ToolPart)
		{

		}
	}
}
