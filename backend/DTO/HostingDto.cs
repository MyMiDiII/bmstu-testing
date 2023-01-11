namespace ServerING.DTO {
    public class HostingBaseDto {
        public string Name {get; set;}
        public int? PricePerMonth {get; set;}
        public ushort? SubMonths {get; set;}
    }

    public class HostingDto : HostingBaseDto {
        public int Id {get; set;}
    }
}
