using System;

namespace Canary
{
    public interface ICurrentAppUser
    {
        int ID { get; }

        Guid SessionUID { get; }

    }
}
