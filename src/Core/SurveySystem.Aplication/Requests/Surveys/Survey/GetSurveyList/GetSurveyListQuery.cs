using MediatR;
using SurveySystem.Requests.Surveys.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Surveys.Survey.GetSurveyList
{
    public class GetSurveyListQuery : GetSurveysListRequest, IRequest<GetSurveysListResponse>
    {
    }
}
