using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows;
using Illusion;

namespace Illusion.Demo.ViewModels
{
	using Caliburn.Micro;
	[Export(typeof(IPartManager<IMenuPart>))]
	public class MenuViewModel : PartManager<IMenuPart, IMenuPartMetaData>
	{
	}
}
