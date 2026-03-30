using Models.User;
using Repositories.Interfaces;

namespace Tests.Mock
{
    public class UserRepositoryMock : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _nextId = 1;

        public Task Add(User user)
        {
            if (user.Id == 0)
            {
                user.Id = _nextId++;
            }
            else if (user.Id >= _nextId)
            {
                _nextId = user.Id + 1;
            }

            _users.Add(new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            });

            return Task.CompletedTask;
        }

        public Task<bool> ExistsUserByEmailAsync(string email)
        {
            return Task.FromResult(_users.Any(u => u.Email == email));
        }

        public Task<User> GetByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email)
                ?? throw new Exception("UserNotFound");

            return Task.FromResult(user);
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id)
                ?? throw new Exception("UserNotFound");

            return Task.FromResult(user);
        }

        public Task Update(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id)
                ?? throw new Exception("UserNotFound");

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            return Task.CompletedTask;
        }
    }
}
