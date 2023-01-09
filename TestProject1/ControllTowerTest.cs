namespace TestProject1
{

    public class ControllTowerTest
    {
        public IHubContext<AirportHub> Hub { get; }
        private readonly List<Station> AllStations;
        MoqedModels mm = new();

        //ControllTower SUT = new ControllTower(Hub);
        public ControllTowerTest()
        {
            Hub = A.Fake<IHubContext<AirportHub>>();
            ControllTower SUT = new ControllTower(Hub);
            AllStations = SUT.AllStations;
        }

        [Fact]
        public void GetRouteForLand_PassLand_ReturnCorrectRoute()
        {
            // Arrange
            var _stations = new List<Station>
                        {
                            AllStations[0],
                            AllStations[1],
                            AllStations[2],
                            AllStations[3],
                            AllStations[4],
                            AllStations[5],
                            AllStations[6],
                        };
            var route = new PlaneRoute
            {
                _stations = _stations
            };
            ControllTower SUT = new ControllTower(Hub);

            // Act
            var res = SUT.GetRoute("land");
            // Assert
            res.Should().BeEquivalentTo(route);
        }
        [Fact]
        public void GetRouteForTakeOff_PassTakeOff_ReturnCorrectRoute()
        {
            // Arrange
            var _stations = new List<Station>
                        {
                            AllStations[5],
                            AllStations[6],
                            AllStations[7],
                            AllStations[3],
                            AllStations[8],
                        };
            var route = new PlaneRoute
            {
                _stations = _stations
            };
            ControllTower SUT = new ControllTower(Hub);

            // Act
            var res = SUT.GetRoute("takeOff");
            // Assert
            res.Should().BeEquivalentTo(route);
        }
        [Fact]
        public void GetRouteForWrongRoute_PassWrongRoute_ReturnNull()
        {
            // Arrange
            var _stations1 = new List<Station>
                        {
                            AllStations[5],
                            AllStations[6],
                            AllStations[7],
                            AllStations[3],
                            AllStations[8],
                        };
            var route1 = new PlaneRoute
            {
                _stations = _stations1
            };
            var _stations2 = new List<Station>
                        {
                            AllStations[5],
                            AllStations[6],
                            AllStations[7],
                            AllStations[3],
                            AllStations[8],
                        };
            var route2 = new PlaneRoute
            {
                _stations = _stations2
            };
            ControllTower SUT = new ControllTower(Hub);

            // Act
            var res = SUT.GetRoute("a");
            // Assert
            res.Should().NotBeEquivalentTo(route1);
            res.Should().NotBeEquivalentTo(route2);
            res.Should().Be(null);
        }

        [Fact]
        public void GetStateOfOneOccupiedStation_SetsPlaneOnStationIndex4_ReturnsOnlyIndex4()
        {
            // Arrange
            ControllTower SUT = new ControllTower(Hub);
            SUT.AllStations[4].CurrentPlane = mm.GetPlaneLanding();
            var s = SUT.AllStations[4].StationName;
            // Act
            var res = SUT.GetCurrentState();
            // Assert
            res.Should().ContainSingle(x => x.StationName == s);
            res.Should().NotContain(x => x.StationName != s && x.CurrentPlane != null);
        }
    }
}
