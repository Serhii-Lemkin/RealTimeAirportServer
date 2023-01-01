namespace Airport.Models.Landings
{
    public class Landing : ILanding
    {
        public TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        private Plane plane;
        private PlaneRoute _route;
        public Landing(Plane plane, PlaneRoute route)
        {
            this.plane = plane;
            _route = route;
        }
        public async Task Land()
        {
            await Task.Run(async () =>
            {
                //var terminal1 = _route._stations!.First(x => x.StationId == 6);
                //await terminal1.Enter(new Plane { PlaneName = "bloop"});
                var station = _route.GetNextStation();
                Station prevStation = null;
                var task = tcs.Task;
                while (station != null)
                {
                    
                    await station.Enter(plane);
                    if (prevStation != null) prevStation.Exit();
                    Thread.Sleep(3000);
                    prevStation = station;
                    station = _route.GetNextStation();
                    if (station == null)
                    {
                        prevStation.Exit();
                        return;
                    }
                    if (station.StationId == 6)
                    {
                        
                        var term1 = station;
                        //var term2 = _route._stations!.First(x => x.StationId == 7);
                        var term2 = _route.GetNextStation();
                        await Task.WhenAny(term1.Enter(plane, tcs), term2.Enter(plane, tcs));
                        string s = await task;
                        prevStation.Exit();
                        Thread.Sleep(3000);

                        if (s == "Terminal 1") term1.Exit();
                        if (s == "Terminal 2") term2.Exit();
                        return;
                    }
                }

            });
        }
    }
}
