using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wedding.Models;

namespace wedding.Controllers
{
  public class HomeController : Controller
  {
    private weddingContext dbContext;
    public HomeController(weddingContext context)
    {
      dbContext = context;
    }

    [HttpGet("")]
    public IActionResult Home()
    {
      return View();
    }

    [HttpGet("index")]
    public IActionResult Index()
    {
      int? UserId = HttpContext.Session.GetInt32("UserId");
      if(User != null)
      {
        List<Wedding> AllWeddings = dbContext.Weddings
          .Include(w => w.Guests)
          .ThenInclude(a => a.User)
          .ToList();
        return View(AllWeddings);
      }
      return RedirectToAction("Home");
    }

    [HttpGet("add/wedding")]
    public IActionResult AddWedding()
    {
      int? userid = HttpContext.Session.GetInt32("UserId");
      if(userid == null)
      {
        return RedirectToAction("Home");
      }
      return View();
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Home");
    }

    [HttpGet("add/{userId}/{weddingId}")]
    public IActionResult AddUserToWedding(int userId, int weddingId)
    {
      Wedding oneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
      User oneUser = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
      RSVP newRSVP = new RSVP()
      {
        WeddingId = oneWedding.WeddingId,
        UserId = oneUser.UserId
      };
      dbContext.RSVPs.Add(newRSVP);
      dbContext.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("remove/{userId}/{weddingId}")]
    public IActionResult RemoveUserFromWedding(int userId, int weddingId)
    {
      Wedding oneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
      User oneUser = dbContext.Users.FirstOrDefault(u => u.UserId == userId);
      RSVP oneRSVP = dbContext.RSVPs.FirstOrDefault(r => r.UserId == userId && r.WeddingId == weddingId);

      oneWedding.Guests.Remove(oneRSVP);
      dbContext.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpGet("view/{weddingId}")]
    public IActionResult ViewWedding(int weddingId)
    {
      int? userId = HttpContext.Session.GetInt32("UserId");
      if(userId == null)
      {
        return RedirectToAction("Home");
      }
      List<RSVP> Users = dbContext.RSVPs
        .Include(u => u.User)
        .Where(u => u.WeddingId == weddingId)
        .ToList();

        Wedding oneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
        return View(oneWedding);
    }

    [HttpGet("delete/{weddingId}")]
    public IActionResult DeleteWedding(int weddingId)
    {
      Wedding oneWedding = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
      dbContext.Remove(oneWedding);
      dbContext.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost("register")]
    public IActionResult Register(Both newUser)
    {
      User user = newUser.user;
      if(ModelState.IsValid)
      {
        if(dbContext.Users.Any(u => u.Email == user.Email))
        {
          ModelState.AddModelError("user.Email", "Email is already in use!");
          return View("Home");
        }
        else
        {
          PasswordHasher<User> Hasher = new PasswordHasher<User>();
          user.Password = Hasher.HashPassword(newUser.user, user.Password);
          dbContext.Add(newUser.user);
          dbContext.SaveChanges();
          HttpContext.Session.SetInt32("UserId", user.UserId);
          return RedirectToAction("Index");
        }
      }
      else
      {
        return View("Home");
      }
    }

    [HttpPost("login")]
    public IActionResult Login(Both userSubmission)
    {
      LoginUser login = userSubmission.login;
      if(ModelState.IsValid)
      {
        var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == login.Email);

        if(userInDb == null)
        {
          ModelState.AddModelError("login.Email", "Invalid Email/Password");
          return View("Home");
        }
        var hasher = new PasswordHasher<LoginUser>();
        var result = hasher.VerifyHashedPassword(userSubmission.login, userInDb.Password, login.Password);

        if(result == 0)
        {
          ModelState.AddModelError("login.Email", "Invalid Email/Password");
          return View("Home");
        }
        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
        return RedirectToAction("Index");
      }
      else
      {
        return View("Home");
      }
    }

    [HttpPost("create/wedding")]
    public IActionResult CreateWedding(Wedding newWedding)
    {
      if(ModelState.IsValid)
      {
        int? userId = HttpContext.Session.GetInt32("UserId");
        newWedding.UserId =(int)userId;
        dbContext.Add(newWedding);
        dbContext.SaveChanges();
        return RedirectToAction("ViewWedding");
      }
      else
      {
        return View("AddWedding");
      }
    }


  }
}
