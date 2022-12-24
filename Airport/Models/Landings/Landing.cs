namespace Airport.Models.Landings
{
    public class Landing : ILanding
    {
        private Plane plane;
        private PlaneRoute _route;
        public Landing()
        {

        }
        public Task Land()
        {
            return Task.Run(() =>
            {
                var station = _route.GetNextStation();

                while (station != null)
                {
                    Console.WriteLine($"Aitrplain {plane.PlaneName} is landing, station={station.StationId}");
                    //station.Enter(plane.Name);
                    Thread.Sleep(3000);
                    //station.Exit();
                    station = _route.GetNextStation();
                }
            });
        }
    }
}
