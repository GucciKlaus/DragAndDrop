using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDrop
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Firstname { get; set;}
        public string Lastname { get; set;}
        public DateOnly BirthDate { get; set; }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} ";
        }
    }
}
