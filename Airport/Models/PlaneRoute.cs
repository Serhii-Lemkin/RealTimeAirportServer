namespace Airport.Models
{
    public class PlaneRoute
    {
        List<Station> _stations;
        int _current = 0;
        public PlaneRoute(List<Station> stations)
        {
            _stations = stations;
        }
        public Station GetNextStation()
        {
            return _stations[_current++];
        }
    }
}
