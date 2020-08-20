using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Resources;
using System.Globalization;

using Illusion;
using System.Windows.Media;

namespace Illusion.Demo
{
	[Export(typeof(IResource))]
	public class Resource : IResource
	{
		private ResourceManager stringResource;
		private ResourceManager imageSource;
		private CultureInfo culture = new CultureInfo("en-us");

		public Resource()
		{
			stringResource = new ResourceManager("Illusion.Demo.Resources.StringResource", typeof(Resource).Assembly);
			imageSource = new ResourceManager("Illusion.Demo.Resources.ImageResource", typeof(Resource).Assembly);
		}

		#region IResource Members

		public CultureInfo CurrentCulture 
		{ 
			set
			{
				culture = value;
			}
		}

		public string GetString(string name)
		{
			return stringResource.GetString(name, culture);
		}

		public ImageSource GetImage(string name)
		{
			return imageSource.GetImageSource(name);
		}

		#endregion
	}
}
