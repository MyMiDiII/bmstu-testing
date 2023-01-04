namespace ServerING.ModelsBL {
    public class FavoriteServerBL {
        public int Id { get; set; }
        public int ServerID { get; set; }
        public int UserID { get; set; }

        public virtual ServerBL Server { get; set; }
        public virtual UserBL User { get; set; }
    }
}
