using SurveySystem.Domain.Entities.Base;
using SurveySystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveySystem.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        private string _login;
        private string _passwordHash;
        private Role _role;
        private Student _student;

        private string fullName;

        public Guid? StudentId { get; private set; }

        public Student? Student
        {
            get => _student;
            private set
            {
                ArgumentNullException.ThrowIfNull(value);

                StudentId = value.Id;
                _student = value;
            }
        }
    }
}
