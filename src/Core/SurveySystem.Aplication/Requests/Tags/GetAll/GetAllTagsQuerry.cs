using MediatR;
using SurveySystem.Requests.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Aplication.Requests.Tags.GetAll
{
    public class GetAllTagsQuerry : IRequest<GetAllTagsResponse>
    {
    }
}
