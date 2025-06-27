﻿using Dtos.Request.User;
using Models.User;
using System.Diagnostics.CodeAnalysis;

namespace Services.Validator
{
	[ExcludeFromCodeCoverage]
	public static class UserValidator
	{
		public static void CreateUserValidator(UserCreateRequest userCreateRequest)
		{
			if (userCreateRequest == null)
				throw new Exception("RequestIsNull");

			CheckAllFields(userCreateRequest);
		}

		private static void CheckAllFields(UserCreateRequest userCreateRequest)
		{
			if (string.IsNullOrEmpty(userCreateRequest.Email))
				throw new Exception("EmailIsEmpty");

			if (string.IsNullOrEmpty(userCreateRequest.Name))
				throw new Exception("NameIsEmpty");

			if (string.IsNullOrEmpty(userCreateRequest.Password))
				throw new Exception("PasswordIsEmpty");
		}
	}
}
