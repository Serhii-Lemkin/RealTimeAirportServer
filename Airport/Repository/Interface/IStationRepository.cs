using Airport.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Collections.Generic;

namespace Airport.Repository.Interface
{
    public interface IStationRepository
    {
        public IEnumerable<Station> GetStations();
        public Station GetStation(int id);
        public void UpdateStation(Station station); 
    }
}
