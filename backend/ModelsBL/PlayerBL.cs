using System;

namespace ServerING.ModelsBL {
    public class PlayerBL {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public int HoursPlayed { get; set; }
        public DateTime LastPlayed { get; set; }
    }
}
