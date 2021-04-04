using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class Utility_class_UserData: IBasicData
    {
        public long iD { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string USER_KIND { get; set; }

        public Int64 AIRLINE_ID { get; set; }
        public Int64 CUSTOMER_ID { get; set; }
        public Int64 ADMINISTRATOR_ID { get; set; }

        public Customer customer { get; set; }
        public AirlineCompany airline { get; set; }
        public Administrator administrator { get; set; }


        public string USERNAME { get => "forInterfaceImplementation"; set { } }
        public long USER_ID { get => -111111L; set { } }
    }
}
