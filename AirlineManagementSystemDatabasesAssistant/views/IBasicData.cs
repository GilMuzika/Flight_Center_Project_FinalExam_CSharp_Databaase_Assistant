using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    interface IBasicData
    {
        string PASSWORD { get; set; }
        string USERNAME { get; set; }
        string USER_KIND { get; set; }
        long USER_ID { get; set; }
    }
}
