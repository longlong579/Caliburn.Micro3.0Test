﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Resources;
using System.Globalization;

using Illusion;
using System.Windows.Media;

namespace Illusion.AddIn2
{
	[Export(typeof(IResource))]
	public class Resource : IResource
	{
		private ResourceManager stringResource;
		private CultureInfo culture = new CultureInfo("en-us");

		public Resource()
		{
			stringResource = new ResourceManager("Illusion.AddIn2.Resources.StringResource", typeof(Resource).Assembly);
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
			return null;
		}

		#endregion
	}
}
