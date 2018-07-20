using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BrightIdeas.Models;

namespace BrightIdeas.Controllers
{
    public class HomeController : Controller
    {
        private YourContext _context;

        public HomeController(YourContext context) {
            _context = context;
        }



        [HttpGet]
        [Route("Ideas")]
            public IActionResult Ideas()
        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            
            User CurrentUser = _context.Users.SingleOrDefault(user => user.Id == HttpContext.Session.GetInt32("id"));           
            ViewBag.CurrentUser = CurrentUser;    
            List<Idea> allIdeas =_context.Ideas
                                        .Include(idea => idea.Creator)
                                        .Include(idea => idea.Likers)
                                            .ThenInclude(like => like.Liker)
                                        .ToList();

            ViewBag.allIdeas = allIdeas.OrderByDescending(sec => sec.Likers.Count);

            ViewBag.CurrentTime = DateTime.Now;        
            return View();
        }

        [HttpPost]
        [Route("addIdea")]
        public IActionResult addIdea(Idea idea)
        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            User CurrentUser = _context.Users.SingleOrDefault(user => user.Id == HttpContext.Session.GetInt32("id"));           
            ViewBag.User = CurrentUser;
           

            if(ModelState.IsValid)
            {
                idea.Creator = CurrentUser;
                _context.Add(idea);
                _context.SaveChanges();



                return Redirect("Ideas");
            }
            TempData["IdeaError"] = "Please enter between 8 and 255 characters";
            ModelState.AddModelError("Content", "Please enter between 8 and 255 characters");
            return Redirect("Ideas");

        }

        [HttpGet]
        [Route("Like/{idea_id}")]
        public IActionResult Like(int idea_id)
        
        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            
             User CurrentUser = _context.Users.SingleOrDefault(user => user.Id == user_id);
             Idea CurrentIdea = _context.Ideas.SingleOrDefault(idea => idea.Id == idea_id);
            

            Like liked = new Like();
            liked.Liker =CurrentUser;
            liked.UserId = CurrentUser.Id;
            liked.IdeaId = idea_id;
            liked.Idea = CurrentIdea;
            _context.Add(liked);
            _context.SaveChanges();

            return RedirectToAction("Ideas");

        }

        [HttpGet]
        [Route("Delete/{idea_id}")]
        public IActionResult Delete(int idea_id)
        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            
            Idea CurrentIdea = _context.Ideas.SingleOrDefault(idea => idea.Id == idea_id);         
            _context.Remove(CurrentIdea);
            _context.SaveChanges();
        
            return RedirectToAction("Ideas");
        }

        [HttpGet]
        [Route("Profile/{creator_id}")]

        public IActionResult Profile(int creator_id )

        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            User CreatorProfile = _context.Users.SingleOrDefault(user => user.Id == creator_id);
            ViewBag.CreatorProfile = CreatorProfile; 

            List<Idea> totalPosts =_context.Ideas
                            .Include(idea => idea.Creator)
                            .Include(idea => idea.Likers)
                                .ThenInclude(like => like.Liker)
                            .ToList();
                        
            int postcounter = 0;
            foreach (var item in totalPosts)
            {
                
                if(item.Creator.Id == creator_id){
                    postcounter++;
                }

            }
            ViewBag.Totalposts = postcounter;

            List<Like> totalLikes = _context.Likes
                                    .Include(idea => idea.Liker).ToList();
           

            int likecounter =0;
            foreach (var like in totalLikes)
            {
                if(like.Liker.Id == creator_id)
                {
                    likecounter++;
                }
            }
            ViewBag.Totallikes = likecounter;
            


            return View("Profile");
        }
        



        [HttpGet]
        [Route("LikeList/{idea_id}")]
         public IActionResult LikeList(int idea_id )
         
        {
            int? user_id = HttpContext.Session.GetInt32("id");
            if(user_id == null){
                return Redirect("/");
            }
            

            Idea Idea= _context.Ideas.SingleOrDefault(idea => idea.Id == idea_id);
            System.Console.WriteLine("*****************" +Idea.UserId);

           User User = _context.Users.SingleOrDefault(user =>user.Id == Idea.UserId);

            List<Idea> Idealist =_context.Ideas.Where(a=>a.Id == idea_id)
                            .Include(idea => idea.Likers)
                                .ThenInclude(like => like.Liker)
                            .ToList();
            ViewBag.Idealist = Idealist;
            ViewBag.Idea = Idea;
            ViewBag.User = User;
         

            return View("LikeList");
        }   
        

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
