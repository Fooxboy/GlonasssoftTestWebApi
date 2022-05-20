
using GlonasssoftTestWebApi.Db;
using GlonasssoftTestWebApi.Entities;
using GlonasssoftTestWebApi.Exceptions;
using GlonasssoftTestWebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GlonasssoftTestWebApi.Tests
{
    public class ReportsTests
    {

        private List<User> UsersData = new List<User>()
        {
            new User() {Id = new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3")},
            new User() {Id = new Guid("2babb2db-81e0-4137-901b-15d25afdf3a2")},
            new User() {Id = new Guid("229b329e-b899-4b21-9ae5-5937ece6bb0a")},
            new User() {Id = new Guid("7dccedc7-2ad8-42dd-9005-fccf1aa82d03")},
        };

        private List<Report> ReportsData = new List<Report>()
        {

        };

        private List<Request> RequestsData = new List<Request>()
        {
            new Request()
            {
                Id = new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"), CompletionTime = DateTime.UtcNow + TimeSpan.FromSeconds(60),
                FromDate = new DateTime(2022, 1, 10),
                ToDate = DateTime.UtcNow,
                User = new User()
                {
                    Id = new Guid("2babb2db-81e0-4137-901b-15d25afdf3a2")
                }, 
                
                UserId = new Guid("2babb2db-81e0-4137-901b-15d25afdf3a2")
            }
            
        };


        private IConfiguration Configuration = new ConfigurationBuilder()
         .AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ProcessTime", "60000"},
            })
         .Build();

        [Fact]
        public async Task CreateRequestTest()
        {

            var mockContext = new Mock<ApplicationDatabaseContext>();

            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x => x.Requests).ReturnsDbSet(RequestsData);


            var service = new ReportsService(Configuration, mockContext.Object);

            var result = await service.CreateRequestAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"), new DateTime(2022, 1, 10), new DateTime(2022, 5, 10));

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CreateRequestInvalidUserIdTest()
        {
            
            var mockContext = new Mock<ApplicationDatabaseContext>();

            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x => x.Requests).ReturnsDbSet(RequestsData);


            var service = new ReportsService(Configuration, mockContext.Object);


            await Assert.ThrowsAsync<UserNotFoundException>(async() =>
            {
                 await service.CreateRequestAsync(new Guid("00000000-0000-0000-0000-07746f81b8c3"), new DateTime(2022, 1, 10), new DateTime(2022, 5, 10));
            });
        }

        [Fact]
        public async Task CreateRequestInvalidDatesTest()
        {
            var mockContext = new Mock<ApplicationDatabaseContext>();

            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x => x.Requests).ReturnsDbSet(RequestsData);


            var service = new ReportsService(Configuration, mockContext.Object);


            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.CreateRequestAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"), DateTime.UtcNow + TimeSpan.FromDays(1), new DateTime(2022, 5, 10));

            });

            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await service.CreateRequestAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"), new DateTime(2022, 2, 10), new DateTime(2022, 1, 10));

            });
        }

        [Fact]
        public async Task GetReportTest()
        {
            var mockContext = new Mock<ApplicationDatabaseContext>();

            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x => x.Requests).ReturnsDbSet(RequestsData);
            mockContext.Setup(x => x.Reports).ReturnsDbSet(ReportsData);

           
            var service = new ReportsService(Configuration, mockContext.Object);

            var res = await service.GetReportAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"));

            Assert.True(res.Result == null);
        }

        [Fact]
        public async Task GetReportNotFoundException()
        {
            var mockContext = new Mock<ApplicationDatabaseContext>();

            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x => x.Requests).ReturnsDbSet(RequestsData);
            mockContext.Setup(x => x.Reports).ReturnsDbSet(ReportsData);

            

            var service = new ReportsService(Configuration, mockContext.Object);

            await Assert.ThrowsAsync<RequestNotFoundException>(async () =>
            {
                var res = await service.GetReportAsync(new Guid("00000000-5e93-4438-b7cd-07746f81b8c3"));
            });
        }

        [Fact]
        public async Task ReportProcessPercentTest()
        {
            var requests = new List<Request>()
            {
                new Request() {
                    Id = new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"),
                    CompletionTime = DateTime.UtcNow + TimeSpan.FromSeconds(60),
                    FromDate = new DateTime(2022, 1, 12),
                    ToDate = new DateTime(2022, 5, 12),
                    UserId = new Guid("2babb2db-81e0-4137-901b-15d25afdf3a2")}
            };

            var reports = new List<Report>();

            var mockContext = new Mock<ApplicationDatabaseContext>();
            mockContext.Setup(x => x.Users).ReturnsDbSet(UsersData);
            mockContext.Setup(x=> x.Requests).ReturnsDbSet(requests);
            mockContext.Setup(x=> x.Reports).ReturnsDbSet(reports);

            var service = new ReportsService(Configuration, mockContext.Object);


            var res = await service.GetReportAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"));

            Assert.True(res.Result == null);
            Assert.True(res.Percent < 5);

            await Task.Delay(TimeSpan.FromSeconds(30));

            res = await service.GetReportAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"));
            Assert.True(res.Result == null);
            Assert.True(res.Percent >= 50 && res.Percent < 55);

            await Task.Delay(TimeSpan.FromSeconds(15)); //45 секунд

            res = await service.GetReportAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"));
            Assert.True(res.Result == null);
            Assert.True(res.Percent >= 75 && res.Percent < 80);

            await Task.Delay(TimeSpan.FromSeconds(15)); //60 секунд

            res = await service.GetReportAsync(new Guid("7d8867f7-5e93-4438-b7cd-07746f81b8c3"));
            Assert.True(res.Result != null);
            Assert.True(res.Percent == 100);

        }

    }
}
