using System;

namespace AirlineManagementSystemDatabasesAssistant
{

  public enum Admin_ValuePropertyNumber
  {
           ID = 0,
           adminMail = 1,
           ModelRepetitivePart = 2
  }

  public class Admin_Value : IDisposable, IPoco
   {
       public Int64 ID { get; set; }
       public String adminMail { get; set; }
       public String ModelRepetitivePart { get; set; }


       public Admin_Value( String aDMINMAIL, String mODELREPETITIVEPART)
       {
           adminMail = aDMINMAIL;
           ModelRepetitivePart = mODELREPETITIVEPART;
       }
       public Admin_Value()
       {
           adminMail = "-=DEFAULT_STRING=-";
           ModelRepetitivePart = "-=DEFAULT_STRING=-";
       }



        public static bool operator ==(Admin_Value c1, Admin_Value c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null)) return true;

            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null)) return false;

            return c1.ID == c2.ID;
        }

        public static bool operator !=(Admin_Value c1, Admin_Value c2)
        {
            return !(c1 == c2);
        }
        public override bool Equals(object obj)
        {
            var thisType = obj as Admin_Value;
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
