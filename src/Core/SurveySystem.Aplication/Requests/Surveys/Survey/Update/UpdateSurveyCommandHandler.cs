using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.Update
{
    public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand>
    {
        private readonly IDbContext _dbContext;

        public UpdateSurveyCommandHandler(IDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
        {   
            ArgumentNullException.ThrowIfNull(request);

            if (request.TestQuestions == null || request.TestQuestions.Count() < 1)
                throw new BadDataException("Невозможно создать анкету, если вопросов меньше 1");

            var survey = await _dbContext.Surveys.Include(s => s.Questions)
                .FirstOrDefaultAsync(x => x.Id == request.Id)                
                ?? throw new NotFoundException("анкеты не была найдена");

            UpdateSurveyInfo(survey, request);
            UpdateSurveyTestQuestions(survey, request);

            await _dbContext.SaveChangesAsync();

            return default;
        }


        private async void UpdateSurveyInfo(Domain.Entities.Surveys.Survey survey, UpdateSurveyCommand request)
        {
            var _tags = request.TagsId == null ? null :
                await _dbContext.Tags.Where(i => request.TagsId.Contains(i.Id)).ToListAsync();
            if (_tags?.Count != request.TagsId?.Count)
                throw new NotFoundException("Не все теги были найдены в базе данных!");

            var _institutes = request.InstitutesId == null ? null :
                await _dbContext.Institutes.Where(i => request.InstitutesId.Contains(i.Id)).ToListAsync();
            if (_institutes?.Count != request.InstitutesId?.Count)
                throw new NotFoundException("Не все институты были найдены в базе данных!");

            var _faculties = request.FacultiesId == null ? null :
                await _dbContext.Faculties.Where(i => request.FacultiesId.Contains(i.Id)).ToListAsync();
            if (_faculties?.Count != request.FacultiesId?.Count)
                throw new NotFoundException("Не все кафедры были найдены в базе данных!");

            var _semesters = request.Semesters == null ? null :
                await _dbContext.Semesters.Where(i => request.Semesters.Contains(i.Number)).ToListAsync();

            if (_semesters?.Count != request.Semesters?.Count)
                throw new NotFoundException("Не все семестры были найдены в базе данных!");

            survey.UpdateInfo(name: request.Name, startDate: request.StartDate, isRepetable: request.IsRepetable, isVisible: request.IsVisible,
                institutes: _institutes, faculties: _faculties, semesters: _semesters, tags: _tags);
        }

        private async void UpdateSurveyTestQuestions(Domain.Entities.Surveys.Survey survey, UpdateSurveyCommand request)
        {
            List<SurveyTestQuestion> surveyTestQuestionsList = new List<SurveyTestQuestion>();

            var testQuestionsDictionary = request.TestQuestions
                .ToDictionary(question => question.QuestionId, question => question.Position);

            var listToDelete = new List<Guid>();
            foreach (var surveyQuestion in survey.Questions!)
            {
                if (testQuestionsDictionary.ContainsKey(surveyQuestion.QuestionId))
                {
                    surveyQuestion.Position = testQuestionsDictionary[surveyQuestion.QuestionId];
                    surveyTestQuestionsList.Add(surveyQuestion);
                    testQuestionsDictionary.Remove(surveyQuestion.QuestionId);
                }
                else
                    listToDelete.Add(surveyQuestion.QuestionId);
            }

            if (listToDelete.Count() > 1)
            {
                var questionsToDelete = await _dbContext.SurveysTestQuestions
                    .Where(x => x.SurveyId == survey.Id && listToDelete.Contains(x.QuestionId))
                    .ToListAsync();
                _dbContext.SurveysTestQuestions.RemoveRange(questionsToDelete);
            }

            var questions = await _dbContext.Questions.Where(q => testQuestionsDictionary.ContainsKey(q.Id)).ToListAsync();

            if (questions.Count != testQuestionsDictionary.Count)
                throw new ExceptionBase("Не все вопросы были найдены в базе данных!");

            foreach (var quesiton in questions)
            {
                var newQuestion = new SurveyTestQuestion(testQuestionsDictionary[quesiton.Id], survey, quesiton);
                _dbContext.SurveysTestQuestions.Add(newQuestion);
                surveyTestQuestionsList.Add(newQuestion);
            }

            survey.UpdateSurveyQuestions(surveyTestQuestionsList);
        }
    }
}
