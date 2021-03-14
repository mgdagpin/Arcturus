using Arcturus.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcturus.MvcUI.Common
{
    public class AppDateTime : IDateTime
    {
        public DateTime Now { get { return DateTime.UtcNow; } }
    }
}
