using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public int Strength { get; set; }
        public int MagicPower { get; set; }
        public int Armor { get; set; }
        public int Dexterity { get; set; }
        public int Swiftness { get; set; }
        public int Health { get; set; }
        public byte[] ?ItemArray { get; set; }
        public Guid AccountId { get; set; }
        






    }
}
