using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;
using System.Text.RegularExpressions;

namespace StackOverflow.Services
{
    public class ApplicationUsersService : IService<string, ApplicationUserDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly TagsService _tagsService;
        private readonly AnswersService _answersService;
        private readonly QuestionsService _questionService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ApplicationUsersService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, TagsService tagsService, AnswersService answersService, QuestionsService questionsService, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _tagsService = tagsService;
            _answersService = answersService;
            _questionService = questionsService;
            _signInManager = signInManager;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _userManager.Users
                    .Select
                    (
                        u => new
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            Email = u.Email,
                            Score = u.Score,
                            Banned = u.Banned
                        }
                    )
            );
        }

        public ServiceResult Get(string id)
        {
            ApplicationUser? user = _userManager.Users
                .Include(u => u.Answers)
                .Include(u => u.Questions)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return new ServiceResult("User Id not found", false);

            return new ServiceResult
            (
                new 
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Score = user.Score,
                    Banned = user.Banned,
                    Questions = user.Questions?
                        .Select
                        (
                            q => new
                            {
                                Id = q.Id,
                                Title = q.Title,
                                Text = q.Text,
                                CreationDate = q.CreationDate,
                                VoteCount = _questionService.GetVoteCount(q.Id),
                                Tags = _tagsService.GetByQuestion(q)
                            }
                        ),
                    Answers = user.Answers?
                        .Select
                        (
                            a => new
                            {
                                Id = a.Id,
                                Text = a.Text,
                                CreationDate = a.CreationDate,
                                VoteCount = _answersService.GetVoteCount(a.Id),
                                QuestionId = a.QuestionId
                            }
                        )
                }
            );
        }

        public ServiceResult Post(ApplicationUserDTO value, HttpContext httpContext)
        {
            string emailPattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            
            if(!Regex.Match(value.Email.Trim(), emailPattern, RegexOptions.IgnoreCase).Success)
                return new ServiceResult("Invalid email address", false);

            if (value.Password != value.ConfirmPassword)
                return new ServiceResult("Passwords do not match", false);

            ApplicationUser user = new ApplicationUser()
            {
                UserName = value.UserName,
                Email = value.Email,
                Score = 0
            };

            IdentityResult identityResult = _userManager.CreateAsync(user, value.Password).Result;

            if (!identityResult.Succeeded)
                return new ServiceResult(identityResult.Errors, false);

            string token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
            identityResult = _userManager.ConfirmEmailAsync(user, token).Result;

            if (!identityResult.Succeeded)
                return new ServiceResult(identityResult.Errors, false);

            return new ServiceResult("User created");
        }

        public ServiceResult Put(string id, ApplicationUserDTO value, HttpContext httpContext)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return new ServiceResult("User Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != user.Id)
                return new ServiceResult("Cannot edit other accounts", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]))
            {
                SignInResult signInResult = _signInManager.PasswordSignInAsync(user.UserName, value.Password, false, false).Result;

                if (!signInResult.Succeeded)
                    return new ServiceResult("Invalid Password", false);
            }

            if (!string.IsNullOrEmpty(value.UserName))
            {
                IdentityResult identityResult = _userManager.SetUserNameAsync(user, value.UserName).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            if (!string.IsNullOrEmpty(value.Email))
            {
                string emailPattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";

                if (!Regex.Match(value.Email.Trim(), emailPattern, RegexOptions.IgnoreCase).Success)
                    return new ServiceResult("Invalid email address", false);

                IdentityResult identityResult = _userManager.SetEmailAsync(user, value.Email).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            if (!string.IsNullOrEmpty(value.NewPassword) && !string.IsNullOrEmpty(value.ConfirmPassword))
            {
                if (value.NewPassword != value.ConfirmPassword)
                    return new ServiceResult("Passwords do not match", false);

                IdentityResult identityResult = _userManager.ChangePasswordAsync(user, value.Password, value.NewPassword).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            return new ServiceResult("User updated");
        }

        public ServiceResult Delete(string id, HttpContext httpContext)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return new ServiceResult("User Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]) && httpContext.Items["UserId"]?.ToString() != user.Id)
                return new ServiceResult("Cannot delete other accounts", false);

            _userManager.DeleteAsync(user);

            return new ServiceResult("User deleted");
        }

        public void UpdateScore(string id, int value)
        {
            ApplicationUser? user = _db.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return;

            user.Score += value;

            _db.Users.Update(user);
            _db.SaveChanges();
        }

        public ServiceResult Authenticate(ApplicationUserDTO user)
        {
            SignInResult signInResult = _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false).Result;

            if (!signInResult.Succeeded)
                return new ServiceResult("Invalid Username or Password", false);

            ApplicationUser signedUser = _userManager.FindByNameAsync(user.UserName).Result;
            
            if (signedUser.Banned)
                return new ServiceResult("This account is banned", false);

            return new ServiceResult
            (
                new
                {
                    id = signedUser.Id,
                    userName = signedUser.UserName,
                    email = signedUser.Email,
                    token = TokenGenerator.Generate(signedUser),
                    admin = _userManager.IsInRoleAsync(signedUser, "Admin").Result
                }
            );
        }

        public ServiceResult Ban(string id, bool ban, HttpContext httpContext)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return new ServiceResult("User Id not found", false);

            if (!Convert.ToBoolean(httpContext.Items["Admin"]))
                return new ServiceResult("Admin permissions needed", false);

            user.Banned = ban;

            _db.Users.Update(user);
            _db.SaveChanges();

            if (ban)
                MailSender.Send(user);

            return new ServiceResult("User " + (ban ? "banned" : "unbanned"));
        }
    }
}
