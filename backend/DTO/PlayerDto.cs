using System;

namespace ServerING.DTO {
    public class PlayerBaseDto {
        public string Nickname { get; set; }
        public int? HoursPlayed { get; set; }
        public DateTime? LastPlayed { get; set; }
    }

    public class PlayerDto : PlayerBaseDto {
        public int Id { get; set; }
    }
}
