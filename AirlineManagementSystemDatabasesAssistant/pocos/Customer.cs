using AirlineManagementSystemDatabasesAssistant.views;
using System;

namespace AirlineManagementSystemDatabasesAssistant
{

  public enum CustomerPropertyNumber
  {
           ID = 0,
           FIRST_NAME = 1,
           LAST_NAME = 2,
           ADDRESS = 3,
           PHONE_NO = 4,
           CREDIT_CARD_NUMBER = 5,
           IMAGE = 6,
           IDENTIFIER = 7,
           USER_ID = 8
  }

  public class Customer : IDisposable, IPoco, IAsUserForConvertingToData
    {
       public Int64 ID { get; set; }
       public String FIRST_NAME { get; set; }
       public String LAST_NAME { get; set; }
       public String ADDRESS { get; set; }
       public String PHONE_NO { get; set; }
       public String CREDIT_CARD_NUMBER { get; set; }
       public String IMAGE { get; set; }
       public String IDENTIFIER { get; set; }
       public Int64 USER_ID { get; set; }


       public Customer( String fIRST_NAME, String lAST_NAME, String aDDRESS, String pHONE_NO, String cREDIT_CARD_NUMBER, String iMAGE, String iDENTIFIER, Int64 uSER_ID)
       {
           FIRST_NAME = fIRST_NAME;
           LAST_NAME = lAST_NAME;
           ADDRESS = aDDRESS;
           PHONE_NO = pHONE_NO;
           CREDIT_CARD_NUMBER = cREDIT_CARD_NUMBER;
           IMAGE = iMAGE;
           IDENTIFIER = iDENTIFIER;
           USER_ID = uSER_ID;
       }
       public Customer()
       {
           FIRST_NAME = "-=DEFAULT_STRING=-";
           LAST_NAME = "-=DEFAULT_STRING=-";
           ADDRESS = "-=DEFAULT_STRING=-";
           PHONE_NO = "-=DEFAULT_STRING=-";
           CREDIT_CARD_NUMBER = "-=DEFAULT_STRING=-";
           IMAGE = "-=DEFAULT_STRING=-";
           IDENTIFIER = "-=DEFAULT_STRING=-";
           USER_ID = -9999;
       }



        public static bool operator ==(Customer c1, Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null)) return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            return c1.ID == c2.ID;
        }

        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            var thisType = obj as Customer;
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
