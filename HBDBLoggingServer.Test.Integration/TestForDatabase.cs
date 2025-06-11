using Microsoft.VisualStudio.TestTools.UnitTesting;
using HBLoggingService.Data;
using HamBlocks.Library.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HBLoggingService.Tests.Integration
{
    [TestClass]
    public class QsoIntegrationTests
    {
        private LoggingDbContext _dbContext;

        public QsoIntegrationTests(LoggingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<LoggingDbContext>()
                .UseNpgsql("Host=localhost;Port=5433;Database=HamRadioLogs;Username=postgres;Password=password")
                .Options;
            _dbContext = new LoggingDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext.Dispose();
        }

        [TestMethod]
        public async Task Add50Qsos_WithDetails_ToDatabase_Success()
        {
            await AddMultipleQsosWithDetailsToDatabaseAndOptionallyDelete(deleteAfter: false);
        }

        private async Task AddMultipleQsosWithDetailsToDatabaseAndOptionallyDelete(bool deleteAfter = false)
        {
            var qsos = new List<Qso>();
            for (int i = 0; i < 50; i++)
            {
                var qso = new Qso
                {
                    Id = Guid.NewGuid(),
                    Call = $"TEST{i:D3}",
                    Band = "20m",
                    Mode = "SSB",
                    QsoDate = DateTime.UtcNow,
                    MyCall = "MYCALL",
                    RstRcvd = "59",
                    RstSent = "59",
                    Freq = 14.250m,
                    Details = new List<QsoDetail>
                    {
                        new QsoDetail { FieldName = "IOTA", FieldValue = $"K-{i:0000}" },
                        new QsoDetail { FieldName = "POTA", FieldValue = $"P-{i:0000}" }
                    }
                };
                qsos.Add(qso);
            }

            _dbContext.Qsos.AddRange(qsos);
            await _dbContext.SaveChangesAsync();

            var ids = qsos.Select(q => q.Id).ToList();
            var count = await _dbContext.Qsos.CountAsync(q => ids.Contains(q.Id));
            Assert.AreEqual(50, count, "Not all Qsos were added to the database.");

            // Verify details
            var detailsCount = await _dbContext.QsoDetails.CountAsync(d => ids.Contains(d.QsoId));
            Assert.AreEqual(100, detailsCount, "Not all QsoDetails were added to the database.");

            if (deleteAfter)
            {
                _dbContext.Qsos.RemoveRange(qsos);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
