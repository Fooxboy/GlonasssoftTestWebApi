namespace GlonasssoftTestWebApi.Exceptions
{
    public class RequestNotFoundException : Exception
    {
        public RequestNotFoundException(): base("Request not found")
        {
        }
    }
}
