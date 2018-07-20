using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{

    public class Idea
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter between 2 and 255 characters")]
        [MinLength(2),MaxLength(255)]
        public string Content {get; set;}
       
        
           
        public int UserId { get; set; } ///Column foreign key
        public User Creator { get; set; }
        
        public List<Like> Likers {get; set;}
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;
        
        
        public Idea(){
            Likers = new List<Like>();
            
            
            
        }
        

        
    }
}