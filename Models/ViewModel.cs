using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{

    public class ViewModel
    {
        public Idea Ideas {get; set;}
        public LoginCheck loginUser {get; set;}
        public Like Likes {get; set;}

        public User regUser {get; set;}
        public List<Idea>UserIdeas {get;set;}
        public List<Like> Likers {get; set;}
       
    }
    



}