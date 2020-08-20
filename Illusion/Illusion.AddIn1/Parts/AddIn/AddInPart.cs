using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.AddIn1
{
	using Microsoft.CSharp;
	[MenuPart(NextMenu = WorkbenchName.HelpPart, PreviousMenu=WorkbenchName.ToolPart)]
	public class AddInPart : MenuPart
	{
		public AddInPart()
			: base(AddInWorkbenchName.AddInPart)
		{
			
		}
	}
}
