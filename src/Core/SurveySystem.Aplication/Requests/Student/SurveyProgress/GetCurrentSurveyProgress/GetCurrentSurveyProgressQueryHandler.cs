using MediatR;
using Microsoft.EntityFrameworkCore;
using SurveySystem.Aplication.Interfaces;
using SurveySystem.Domain.Exceptions;
using SurveySystem.Requests.Students.StudentProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Student.SurveyProgress.GetCurrentSurveyProgress
{
    public class GetCurrentSurveyProgressQuerryHandler : IRequestHandler<GetCurrentSurveyProgressQuerry, ProgressDTO>
    {
        private readonly IDbContext _dbContext;
        private readonly IUserContext _userContext;

        public GetCurrentSurveyProgressQuerryHandler(IDbContext dbContext, IUserContext userContext)
        {
            _dbContext = dbContext;
            _userContext = userContext;
        }


        public async Task<ProgressDTO> Handle(GetCurrentSurveyProgressQuerry request, CancellationToken cancellationToken)
        {
            var progress = await _dbContext.SurveyProgress.Include(sP => sP.Survey)
                .ThenInclude(s => s.Questions).FirstOrDefaultAsync() ??
                throw new NotFoundException("Прогресс прохождения не найден");

            if (progress.StudentId != _userContext.CurrentUserId && _userContext.CurrentUserRole == Domain.Enums.Role.Student)
                throw new AuthentificateException("Вы не можете получить не свой прогресс прохождения");




            return null;
        }
    }
}
