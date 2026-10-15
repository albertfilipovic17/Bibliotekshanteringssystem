using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotekshanteringssystem
{
    internal class User
    {
        //Egenskaper för användar objektet
        public string Användarnamn { get; set; } = "";
        public string Lösenord { get; set; } = "";
        public bool ÄrAdmin { get; set; } = false;
    }
}
