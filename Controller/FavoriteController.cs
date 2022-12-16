using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RadioAPI.Data;
using RadioAPI.Data.Dto.StationFolder;
using RadioAPI.Data.Requests.Favorite;
using System.Diagnostics;
using static RadioAPI.Program;

namespace RadioAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly RadioDbContext _context;
        private readonly IMapper _mapping;

        public FavoriteController(RadioDbContext context, IMapper mapping)
        {
            _context = context;
            _mapping = mapping;
        }


        //ADD FAVORITE: api/Stations/AddFavorite
        [HttpPost("/AddFavoriteRegistered")]
        public List<FavoriteListDto> AddFavoriteRegistered(UserStationIdRequest insertFavorite, ChooseOrder chooseOrder = ChooseOrder.NameAsc)
        {
            var station = _context.Station
                .Include(s => s.User.Where(u => u.Id == insertFavorite.UserId))
                .FirstOrDefault(s => s.Id == insertFavorite.StationId);

            var user = _context.User
                .Include(u => u.Station)
                .FirstOrDefault(u => u.Id == insertFavorite.UserId);



            station.User.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw e;

            }
            if (chooseOrder == ChooseOrder.NameAsc)
            {
                return _mapping.Map<List<FavoriteListDto>>(user.Station.OrderBy(us => us.Name));
            }

            return _mapping.Map<List<FavoriteListDto>>(user.Station.OrderByDescending(us => us.Name));
        }



        [HttpDelete("/DeleteFavoriteRegistered")]

        //DELETE FAVORITE: api/Stations/DeleteFavorite/5
        public List<FavoriteListDto> DeleteFavoriteRegistered(UserStationIdRequest deleteFavorite, ChooseOrder chooseOrder = ChooseOrder.NameAsc)
        {
            var station = _context.Station
                .FirstOrDefault(s => s.Id == deleteFavorite.StationId);

            var user = _context.User
                .Include(u => u.Station)
                .FirstOrDefault(u => u.Id == deleteFavorite.UserId);


            station.User.Remove(user);
            _context.Entry(station).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Debug.WriteLine(e.Message);
                throw;

            }
            if (chooseOrder == ChooseOrder.NameAsc)
            {
                return _mapping.Map<List<FavoriteListDto>>(user.Station.OrderBy(us => us.Name));
            }
            return _mapping.Map<List<FavoriteListDto>>(user.Station.OrderByDescending(us => us.Name));




        }





        [HttpPost("/AddFavoriteUnregistered")]
        public List<FavoriteListDto> AddFavoriteUnregistered(AnonyUserStationIdRequest insertFavorite, ChooseOrder chooseOrder = ChooseOrder.NameAsc)
        {
            var station = _context.Station
                .Include(s => s.AnonymUser.Where(au => au.Id == insertFavorite.AnonymUserId))
                .FirstOrDefault(s => s.Id == insertFavorite.StationId);

            var anonymUser = _context.AnonymUser
                .Include(au => au.Station)
                .FirstOrDefault(au => au.Id == insertFavorite.AnonymUserId);

            station.AnonymUser.Add(anonymUser);
            _context.Entry(station).State = EntityState.Modified;


            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {

                throw e;
            }



            if (chooseOrder == ChooseOrder.NameAsc)
            {
                return _mapping.Map<List<FavoriteListDto>>(anonymUser.Station.OrderBy(aus => aus.Name));
            }
            return _mapping.Map<List<FavoriteListDto>>(anonymUser.Station.OrderByDescending(aus => aus.Name));



        }

        [HttpDelete("/DeleteFavoriteUnregistered")]
        public List<FavoriteListDto> DeleteFavoriteUnregistered(AnonyUserStationIdRequest deleteFavorite, ChooseOrder chooseOrder = ChooseOrder.NameAsc)
        {
            var station = _context.Station
                .FirstOrDefault(s => s.Id == deleteFavorite.StationId);
            var Anonymuser = _context.AnonymUser
                .Include(au => au.Station)
                .FirstOrDefault(au => au.Id == deleteFavorite.AnonymUserId);


            station.AnonymUser.Remove(Anonymuser);
            _context.Entry(station).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                Debug.WriteLine(e.Message);
                throw;

            }
            if (chooseOrder == ChooseOrder.NameAsc)
            {
                return _mapping.Map<List<FavoriteListDto>>(Anonymuser.Station.OrderBy(aus => aus.Name));
            }

            return _mapping.Map<List<FavoriteListDto>>(Anonymuser.Station.OrderByDescending(aus => aus.Name));
        }


    }
}
