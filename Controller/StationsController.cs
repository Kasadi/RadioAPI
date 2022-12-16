using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioAPI.Data;
using RadioAPI.Data.Dto.StationFolder;
using RadioAPI.Data.Requests.Station;
using RadioAPI.Model;
using static RadioAPI.Program;

namespace RadioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : ControllerBase
    {
       

        private readonly RadioDbContext _context;

        public StationsController(RadioDbContext context)
        {
            _context = context;
        }

        // GET: api/Stations
        [HttpGet("/Stations/{page},{items}")]
        public async Task<ActionResult<StationIsLastDto>> GetStations(int page, int items, ChooseOrder chooseOrder = ChooseOrder.NameAsc)
        {

            if (items > 100)
            {
                items = 100;
            }


            var last = false;
            List<Station> stations;


            if (chooseOrder == ChooseOrder.NameAsc)
            {
                stations = await _context.Station
                    .OrderBy(s => s.Name)
                    .Skip(page * items)
                    .Take(items)
                    .ToListAsync();

                if (stations.Last() == _context.Station.OrderBy(s => s.Name).Last())
                {
                    last = true;
                }
            }
            else
            {
                stations = await _context.Station
                    .OrderByDescending(s => s.Name)
                    .Skip(page * items)
                    .Take(items)
                    .ToListAsync();

                if (stations.Last() == _context.Station.OrderByDescending(s => s.Name).Last())
                {
                    last = true;
                }
            }


            return new StationIsLastDto { Stations = stations, IsLast = last };
        }




        //GET: api/Stations/5
        [HttpGet("/SearchByShow")]
        public IQueryable<StationSearchDto> SearchByShow(string searched)
        {
            var query =
                from station in _context.Station
                join show in _context.Show on station.Id equals show.StationId
                where show.Name.Contains(searched)
                select new StationSearchDto { StationName = station.Name };

            return query;
        }

        // PUT: api/Stations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStation(int id, StationChangeRequest stationChange)
        {
            var cs = new Station { Id = id, Name = stationChange.From, Chanel = stationChange.To };
            _context.Station.Attach(cs);
            var entiry = _context.Entry(cs);
            entiry.Property(x => x.Name).IsModified = true;
            entiry.Property(x => x.Chanel).IsModified = true;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Debug.WriteLine(e.Message);
                if (!StationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // POST: api/Stations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("/NewStation")]
        public async Task<ActionResult<Station>> AddNewStation(NewStationRequest station)
        {
            _context.Station.Add(new Station { Name = station.Name, Chanel = station.Channel, Image = station.Image });
            await _context.SaveChangesAsync();


            var values = new StationPageItemRequest { Page = 0, Items = 10 };

            return CreatedAtAction("GetStations", values, station);
        }

        // DELETE: api/Stations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStation(int id)
        {
            var station = await _context.Station.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }

            _context.Station.Remove(station);
            await _context.SaveChangesAsync();

            return NoContent();
        }




        private bool StationExists(int id)
        {
            return _context.Station.Any(e => e.Id == id);
        }
    }
}
