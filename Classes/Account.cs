public class Account
    {
        public string? Name { get; set; }
        public List<Transaction> MoneyIn = new List<Transaction>();
        public List<Transaction> MoneyOut = new List<Transaction>();


        public float TotalMoneyIn(List<Transaction> transactions)
        {

            float total = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.To == Name)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        } 
        
        
        public float TotalMoneyOut(List<Transaction> transactions)
        {

            float total = 0;
            foreach (var transaction in transactions)
            {
                if (transaction.From == Name)
                {
                    total += transaction.Amount;
                }
            }
            return total;
        } 
    
    }