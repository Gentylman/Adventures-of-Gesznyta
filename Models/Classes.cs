using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Classes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Power { get; set; }
    }
}
