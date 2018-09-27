using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using Xunit;

namespace BankBinCodeChecker.Tests
{
    public class BankBinCodeCheckerTests
    {
        private readonly Mock<ILogger<BankBinCodeChecker>> _loggerMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly HttpClient _client;
        private readonly IBankBinCodeChecker _bankBinCodeChecker;

        public BankBinCodeCheckerTests()
        {
            _client = new HttpClient();
            _loggerMock = new Mock<ILogger<BankBinCodeChecker>>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(x => x.CreateClient("")).Returns(_client);
            _bankBinCodeChecker = new BankBinCodeChecker(_loggerMock.Object, _httpClientFactoryMock.Object);
        }

        [Theory]
        [InlineData(45717360)]
        public async Task CheckBankBinCode_Should_Return_Valid_Result(int bankBinCode)
        {
            //Arrange

            //Act
            var result = await _bankBinCodeChecker.CheckBankBinCodeAsync(bankBinCode);

            //Assert
            result.Should().NotBeNull();
            result.Scheme.Should().Be("visa");
            result.Type.Should().Be("debit");
            result.Bank.Should().NotBeNull();
            result.Bank.Name.Should().Be("Jyske Bank");
        }

        [Theory]
        [InlineData(45717360)]
        public void CheckBankBinCodeAsync_Should_Return_Valid_Result(int bankBinCode)
        {
            //Arrange

            //Act
            var result = _bankBinCodeChecker.CheckBankBinCode(bankBinCode);

            //Assert
            result.Should().NotBeNull();
            result.Scheme.Should().Be("visa");
            result.Type.Should().Be("debit");
            result.Bank.Should().NotBeNull();
            result.Bank.Name.Should().Be("Jyske Bank");
        }

        [Theory]
        [InlineData(0)]
        public void CheckBankBinCodeAsync_Should_Log_When_Invalid_Bin_Code(int bankBinCode)
        {
            //Arrange

            //Act
            var result = _bankBinCodeChecker.CheckBankBinCode(bankBinCode);

            //Assert
            result.Should().BeNull();
            _loggerMock.Verify(x => x.Log(LogLevel.Warning, 0, It.IsAny<FormattedLogValues>(), It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()));
        }
    }
}