using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Versioning;
using RpgGame.Data;
using RpgGame.Functions;
using RpgGame.Models;

namespace RpgGame.Controllers
{
    public class PlayersController : Controller
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly RpgContext _context;

        public PlayersController(IHttpContextAccessor httpContextAccessor,RpgContext context, UserManager<Account> userManager,SignInManager<Account> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              string name = _httpContextAccessor.HttpContext?.User.Identity.Name;

            if(name == null)
            {
                var user = _userManager.FindByNameAsync(name);
                
                if(user == null)
                {
                    return RedirectToAction(nameof(AccessDenied));
                }
            }
            else
            {
               
                var user = await _userManager.FindByNameAsync(name);
                ViewBag.currUser = user;
               
                return View();
            }
            return RedirectToAction(nameof(AccessDenied));

        }

        public async Task<IActionResult> Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login([Bind("UserName,Password")] Account account)
        {
            List<Account> accList = _context.playerAccounts.ToList();
            var user = _userManager.FindByNameAsync(account.UserName);

            string username = account.Name;
         
            Account acc = accList.Find(c => c.UserName == username);
            var PasswordResult = _userManager.PasswordHasher.VerifyHashedPassword(user.Result, user.Result.PasswordHash, account.Password);
            if (PasswordResult == PasswordVerificationResult.Success)
            {
                var result = _signInManager.PasswordSignInAsync(user.Result.UserName,account.Password,false,false);
                if (result.Result.Succeeded)
                {
               
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
               
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
            }
            


        }
        public async Task<IActionResult> Register()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Register([Bind("Name,Email,Password")] Account account)
        {
            Account acc = new Account {Name=account.Name, UserName = account.Name,Email=account.Email,AccountId = Guid.NewGuid()};

            var result = await _userManager.CreateAsync(acc,account.Password);
            if (result.Succeeded)
            {
                
                var defaultrole = _roleManager.FindByNameAsync("player").Result;
                IdentityResult roleResult = await _userManager.AddToRoleAsync(acc, defaultrole.Name);
                return RedirectToAction(nameof(Login));
                _context.Add(account);
                await _context.SaveChangesAsync();
            }
          

            
                return RedirectToAction(nameof(Register));
 
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            string Accname = _httpContextAccessor.HttpContext?.User.Identity.Name;
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> AccessPlayer()
        {
            List<Player> AllPlayersList = _context.players.ToList();
            string accName = _httpContextAccessor.HttpContext?.User.Identity.Name;

            var user = _userManager.FindByNameAsync(accName).Result;
            Guid accId = user.AccountId;

            List<Player> currPlayers = AllPlayersList.FindAll(c => c.AccountId == accId);
            ViewBag.currPlayers = currPlayers;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreatePlayer()
        {
            List<Classes> classes = _context.classes.ToList();
            ViewBag.classes = classes;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePlayer([Bind("Name,ClassId")] Player player)
        {
            List<Classes> classes = _context.classes.ToList();
            string accName = _httpContextAccessor.HttpContext?.User.Identity.Name;
            var user = _userManager.FindByNameAsync(accName).Result;
            ViewBag.classes = classes;
            Player newPlayer = PlayerCommands.CreatePlayer(player.Name, player.ClassId,user.AccountId);
            _context.players.Add(newPlayer);
            _context.SaveChanges();

            return View();
        }
        public async Task<IActionResult> MainPlayerView([Bind("Id")] Player PlayerIdObject)
        {
            Guid PlayerId = PlayerIdObject.Id;
            Player player = _context.players.Find(PlayerId);
            Opponents oppo = new Opponents();
            oppo.Health = 100;
            PlayerCommands.Fight(player,oppo);
           

            return View();
        }


    }
       
    }

