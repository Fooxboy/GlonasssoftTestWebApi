using System.Text.Json.Serialization;

namespace GlonasssoftTestWebApi.Models
{
    public class ReportResultModel
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; private set; }

        [JsonPropertyName("count_sign_in")]
        public uint CountSignIn { get; private set; }

        public ReportResultModel(Guid userId, uint countSignIn)
        {
            this.UserId = userId.ToString();

            this.CountSignIn = countSignIn;
        }
    }
}
