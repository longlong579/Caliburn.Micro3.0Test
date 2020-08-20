using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

namespace Illusion
{
	using Caliburn.Micro;
	/// <summary>
	/// A menu part for various implementations of <see cref="IMenuPart"/>.
	/// </summary>
	public class MenuPart : PartBase, IMenuPart, IObservableParent<IMenuPart>
	{
		public MenuPart()
			: base()
		{
		}

		public MenuPart(string name)
			: base(name)
		{

		}

		public MenuPart(string name, System.Action<PartBase> execute, Func<bool> canExecute = null)
			: base(name, execute, canExecute)
		{
		}

		private IObservableCollection<IMenuPart> items = new BindableCollection<IMenuPart>();
		IObservableCollection<IMenuPart> IObservableParent<IMenuPart>.Items
		{
			get { return items; }
		}

		#region ISeparaterPart Members

		private bool _isSeparator = false;
		public bool IsSeparator
		{
			get
			{
				return _isSeparator;
			}
			protected set
			{
				_isSeparator = value;
				NotifyOfPropertyChange(() => IsSeparator);
			}
		}

		#endregion

		#region IMenuPart Members

		private bool _isCheckable = false;
		public bool IsCheckable
		{
			get
			{
				return _isCheckable;
			}
			set
			{
				_isCheckable = value;
				NotifyOfPropertyChange(() => IsCheckable);
			}
		}

		private bool _isChecked = false;
		public bool IsChecked
		{
			get
			{
				return _isChecked;
			}
			set
			{
				_isChecked = value;
				NotifyOfPropertyChange(() => IsChecked);
			}
		}

		#endregion
	}
}
