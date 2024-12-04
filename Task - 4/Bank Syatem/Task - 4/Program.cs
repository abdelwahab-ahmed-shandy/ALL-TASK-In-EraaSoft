namespace Task___4
{
    public class Account
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Account(string name = "Null", decimal balance = 0)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("You Did Not Enter A Name");
            if (balance < 0)
                throw new Exception("Can Not Be Less Than Zero");
            Name = name;
            Balance = balance;
        }
        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
        public bool Withdrow(decimal amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount ;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name Is {Name} , Balance {Balance}";
        }
    }
    public static class AccountUntil
    {
        public static void DesplayAccount(List<Account> accounts)
        {
            Console.WriteLine("\n==== Account =====================================");
            foreach (var account in accounts)
            {
                Console.WriteLine(account);
            }
        }
        public static void DesplayDeposit(List<Account> accounts, decimal amount)
        {
            Console.WriteLine("\n==== Deposited To Accounts =====================================");
            foreach (var account in accounts)
            {
                if (account.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {account}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {account}");
            }
        }
        public static void DesplayWithdrow(List<Account> accounts, decimal amount)
        {
            Console.WriteLine("\n==== Withdrawing from Accounts =====================================");
            foreach (var account in accounts)
            {
                if (account.Withdrow(amount))
                    Console.WriteLine($"Withdrew {amount} from {account}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {account}");
            }
        }
    }
    public class SavingsAccount : Account
    {
        public decimal InterstRate { get; set; }
        public SavingsAccount(string name = "null", decimal balance = 0, decimal interstRate = 0) : base(name, balance)
        {
            if (InterstRate < 0)
                throw new ArgumentException("Can Not Be Less Than Zero");
            InterstRate = interstRate;
        }

        public override string ToString()
        {
            return $"{base.ToString()} Rate is {InterstRate}";
        }
    }
    public class CheckingAccount:Account
    {
        const decimal Fee = 1.50m;
        public CheckingAccount(string name = "Null", decimal balance = 0) : base(name, balance)
        {
            balance = balance - Fee;
        }
        public override string ToString()
        {
            return $"Name {Name} Balance {Balance - Fee}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Account 
            var accounts = new List<Account>();
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUntil.DesplayAccount(accounts);
            AccountUntil.DesplayDeposit(accounts, 1000);
            AccountUntil.DesplayWithdrow(accounts, 500);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0m));

                            // Thid new next search 
            var accountList = savAccounts.ConvertAll(account => (Account)account);
            AccountUntil.DesplayAccount(accountList);
            AccountUntil.DesplayDeposit(accountList, 1000);
            AccountUntil.DesplayWithdrow(accountList, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            var CheckList = checAccounts.ConvertAll(check => (Account)check);
            AccountUntil.DesplayAccount(CheckList);
            AccountUntil.DesplayDeposit(CheckList, 2000);
            AccountUntil.DesplayWithdrow(CheckList, 2000);

        }
    }
}
