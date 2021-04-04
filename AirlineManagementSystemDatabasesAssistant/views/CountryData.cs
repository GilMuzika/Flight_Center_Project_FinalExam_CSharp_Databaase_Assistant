using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class CountryData: IBasicData
    {
        public long iD { get; set; }
        public String Name { get; set; }
        public string PASSWORD { get => "forInterfaceImplementation"; set {  } }
        public string USERNAME { get => "forInterfaceImplementation"; set { } }
        public string USER_KIND { get => "forInterfaceImplementation"; set { } }
        public long USER_ID { get => -111111L; set { } }

        public override string ToString()
        {
            return $"{iD}, {Name}";
        }
    }
}
