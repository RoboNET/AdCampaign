using System.ComponentModel.DataAnnotations;
using AdCampaign.DAL.Entities;

namespace AdCampaign.Models
{
    public class EditUserRequest
    {
        [Required] 
        public long Id { get; set; }
        
        [Required] 
        public string Username { get; set; }
        
        [Required] 
        public string Password { get; set; }
        
        [Required] 
        public string Email { get; set; }
        
        [Required] 
        public string Phone { get; set; }
        
        [Required] 
        public Role Role { get; set; }
    }
}