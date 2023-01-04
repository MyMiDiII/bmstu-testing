using System;
using System.ComponentModel.DataAnnotations;

namespace ServerING.Models {
    public class Player {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Nickname { get; set; }

        [Required]
        public int HoursPlayed { get; set; }

        [Required]
        public DateTime LastPlayed { get; set; }
    }
}
