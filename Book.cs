using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotekshanteringssystem
{
    internal class Book
    {
        //Egenskaper för boken objektet
        public string Titel { get; set; } 
        public string Författare { get; set; }
        public string ISBN { get; set; }
        public bool ÄrTillgänglig { get; set; }
    }
}
