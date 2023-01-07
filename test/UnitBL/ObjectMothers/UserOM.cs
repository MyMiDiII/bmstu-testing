using Builders;

namespace ObjectMothers
{
    public class UsersOM 
    {
        public static UserBuilder Guest(int id) {
            return new UserBuilder()
                       .withId(id)
                       .withLogin("guest")
                       .withPassword("guest")
                       .withRole("guest");
        }

        public static UserBuilder User(int id) {
            return new UserBuilder()
                       .withId(id)
                       .withLogin(string.Format("user{0}", id))
                       .withPassword(string.Format("password{0}", id))
                       .withRole("user");
        }

        public static UserBuilder Admin(int id) {
            return new UserBuilder()
                       .withId(id)
                       .withLogin(string.Format("admin{0}", id))
                       .withPassword(string.Format("admin{0}", id))
                       .withRole("admin");
        }
    } 
}