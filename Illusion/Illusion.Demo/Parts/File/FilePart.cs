using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.Parts
{
    [MenuPart]
	public class FilePart : MenuPart
	{
        public FilePart()
			: base(WorkbenchName.FilePart)
		{

		}
	}
}
