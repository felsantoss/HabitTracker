using Configuration.ExceptionHandle;
using Dtos.Request.User;
using Dtos.Response.User;
using Models.User;
using Repositories.Interfaces;
using Services.Interface;
using Services.Security;
using Services.Validator;

namespace Services.Services
{
	public class UserService(IUserRepository userRepository) : IUserService
	{
		private readonly IUserRepository _userRepository = userRepository;

		public async Task<UserCreateResponse> Create(UserCreateRequest userCreateRequest)
		{
			UserValidator.CreateUserValidator(userCreateRequest);

			var userAlreadyExist = await _userRepository.ExistsUserByEmailAsync(userCreateRequest.Email);

			if (userAlreadyExist)
				throw new ExceptionHandle("UserAlreadyRegistered");

			var encryptedPassword = SecurityHandler.GenerateEncryptedPassword(userCreateRequest.Password);

			var user = new User
			{
				Name = userCreateRequest.Name,
				Email = userCreateRequest.Email,
				Password = encryptedPassword
			};

			await _userRepository.Add(user);

			return new UserCreateResponse
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email
			};
		}

		public async Task<bool> Update(int id, UserUpdateRequest userUpdateRequest)
		{
			UserValidator.UpdateUserValidator(userUpdateRequest);

			var user = await _userRepository.GetByIdAsync(id);

			user.Name = userUpdateRequest.Name;

			await _userRepository.Update(user);

			return true;
		}
	}
}
