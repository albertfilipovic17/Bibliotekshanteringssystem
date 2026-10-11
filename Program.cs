using Bibliotekshanteringssystem;

Console.WriteLine("Välkommen till Biblioteket!");

Bibliotek bibliotek = new Bibliotek();


bibliotek.LäggTillBok();
bibliotek.LäggTillBok();

bibliotek.VisaBöcker();
bibliotek.SökPåISBN();
