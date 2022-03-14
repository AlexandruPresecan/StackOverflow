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

        public ApplicationUsersService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
            _tagsService = new TagsService(db);
        }

        public ServiceResult Get()
        {
            _db.Questions.Include(q => q.Votes);
            _db.Answers.Include(a => a.Votes);

            return new ServiceResult
            (
                _userManager.Users
                    .Include(u => u.Answers)
                    .Include(u => u.Questions)
                    .Include(u => u.AnswerVotes)
                    .Include(u => u.QuestionVotes)
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
                .Include(u => u.AnswerVotes)
                .Include(u => u.QuestionVotes)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
                return new ServiceResult("User Id not found", false);

            _db.Questions.Include(q => q.Votes);
            _db.Answers.Include(a => a.Votes);

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
                                CreationDate = q.CreationDate,
                                VoteCount = q.VoteCount,
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
                                VoteCount = a.VoteCount,
                                QuestionId = a.QuestionId
                            }
                        )
                }
            );
        }

        public ServiceResult Post(ApplicationUserDTO value)
        {
            IdentityResult identityResult = _userManager.CreateAsync
            (
                new ApplicationUser()
                {
                    UserName = value.UserName,
                    Email = value.Email
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
    }
}
