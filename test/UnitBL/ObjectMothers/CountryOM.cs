using Builders;

namespace ObjectMothers
{
    public class CountriesOM
    {
        public static CountryBuilder NumberedCountry(int number) {
            return new CountryBuilder()
                       .withId(number)
                       .withName(string.Format("C{0}", number))
                       .withInterestLevel(number)
                       .withPlayers(number);
        }
    } 
}