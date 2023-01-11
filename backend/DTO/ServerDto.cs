using ServerING.Enums;

namespace ServerING.DTO {
    public class ServerDtoBase {
        public string Name {get; set;}
        public string Ip {get; set;}
        public string GameName {get; set;}
        public ServerStatus? Status {get; set;}
        public int? HostingID { get; set; }
        public int? PlatformID { get; set; }
        public int? CountryID { get; set; }
        public int? OwnerID { get; set; }
    }

    public class ServerUpdateDto : ServerDtoBase {
        public int? Rating{ get; set; }
    }

    public class ServerDto : ServerDtoBase {
        public int Id { get; set; }
        public int Rating{ get; set; }
    }

    public class ServerFilterDto {
        public string Name {get; set;}
        public string Game {get; set;}
        public int? PlatformID {get; set;}
        public ServerStatus? Status {get; set;}
        public int? OwnerID { get; set; }
    }
}
