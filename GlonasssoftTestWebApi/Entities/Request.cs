using System.ComponentModel.DataAnnotations;

namespace GlonasssoftTestWebApi.Entities
{
    /// <summary>
    /// Сущность запроса
    /// </summary>
    public class Request
    {
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Дата завершения обработки запроса
        /// </summary>
        public DateTime CompletionTime { get; set; } 

        /// <summary>
        /// Пользователь, отчет которого был запрошен
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отчет которого был запрошен
        /// </summary>
        public Guid UserId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
