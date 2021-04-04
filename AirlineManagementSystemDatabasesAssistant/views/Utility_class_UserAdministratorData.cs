using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class Utility_class_UserAdministratorData: Utility_class_UserData
    {
        public long AdministratoriD { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {iD}, {iD}, {Name}";
        }
    }
}
