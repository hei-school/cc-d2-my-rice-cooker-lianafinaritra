
namespace RiceCookerSpace
{
    /// <summary>
    /// Represents a rice cooker that can be used to cook or reheat elements.
    /// </summary>
    public class RiceCooker
    {
        private bool isCooking = false;
        private string temperature = "32";
        public bool hasWater = false;

        private Timer showLoadingAnimation(bool isReheating)
        {
            string[] frames = { "(°)", "(¬)", "(°)", "(¬)" };
            int frameIndex = 0;

            Timer animationTimer = new Timer(_ =>
            {
                Console.Write($"\r{frames[frameIndex]} En cours de cuisson...");
                if (isReheating == true)
                {
                    Console.Write($"\r{frames[frameIndex]} En train de réchauffer...");
                }
                else
                {
                    Console.Write($"\r{frames[frameIndex]} En cours de cuisson...");
                }
                frameIndex = (frameIndex + 1) % frames.Length;
            }, null, 0, 500);

            return animationTimer;
        }

        public void addWater()
        {
            hasWater = true;
        }

        public void ShowConfiguration()
        {
            Console.WriteLine($"\n La température de globale de cuisson est: {temperature}° \n");
        }

        public void ChangeConfiguration(string newTemp)
        {
            if (int.TryParse(newTemp, out int convertedTemp))
            {
                temperature = newTemp;
            }
            else
            {
                Console.WriteLine("\n Température invalide \n");
            }
        }

        public async System.Threading.Tasks.Task StartReheatingAsync(string elementType)
        {
            if (elementType == "ronono" || elementType == "vary" || elementType == "laoka")
            {
                var searchType = Ingredients.Find(item => item.Name == elementType);
                isCooking = true;
                hasWater = false;
                var loadingAnimation = showLoadingAnimation(true);
                var heatingTime = searchType?.Time ?? 5;

                try
                {
                    await System.Threading.Tasks.Task.Delay(heatingTime * 1000);
                }
                catch (Exception)
                {
                    Console.WriteLine("\n Cuisson interrompue \n");
                    loadingAnimation.Dispose();
                    StopCooking();
                }

                loadingAnimation.Dispose();

                Console.WriteLine("\n Réchauffement terminé \n");

                StopCooking();
            }
            else
            {
                Console.WriteLine("Le rice cooker ne peut pas cuire ce type d'élément");
            }
        }

        public async System.Threading.Tasks.Task StartCookingAsync(string elementType, int cookingTime)
        {
            if (elementType == "vary gasy" || elementType == "vary mena" || elementType == "ronono")
            {
                var searchType = Elements.Find(item => item.Name == elementType);
                isCooking = true;
                hasWater = false;
                var loadingAnimation = showLoadingAnimation(false);
                var limit = searchType?.Time ?? 5;

                try
                {
                    await System.Threading.Tasks.Task.Delay(cookingTime * 1000);
                }
                catch (Exception)
                {
                    Console.WriteLine("\n Cuisson interrompue \n");
                    loadingAnimation.Dispose();
                    StopCooking();
                }

                loadingAnimation.Dispose();

                if (limit > cookingTime)
                {
                    Console.WriteLine("\n Le riz n'est pas assez cuit, il manque du temps \n");
                }
                else if ((cookingTime > limit - 1) && (limit + 2 > cookingTime))
                {
                    Console.WriteLine("\n Le riz est cuit. \n");
                }
                else
                {
                    Console.WriteLine("\n Trop longtemps, le riz est brulé \n");
                }

                StopCooking();
            }
            else
            {
                Console.WriteLine("Le rice cooker ne peut pas cuire ce type d'élément");
            }
        }

        public void StopCooking()
        {
            if (isCooking)
            {
                isCooking = false;
            }
            else
            {
                Console.WriteLine("\n Le rice cooker n'est pas en train de cuire. \n");
            }
        }

        private class Element
        {
            public string Name { get; set; }
            
            public int Time { get; set; }
        }

        private static List<Element> Elements = new List<Element>
        {
            new Element { Name = "vary gasy", Time = 3 },
            new Element { Name = "vary mena", Time = 4 },
            new Element { Name = "ronono", Time = 5 }
        };

        private static List<Element> Ingredients = new List<Element>
        {
            new Element { Name = "ronono", Time = 3 },
            new Element { Name = "vary", Time = 4 },
            new Element { Name = "laoka", Time = 5 }
        };
    }
}
