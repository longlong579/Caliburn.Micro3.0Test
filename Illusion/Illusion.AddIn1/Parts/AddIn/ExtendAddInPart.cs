using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.AddIn1
{
	using Microsoft.CSharp;
	[MenuPart(BaseMenu=AddInWorkbenchName.AddInPart)]
	public class AddIn1Part : MenuPart
	{
		public AddIn1Part()
			: base(AddInWorkbenchName.AddIn1Part)
		{
			Icon = "Icons.16x16.AboutIcon";
		}
	}
}
