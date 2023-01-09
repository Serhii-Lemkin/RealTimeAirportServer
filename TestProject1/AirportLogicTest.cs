namespace TestProject1
{
    public class AirportLogicTest
    {

        private readonly IControllTower _controllTower;
        private readonly IHubContext<AirportHub> hub;
        private readonly IPlaneHistoryRepository history;



        //Arrange Properties
        MoqedModels mm;
        public AirportLogicTest()
        {
            _controllTower = A.Fake<IControllTower>();
            hub = A.Fake<IHubContext<AirportHub>>();
            history = A.Fake<IPlaneHistoryRepository>();
            mm = new MoqedModels();
        }
        [Fact]
        public async void TestLanding_PassPlane_AddsToListThePlane()
        {
            //Arrange
            Plane p = mm.GetPlaneLanding();
            AirportLogic SUT = new AirportLogic(_controllTower, hub, history);

            //Act
            SUT.Land(p);
            //Assert
            SUT.GetLandings().Should().Contain(x => x.PlaneName == p.PlaneName);
        }

        [Fact]
        public void TestTakeOff_PassPlane_AddsToListThePlane()
        {
            // Arrange
            Plane p = mm.GetPlaneLanding();
            AirportLogic SUT = new AirportLogic(_controllTower, hub, history);

            // Act
            SUT.TakeOff(p);

            // Assert
            SUT.GetTakeOffs().Should().Contain(x => x.PlaneName == p.PlaneName);

        }

        [Fact]
        public void GetTakeOffs_Send3WithOneFinished_Returns2inList()
        {
            // Arrange
            var p1 = mm.GetPlaneTakeOff();
            var p2 = mm.GetPlaneTakeOff();
            p2.Finished = true;
            var p3 = mm.GetPlaneTakeOff();
            AirportLogic SUT = new AirportLogic(_controllTower, hub, history);

            SUT.TakeOff(p1);
            SUT.TakeOff(p2);
            SUT.TakeOff(p3);
            // Act
            var list = SUT.GetTakeOffs();
            // Assert
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void TestGetLandings_Send3WithOneFinished_Returns2inList()
        {
            // Arrange
            var p1 = mm.GetPlaneLanding();
            var p2 = mm.GetPlaneLanding();
            p2.Finished = true;
            var p3 = mm.GetPlaneLanding();
            AirportLogic SUT = new AirportLogic(_controllTower, hub, history);

            SUT.Land(p1);
            SUT.Land(p2);
            SUT.Land(p3);
            // Act
            var list = SUT.GetLandings();
            // Assert
            Assert.Equal(2, list.Count);
        }
    }
}