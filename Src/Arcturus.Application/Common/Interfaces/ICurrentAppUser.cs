using System;
using System.Collections.Generic;

namespace Arcturus
{
    public interface ICurrentAppUser
    {
        int ID { get; }
        string Name { get; }

        Guid SessionUID { get; }

        IEnumerable<string> Roles { get; }

    }
}
