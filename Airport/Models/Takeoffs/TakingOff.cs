namespace Airport.Models.Takeoffs
{
    public class TakingOff : ITakeingOff
    {
        public TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        private Plane plane;
        private PlaneRoute _route;
        public TakingOff(Plane plane, PlaneRoute route)
        {
            this.plane = plane;
            _route = route;
        }
        public async Task TakeOff()
        {
            await Task.Run(async () =>
            {
                //var terminal1 = _route._stations!.First(x => x.StationId == 6);
                //await terminal1.Enter(new Plane { PlaneName = "bloop" });
                var station = _route.GetNextStation();
                var task = tcs.Task;
                Station prevStation = null;
                while (station != null)
                {
                    if (station.StationId == 6)
                    {
                        var term1 = station;
                        var term2 = _route.GetNextStation();


                        
                        CancellationTokenSource ct = new();
                        string s = await EnterOneStation(term1, term2 ,ct.Token);
                        ct.Cancel();
                        //tcs.SetResult("");
                        //var result = await Task.WhenAny(manyStations);

                        Thread.Sleep(3000);

                        if (s == "Terminal 1") station = term1;
                        if (s == "Terminal 2") station = term2;
                        prevStation = station;

                        station = _route.GetNextStation();
                    }
                    await station.Enter(plane);
                    prevStation?.Exit();
                    prevStation = station;
                    station = _route.GetNextStation();
                     //prevStation.Exit();
                    Thread.Sleep(3000);
                    if (station == null)
                    {
                        prevStation.Exit();
                    }
                }
            });
        }

        async Task<string> EnterOneStation(Station s1, Station s2, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                var result = await Task.WhenAny<string>(s1.Enter(plane, tcs, token), s2.Enter(plane, tcs, token));
                string s = await result;
                return s;
            });
        }
    }
}
