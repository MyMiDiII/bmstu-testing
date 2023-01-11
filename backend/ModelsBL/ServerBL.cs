using ServerING.Enums;

namespace ServerING.ModelsBL {
    public class ServerBL {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ip { get; set; }
        public string GameName { get; set; }
        public int Rating{ get; set; }
        public ServerStatus Status {get; set;}
        public int HostingID { get; set; }
        public int PlatformID { get; set; }
        public int CountryID { get; set; }
        public int OwnerID { get; set; }
        
        public virtual WebHostingBL Hosting { get; set; }
        public virtual PlatformBL Platform { get; set; }
        public virtual CountryBL Country { get; set; }
        public virtual UserBL Owner { get; set; }
    }
}
