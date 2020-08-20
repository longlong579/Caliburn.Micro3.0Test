using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Illusion;


namespace Illusion.Demo
{
	using Caliburn.Micro;
	[Export(typeof(IOptionPage))]
	public class LanguageOption : OptionPageBase
	{
		public LanguageOption()
			: base(WorkbenchName.LanguageOption)
		{
			
		}

		private string Lanaguage { get; set; }

		public void ChangeToChinese()
		{
			Lanaguage = "zh-cn";
		}

		public void ChangeToEnglish()
		{
			Lanaguage = "en-us";
		}

		public override void Commit()
		{
			IoC.Get<IResourceService>().ChangeLanguage(Lanaguage);
		}
	}
}
