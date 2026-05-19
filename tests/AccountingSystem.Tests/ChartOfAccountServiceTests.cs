using Xunit;
using Moq;
using AccountingSystem.Application.DTOs;
using AccountingSystem.Application.Services;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Domain.Interfaces;

namespace AccountingSystem.Tests
{
    public class ChartOfAccountServiceTests
    {
        private readonly Mock<IRepository<ChartOfAccount>> _mockRepository;
        private readonly ChartOfAccountService _service;

        public ChartOfAccountServiceTests()
        {
            _mockRepository = new Mock<IRepository<ChartOfAccount>>();
            _service = new ChartOfAccountService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateAccountAsync_WithValidData_ShouldCreateAccount()
        {
            // Arrange
            var dto = new ChartOfAccountDTO
            {
                AccountCode = "1000",
                AccountName = "Cash",
                AccountType = AccountType.Asset,
                IsActive = true
            };

            var createdAccount = new ChartOfAccount
            {
                Id = 1,
                AccountCode = dto.AccountCode,
                AccountName = dto.AccountName,
                AccountType = dto.AccountType,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _mockRepository.Setup(x => x.AddAsync(It.IsAny<ChartOfAccount>()))
                .ReturnsAsync(createdAccount);
            _mockRepository.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(true);

            // Act
            var result = await _service.CreateAccountAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("1000", result.AccountCode);
            Assert.Equal("Cash", result.AccountName);
            Assert.Equal(AccountType.Asset, result.AccountType);
            _mockRepository.Verify(x => x.AddAsync(It.IsAny<ChartOfAccount>()), Times.Once);
            _mockRepository.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAccountAsync_WithoutAccountCode_ShouldThrowException()
        {
            // Arrange
            var dto = new ChartOfAccountDTO
            {
                AccountCode = "",
                AccountName = "Cash",
                AccountType = AccountType.Asset
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAccountAsync(dto));
        }

        [Fact]
        public async Task GetAccountByIdAsync_WithValidId_ShouldReturnAccount()
        {
            // Arrange
            var accountId = 1;
            var account = new ChartOfAccount
            {
                Id = accountId,
                AccountCode = "1000",
                AccountName = "Cash",
                AccountType = AccountType.Asset,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _mockRepository.Setup(x => x.GetByIdAsync(accountId))
                .ReturnsAsync(account);

            // Act
            var result = await _service.GetAccountByIdAsync(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accountId, result.Id);
            Assert.Equal("Cash", result.AccountName);
        }

        [Fact]
        public async Task GetAllAccountsAsync_ShouldReturnAllAccounts()
        {
            // Arrange
            var accounts = new List<ChartOfAccount>
            {
                new ChartOfAccount { Id = 1, AccountCode = "1000", AccountName = "Cash", AccountType = AccountType.Asset },
                new ChartOfAccount { Id = 2, AccountCode = "2000", AccountName = "Accounts Payable", AccountType = AccountType.Liability }
            };

            _mockRepository.Setup(x => x.GetAllAsync())
                .ReturnsAsync(accounts);

            // Act
            var result = await _service.GetAllAccountsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}