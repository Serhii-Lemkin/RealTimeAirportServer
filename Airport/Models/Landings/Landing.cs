using Airport.Hubs;
using Airport.Repositories;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.Models.Landings
{
    public class Landing : ILanding
    {
        public TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        private Plane plane;
        private PlaneRoute _route;
        private readonly IHubContext<AirportHub> hub;
        private readonly IPlaneHistoryRepository history;

        public Landing(
            Plane plane,
            PlaneRoute route,
            IHubContext<AirportHub> hub,
            IPlaneHistoryRepository history)
        {
            this.plane = plane;
            _route = route;
            this.hub = hub;
            this.history = history;
        }

        public Plane GetPlane() => plane;
        

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
                    plane.CurrentStation = station.StationName;
                    UpdateUI();
                    if (prevStation != null) prevStation.Exit();
                    Thread.Sleep(3000);
                    prevStation = station;
                    station = _route.GetNextStation();
                    if (station == null)
                    {
                        prevStation.Exit();
                        UpdateUI();
                        return;
                    }
                    if (station.StationId == 6)
                    {
                        
                        var term1 = station;
                        var term2 = _route.GetNextStation();
                        CancellationTokenSource ct = new();
                        string s = await EnterOneStation(term1, term2, ct.Token);
                        plane.CurrentStation = s;
                        UpdateUI();
                        ct.Cancel();
                        prevStation.Exit();
                        Thread.Sleep(3000);

                        if (s == "Terminal 1") term1.Exit();
                        if (s == "Terminal 2") term2.Exit();
                        plane.CurrentStation = "";
                        plane.Finished = true;
                        UpdateUI();
                        return;
                    }
                }
                
            });
        }

        public async Task<string> EnterOneStation(Station s1, Station s2, CancellationToken token)
        {
            return await Task.Run(async()=>
            {
                var result = await Task.WhenAny<string>(s1.Enter(plane, tcs, token), s2.Enter(plane, tcs, token));
                string s = await result;
                return s;
            });
        }
        async Task UpdateUI()
        {
            plane.TimeOfAction = DateTime.Now.ToUniversalTime();
            history.AddRecord(plane);
            _ = hub.Clients.All.SendAsync("land", plane);
        }
    }
}
