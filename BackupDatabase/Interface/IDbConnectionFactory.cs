using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupDatabase.Interface
{
    public interface IDbConnectionFactory
    {
        string DbUser { get; set; }
        string DbPassword { get; set; }
        string Database { get; set; }
    }

    public class DbConnectionFactory : IDbConnectionFactory
    {
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string Database { get; set; }
    }
}
