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
using System.Windows.Controls;

namespace Illusion.Demo
{
	[Export(typeof(ITheme))]
	[MenuPart(BaseMenu = WorkbenchName.ThemePart)]
	public class ExpressionDarkTheme : MenuPart, ITheme
	{
		public ExpressionDarkTheme()
			: base(WorkbenchName.ExpressionDarkThemePart)
		{
			IsCheckable = true;
		}

		private IEnumerable<ResourceDictionary> resources;
		public IEnumerable<ResourceDictionary> Resources
		{
			get
			{
				if (resources == null)
				{
					resources = new ResourceDictionary[] { 
						this.LoadResourceDictionary(new Uri("Themes/Color_Gray.xaml", UriKind.Relative)),
						this.LoadResourceDictionary(new Uri("Themes/Theme_Expression.xaml", UriKind.Relative)),
						this.LoadResourceDictionary(new Uri("/AvalonDock.Themes;component/themes/ExpressionDark.xaml", UriKind.Relative))};
				}
				return resources;
			}
		}

		public override void Execute()
		{
			IoC.Get<IThemeService>().ChangeTheme(Name);
		}
	}
}
