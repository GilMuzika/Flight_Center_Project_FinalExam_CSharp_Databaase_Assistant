using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class CustomerData : IBasicData
    {
        public long iD { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone_Num { get; set; }
        public string Credit_Card { get; set; }
        public Bitmap Image { get; set; }



        public string PASSWORD { get; set; }
        public string USERNAME { get; set; }
        public string USER_KIND { get; set; }
        public long USER_ID { get; set; }


        public override string ToString()
        {
            return $"{iD}, {FirstName} {LastName}";
        }
    }
}
