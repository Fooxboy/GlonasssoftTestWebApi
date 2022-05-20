using GlonasssoftTestWebApi.Models;
using GlonasssoftTestWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlonasssoftTestWebApi.Controllers
{
    [Route("report")]
    [ApiController]
    public class ReportController:Controller
    {

        private readonly ReportsService reportsService;

        public ReportController(ReportsService reportsService)
        {
            this.reportsService = reportsService;
        }

        [HttpPost("user_statistics")]
        public async Task<string> UserStatistics(Guid userId, DateTime fromDate, DateTime toDate)
        {
            var requestId = await reportsService.CreateRequestAsync(userId, fromDate, toDate);

            return requestId.ToString();
        }

        [HttpGet("info")]
        public async Task<ReportModel> GetInfo(Guid requestId)
        {
            var result = await reportsService.GetReportAsync(requestId);

            return result;
        }
    }
}
