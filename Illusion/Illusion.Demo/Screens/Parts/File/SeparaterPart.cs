using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
	[MenuPart(BaseMenu = WorkbenchName.FilePart, PreviousMenu = WorkbenchName.OpenProjectPart)]
	public class FileSeparater1 : MenuPart
	{
		public FileSeparater1()
			: base()
		{
			IsSeparator = true;
		}
	}
}
