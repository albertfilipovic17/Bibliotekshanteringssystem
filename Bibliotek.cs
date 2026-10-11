using System;
using System.Collections.Generic;
using System.Text;

namespace Bibliotekshanteringssystem
{
    internal class Bibliotek
    {
        private List<Book> böcker = new List<Book>(); //Skapa en lista för att lagra böcker i biblioteket

        public void LäggTillBok() //Metod för att lägga till en bok i biblioteket
        {
            Console.Write("Ange boktitel: ");
            string? titel = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titel))
            {
                Console.WriteLine("Titeln får inte vara tom. Försök igen.");
                return;
            }

            Console.Write("Ange författare: ");
            string? författare = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(författare))
            {
                Console.WriteLine("Författaren får inte vara tom. Försök igen.");
                return;
            }

            Console.Write("Ange ISBN: ");
            string? isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                return;
            }

            foreach (Book befintligBok in böcker)
            {
                if (befintligBok.ISBN == isbn)
                {
                    Console.WriteLine($"Boken med ISBN {isbn} finns redan i biblioteket.");
                    return;
                }
            }

            Book bok = new Book //Skapa ett nytt bokobjekt 
            {
                Titel = titel,
                Författare = författare,
                ISBN = isbn,
                ÄrTillgänglig = true
            };

            böcker.Add(bok);
            Console.WriteLine($"Boken {bok.Titel} har lagts till i biblioteket.");
        }

        public void TaBortBok() //Metod för att ta bort en bok från biblioteket
        {
            Console.Write("Ange ISBN-Nummer: ");
            string? isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                return;
            }

            Book? bokAttTaBort = null;

            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn)
                {
                    bokAttTaBort = bok;
                    break;
                }
            }
            if (bokAttTaBort == null)
            {
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                return;
            }
            if (!bokAttTaBort.ÄrTillgänglig)
            {
                Console.WriteLine($"Boken {bokAttTaBort.Titel} är inte tillgänglig för borttagning eftersom den är utlånad.");
                return;
            }
            böcker.Remove(bokAttTaBort);
            Console.WriteLine($"Boken {bokAttTaBort.Titel} har tagits bort från biblioteket.");
        }

        public void VisaBöcker() //Metod för att visa alla böcker i biblioteket
        {
            Console.WriteLine("Samtliga böcker i biblioteket:");
            if (böcker.Count == 0)
            {
                Console.WriteLine("Det finns inga böcker i biblioteket.");
                return;
            }
            foreach (Book bok in böcker)
            {
                if (bok.ÄrTillgänglig)
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");

                else
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
            }
        }

        public void SökPåISBN() //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange ISBN-Nummer: ");
            string? isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                return;
            }

            bool hittad = false;

            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn && bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");
                    Console.Write("Vill du låna boken? (ja/nej): ");
                    string? svar = Console.ReadLine();
                   
                    if (svar?.ToLower() == "ja")
                    {
                        LånaBok(isbn);
                    }
                    else
                    {
                        Console.WriteLine("Du valde att inte låna boken.");
                    }
                    break;
                }
                else if (bok.ISBN == isbn && !bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                    break;
                }
            }
            if (!hittad)
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
        }

        public void SökPåTitel() //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange boktitel: ");
            string? titel = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titel))
            {
                Console.WriteLine("Titeln får inte vara tom. Försök igen.");
                return;
            }

            bool hittad = false;

            foreach (Book bok in böcker)
            {
                if (bok.Titel.ToLower().Contains(titel.ToLower()) && bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");
                    Console.Write("Vill du låna boken? (ja/nej): ");
                    string? svar = Console.ReadLine();
                    
                    if (svar?.ToLower() == "ja")
                    {
                        LånaBok(bok.ISBN);
                    }
                    else
                    {
                        Console.WriteLine("Du valde att inte låna boken.");
                    }
                }
                else if (bok.Titel.ToLower().Contains(titel.ToLower()) && !bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                }
            }
            if (!hittad)
                Console.WriteLine($"Ingen bok med titeln '{titel}' hittades i biblioteket.");
        }

        public void SökPåFörfattare() //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange författare: ");
            string? författare = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(författare))
            {
                Console.WriteLine("Författarnamnet får inte vara tomt. Försök igen.");
                return;
            }

            bool hittad = false;

            foreach (Book bok in böcker)
            {
                if (bok.Författare.ToLower().Contains(författare.ToLower()) && bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");
                    Console.Write("Vill du låna boken? (ja/nej): ");
                    string? svar = Console.ReadLine();
                    
                    if (svar?.ToLower() == "ja")
                    {
                        LånaBok(bok.ISBN);
                    }
                    else
                    {
                        Console.WriteLine("Du valde att inte låna boken.");
                    }
                }
                else if (bok.Författare.ToLower().Contains(författare.ToLower()) && !bok.ÄrTillgänglig)
                {
                    hittad = true;
                    Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                }
            }
            if (!hittad)
                Console.WriteLine($"Ingen bok av författaren '{författare}' hittades i biblioteket.");
        }

        public void LånaBok(string isbn) //Metod för att låna en bok från biblioteket
        {
            Book? bokAttLåna = null;

            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn)
                {
                    bokAttLåna = bok;
                    break;
                }
            }
            if (bokAttLåna != null)
            {
                if (bokAttLåna.ÄrTillgänglig)
                {
                    bokAttLåna.ÄrTillgänglig = false;
                    Console.WriteLine($"Du har lånat boken {bokAttLåna.Titel}.");
                }
                else
                {
                    Console.WriteLine($"Boken {bokAttLåna.Titel} är tyvärr inte tillgänglig för utlåning.");
                }
            }
            else
            {
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
            }

        }
        public void LämnaTillbakaBok(string isbn) //Metod för att lämna tillbaka en bok till biblioteket
        {
            Book? bokAttLämnaTillbaka = null;
            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn)
                {
                    bokAttLämnaTillbaka = bok;
                    break;
                }
            }
            if (bokAttLämnaTillbaka != null)
            {
                if (!bokAttLämnaTillbaka.ÄrTillgänglig)
                {
                    bokAttLämnaTillbaka.ÄrTillgänglig = true;
                    Console.WriteLine($"Du har lämnat tillbaka boken {bokAttLämnaTillbaka.Titel}.");
                }
                else
                {
                    Console.WriteLine($"Boken {bokAttLämnaTillbaka.Titel} är redan tillgänglig i biblioteket.");
                }
            }
            else
            {
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
            }
        }
    }
}
