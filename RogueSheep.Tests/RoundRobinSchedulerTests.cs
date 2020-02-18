using System;
using Xunit;
using RogueSheep;
using RogueSheep.Schedulers;

namespace RogueSheep.Tests
{
    public class RoundRobinSchedulerTests
    {
        private string First = "first";
        private string Second = "second";
        private string Third = "third";

        [Fact]
        public void RoundRobinScheduler_KeepsSequence()
        {
            // Arrange
            var scheduler = new RoundRobinScheduler<string>();
            scheduler.Add(First, Second, Third);

            // Act & Assert
            Assert.Equal(First, scheduler.Next());
            Assert.Equal(Second, scheduler.Next());
            Assert.Equal(Third, scheduler.Next());
            Assert.Equal(First, scheduler.Next());
        }
    }
}
