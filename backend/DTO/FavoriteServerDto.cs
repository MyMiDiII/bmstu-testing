namespace ServerING.Models {
    public class FavoriteServerDtoBase {
        public int ServerID { get; set; }
        public int UserID { get; set; }
    }

    public class FavoriteServerDto : FavoriteServerDtoBase {
        public int Id { get; set; }
    }
}
