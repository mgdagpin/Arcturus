using System;
using System.Collections.Generic;

namespace Canary
{
    public interface ICurrentAppUser
    {
        int ID { get; }
        string Name { get; }

        Guid SessionUID { get; }

        IEnumerable<string> Roles { get; }

    }
}
