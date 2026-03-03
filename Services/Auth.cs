using SafeBanking.Data;
using SafeBanking.Models;
using BCrypt.Net;
using System.Globalization;
using SafeBanking.UI.Prompt;

namespace SafeBanking.Services
{
    public static class Auth
    {
        private const string BankCode = "021";

        private static string GenerateAccountNumber(AppDbContext context)
        {
            var random = new Random();
            string accountNumber;

            do
            {
                int number = random.Next(0, 100000);
                accountNumber = $"{BankCode}{number:D5}";
            }
            while (context.Accounts.Any(a => a.AccountNumber == accountNumber));

            return accountNumber;
        }

        public static void SignUp()
        {
            using var context = new AppDbContext();

            Console.WriteLine("Full name:");
            string? rawName = Console.ReadLine();
            string fullName = NullRm.Sanitize(rawName);

            Console.WriteLine("CPF:");
            string? cpf = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(cpf))
            {
                Response.InvalidInput();
                return;
            }

            if (context.Accounts.Any(a => a.Cpf == cpf))
            {
                Response.Error("CPF already registered.");
                return;
            }

            Console.WriteLine("Birth date (yyyy-MM-dd):");
            string? birthInput = Console.ReadLine();

            if (!DateOnly.TryParseExact(
                birthInput,
                "yyyy-MM-dd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateOnly birthDate))
            {
                Response.InvalidDate();
                return;
            }

            Console.WriteLine("Income:");
            if (!decimal.TryParse(Console.ReadLine(), out decimal income) || income < 0)
            {
                Response.InvalidInput();
                return;
            }

            Console.WriteLine("Login password:");
            string? password = Console.ReadLine();

            Console.WriteLine("Four digits operation password:");
            string? opPassword = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(opPassword))
            {
                Response.InvalidInput();
                return;
            }

            var account = new Account
            {
                Name = fullName,
                Cpf = cpf,
                BirthDate = birthDate,
                Income = income,
                Balance = 0,
                UsedCredit = 0,
                AccountNumber = GenerateAccountNumber(context),
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(password),
                HashedOpPassword = BCrypt.Net.BCrypt.HashPassword(opPassword)
            };

            context.Accounts.Add(account);
            context.SaveChanges();

            Response.Success($"Account created! Number: {account.AccountNumber}");
        }

        public static Account? LogIn()
        {
            using var context = new AppDbContext();

            Console.WriteLine("Account number:");
            string? accountNumber = Console.ReadLine();

            Console.WriteLine("Password:");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(accountNumber) ||
                string.IsNullOrWhiteSpace(password))
            {
                Response.InvalidCredentials();
                return null;
            }

            var account = context.Accounts
                .FirstOrDefault(a => a.AccountNumber == accountNumber);

            if (account == null ||
                !BCrypt.Net.BCrypt.Verify(password, account.HashedPassword))
            {
                Response.InvalidCredentials();
                return null;
            }

            Response.Success("Login successful.");
            UI.Prompt.Menu.PromptMenu(account);
            return account;
        }

        public static void DeleteAccount()
        {
            using var context = new AppDbContext();

            Console.WriteLine("Account number:");
            string? number = Console.ReadLine();

            Console.WriteLine("Password:");
            string? password = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(number) ||
                string.IsNullOrWhiteSpace(password))
            {
                Response.InvalidCredentials();
                return;
            }

            var account = context.Accounts
                .FirstOrDefault(a => a.AccountNumber == number);

            if (account == null ||
                !BCrypt.Net.BCrypt.Verify(password, account.HashedPassword))
            {
                Response.InvalidCredentials();
                return;
            }

            if (account.Balance > 0)
            {
                Response.Error("Account cannot be deleted: balance is not zero.");
                return;
            }

            if (account.UsedCredit > 0)
            {
                Response.Error("Account cannot be deleted: outstanding credit in use.");
                return;
            }

            context.Accounts.Remove(account);
            context.SaveChanges();

            Response.Success("Account deleted successfully.");
        }
    }
}