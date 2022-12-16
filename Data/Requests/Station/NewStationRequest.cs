namespace RadioAPI.Data.Requests.Station
{
    public class NewStationRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Channel { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
