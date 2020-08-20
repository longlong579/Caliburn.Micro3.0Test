using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.AddIn2
{
	using Microsoft.CSharp;
	[MenuPart(BaseMenu = AddInWorkbenchName.AddInPart)]
	public class AddIn2Part : MenuPart
	{
		public AddIn2Part()
			: base(AddInWorkbenchName.AddIn2Part)
		{

		}
	}
}
