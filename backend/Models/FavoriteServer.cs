using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerING.Models {
    public class FavoriteServer {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Server")]
        public int ServerID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        public virtual Server Server { get; set; }
        public virtual User User { get; set; }
    }
}
