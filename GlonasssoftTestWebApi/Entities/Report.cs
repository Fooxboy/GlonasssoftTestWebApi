using System.ComponentModel.DataAnnotations;

namespace GlonasssoftTestWebApi.Entities
{
    /// <summary>
    /// Сущность отчета
    /// </summary>
    public class Report
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Владелец отчета
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Идентификатор владельца отчета
        /// </summary>

        public Guid UserId { get; set; }
        
        /// <summary>
        /// Количество входов пользователя
        /// </summary>
        public uint CountSignIn { get; set; }

    }
}
