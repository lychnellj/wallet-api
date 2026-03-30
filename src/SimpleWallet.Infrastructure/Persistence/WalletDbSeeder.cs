using SimpleWallet.Domain.Entities;

namespace SimpleWallet.Infrastructure.Persistence;

public static class WalletDbSeeder
{
    public static void  Seed(WalletDbContext context)
    {
        if (context.Users.Any())
        {
            // if the db is already seeded
            return;
        }

        // seed data

        var user1 = new User("Alice", "alice@example.com");
        var user2 = new User("Bob", "bob@example.com");

        context.Users.AddRange(user1, user2);

        var wallet1 = new Wallet(user1.Id);
        var wallet2 = new Wallet(user2.Id);

        // var wallet1 = new SimpleWallet.Domain.Entities.Wallet(user1.Id);
        // var wallet2 = new SimpleWallet.Domain.Entities.Wallet(user1.Id);

        // fill seed wallets with balance
        wallet1.Deposit(1000);
        wallet2.Deposit(500);

        context.Wallets.AddRange(wallet1, wallet2);

        var t1 = new Transaction(wallet1.Id, 1000, Domain.Enums.TransactionType.Deposit);
        var t2 = new Transaction(wallet2.Id, 500, Domain.Enums.TransactionType.Deposit);

        context.Transactions.AddRange(t1, t2);

        context.SaveChanges();
    }
}
