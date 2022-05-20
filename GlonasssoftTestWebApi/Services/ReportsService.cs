using GlonasssoftTestWebApi.Db;
using GlonasssoftTestWebApi.Entities;
using GlonasssoftTestWebApi.Exceptions;
using GlonasssoftTestWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GlonasssoftTestWebApi.Services
{
    public class ReportsService
    {
        private readonly IConfiguration configuration;

        private readonly ApplicationDatabaseContext db;
        public ReportsService(IConfiguration configuration, ApplicationDatabaseContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        /// <summary>
        /// Создать запрос для получения отчета
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="from">Период с </param>
        /// <param name="to">Период по</param>
        /// <returns>Идентификатор запроса</returns>
        public async Task<Guid> CreateRequestAsync(Guid userId, DateTime from, DateTime to)
        {

            ValidateDates(from, to);

            var user = await db.Users.FirstOrDefaultAsync(u=> u.Id == userId);

            ValidateUser(user);

            var processTimeMs = configuration.GetValue<int>("ProcessTime");

            var time = DateTime.UtcNow + TimeSpan.FromMilliseconds(processTimeMs);

            var requestId = Guid.NewGuid();
            var request = new Request();
            request.Id = requestId;
            request.User = user;
            request.ToDate = to;
            request.FromDate = from;
            request.CompletionTime = time;

            await db.Requests.AddAsync(request);

            db.SaveChanges();

            return requestId;
        }

        private bool ValidateDates(DateTime from, DateTime to)
        {
            if (from > to)
            {
                throw new ArgumentException(nameof(from) + " and " + nameof(to));
            }

            if (from > DateTime.UtcNow)
            {
                throw new ArgumentException(nameof(from));
            }

            return true;
        }

        private bool ValidateUser(User? user)
        {
            if(user is null)
            {
                throw new UserNotFoundException();
            }

            return true;
        }


        /// <summary>
        /// Получить отчет
        /// </summary>
        /// <param name="requestId">Идентификатор запроса</param>
        /// <returns>Информация о отчете</returns>
        public async Task<ReportModel> GetReportAsync(Guid requestId)
        {
            var request = await db.Requests.FirstOrDefaultAsync(r=> r.Id == requestId);

            ValidateRequest(request);

            var percent = 0u;

            if(DateTime.UtcNow >= request.CompletionTime) //если запрос обработан
            {
                percent = 100;

                var usr = await db.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);

                if(usr == null)
                {
                    throw new UserNotFoundException();
                }

                var report = new Report();
                report.Id = Guid.NewGuid();
                report.User = usr;
                var randomCount = (uint)((request.ToDate - request.FromDate).TotalDays + new Random().Next(0, 100));
                report.CountSignIn = randomCount;

                var dto = new ReportModel(requestId, percent, request.UserId, randomCount);

                await db.Reports.AddAsync(report);

                await db.SaveChangesAsync();

                return dto;
            }


            var processTimeMs = configuration.GetValue<int>("ProcessTime");

            var time = (request.CompletionTime - DateTime.UtcNow).TotalMilliseconds;

            percent = 100 - (uint)(time / (processTimeMs / 100));

            var data = new ReportModel(requestId, percent);

            return data;

        }

        private bool ValidateRequest(Request? request)
        {
            if(request is null)
            {
                throw new RequestNotFoundException();
            }

            return true;
        }

    }
}
