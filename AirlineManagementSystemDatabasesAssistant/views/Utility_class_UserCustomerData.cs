using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagementSystemDatabasesAssistant.views
{
    class Utility_class_UserCustomerData: Utility_class_UserData
    {
        public long CustomeriD { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone_Num { get; set; }
        public string Credit_Card { get; set; }
        public Bitmap Image { get; set; }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {iD}, {FirstName} {LastName}";
        }
    }
}
