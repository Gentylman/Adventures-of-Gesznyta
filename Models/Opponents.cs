using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Opponents
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public int Damage { get; set; }
        public int Swiftness { get; set; }
        public int Health { get; set; }



    }
}
