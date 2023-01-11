using ServerING.DTO;
using ServerING.ModelsBL;
using ServerING.Services;

namespace ServerING.ModelConverters {
    public class UserConverters {

        private readonly IUserService userService;

        public UserConverters(IUserService userService) {
            this.userService = userService;
        }

        public UserBL convertPatch(int id, UserPasswordDto user) {
            var existedUser = userService.GetUserByID(id);

            return new UserBL {
                Id = id,
                Login = user.Login ?? existedUser.Login,
                Password = user.Password ?? existedUser.Password,
                Role = user.Role ?? existedUser.Role
            };
        }
    }
}
