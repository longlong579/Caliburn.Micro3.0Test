using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

using Illusion;

namespace Illusion.Demo.ViewModels
{
	using Caliburn.Micro;
	[Export(typeof(IPartManager<IToolBarPart>))]
	public class ToolBarViewModel : PartManager<IToolBarPart, IToolBarPartMetaData>
	{
	}
}
