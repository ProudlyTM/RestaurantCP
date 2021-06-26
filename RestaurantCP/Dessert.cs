namespace RestaurantCP
{
    class Dessert : Product
    {
        private double calories;

        public double Grams { get; set; }
        public double Calories
        {
            get { return calories; }
            set { calories = Grams * 1.5; } 
        }

        public Dessert(string name, double price, double grams) : base(name, price)
        {
            Grams = grams;
        }
    }
}
