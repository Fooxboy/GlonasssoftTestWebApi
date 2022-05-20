using System.ComponentModel.DataAnnotations;

namespace GlonasssoftTestWebApi.Entities
{
    /// <summary>
    /// Сущность пользователя
    /// </summary>
    public class User
    {
        [Key]
        public Guid Id { get; set; }
    }
}
