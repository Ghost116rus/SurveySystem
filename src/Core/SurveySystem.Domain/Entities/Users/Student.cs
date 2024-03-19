using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Entities.Users
{
    public class Student : BaseEntity
    {

        private Faculty? _faculty;
        public DateOnly StartDateOfLearning { get; set; }
    }
}
