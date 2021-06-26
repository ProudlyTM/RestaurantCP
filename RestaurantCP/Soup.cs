namespace RestaurantCP
{
    class Soup : Product
    {
        public double Grams { get; set; }

        public Soup(string name, double price, double grams) : base(name, price)
        {
            Grams = grams;
        }
    }
}
