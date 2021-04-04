using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class AdministratorData: IBasicData
    {
        public long iD { get; set; }
        public string Name { get; set; }

        public string PASSWORD { get; set; }
        public string USERNAME { get; set; }
        public string USER_KIND { get; set; }
        public long USER_ID { get; set; }

        public override string ToString()
        {
            return $"{iD}, {Name}";
        }


    }
}
