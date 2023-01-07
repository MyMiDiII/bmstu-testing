using ServerING.Models;
using ServerING.ModelsBL;

namespace Builders
{
    public class UserBuilder {
        private int id;
        private string login = "";
        private string password = "";
        private string role = "";

        public UserBuilder withId(int id) {
            this.id = id;
            return this;
        }

        public UserBuilder withLogin(string login) {
            this.login = login; 
            return this;
        }

        public UserBuilder withPassword(string password) {
            this.password = password;
            return this;
        }

        public UserBuilder withRole(string role) {
            this.role = role;
            return this;
        }

        public User build() {
            var user = new User() {
                Id = id,
                Login = login,
                Password = password,
                Role = role
            };
            return user;
        }

        public UserBL buildBL() {
            var user = new UserBL() {
                Id = id,
                Login = login,
                Password = password,
                Role = role
            };
            return user;
        }
    }
}