using System;
using System.Collections.Generic;
using System.Text;

namespace CodeSoda.IOC
{
	public partial class Container
	{
		#region singleton instance

		private static IContainer _container = null;
		private static readonly Object _containerLock = new Object();
		public static IContainer Instance {
			get {
				lock (_containerLock) {
					if (_container == null) _container = new Container();
					return _container;
				}
			}
		}

		#endregion
	}
}
