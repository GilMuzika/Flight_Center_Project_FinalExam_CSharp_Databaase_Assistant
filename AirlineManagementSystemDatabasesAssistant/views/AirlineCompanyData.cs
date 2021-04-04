using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    public class AirlineCompanyData: IBasicData
    {
        public long iD { get; set; }
        public string Adorning { get; set; }
        public Bitmap Image { get; set; }
        public string AirlineName { get; set; }
        public string BaseCountryName { get; set; }

        public string PASSWORD { get; set; }
        public string USERNAME { get; set; }
        public string USER_KIND { get; set; }
        public long USER_ID { get; set; }

        public override string ToString()
        {
            return $"{iD}, {AirlineName}, {BaseCountryName}";
        }
    }

    
    
}
