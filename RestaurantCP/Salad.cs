namespace RestaurantCP
{
    class Salad : Product
    {
        public double Grams { get; set; }

        public Salad(string name, double price, double grams) : base(name, price)
        {
            Grams = grams;
        }
    }
}
