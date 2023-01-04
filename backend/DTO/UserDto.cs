namespace ServerING.DTO {
    public class UserPasswordDto : UserBaseDto {
        public string Password { get; set; }
    }

    public class UserBaseDto {
        public string Login { get; set; }
        public string Role{ get; set; }
    }

    public class UserIdPasswordDto : UserPasswordDto {
        public int Id { get; set; }
    }

    public class UserDto : UserBaseDto {
        public int Id { get; set; }
    }

    public class LoginDto {
        public string Login { get; set;}
        public string Password { get; set;}
    }
}
