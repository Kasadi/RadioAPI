using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RadioAPI.Model
{
    public class Station
    {


        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Chanel { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Image { get; set; } = string.Empty;



        public List<Show> Shows { get; set; } = new List<Show>();

        public List<User> User { get; set; } = new List<User>();

        public List<AnonymUser> AnonymUser { get; set; } = new List<AnonymUser>();

    }
}