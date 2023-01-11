using Builders;

namespace ObjectMothers
{
    public class ServersOM
    {
        public static ServerBuilder NumberedServer(int number) {
            return new ServerBuilder()
                       .withId(number)
                       .withName(string.Format("Server{0}", number))
                       .withIp(string.Format("{0}.{0}.{0}.{0}", number))
                       .withGame(string.Format("Game{0}", number))
                       .withRating(number);
        }
    } 
}