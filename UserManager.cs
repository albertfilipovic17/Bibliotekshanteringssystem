using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Bibliotekshanteringssystem
{
    internal class UserManager
    {
        private Dictionary<string, User> användare = new Dictionary<string, User>();

        public void RegistreraAnvändare()
        {
            Console.Write($"Ange användarnamn: ");
            string? användarnamn = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(användarnamn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Användarnamnet får inte vara tomt.");
                Console.ResetColor();
                return;
            }
            Console.Write($"Ange lösenord: ");
            string? lösenord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(lösenord))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lösenordet får inte vara tomt.");
                Console.ResetColor();
                return;
            }

            if (användare.ContainsKey(användarnamn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Användarnamnet är redan taget.");
                Console.ResetColor();
                return;
            }
            User nyAnvändare = new User
            {
                Användarnamn = användarnamn,
                Lösenord = lösenord
            };
            användare.Add(användarnamn, nyAnvändare);
            SparaAnvändare();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Användaren har registrerats.");
            Console.ResetColor();
        }

        public User? LoggaIn()
        {
            Console.Write($"Ange användarnamn: ");
            string? användarnamn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(användarnamn))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Användarnamnet får inte vara tomt.");
                Console.ResetColor();
                return null;
            }
            Console.Write($"Ange lösenord: ");
            string? lösenord = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lösenord))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Lösenordet får inte vara tomt.");
                Console.ResetColor();
                return null;
            }
            if (!användare.ContainsKey(användarnamn) || användare[användarnamn].Lösenord != lösenord)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt användarnamn eller lösenord.");
                Console.ResetColor();
                return null;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Inloggning lyckades. Välkommen, {användarnamn}!");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            return användare[användarnamn];
        }

        public void SkapaAdmin()
        {
            if (användare.ContainsKey("admin"))
            {
                return;
            }

            User admin = new User
            {
                Användarnamn = "admin",
                Lösenord = "admin",
                ÄrAdmin = true
            };
            användare.Add(admin.Användarnamn, admin);
            SparaAnvändare();
        }

        public void LäsInAnvändare()
        {
            if (File.Exists("anvandare.json"))
            {
                string json = File.ReadAllText("anvandare.json");

                användare = JsonSerializer.Deserialize<Dictionary<string, User>>(json)
                            ?? new Dictionary<string, User>();
            }
        }

        public void SparaAnvändare()
        {
            string json = JsonSerializer.Serialize(användare, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText("anvandare.json", json);
        }
    }
}
