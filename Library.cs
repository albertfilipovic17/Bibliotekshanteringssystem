using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Bibliotekshanteringssystem
{
    internal class Library
    {
        private List<Book> böcker = new List<Book>(); //Skapa en lista för att lagra böcker i biblioteket

        public void LäggTillBok() //Metod för att lägga till en bok i biblioteket
        {
            Console.Write("Ange boktitel: ");
            string? titel = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titel))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Titeln får inte vara tom. Försök igen.");
                Console.ResetColor();
                return;
            }

            Console.Write("Ange författare: ");
            string? författare = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(författare))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Författaren får inte vara tom. Försök igen.");
                return;
            }

            Console.Write("Ange ISBN: ");
            string? isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                Console.ResetColor();
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
            SparaBöcker(); //Spara böckerna i JSON-filen
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Boken {bok.Titel} har lagts till i biblioteket.");
            Console.ResetColor();
        }

        public void TaBortBok() //Metod för att ta bort en bok från biblioteket
        {
            Console.Write("Ange ISBN-Nummer: ");
            string? isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                Console.ResetColor();
                return;
            }
            if (!bokAttTaBort.ÄrTillgänglig)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken {bokAttTaBort.Titel} är inte tillgänglig för borttagning eftersom den är utlånad.");
                Console.ResetColor();
                return;
            }
            böcker.Remove(bokAttTaBort);
            SparaBöcker(); //Spara böckerna i JSON-filen
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Boken {bokAttTaBort.Titel} har tagits bort från biblioteket.");
            Console.ResetColor();
        }

        public void VisaBöcker() //Metod för att visa alla böcker i biblioteket
        {
            Console.WriteLine("Samtliga böcker i biblioteket:");
            Console.WriteLine();

            if (böcker.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Det finns inga böcker i biblioteket.");
                Console.ResetColor();
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

        public void SökPåISBN(bool ärAdmin) //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange ISBN-Nummer: ");
            string? isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                Console.ResetColor();
                return;
            }

            bool hittad = false;

            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn)
                {
                    hittad = true;

                    if (bok.ÄrTillgänglig)
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");

                        if (!ärAdmin)
                        {
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
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                    }
                    break;
                }
            }

            if (!hittad)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                Console.ResetColor();
            }
        }

        public void SökPåTitel(bool ärAdmin) //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange boktitel: ");
            string? titel = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(titel))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Titeln får inte vara tom. Försök igen.");
                Console.ResetColor();
                return;
            }

            bool hittad = false;
            List<Book> sökresultatTitel = new List<Book>();

            foreach (Book bok in böcker)
            {
                if (bok.Titel.ToLower().Contains(titel.ToLower()))
                {
                    hittad = true;
                    sökresultatTitel.Add(bok);

                    if (bok.ÄrTillgänglig)
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");
                    }
                    else
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                    }
                }
            }
            if (hittad)
            {
                if (!ärAdmin)
                {
                    Console.Write("Vill du låna en bok från din sökning? (ja/nej): ");
                    string? svar = Console.ReadLine();
                    if (svar?.ToLower() == "ja")
                    {
                        Console.Write("Ange ISBN för boken du vill låna: ");
                        string? isbn = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(isbn))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ISBN får inte vara tomt.");
                            Console.ResetColor();
                            return;
                        }

                        bool isbnFinns = false;
                        foreach (Book bok in sökresultatTitel)
                        {
                            if (bok.ISBN == isbn)
                            {
                                isbnFinns = true;
                                break;
                            }
                        }

                        if (isbnFinns)
                        {
                            LånaBok(isbn);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ISBN-numret finns inte bland sökresultaten.");
                            Console.ResetColor();
                        }

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du valde att inte låna boken.");
                        Console.ResetColor();
                    }
                }
            }

            if (!hittad)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ingen bok med titeln '{titel}' hittades i biblioteket.");
                Console.ResetColor();
            }
        }

        public void SökPåFörfattare(bool ärAdmin) //Metod för att söka efter en bok i biblioteket
        {
            Console.Write("Ange författare: ");
            string? författare = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(författare))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Författarnamnet får inte vara tomt. Försök igen.");
                Console.ResetColor();
                return;
            }

            bool hittad = false;
            List<Book> sökresultatFörfattare = new List<Book>();

            foreach (Book bok in böcker)
            {
                if (bok.Författare.ToLower().Contains(författare.ToLower()))
                {
                    hittad = true;
                    sökresultatFörfattare.Add(bok);

                    if (bok.ÄrTillgänglig)
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: JA");
                    }
                    else
                    {
                        Console.WriteLine($"Titel: {bok.Titel}, Författare: {bok.Författare}, ISBN: {bok.ISBN}, Tillgänglig: NEJ");
                    }
                }
            }
            if (hittad)
            {
                if (!ärAdmin)
                {
                    Console.Write("Vill du låna en bok från din sökning? (ja/nej): ");
                    string? svar = Console.ReadLine();
                    if (svar?.ToLower() == "ja")
                    {
                        Console.Write("Ange ISBN för boken du vill låna: ");
                        string? isbn = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(isbn))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ISBN får inte vara tomt.");
                            Console.ResetColor();
                            return;
                        }

                        bool isbnFinns = false;
                        foreach (Book bok in sökresultatFörfattare)
                        {
                            if (bok.ISBN == isbn)
                            {
                                isbnFinns = true;
                                break;
                            }
                        }
                        if (isbnFinns)
                        {
                            LånaBok(isbn);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ISBN-numret finns inte bland sökresultaten.");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Du valde att inte låna boken.");
                        Console.ResetColor();
                    }
                }
            }

            if (!hittad)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ingen bok av författaren '{författare}' hittades i biblioteket.");
                Console.ResetColor();
            }
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
                    SparaBöcker(); //Spara böckerna i JSON-filen
                    Console.WriteLine($"Du har lånat boken {bokAttLåna.Titel}.");
                }
                else
                {
                    Console.WriteLine($"Boken {bokAttLåna.Titel} är tyvärr inte tillgänglig för utlåning.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                Console.ResetColor();
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
                    SparaBöcker(); //Spara böckerna i JSON-filen
                    Console.WriteLine($"Du har lämnat tillbaka boken {bokAttLämnaTillbaka.Titel}.");
                }
                else
                {
                    Console.WriteLine($"Boken {bokAttLämnaTillbaka.Titel} är redan tillgänglig i biblioteket.");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                Console.ResetColor();
            }
        }

        public void LäsInBöcker()// Metod för att läsa in böcker från JSON-filen
        {
            if (File.Exists("bocker.json"))
            {
                string json = File.ReadAllText("bocker.json");
                böcker = JsonSerializer.Deserialize<List<Book>>(json)
                ?? new List<Book>();

            }
        }  

        public void SparaBöcker() //Metod för att spara böcker till JSON-filen
        {
            string json = JsonSerializer.Serialize(böcker, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            File.WriteAllText("bocker.json", json);
        }

        public void UppdateraBok() //Metod för att uppdatera en bok i biblioteket
        {
            Console.Write("Ange ISBN-Nummer för boken du vill uppdatera: ");
            string? isbn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(isbn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ISBN får inte vara tomt. Försök igen.");
                Console.ResetColor();
                return;
            }
            Book? bokAttUppdatera = null;
            foreach (Book bok in böcker)
            {
                if (bok.ISBN == isbn)
                {
                    bokAttUppdatera = bok;
                    break;
                }
            }
            if (bokAttUppdatera == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Boken med ISBN {isbn} hittades inte i biblioteket.");
                Console.ResetColor();
                return;
            }
            Console.WriteLine();
            Console.WriteLine($"Nuvarande titel: {bokAttUppdatera.Titel}");
            Console.Write("Ange ny titel: ");
            string? nyTitel = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nyTitel))
            {
                bokAttUppdatera.Titel = nyTitel;
            }
            Console.WriteLine();
            Console.WriteLine($"Nuvarande författare: {bokAttUppdatera.Författare}");
            Console.Write("Ange ny författare: ");
            string? nyFörfattare = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nyFörfattare))
            {
                bokAttUppdatera.Författare = nyFörfattare;
            }

            Console.WriteLine();
            Console.WriteLine($"Nuvarande ISBN: {bokAttUppdatera.ISBN}");
            Console.Write("Ange nytt ISBN: ");
            string? nyttIsbn = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nyttIsbn))
            {
                bool isbnFinns = false;

                foreach (Book bok in böcker)
                {
                    if (bok != bokAttUppdatera && bok.ISBN == nyttIsbn)
                    {
                        isbnFinns = true;
                        break;
                    }
                }

                if (isbnFinns)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Det nya ISBN-numret används redan av en annan bok.");
                    Console.ResetColor();
                }
                else
                {
                    bokAttUppdatera.ISBN = nyttIsbn;
                }
            }

            SparaBöcker();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Bokens uppgifter har uppdaterats.");
            Console.ResetColor();
        }

    }
}
