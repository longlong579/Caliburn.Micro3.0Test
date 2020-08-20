using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Resources;
using System.Globalization;
using System.Windows.Markup;

using Illusion;
using System.Windows;

namespace Illusion.Demo
{
	[Export(typeof(ITheme))]
	[MenuPart(BaseMenu = WorkbenchName.ThemePart, NextMenu=WorkbenchName.ExpressionDarkThemePart)]
	public class DefaultTheme : MenuPart, ITheme
	{
		public DefaultTheme()
			: base(WorkbenchName.DefaultThemePart)
		{
			IsCheckable = true;
		}

		public IEnumerable<ResourceDictionary> Resources
		{
			get
			{
				return null;
			}
		}

		public override void Execute()
		{
			IoC.Get<IThemeService>().ChangeTheme(this);
		}
	}
}
