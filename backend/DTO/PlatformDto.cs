namespace ServerING.DTO {
    public class PlatformBaseDto {
        public string Name {get; set;}

        public int? Cost {get; set;}

        public ushort? Popularity {get; set;}
    }

    public class PlatformDto : PlatformBaseDto {
        public int Id {get; set;}
    }
}
