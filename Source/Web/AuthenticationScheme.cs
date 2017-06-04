using doLittle.Read;

namespace Web
{
    public class AuthenticationScheme : IReadModel
    {
        public string DisplayName {get; set; }
        public string Scheme  { get; set;}
    }
}