using System.ComponentModel.DataAnnotations;

namespace ReviewExample.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя пользователя обязательно для заполнения.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Название организации обязательно для заполнения.")]
        public string OrganizationName { get; set; }

        public string Address { get; set; }

        [Required(ErrorMessage = "Укажите, что понравилось.")]
        public string Liked { get; set; }

        [Required(ErrorMessage = "Укажите, что не понравилось.")]
        public string Disliked { get; set; }

        public string Comments { get; set; }

        [Range(1, 5, ErrorMessage = "Оценка должна быть от 1 до 5.")]
        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
