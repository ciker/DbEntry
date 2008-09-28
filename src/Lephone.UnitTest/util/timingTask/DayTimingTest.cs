using System;
using Lephone.Util.TimingTask;
using Lephone.Util.TimingTask.Timings;
using NUnit.Framework;

namespace Lephone.UnitTest.util.timingTask
{
	[TestFixture]
	public class DayTimingTest
	{
		[Test]
		public void TestIt()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,3,5,10,5,10,0));
			ITiming t = new DayTiming(new TimeOfDayStructure(7, 12, 3), ntp);

			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(1,2,3));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,3,6,1,0,0));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(2,2,1));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(2,6,9));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(1,51,49));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,3,6,7,12,1));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(true, t.TimesUp());

			t.Reset();
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(false, t.TimesUp());
		}

		[Test]
		public void TestIt2()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,3,5,10,5,10,0));
			ITiming t = new DayTiming(new TimeOfDayStructure(7, 12, 3), ntp);

			ntp.SetNow(new DateTime(2004,3,6,7,12,2));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(true, t.TimesUp());

			t.Reset();
			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(false, t.TimesUp());
		}

		[Test]
		public void TestOverTimingPoint()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,3,5,7,12,2,0));
			ITiming t = new DayTiming(new TimeOfDayStructure(7, 12, 3), ntp);

			Assert.AreEqual(false, t.TimesUp());
			
			ntp.Add(new TimeSpan(0,0,10));
			Assert.AreEqual(true, t.TimesUp());
		}

		[Test]
		public void TestAtTimingPoint()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,3,5,7,12,5,0));
			ITiming t = new DayTiming(new TimeOfDayStructure(7, 12, 3), ntp);

			Assert.AreEqual(false, t.TimesUp());
			
			ntp.Add(new TimeSpan(0,0,10));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,50));
			Assert.AreEqual(false, t.TimesUp());
		}

		[Test]
		public void TestOneDayOnce()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,3,5,7,10,2,0));
			ITiming t = new DayTiming(new TimeOfDayStructure(7, 12, 3), ntp);

			Assert.AreEqual(false, t.TimesUp());
			
			ntp.SetNow(new DateTime(2004,3,5,7,12,2,0));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,10));
			Assert.AreEqual(true, t.TimesUp());

			t.Reset();
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,1));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,10));
			Assert.AreEqual(false, t.TimesUp());

			ntp.Add(new TimeSpan(0,0,20));
			Assert.AreEqual(false, t.TimesUp());
		}
	}
}