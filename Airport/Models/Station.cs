using Airport.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml;

namespace Airport.Models
{
    public class Station
    {
        readonly SemaphoreSlim _sem = new SemaphoreSlim(1);
        private readonly IHubContext<AirportHub> hub;
        private Plane? currentPlane;
        public bool incomingDelayed { get; set; }
        public bool outcomingDelayed { get; set; }
        public int StationId { get; set; }
        public Plane? CurrentPlane
        {
            get => currentPlane;
            set
            {
                currentPlane = value;
                hub.Clients.All.SendAsync(StationName!, currentPlane);
            }
        }
        public string? PlaneName { get; set; }
        public string? StationName { get; set; }

        public Station(IHubContext<AirportHub> hub)
        {
            this.hub = hub;
            incomingDelayed = false;
        }

        internal async Task Enter(Plane plane, TaskCompletionSource<string>? tcs = null)
        {
            if (tcs != null && tcs.Task.IsCompleted) return;
            while (true)
            {

                await _sem.WaitAsync();
                //if (StationName == "Terminal 1" || StationName == "Terminal 2") Console.WriteLine($"{StationName} entered");
                if (tcs != null && tcs.Task.IsCompleted)
                {
                    _sem.Release();
                    return;
                }
                if (plane.Destination == "land" && incomingDelayed)
                {
                    _sem.Release();
                    continue;
                }
                if (plane.Destination == "takeOff" && outcomingDelayed)
                {
                    _sem.Release();
                    continue;
                }
                if (tcs != null && tcs.Task.IsCompleted)
                {
                    _sem.Release();
                    return;
                }
                CurrentPlane = plane;
                break;
            }
            tcs?.SetResult(StationName!);
            tcs?.SetCanceled();
        }

        internal void Exit()
        {
            CurrentPlane = null;
            _sem.Release();
        }
    }
}
