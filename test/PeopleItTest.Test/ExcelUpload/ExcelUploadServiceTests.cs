using Microsoft.AspNetCore.Components.Forms;
using Moq;
using PeopleItTest.Models;
using PeopleItTest.Services.ExcelUpload;

namespace PeopleItTest.Test.ExcelUpload
{
    public class ExcelUploadServiceTests
    {
        private readonly IExcelUploadService _service;
        private readonly Mock<IBrowserFile> _mockFile;

        public ExcelUploadServiceTests()
        {
            _service = new ExcelUploadService();
            _mockFile = new Mock<IBrowserFile>();
        }

        private void SetupMockFileWithContent(string filePath)
        {
            var memoryStream = new MemoryStream();
            using (var fs = File.OpenRead(filePath))
            {
                fs.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;

            _mockFile.Setup(_ => _.OpenReadStream(It.IsAny<long>(), It.IsAny<CancellationToken>())).Returns(memoryStream);
        }

        [Fact]
        public async Task ProcessExcelFile_ReturnsCorrectNumberOfQuotes()
        {
            // Arrange
            var filePath = "ExcelUpload/TestDataExport1.xlsx";
            SetupMockFileWithContent(filePath);

            // Act
            var quotes = await _service.ProcessExcelFile(_mockFile.Object);

            // Assert
            Assert.NotNull(quotes);
            Assert.Equal(2, quotes.Count); 
        }

        [Fact]
        public async Task ProcessExcelFile_ThrowsNullReferenceExceptionOnNullFile()
        {
            // Arrange is skipped here since we want to pass null

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => _service.ProcessExcelFile(null));
        }

        [Fact]
        public async Task ProcessExcelFile_ThrowsInvalidDataExceptionOnBadDate()
        {
            // Arrange
            var filePath = "ExcelUpload/TestDataExport2.xlsx";
            SetupMockFileWithContent(filePath);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => _service.ProcessExcelFile(_mockFile.Object));
        }

        [Fact]
        public async Task ProcessExcelFile_ParsesDataCorrectly()
        {
            // Arrange
            var filePath = "ExcelUpload/TestDataExport1.xlsx";
            SetupMockFileWithContent(filePath);

            // Act
            var quotes = await _service.ProcessExcelFile(_mockFile.Object);

            // Assert
            var expectedQuote = new ProjectQuote
            {
                QuoteSentDate = DateTime.Parse("10/26/2023"),
                Salesperson = "Tyler Berens",
                ProjectName = "Test Project Name 5",
                ProjectCode = "1234",
                Customer = "Tyler Berens",
                CustomerCity = "Holland",
                CustomerState = "MI",
                MarketingCategory = "IT",
                NumberOfQuotes = 1,
                TotalNet = 12345M,
            };

            Assert.Contains(quotes, q =>
                q.QuoteSentDate == expectedQuote.QuoteSentDate &&
                q.Salesperson == expectedQuote.Salesperson &&
                q.ProjectName == expectedQuote.ProjectName &&
                q.ProjectCode == expectedQuote.ProjectCode &&
                q.Customer == expectedQuote.Customer &&
                q.CustomerCity == expectedQuote.CustomerCity &&
                q.CustomerState == expectedQuote.CustomerState &&
                q.MarketingCategory == expectedQuote.MarketingCategory &&
                q.NumberOfQuotes == expectedQuote.NumberOfQuotes &&
                q.TotalNet == expectedQuote.TotalNet 
            );
        }

    }
}
