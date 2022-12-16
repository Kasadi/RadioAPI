using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RadioAPI.Model
{
    public class AnonymUser
    {


        [JsonIgnore]
        [Key]
        public int Id { get; set; }

        public long UserId { get; set; }

        [JsonIgnore]
        public List<Station> Station { get; set; } = new List<Station>();

    }
}
