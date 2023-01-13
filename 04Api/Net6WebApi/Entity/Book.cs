namespace Net6WebApi.Entity
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return $"Id={Id},Name={Name},Price={Price}";
        }
    }
}
