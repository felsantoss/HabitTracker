using Configuration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.UserRepository
{
	public class UserRepository
	{
		private readonly DataContext _dataContext;

		public UserRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}
	}
}
