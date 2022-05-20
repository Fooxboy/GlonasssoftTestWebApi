using System.Text.Json.Serialization;

namespace GlonasssoftTestWebApi.Models
{
    public class ReportModel
    {
        [JsonPropertyName("query")]
        public string Query { get; private set; }

        [JsonPropertyName("percent")]
        public uint Percent { get; private set; }

        [JsonPropertyName("result")]
        public ReportResultModel? Result { get; private set; }

        public ReportModel(Guid requestId, uint percent, Guid? userId = null, uint? countSignIn = null)
        {
            this.Query = requestId.ToString();
            this.Percent = percent;

            if(userId != null && countSignIn != null)
            {
                this.Result = new ReportResultModel(userId.Value, countSignIn.Value);
            }
        }
    }
}
