﻿using Dtos.Request.User;
using Dtos.Response.User;

namespace Services.Interface
{
	public interface IUserService
	{
		Task<UserCreateResponse> Create(UserCreateRequest userCreateRequest);

		Task<bool> Update(int id, UserUpdateRequest userUpdateRequest);
	}
}
