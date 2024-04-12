using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class Configure
    {
        public Configure(string _connect) 
        {
            ConnectionString = _connect;
        }
        public string ConnectionString { get; set; }
    }
}
