namespace RestaurantCP
{
    class Drink : Product
    {
        private double calories;

        public double Milliliters { get; set; }
        public double Calories
        {
            get { return calories; }
            set { calories = Milliliters * 1; }
        }

        public Drink(string name, double price, double milliliters) : base(name, price)
        {
            Milliliters = milliliters;
        }
    }
}
