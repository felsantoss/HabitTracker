using Dtos.Request.User;
using Dtos.Response.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
	public interface IUserService
	{
		Task<UserCreateResponse> Create(UserCreateRequest userCreateRequest);
	}
}
