namespace Airport.Models.Landings
{
    public interface ILanding
    {
        public Plane GetPlane();
        public Task Land();
    }
}
