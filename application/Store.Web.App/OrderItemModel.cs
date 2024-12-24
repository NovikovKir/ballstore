namespace BallStore.Models
{
    public class OrderItemModel
    {
        public int BallId { get; set; }

        public string Brand {  get; set; }

        public string Model { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }
    }
}
