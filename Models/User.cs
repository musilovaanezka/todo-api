using System.ComponentModel.DataAnnotations.Schema;

namespace TODOApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [ForeignKey("PasswordId")]
        public int PasswordId { get; set; }
        public UserPassword Password { get; set; }
    }
}
