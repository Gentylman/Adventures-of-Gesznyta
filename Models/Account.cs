
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RpgGame.Models
{
    public class Account:IdentityUser
    {
      

        [Key]
     
        public Guid AccountId { get; set; }
    
        public string Name { get; set; }
    
        public string? Password { get; set; }
       
        public string Email { get; set; }

    }
}
