using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.StudentProgress;

namespace SurveySystem.Aplication.Requests.Student.SurveyProgress.GetAllSurveyProgresses
{
    public class GetAllSurveyProgressesQueryHandler : IRequestHandler<GetAllSurveyProgressesQuery, GetAllStudentProgressResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetAllSurveyProgressesQueryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<GetAllStudentProgressResponse> Handle(GetAllSurveyProgressesQuery request, CancellationToken cancellationToken)
        {
            if (_userContext.CurrentUserRole != Domain.Enums.Role.Student)
                throw new BadDataException("Прогресс может быть только у студентов");

            var student = await _dbContext.Students.Include(u => u.Progresses).ThenInclude(sP => sP.Survey).FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId) ??
                throw new NotFoundException("Пользователь не был найден");

            var studentProgressList = new List<LightStudentProgressDTO>();

            foreach (var progress in student.Progresses)
            {
                if (progress.Survey == null)
                    throw new ExceptionBase("У прогресса оказалась пустая ссылка на Анкету\n, Обратитесь к администратору.");

                studentProgressList.Add(new LightStudentProgressDTO()
                {
                    Id = progress.Id,
                    SurveyName = progress.Survey!.Name,
                    IsCompleted = progress.IsCompleted,
                });
            } 

            return new GetAllStudentProgressResponse() { LightStudentProgressDTOs = studentProgressList };
        }
    }
}
