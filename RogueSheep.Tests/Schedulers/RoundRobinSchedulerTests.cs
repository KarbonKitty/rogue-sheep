using NUnit.Framework;
using RogueSheep.Schedulers;

namespace RogueSheep.Tests
{
    public class RoundRobinSchedulerTests
    {
        private const string First = "first";
        private const string Second = "second";
        private const string Third = "third";

        [Test]
        public void RoundRobinScheduler_KeepsSequence()
        {
            // Arrange
            var scheduler = new RoundRobinScheduler<string>();
            scheduler.Add(First, Second, Third);

            // Act & Assert
            Assert.That(First, Is.EqualTo(scheduler.Next()));
            Assert.That(Second, Is.EqualTo(scheduler.Next()));
            Assert.That(Third, Is.EqualTo(scheduler.Next()));
            Assert.That(First, Is.EqualTo(scheduler.Next()));
        }
    }
}
