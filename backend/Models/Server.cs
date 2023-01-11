using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServerING.Enums;

namespace ServerING.Models {
    public class Server {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [Required, MaxLength(30)]
        public string Ip { get; set; }

        [Required, MaxLength(30)]
        public string GameName { get; set; }

        /*[Required]*/
        public int Rating{ get; set; }

        [Required]
        public ServerStatus Status {get; set;}

        [ForeignKey("WebHosting")]
        public int HostingID { get; set; }

        [ForeignKey("Platform")]
        public int PlatformID { get; set; }

        [ForeignKey("Country")]
        public int CountryID { get; set; }

        [ForeignKey("Owner")]
        public int OwnerID { get; set; }
        
        public virtual WebHosting Hosting { get; set; }
        public virtual Platform Platform { get; set; }
        public virtual Country Country { get; set; }
        public virtual User Owner { get; set; }
    }
}
