namespace RestaurantCP
{
    class MainDish : Product
    {
        private double calories;

        public double Grams { get; set; }
        public double Calories
        {
            get { return calories; }
            set { calories = Grams * 3; }
        }

        public MainDish(string name, double price, double grams) : base(name, price)
        {
            Grams = grams;
        }
    }
}
