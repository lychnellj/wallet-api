using SimpleWallet.Domain.Entities;
using SimpleWallet.Domain.Enums;
using SimpleWallet.Domain.Exceptions;

namespace SimpleWallet.UnitTests;

public class WalletDomainTests
{
    private Wallet CreateTestWallet()
    {
        var userId = Guid.NewGuid();
        var wallet = new Wallet(userId);
        wallet.Deposit(1000); // deposit an initial amount
        return wallet;
    }

    [Fact]
    public void Withdraw_WithInsufficientFunds_ThrowsInsufficientFundsException()
    {
        // arrange
        var wallet = CreateTestWallet();
        var initialBalance = wallet.Balance;
        var withdrawAmount = initialBalance + 100;

        // act and assert
        var exception = Assert.Throws<InsufficientFundsException>(() => wallet.Withdraw(withdrawAmount));
        Assert.Equal(initialBalance, wallet.Balance); // balance shouldn't change
    }

    [Fact]
    public void Deposit_WithValidAmount_IncreasesBalance()
    {
        // arrange
        var wallet = CreateTestWallet();
        var initialBalance = wallet.Balance;
        var depositAmount = 500;
        var expectedBalance = initialBalance + depositAmount;

        // act
        wallet.Deposit(depositAmount);

        Assert.Equal(expectedBalance, wallet.Balance);
    }

    [Fact]
    public void TransferTo_WithSufficientFunds_DecreasesSourceAndIncreasesDestination()
    {
        // arrange
        var sourceWallet = CreateTestWallet();
        var destWallet = CreateTestWallet();
        var initialSourceBalance = sourceWallet.Balance;
        var initialDestBalance = destWallet.Balance;
        var transferAmount = 200;

        // act
        sourceWallet.TransferTo(destWallet, transferAmount);

        Assert.Equal(initialSourceBalance - transferAmount, sourceWallet.Balance);
        Assert.Equal(initialDestBalance + transferAmount, destWallet.Balance);
    }
}

