using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Items
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int Defense { get; set; }
        public int Dexterity { get; set; }
        public int Health { get; set; }


    }
}
