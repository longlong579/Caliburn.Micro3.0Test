using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.EditPart, PreviousMenu = WorkbenchName.RedoPart)]
	public class EditSeparater1 : MenuPart
	{
		public EditSeparater1()
			: base()
		{
			IsSeparator = true;
		}
	}
}
