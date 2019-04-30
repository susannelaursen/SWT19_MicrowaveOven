using System.Threading;
using NSubstitute;
using NUnit.Framework;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Test.Unit
{
    [TestFixture]
    public class TimerTest
    {
        private Timer uut;

        [SetUp]
        public void Setup()
        {
            uut = new Timer();
        }

        [Test]
        public void Start_TimerTick_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2000);

            // wait for a tick, but no longer
            Assert.That(pause.WaitOne(1100));

            uut.Stop();
        }

        [Test]
        public void Start_TimerTick_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.TimerTick += (sender, args) => pause.Set();
            uut.Start(2000);

            // wait shorter than a tick, shouldn't come
            Assert.That(!pause.WaitOne(900));

            uut.Stop();
        }

        [Test]
        public void Start_TimerExpires_ShortEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2000);

            // wait for expiration, but not much longer, should come
            Assert.That(pause.WaitOne(2100));

            uut.Stop();
        }

        [Test]
        public void Start_TimerExpires_LongEnough()
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Expired += (sender, args) => pause.Set();
            uut.Start(2000);

            // wait shorter than expiration, shouldn't come
            Assert.That(!pause.WaitOne(1900));

            uut.Stop();
        }

        [Test]
        public void Start_TimerTick_CorrectNumber()
        {
            ManualResetEvent pause = new ManualResetEvent(false);
            int notifications = 0;

            uut.Expired += (sender, args) => pause.Set();
            uut.TimerTick += (sender, args) => notifications++;

            uut.Start(2000);

            // wait longer than expiration
            Assert.That(pause.WaitOne(2100));
            uut.Stop();

            Assert.That(notifications, Is.EqualTo(2));
        }

        [TestCase(10)]
        [TestCase(60)]
        public void Start_CorrectTimeSaved(int time)
        {
            uut.Start(time);
            Assert.That(uut.TimeRemaining, Is.EqualTo(time));
        }

        [TestCase(10)]
        [TestCase(60)]
        public void Start_TimerTick_CorrectTimeRemaining(int time)
        {
            ManualResetEvent pause = new ManualResetEvent(false);

            uut.Start(time);
            pause.WaitOne(1100);
 
            Assert.That(uut.TimeRemaining, Is.EqualTo(time-1));
        }
    }
}