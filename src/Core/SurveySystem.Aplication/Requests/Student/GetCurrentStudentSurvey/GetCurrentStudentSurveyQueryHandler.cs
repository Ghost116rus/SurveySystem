﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Users;
using SurveySystem.Domain.Enums;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.GetCurrentStudentSurvey;

namespace SurveySystem.Aplication.Requests.Student.GetCurrentStudentSurvey
{
    public class GetCurrentStudentSurveyQueryHandler : IRequestHandler<GetCurrentStudentSurveyQuery, GetCurrentStudentSurveyResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;
        private readonly IQuestionService _questionService;

        public GetCurrentStudentSurveyQueryHandler(IDbContext dbContext, IUserContext userContext, IQuestionService questionService)
        {
            _dbContext = dbContext;
            _userContext = userContext;
            _questionService = questionService;
        }

        public async Task<GetCurrentStudentSurveyResponse> Handle(GetCurrentStudentSurveyQuery request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var studentProgress = await _dbContext.SurveyProgress
                .Include(x => x.Survey).ThenInclude(s => s.Questions).FirstOrDefaultAsync(x => x.Id == request.StudentSurveyId)
                ?? throw new NotFoundException("Заданный прогресс не найден");

            if (_userContext.CurrentUserId != studentProgress.StudentId &&
                _userContext.CurrentUserRoleName != Enum.GetName(Role.Administrator))
                throw new ForbibenException("У вас нет доступа");

            var allAnswers = await _dbContext.StudentAnswers
                .Include(sA => sA.Answer)
                .ThenInclude(a => a.Question)
                .ThenInclude(q => q.Answers)
                .Where(sA => sA.SurveyProgressId == studentProgress.Id)
                .OrderBy(sA => sA.CreatedOn)
                .ToListAsync();

            var answers = allAnswers
                .GroupBy(x => x.CreatedOn)
                .SelectMany(g => g.GroupBy(x => x.Answer.Question))
                .Select(x => new CurrentStudentSurveyTestQuestionDTO()
                {
                    Id = x.Key.Id,
                    QuestionText = x.Key.Text,
                    MaxCountOfAnswers = x.Key.MaxCountOfAnswers,
                    Type = x.Key.Type,
                    IsActual = x.First().IsActual,
                    AnswerTime = x.First().ModifiedOn.ToLocalTime().ToString(),
                    Answers = x.Key!.Answers!.OrderBy(x => x.PositionInQuestion).Select(a => new CurrentStudentSurveyQuestionAnswerDTO()
                    {
                        Id = a.Id,
                        Text = a.Text,
                        IsSelected = x.Any(sA => sA.AnswerId == a.Id)
                    }).ToList()
                }).ToList();

            CurrentStudentSurveyTestQuestionDTO? currentQuestion = await _questionService.GetCurrentQuestionDTOAsync(studentProgress);

            return new GetCurrentStudentSurveyResponse()
            {
                IsRepetable = studentProgress.Survey!.IsRepetable,
                IsCompleted = studentProgress.IsCompleted,
                Answers = answers,
                CurrentQuestion = currentQuestion
            };
        }
    }
}
