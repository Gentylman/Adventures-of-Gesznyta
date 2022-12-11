namespace RpgGame.Models
{
    public class Equipment
    {
        public Guid PlayerId { get; set; }
        public List<Items> Items { get; set; } = new List<Items>();


    }
}
