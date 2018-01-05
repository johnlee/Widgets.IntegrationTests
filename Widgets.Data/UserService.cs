using Highway.Data;
using Widgets.Data.Queries;

namespace Widgets.Data
{
    public class UserService
    {
        private IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public User CreateUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return null;
            }

            var user = _repository.Find(new GetUserByEmail(email));

            if (user == null)
            {
                user = _repository.Context.Add(User.Create(email));
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            return _repository.Find(new GetUserByEmail(email));
        }
    }
}
