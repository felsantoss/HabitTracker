using Dtos.Request.User;
using Dtos.Response.User;
using Models.User;
using Repositories.Interfaces;
using Services.Interface;
using Services.Validator;

namespace Services.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository) 
		{
			_userRepository = userRepository;
		}

		public async Task<UserCreateResponse> Create(UserCreateRequest userCreateRequest)
		{
			UserValidator.CreateUserValidator(userCreateRequest);

			var userAlreadyExist = await _userRepository.UserAlreadyRegistered(userCreateRequest.Email);

			if (userAlreadyExist)
				throw new Exception("UserAlreadyRegistered");

			var user = new User
			{
				Name = userCreateRequest.Name,
				Email = userCreateRequest.Email,
				Password = userCreateRequest.Password,
			};

			await _userRepository.Add(user);

			return new UserCreateResponse
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email
			};
		}
	}
}
