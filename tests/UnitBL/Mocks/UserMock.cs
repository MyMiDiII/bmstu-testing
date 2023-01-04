using ServerING.Interfaces;
using ServerING.Models;
using System.Collections.Generic;
using System.Linq;

namespace ServerING.Mocks {
    /*
    public class UserMock : IUserRepository {

        private List<User> _users = new List<User>() {
            new User {
                Id = 1,
                Login = "Login1",
                Password = "12345",
                Permission = 1
            },
            new User {
                Id = 2,
                Login = "Login2",
                Password = "12345",
                Permission = 2
            },
            new User {
                Id = 3,
                Login = "Login3",
                Password = "12345",
                Permission = 3
            },
        };

        public void Add(User model) {
            _users.Add(model);
        }

        public User Delete(int id) {
            User user = _users[id - 1];
            _users.Remove(user);

            return user;
        }

        public IEnumerable<User> GetAll() {
            return _users;
        }

        public User GetByID(int id) {
            return _users[id - 1];
        }

        public User GetByLogin(string login) {
            return _users.FirstOrDefault(item => item.Login == login);
        }

        public IEnumerable<User> GetByPermission(ushort permission) {
            return _users.Where(item => item.Permission == permission);
        }

        public IEnumerable<Server> GetFavoriteServersByUserId(int id) {
            return null;
        }

        public void Update(User model) {
            User user = _users[model.Id - 1];

            user.Login = model.Login;
            user.Password = model.Password;
            user.Permission = model.Permission;

            _users[user.Id - 1] = user;
        }
    }
    */
}
