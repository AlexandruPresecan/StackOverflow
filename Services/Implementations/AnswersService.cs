﻿using Microsoft.EntityFrameworkCore;
using StackOverflow.Data;
using StackOverflow.DTOs;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public class AnswersService : IService<int, AnswerDTO>
    {
        private readonly ApplicationDbContext _db;

        public AnswersService(ApplicationDbContext db)
        {
            _db = db;
        }

        public ServiceResult Get()
        {
            return new ServiceResult
            (
                _db.Answers
                    .Include(a => a.Votes)
                    .Include(a => a.Author)
                    .Select
                    (
                        a => new
                        {
                            Id = a.Id,
                            Author = new
                            {
                                Id = a.AuthorId,
                                UserName = a.Author.UserName,
                                Email = a.Author.Email,
                                Score = a.Author.Score
                            },
                            Text = a.Text,
                            CreationDate = a.CreationDate,
                            VoteCount = a.VoteCount,
                            QuestionId = a.QuestionId
                        }
                    )
            );
        }

        public ServiceResult Get(int id)
        {
            Answer? answer = _db.Answers
                .Include(a => a.Votes)
                .Include(a => a.Author)
                .Include(a => a.Question)
                .FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            return new ServiceResult
            (
                new
                {
                    Id = answer.Id,
                    Author = new
                    {
                        Id = answer.AuthorId,
                        UserName = answer.Author.UserName,
                        Email = answer.Author.Email,
                        Score = answer.Author.Score
                    },
                    Text = answer.Text,
                    CreationDate = answer.CreationDate,
                    VoteCount = answer.VoteCount,
                    Question = new
                    {
                        Id = answer.Question.Id,
                        Author = new
                        {
                            Id = answer.Question.AuthorId,
                            UserName = answer.Question.Author.UserName,
                            Email = answer.Question.Author.Email,
                            Score = answer.Question.Author.Score
                        },
                        Title = answer.Question.Title,
                        CreationDate = answer.Question.CreationDate,
                        VoteCount = answer.Question.VoteCount,
                        Tags = answer.Question.QuestionTags.Select(qt => qt.Tag.Name)
                    }
                }
            );
        }

        public ServiceResult Post(AnswerDTO value)
        {
            if (_db.Users.FirstOrDefault(u => u.Id == value.AuthorId) == null)
                return new ServiceResult("Author Id not found", false);

            if (_db.Questions.FirstOrDefault(q => q.Id == value.QuestionId) == null)
                return new ServiceResult("Question Id not found", false);

            Answer answer = new Answer()
            {
                AuthorId = value.AuthorId,
                QuestionId = value.QuestionId,
                CreationDate = DateTime.Now,
                Text = value.Text
            };

            _db.Answers.Add(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer created");
        }

        public ServiceResult Put(int id, AnswerDTO value)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            answer.Text = value.Text == null ? answer.Text : value.Text;

            _db.Answers.Update(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer updated");
        }

        public ServiceResult Delete(int id)
        {
            Answer? answer = _db.Answers.FirstOrDefault(a => a.Id == id);

            if (answer == null)
                return new ServiceResult("Answer Id not found", false);

            _db.Answers.Remove(answer);
            _db.SaveChanges();

            return new ServiceResult("Answer deleted");
        }
    }
}