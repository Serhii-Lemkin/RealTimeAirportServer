Random r = new Random();
var client = new HttpClient();
var dest = 1;
var planeName = "";
var randString = "1234567890qwertyuiopasdfghjklzxcvbnm";
while (true)
{
    dest = r.Next(1, 11);
    Console.Write(dest % 2 == 1 ? "landing " : "takeoff " ) ;

    for (int i = 0; i < 5; i++) planeName += randString[r.Next(0, randString.Length)];
    Console.WriteLine(planeName);

    if(dest % 2 == 1) client.GetAsync($"https://localhost:7160/land/{planeName}");
    if(dest % 2 == 0) client.GetAsync($"https://localhost:7160/takeoff/{planeName}");

    Thread.Sleep(4000);
    planeName = "";
}
