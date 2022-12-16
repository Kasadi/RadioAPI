using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RadioAPI.Model
{
    public class User
    {


        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]

        public string Email { get; set; } = string.Empty;

        [Required]
        [PasswordPropertyText]
        [StringLength(200)]
        public string Password { get; set; } = string.Empty;


        [JsonIgnore]
        public List<Station> Station { get; set; } = new List<Station>();




    }
}
