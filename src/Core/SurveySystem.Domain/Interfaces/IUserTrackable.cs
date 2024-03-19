namespace SurveySystem.Domain.Interfaces
{
    /// <summary>
    /// Сущность, для которой сохраняются данные о пользователе, создавшем и изменившем ее
    /// </summary>
    public interface IUserTrackable
    {
        /// <summary>
        /// Пользователь, создавший сущность
        /// </summary>
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Пользователь, изменивший сущность
        /// </summary>
        public Guid ModifiedByUserId { set; }
    }
}
