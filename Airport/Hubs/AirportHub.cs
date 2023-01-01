using Airport.Models;
using Microsoft.AspNetCore.SignalR;

namespace Airport.Hubs
{
    public class AirportHub : Hub
    {
        public async Task UpdateStation(string s, Plane p)=> await Clients.All.SendAsync(s, p);
    }
}
