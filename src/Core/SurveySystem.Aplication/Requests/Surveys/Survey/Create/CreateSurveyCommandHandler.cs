using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Entities.Surveys;
using SurveySystem.Domain.Exceptions;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.Create
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, Guid>
    {
        private readonly IDbContext _dbContext;

        public CreateSurveyCommandHandler(IDbContext dbContext)
            => _dbContext = dbContext;
        

        public async Task<Guid> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {   
            ArgumentNullException.ThrowIfNull(request);

            if (request.TestQuestions == null || request.TestQuestions.Count() < 1)
                throw new BadDataException("Невозможно создать анкету, если вопросов меньше 1");

            var testQuestionsIds = request.TestQuestions.Select(x => x.QuestionId);

            var questions = await _dbContext.Questions.Where(q => testQuestionsIds.Contains(q.Id))
                .Select(q => new { Question = q, Postion = request.TestQuestions.FirstOrDefault(r => r.QuestionId == q.Id)!.Position}).ToListAsync();

            if (questions.Count != request.TestQuestions.Count)
                throw new ExceptionBase("Не все вопросы были найдены в базе данных!");

            var newSurvey = await CreateSurvey(request);

            _dbContext.Surveys.Add(newSurvey);

            var questionList = new List<SurveyTestQuestion>();

            foreach (var item in questions)
            {
                var newQuestion = new SurveyTestQuestion(item.Postion, newSurvey, item.Question);
                questionList.Add(newQuestion);
                _dbContext.SurveysTestQuestions.Add(newQuestion);
            }

            newSurvey.UpdateSurveyQuestions(questionList); 

            await _dbContext.SaveChangesAsync();

            return newSurvey.Id;
        }


        private async Task<Domain.Entities.Surveys.Survey> CreateSurvey(CreateSurveyCommand request)
        {
            var tags = request.TagsId == null ? null :
                await _dbContext.Tags.Where(i => request.TagsId.Contains(i.Id)).ToListAsync();
            if (tags?.Count != request.TagsId?.Count)
                throw new NotFoundException("Не все теги были найдены в базе данных!");

            var institutes = request.InstitutesId == null ? null :
                await _dbContext.Institutes.Where(i => request.InstitutesId.Contains(i.Id)).ToListAsync();
            if (institutes?.Count != request.InstitutesId?.Count)
                throw new NotFoundException("Не все институты были найдены в базе данных!");

            var faculties = request.FacultiesId == null ? null :
                await _dbContext.Faculties.Where(i => request.FacultiesId.Contains(i.Id)).ToListAsync();
            if (faculties?.Count != request.FacultiesId?.Count)
                throw new NotFoundException("Не все кафедры были найдены в базе данных!");

            var semesters = request.Semesters == null ? null :
                await _dbContext.Semesters.Where(i => request.Semesters.Contains(i.Number)).ToListAsync();

            if (semesters?.Count != request.Semesters?.Count)
                throw new NotFoundException("Не все семестры были найдены в базе данных!");

            return new Domain.Entities.Surveys.Survey
                (name: request.Name, startDate: request.StartDate, isRepetable: request.IsRepetable, isVisible: request.IsVisible,
                institutes: institutes, faculties: faculties, semesters: semesters, tags: tags);
        }
    }
}
