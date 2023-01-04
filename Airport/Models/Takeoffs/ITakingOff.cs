namespace Airport.Models.Takeoffs
{
    public interface ITakingOff
    {
        public Plane GetPlane();
        public Task TakeOff();
    }
}
