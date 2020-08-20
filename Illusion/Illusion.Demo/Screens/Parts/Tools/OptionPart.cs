using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel.Composition;

using Illusion;
using Illusion.Demo.Views;

namespace Illusion.Demo
{
	[MenuPart(BaseMenu = WorkbenchName.ToolPart)]
	public class OptionPart : MenuPart
	{
		public OptionPart()
			: base(WorkbenchName.OptionPart)
		{

		}

		public override void Execute()
		{
			OptionsDialog dialog = new OptionsDialog();
			ViewModelBinder.Bind(IoC.Get<IOptionService>(), dialog, null);
			dialog.ShowDialog();
		}
	}
}
