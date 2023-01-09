using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class LandingTests
    {
        private readonly IHubContext<AirportHub> hub;
        private readonly IPlaneHistoryRepository history;

        MoqedModels mm = new MoqedModels();
        public LandingTests()
        {
            hub = A.Fake<IHubContext<AirportHub>>();
            history = A.Fake<IPlaneHistoryRepository>();
        }


        [Fact]
        public async void TestGoToOneStation_TestStateOfStationsEvery3Seconds_IfEverythingFineGreen()
        {
            // Arrange
            var s1 = new Station(hub) { StationName= "A", StationId=1 };
            var s2 = new Station(hub) { StationName= "B", StationId=2 };

            var route = new PlaneRoute { _stations = new List<Station> { s1, s2 } };
            var plane = mm.GetPlaneLanding();
            Landing l = new Landing(plane, route, hub, history);

            // Act
            l.Land();

            // Assert
            s1.CurrentPlane.Should().BeNull();
            s2.CurrentPlane.Should().BeNull();
            Thread.Sleep(3000);
            s1.CurrentPlane.Should().NotBeNull();
            s2.CurrentPlane.Should().BeNull();
            Thread.Sleep(3000);
            s1.CurrentPlane.Should().BeNull();
            s2.CurrentPlane.Should().NotBeNull();
            Thread.Sleep(3000);
            s1.CurrentPlane.Should().BeNull();
            s2.CurrentPlane.Should().BeNull();

        }

        [Fact]
        public async void TestDontEnterTakenStation_PlaneWaits_ReturnsGreen()
        {
            // Arrange
            var mockPlane = mm.GetPlaneTakeOff();
            var s1 = new Station(hub) { StationName = "A", StationId = 1 };
            var s2 = new Station(hub) { StationName = "B", StationId = 2, CurrentPlane = mockPlane };

            var route = new PlaneRoute { _stations = new List<Station> { s1, s2 } };
            var plane = mm.GetPlaneLanding();
            Landing l = new Landing(plane, route, hub, history);
            await s2.Enter(mockPlane);
            // Act
            l.Land();

            // Assert
            s1.CurrentPlane.Should().BeNull();
            s2.CurrentPlane.Should().Be(mockPlane);
            Thread.Sleep(3000);
            s1.CurrentPlane.Should().Be(l.GetPlane());
            s2.CurrentPlane.Should().Be(mockPlane);
            Thread.Sleep(3000);
            s1.CurrentPlane.Should().Be(l.GetPlane());
            s2.CurrentPlane.Should().Be(mockPlane);
        }

        [Fact]
        public async void TestEnterOnlyFirstWhenSecondIsTaken()
        {
            // Arrange
            var mockPlane = mm.GetPlaneTakeOff();
            var s1 = new Station(hub) { StationName = "A", StationId = 1 };
            var s2 = new Station(hub) { StationName = "B", StationId = 2, CurrentPlane = mockPlane };

            var route = new PlaneRoute { _stations = new List<Station> { s1, s2 } };
            var plane = mm.GetPlaneLanding();
            Landing l = new Landing(plane, route, hub, history);
            await s2.Enter(mockPlane);
            // Act
            CancellationTokenSource ct = new();
            await l.EnterOneStation(s1, s2, ct.Token);
            ct.Cancel();
            // Assert
            s1.CurrentPlane.Should().Be(l.GetPlane());
            s2.CurrentPlane.Should().Be(mockPlane);
        }
        [Fact]
        public async void TestEnterOnlySecondWhenFirstIsTaken()
        {
            // Arrange
            var mockPlane = mm.GetPlaneTakeOff();
            var s1 = new Station(hub) { StationName = "A", StationId = 1 };
            var s2 = new Station(hub) { StationName = "B", StationId = 2, CurrentPlane = mockPlane };

            var route = new PlaneRoute { _stations = new List<Station> { s1, s2 } };
            var plane = mm.GetPlaneLanding();
            Landing l = new Landing(plane, route, hub, history);
            await s1.Enter(mockPlane);
            // Act
            CancellationTokenSource ct = new();
            await l.EnterOneStation(s1, s2, ct.Token);
            ct.Cancel();
            // Assert
            s1.CurrentPlane.Should().Be(mockPlane);
            s2.CurrentPlane.Should().Be(l.GetPlane());
        }
    }
}
