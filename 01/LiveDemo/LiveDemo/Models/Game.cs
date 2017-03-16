namespace LiveDemo.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Size { get; set; }

        public virtual User Owner { get; set; }
    }
}