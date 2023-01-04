namespace ServerING.ModelsBL {
    public class ServerPlayerBL {
        public int Id { get; set; }
        public int ServerID { get; set; }
        public int PlayerID { get; set; }

        public virtual ServerBL Server { get; set; }
        public virtual PlayerBL Player { get; set; }
    }
}
