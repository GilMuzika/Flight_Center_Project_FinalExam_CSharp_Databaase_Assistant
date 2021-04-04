using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class Utility_class_UserAirlineCompanyData: Utility_class_UserData
    {
        public long AirlineCompanyiD { get; set; }
        public string Adorning { get; set; }
        public Bitmap Image { get; set; }
        public string AirlineName { get; set; }
        public string BaseCountryName { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {iD}, {AirlineName}, {BaseCountryName}";
        }
    }
}
