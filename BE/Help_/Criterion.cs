using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Criterion:ICloneable
    {
        public Criterion()
        {
            The_Criterion = "";
            Is_Succeeded = false;
        }
        public Criterion(string the_Criterion, bool? is_Succeeded=null)
        {
            The_Criterion = the_Criterion;
            Is_Succeeded = is_Succeeded;
        }

        public string The_Criterion { get; set; }
        public bool? Is_Succeeded { get; set; }

        public object Clone()
        {
            var d = new Criterion()
            {
                The_Criterion = The_Criterion,
            Is_Succeeded = Is_Succeeded
            };
            return d;
        }
    }
}
