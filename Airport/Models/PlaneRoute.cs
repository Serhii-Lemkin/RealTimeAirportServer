
using System.ComponentModel.DataAnnotations.Schema;

namespace Airport.Models
{
    public class PlaneRoute
    {
         public List<Station>? _stations;

        int _current = 0;
        public PlaneRoute()
        {
        }


        public Station GetNextStation()
        {
            if (_current + 1 > _stations.Count) return null;
            return _stations[_current++] ;
        }
    }
}
