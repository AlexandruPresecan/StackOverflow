using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class ApplicationUsersService : IService<string, ApplicationUserDTO>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly TagsService _tagsService;
        private readonly AnswersService _answersService;
        private readonly QuestionsService _questionService;

        public ApplicationUsersService(ApplicationDbContext db, UserManager<ApplicationUser> userManager, TagsService tagsService, AnswersService answersService, QuestionsService questionsService)
        {
            _db = db;
            _userManager = userManager;
            _tagsService = tagsService;
            _answersService = answersService;
            _questionService = questionsService;
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
                            Score = u.Score
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
                    Questions = user.Questions
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
                    Answers = user.Answers
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

        public ServiceResult Post(ApplicationUserDTO value)
        {
            IdentityResult identityResult = _userManager
                .CreateAsync
                (
                    new ApplicationUser()
                    {
                        UserName = value.UserName,
                        Email = value.Email,
                        Score = 0
                    },
                    value.Password
                )
                .Result;

            if (identityResult.Succeeded)
                return new ServiceResult("User created");
            else
                return new ServiceResult(identityResult.Errors, false);
        }

        public ServiceResult Put(string id, ApplicationUserDTO value)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return new ServiceResult("User Id not found", false);

            if (value.UserName != null)
            {
                IdentityResult identityResult = _userManager.SetUserNameAsync(user, value.UserName).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            if (value.Email != null)
            {
                IdentityResult identityResult = _userManager.SetEmailAsync(user, value.Email).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            if (value.Password != null && value.NewPassword != null)
            {
                IdentityResult identityResult = _userManager.ChangePasswordAsync(user, value.Password, value.NewPassword).Result;

                if (!identityResult.Succeeded)
                    return new ServiceResult(identityResult.Errors, false);
            }

            return new ServiceResult("User updated");
        }

        public ServiceResult Delete(string id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(id).Result;

            if (user == null)
                return new ServiceResult("User Id not found", false);

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
    }
}
