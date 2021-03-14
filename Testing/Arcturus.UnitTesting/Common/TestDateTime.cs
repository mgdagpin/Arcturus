using Arcturus.Application;
using System;

namespace Arcturus.UnitTesting.Common
{
    public class TestDateTime : IDateTime
    {
        private DateTime _now = DateTime.Now;
        private Random _rand;

        public DateTime Now => _now;

        public TestDateTime()
        {
            _rand = new Random();
        }

        public TestDateTime(string newDateTime) : this()
        {
            if (DateTime.TryParse(newDateTime, out DateTime result))
            {
                _now = result;
            }
            else
            {
                _now = DateTime.Now;
                _now = DateTime.Now;
            }
        }

        public TestDateTime(DateTime newDateTime) : this()
        {
            _now = newDateTime;
        }

        public TestDateTime NextByHours(double minAddHours, double maxAddHours)
        {
            var _min = (int)(minAddHours * 3600);
            int _max = (int)(maxAddHours * 3600);

            var _randSeconds = _rand.Next(_min, _max);
            _now = _now.AddSeconds(_randSeconds);

            return new TestDateTime(_now);
        }

        public override string ToString()
        {
            return _now.ToString();
        }
    }
}
