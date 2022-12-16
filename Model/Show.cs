using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RadioAPI.Model
{
    public class Show
    {



        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }





        public int StationId { get; set; }







    }
}
