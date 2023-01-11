using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerING.Models {
    public class ServerPlayer {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Server")]
        public int ServerID { get; set; }

        [ForeignKey("Player")]
        public int PlayerID { get; set; }

        public virtual Server Server { get; set; }
        public virtual Player Player { get; set; }
    }
}
