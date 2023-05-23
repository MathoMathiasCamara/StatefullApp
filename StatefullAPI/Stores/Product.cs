namespace StatefullAPI.Stores
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Product other = obj as Product;
            return other.Id == Id && other.Name == Name;
        }

        public override string ToString()
        {
            return $"Id : {Id} , Name : {Name}";
        }
    }
}
