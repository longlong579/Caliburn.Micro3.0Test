using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.AddIn2
{
	using Microsoft.CSharp;
	[MenuPart(BaseMenu=AddInWorkbenchName.AddIn2Part)]
	public class AddIn2SubPart : MenuPart
	{
		public AddIn2SubPart()
			: base(AddInWorkbenchName.AddIn2SubPart)
		{
			Icon = "Icons.16x16.AboutIcon";
		}
	}
}
