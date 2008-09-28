using System;
using Lephone.Util.TimingTask;
using Lephone.Util.TimingTask.Timings;
using NUnit.Framework;

namespace Lephone.UnitTest.util.timingTask
{
	[TestFixture]
	public class WeekTimingTest
	{
		[Test]
		public void TestIt()
		{
			MockMiscProvider ntp = new MockMiscProvider(new DateTime(2004,11,21,7,10,2,0));
			ITiming t = new WeekTiming(new TimeOfDayStructure(7, 12, 3), DayOfWeek.Monday, ntp);

			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,22,7,10,2));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,22,7,12,2));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,22,7,12,3));
			Assert.AreEqual(true, t.TimesUp());

			t.Reset();
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,23,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,24,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,25,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,26,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,27,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,28,7,12,4));
			Assert.AreEqual(false, t.TimesUp());

			ntp.SetNow(new DateTime(2004,11,29,7,12,4));
			Assert.AreEqual(true, t.TimesUp());
		}
	}
}