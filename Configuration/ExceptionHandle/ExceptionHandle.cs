using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.ExceptionHandle
{
	public class ExceptionHandle : Exception
	{
		public ExceptionHandle(string message) : base(message) { }
	}
}
