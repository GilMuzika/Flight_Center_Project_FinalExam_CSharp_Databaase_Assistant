using System;

namespace AirlineManagementSystemDatabasesAssistant
{

  public enum TicketsHistoryPropertyNumber
  {
           ID = 0,
           FLIGHT_ID = 1,
           IDENTIFIER = 2,
           CUSTOMER_ID = 3
  }

  public class TicketsHistory : IDisposable, IPoco
   {
       public Int64 ID { get; set; }
       public Int64 FLIGHT_ID { get; set; }
       public String IDENTIFIER { get; set; }
       public Int64 CUSTOMER_ID { get; set; }


       public TicketsHistory( Int64 fLIGHT_ID, String iDENTIFIER, Int64 cUSTOMER_ID)
       {
           FLIGHT_ID = fLIGHT_ID;
           IDENTIFIER = iDENTIFIER;
           CUSTOMER_ID = cUSTOMER_ID;
       }
       public TicketsHistory()
       {
           FLIGHT_ID = -9999;
           IDENTIFIER = "-=DEFAULT_STRING=-";
           CUSTOMER_ID = -9999;
       }



        public static bool operator ==(TicketsHistory c1, TicketsHistory c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null)) return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            return c1.ID == c2.ID;
        }

        public static bool operator !=(TicketsHistory c1, TicketsHistory c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            var thisType = obj as TicketsHistory;
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
