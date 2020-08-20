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
	[MenuPart(BaseMenu = WorkbenchName.ThemePart, PreviousMenu=WorkbenchName.ExpressionLightThemePart)]
	public class VS2010Theme : MenuPart, ITheme
	{
		public VS2010Theme()
			:base(WorkbenchName.VS2010ThemePart)
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
						this.LoadResourceDictionary(new Uri("Themes/Color_Light.xaml", UriKind.Relative)), 
						this.LoadResourceDictionary(new Uri("Themes/Theme_Expression.xaml", UriKind.Relative)), 
						this.LoadResourceDictionary(new Uri("/AvalonDock.Themes;component/themes/dev2010.xaml", UriKind.Relative))};
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
