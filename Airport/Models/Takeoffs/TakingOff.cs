using Airport.Hubs;
using Airport.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Models.Takeoffs
{
    public class TakingOff : ITakingOff
    {
        public TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
        Plane plane;
        private PlaneRoute _route;
        private readonly IHubContext<AirportHub> hub;
        private readonly IPlaneHistoryRepository history;

        public TakingOff(Plane plane, PlaneRoute route, Microsoft.AspNetCore.SignalR.IHubContext<Hubs.AirportHub> hub, Repositories.IPlaneHistoryRepository history)
        {
            this.plane = plane;
            _route = route;
            this.hub = hub;
            this.history = history;
        }

        public Plane GetPlane() => plane;

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
                        plane.CurrentStation = s;
                        UpdateUI();
                        //tcs.SetResult("");
                        //var result = await Task.WhenAny(manyStations);

                        Thread.Sleep(3000);

                        if (s == "Terminal 1") station = term1;
                        if (s == "Terminal 2") station = term2;
                        prevStation = station;
                        plane.CurrentStation = station.StationName!;
                        station = _route.GetNextStation();
                    }
                    await station.Enter(plane);
                    plane.CurrentStation = station.StationName!;
                    UpdateUI();
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
                plane.CurrentStation = "";
                plane.Finished = true;
                UpdateUI();
            });
        }

        async Task<string> EnterOneStation(Station s1, Station s2, CancellationToken token)
        {
            return await Task.Run(async () =>
            {
                var result = await Task.WhenAny<string>(s2.Enter(plane, tcs, token), s1.Enter(plane, tcs, token));
                string s = await result;
                return s;
            });
        }
        async Task UpdateUI()
        {
            plane.TimeOfAction = DateTime.Now.ToUniversalTime();
            history.AddRecord(plane);
            _ = hub.Clients.All.SendAsync("takeoff", plane);
        }
    }
}
