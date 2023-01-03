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
                        var term2 = _route.GetNextStation();
                        CancellationTokenSource ct = new();
                        string s = await EnterOneStation(term1, term2, ct.Token);
                        ct.Cancel();
                        //tcs.SetResult("");
                        prevStation.Exit();
                        Thread.Sleep(3000);

                        if (s == "Terminal 1") term1.Exit();
                        if (s == "Terminal 2") term2.Exit();
                        return;
                    }
                }

            });
        }

        async Task<string> EnterOneStation(Station s1, Station s2, CancellationToken token)
        {
            return await Task.Run(async()=>
            {
                var result = await Task.WhenAny<string>(s1.Enter(plane, tcs, token), s2.Enter(plane, tcs, token));
                string s = await result;
                return s;
            });
        }
    }
}
