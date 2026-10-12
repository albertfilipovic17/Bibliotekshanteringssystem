using Bibliotekshanteringssystem;

UserManager userManager = new UserManager();
Library bibliotek = new Library();
userManager.LäsInAnvändare();
userManager.SkapaAdmin();

bibliotek.LäsInBöcker();

bool programAktivt = true;

while (programAktivt)
{

    VisaStartmeny();

    Console.Write("Ange ditt val: ");
    string? val = Console.ReadLine();

    switch (val)
    {

        case "1":

            Console.Clear();
            User? inloggadAnvändare = userManager.LoggaIn();
            if (inloggadAnvändare != null)
            {
                if (inloggadAnvändare.ÄrAdmin)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Välkommen, {inloggadAnvändare.Användarnamn}! Du är inloggad som administratör.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Välkommen, {inloggadAnvändare.Användarnamn}!");
                    Console.ResetColor();
                }
                Console.WriteLine();
                bool menyAktivt = true;

                while (menyAktivt)
                {
                    if (inloggadAnvändare.ÄrAdmin)
                    {
                        VisaAdminMeny();

                        Console.Write("Ange ditt val: ");
                        string? menyVal = Console.ReadLine();
                        Console.Clear();

                        switch (menyVal)
                        {
                            case "1":
                                bibliotek.LäggTillBok();
                                break;
                            case "2":
                                bibliotek.TaBortBok();
                                break;
                            case "3":
                                bibliotek.VisaBöcker();
                                break;
                            case "4":
                                HanteraSökning(true);
                                break;
                            case "0":
                                menyAktivt = false;
                                break;
                            default:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltigt val. Försök igen.");
                                Console.ResetColor();
                                break;
                        }
                    }
                    else
                    {
                        VisaAnvändarMeny();
                        Console.Write("Ange ditt val: ");
                        string? menyVal = Console.ReadLine();
                        Console.Clear();

                        switch (menyVal)
                        {
                            case "1":
                                HanteraSökning(false);
                                break;
                            case "2":
                                Console.Write("Ange ISBN för boken du vill låna: ");
                                string? lånaIsbn = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(lånaIsbn))
                                {
                                    bibliotek.LånaBok(lånaIsbn);
                                }
                                else
                                {
                                    Console.WriteLine("ISBN får inte vara tomt.");
                                }
                                break;
                            case "3":
                                Console.Write("Ange ISBN för boken du vill lämna tillbaka: ");
                                string? lämnaIsbn = Console.ReadLine();

                                if (!string.IsNullOrWhiteSpace(lämnaIsbn))
                                {
                                    bibliotek.LämnaTillbakaBok(lämnaIsbn);
                                }
                                else
                                {
                                    Console.WriteLine("ISBN får inte vara tomt.");
                                }

                                break;
                            case "4":
                                bibliotek.VisaBöcker();
                                break;
                            case "0":
                                menyAktivt = false;
                                break;
                            default:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltigt val. Försök igen.");
                                Console.ResetColor();
                                break;
                        }
                    }

                    
                    if (menyAktivt)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
                        Console.ReadKey();
                    }
                }
                continue;
            }
            break;

        case "2":
            Console.Clear();
            userManager.RegistreraAnvändare();
            break;
        case "0":
            programAktivt = false;
            Console.Clear();
            Console.WriteLine("Avslutar programmet...");
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt val. Försök igen.");
            Console.ResetColor();
            break;
    }
    if (programAktivt)
    {
        Console.WriteLine();
        Console.WriteLine("Tryck på valfri tangent för att återgå till menyn...");
        Console.ReadKey();
    }
}

void VisaStartmeny()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║            BIBLIOTEKET             ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine("Välkommen! Välj ett alternativ nedan.");
    Console.WriteLine();
    Console.WriteLine("  [1]  Logga in");
    Console.WriteLine("  [2]  Registrera användare");
    Console.WriteLine("  [0]  Avsluta");

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("──────────────────────────────────────");
    Console.ResetColor();
}

void VisaAdminMeny()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║           ADMIN MENY               ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine("Välj ett alternativ nedan.");
    Console.WriteLine();
    Console.WriteLine("  [1]  Lägg till bok");
    Console.WriteLine("  [2]  Ta bort bok");
    Console.WriteLine("  [3]  Visa alla böcker");
    Console.WriteLine("  [4]  Sök efter bok");
    Console.WriteLine("  [0]  Logga ut");

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("──────────────────────────────────────");
    Console.ResetColor();
}

void VisaAnvändarMeny()
{
    Console.Clear();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║           ANVÄNDARMENY             ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine("Välj ett alternativ nedan.");
    Console.WriteLine();
    Console.WriteLine("  [1]  Sök efter bok");
    Console.WriteLine("  [2]  Låna bok");
    Console.WriteLine("  [3]  Lämna tillbaka bok");
    Console.WriteLine("  [4]  Visa alla böcker");
    Console.WriteLine("  [0]  Logga ut");
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("──────────────────────────────────────");
    Console.ResetColor();
}

void VisaSökMeny()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║             SÖK BOK                ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.ResetColor();

    Console.WriteLine();
    Console.WriteLine("Hur vill du söka?");
    Console.WriteLine();
    Console.WriteLine("  [1]  Sök på titel");
    Console.WriteLine("  [2]  Sök på författare");
    Console.WriteLine("  [3]  Sök på ISBN");
    Console.WriteLine("  [0]  Återgå till menyn");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("──────────────────────────────────────");
    Console.ResetColor();
}

void HanteraSökning(bool ärAdmin)
{
    VisaSökMeny();
        Console.Write("Ange ditt val: ");
    string? sökVal = Console.ReadLine();
    Console.Clear();

    switch (sökVal)
    {
        case "1":
            bibliotek.SökPåTitel(ärAdmin);
            break;
        case "2":
            bibliotek.SökPåFörfattare(ärAdmin);
            break;
        case "3":
            bibliotek.SökPåISBN(ärAdmin);
            break;
        case "0":
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ogiltigt val. Försök igen.");
            Console.ResetColor();
            break;
    }
}
