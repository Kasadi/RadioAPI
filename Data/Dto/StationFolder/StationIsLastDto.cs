using RadioAPI.Model;

namespace RadioAPI.Data.Dto.StationFolder
{
    public class StationIsLastDto
    {


        public List<Station> Stations { get; set; } = new List<Station>();
        public bool IsLast { get; set; }
    }
}
