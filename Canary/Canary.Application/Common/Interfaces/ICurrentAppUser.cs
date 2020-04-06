using System;

namespace Canary
{
    public interface ICurrentAppUser
    {
        int ID { get; }
        string Name { get; set; }

        Guid SessionUID { get; }

    }
}
