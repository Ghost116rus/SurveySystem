﻿namespace SurveySystem.Requests.Students
{
    public class StudentProgressLightDTO
    {
        public Guid Id { get; set; }
        public string SurveyName { get; set; }

        public DateTime UpdatedTime { get; set; }
    }
}
