using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BrightIdeas.Models
{

    public class Like
    {
        [Key]
        public int Id {get;set;}
       
        public User Liker {get; set;}
        public Idea Idea {get;set;}
        public int UserId {get;set;}
        public int IdeaId {get;set;}


    }
}