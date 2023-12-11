using RiceCookerSpace;

namespace Program
{
    class Program
    {
        static void Main()
        {
            App().Wait();
        }

        static async System.Threading.Tasks.Task App()
        {
            string choice;
            RiceCooker riceCooker = new RiceCooker();


            Console.Clear();
            while (true)
            {
                Show("Appuyer sur un chiffre pour éxecuter une action: " +
                    "\n 1 - Ajouter de l\'eau" + 
                    "\n 2 - Cuire du riz" + 
                    "\n 3 - Réchauffer un ingrédient" + 
                    "\n 4 - Afficher les configurations globales" + 
                    "\n 5 - Changer les configurations globales" + 
                    "\n 0 - Sortir \n");

                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        riceCooker.addWater();
                        break;
                    case "2":
                        Console.Clear();
                        if (riceCooker.hasWater == false)
                        {
                            Show("\n Il faut ajouter de l\'eau avant de cuire \n");
                        }
                        else{
                            Console.Write("Que voulez cuire ?(vary gasy/vary mena/ronono): ");
                            string elementType = Console.ReadLine();
                            Console.Write("Temps de cuisson (en secondes) : ");
                            if (int.TryParse(Console.ReadLine(), out int cookingTime))
                            {
                                await riceCooker.StartCookingAsync(elementType, cookingTime);
                            }
                            else
                            {
                                Show("Temps de cuisson non valide");
                            }
                        }
                        break;
                    case "3":
                        Console.Clear();
                        if (riceCooker.hasWater == false)
                        {
                            Show("\n Il faut ajouter de l\'eau avant de réchauffer \n");
                        }
                        else{
                            Console.Write("Que voulez réchauffer ?(ronono/vary/laoka): ");
                            string elementType = Console.ReadLine();
                            await riceCooker.StartReheatingAsync(elementType);
                        }
                        break;
                    case "4":
                        Console.Clear();
                        riceCooker.ShowConfiguration();
                        break;
                    case "5":
                        Console.Clear();
                        Console.Write("Entrez une nouvelle température: ");
                        string configuration = Console.ReadLine();
                        riceCooker.ChangeConfiguration(configuration);
                        break;
                    case "0":
                        break;
                    default:
                        Console.Clear();
                        Show("\n Choix non valide. \n");
                        break;
                }

                if (choice == "0")
                {
                    break;
                }
            }
        }

        static void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}

