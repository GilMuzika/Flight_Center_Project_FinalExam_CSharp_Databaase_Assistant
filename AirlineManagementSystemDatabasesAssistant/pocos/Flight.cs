using System;

namespace AirlineManagementSystemDatabasesAssistant
{

  public enum FlightPropertyNumber
  {
           ID = 0,
           AIRLINECOMPANY_ID = 1,
           ORIGIN_COUNTRY_CODE = 2,
           DESTINATION_COUNTRY_CODE = 3,
           DEPARTURE_TIME = 4,
           LANDING_TIME = 5,
           IDENTIFIER = 6,
           REMAINING_TICKETS = 7,
           PRICE = 8
  }

  public class Flight : IDisposable, IPoco
   {
       public Int64 ID { get; set; }
       public Int64 AIRLINECOMPANY_ID { get; set; }
       public Int64 ORIGIN_COUNTRY_CODE { get; set; }
       public Int64 DESTINATION_COUNTRY_CODE { get; set; }
       public DateTime DEPARTURE_TIME { get; set; }
       public DateTime LANDING_TIME { get; set; }
       public String IDENTIFIER { get; set; }
       public Int32 REMAINING_TICKETS { get; set; }
       public Decimal PRICE { get; set; }


       public Flight( Int64 aIRLINECOMPANY_ID, Int64 oRIGIN_COUNTRY_CODE, Int64 dESTINATION_COUNTRY_CODE, DateTime dEPARTURE_TIME, DateTime lANDING_TIME, String iDENTIFIER, Int32 rEMAINING_TICKETS, Decimal pRICE)
       {
           AIRLINECOMPANY_ID = aIRLINECOMPANY_ID;
           ORIGIN_COUNTRY_CODE = oRIGIN_COUNTRY_CODE;
           DESTINATION_COUNTRY_CODE = dESTINATION_COUNTRY_CODE;
           DEPARTURE_TIME = dEPARTURE_TIME;
           LANDING_TIME = lANDING_TIME;
           IDENTIFIER = iDENTIFIER;
           REMAINING_TICKETS = rEMAINING_TICKETS;
           PRICE = pRICE;
       }
       public Flight()
       {
           AIRLINECOMPANY_ID = -9999;
           ORIGIN_COUNTRY_CODE = -9999;
           DESTINATION_COUNTRY_CODE = -9999;
           DEPARTURE_TIME = DateTime.MinValue;
           LANDING_TIME = DateTime.MinValue;
           IDENTIFIER = "-=DEFAULT_STRING=-";
           REMAINING_TICKETS = -9999;
           PRICE = -9999m;
       }



        public static bool operator ==(Flight c1, Flight c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null)) return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            return c1.ID == c2.ID;
        }

        public static bool operator !=(Flight c1, Flight c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            var thisType = obj as Flight;
            return this == thisType;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(this.ID);
        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach(var s in this.GetType().GetProperties())
               str += $"{ s.Name}: { s.GetValue(this)}\n";

            return str;
        }


       public void Dispose() { }

   }
}
