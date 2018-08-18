using BackupDatabase.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Unit.Fakes
{
    public class IDbConnectionFactoryFake : IDbConnectionFactory
    {
        public string DbUser { get => throw new NotImplementedException(); set { string.Empty }; }
        public string DbPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Database { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
