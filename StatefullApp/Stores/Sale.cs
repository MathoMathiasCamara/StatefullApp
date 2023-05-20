namespace StatefullApp.Stores
{
    public class Sale
    {
        public int Id { get; set; } 
        public int Qty { get; set; }

        public Product Product { get; set; }

        public override string ToString()
        {
            return $"Id : {Id}, Product : {Product.Name}, Qty : {Qty}";
        }
    }
}
