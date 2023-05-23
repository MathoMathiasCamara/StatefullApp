using System.Text;

namespace StatefullAPI.Stores
{
    public class Sale
    {
        public int Id { get; set; }
        public string Client { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Id : {Id}, Client : {Client}");
            foreach (var item in SaleItems)
            {
                stringBuilder.AppendLine($"Item : {item.ToString()}");
            }

            return stringBuilder.ToString();
        }
    }
}
