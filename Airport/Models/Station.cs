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

        public async Task<string> Enter(Plane plane, TaskCompletionSource<string>? tcs = null, CancellationToken token = new CancellationToken())
        {
            if (token.IsCancellationRequested) return "";
            if (tcs != null && tcs.Task.IsCanceled) return "";
            while (true)
            {
                if (tcs != null && tcs.Task.IsCanceled)
                {
                    //_sem.Release();
                    return "";
                }
                if (plane.Destination == "land" && incomingDelayed)
                {
                    Thread.Sleep(500);
                    continue;
                }
                if (plane.Destination == "takeOff" && outcomingDelayed)
                {
                    Thread.Sleep(500);
                    continue;
                }
                await _sem.WaitAsync();
                if (tcs != null && tcs.Task.IsCompleted)
                {
                    _sem.Release();
                    return "";
                }
                if (token.IsCancellationRequested) { _sem.Release(); return ""; }
                CurrentPlane = plane;
                tcs?.SetCanceled();
                break;
            }
            return StationName;
        }

        internal void Exit()
        {
            CurrentPlane = null;
            _sem.Release();
        }
    }
}
