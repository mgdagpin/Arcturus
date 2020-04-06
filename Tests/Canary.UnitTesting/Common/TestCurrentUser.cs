using System;
using System.Collections.Generic;

namespace Canary.UnitTesting.Common
{
    public class TestCurrentUser : ICurrentAppUser
    {
        public TestCurrentUser(int id, string name = default)
        {
            this.ID = id;
            this.Name = name;
        }
        public int ID { get; private set; }


        public string Name { get; private set; }

        public Guid SessionUID => throw new NotImplementedException();

        public IEnumerable<string> Roles => throw new NotImplementedException();
    }
}
