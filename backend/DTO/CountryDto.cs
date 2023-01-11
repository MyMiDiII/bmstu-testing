
namespace ServerING.DTO {
    public class CountryBaseDto {
        public string Name { get; set; }

        public int? LevelOfInterest { get; set; }

        public int? OverallPlayers { get; set; }
    }

    public class CountryDto : CountryBaseDto {
        public int Id { get; set; }
    }
}
