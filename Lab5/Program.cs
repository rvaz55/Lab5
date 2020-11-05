using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    class Program
    {
         enum MenuOptions
        {
            DisplayCities = 0,
            CityDistances = 1,
            Quit = 2
        }

        static List<City> cities = new List<City>();

        #region //PopulateCities()
        static void populateCities() 
        {
            City sanDiego = new City("San Diego", "CA", "USA", "32.71513","-117.1611");
            cities.Add(sanDiego);
            City sanFrancisco = new City("San Francisco", "CA", "USA", "37.78352", "-122.4169");
            cities.Add(sanFrancisco);
            City losAngeles = new City("Los Angeles", "CA", "USA", "34.04983", "-118.2498");
            cities.Add(losAngeles);
            City newYork = new City("New York", "NY", "USA", "40.71015", "-74.00658");
            cities.Add(newYork);
            City london = new City("London", "London", "England", "51.51786", "-0.102216");
            cities.Add(london);
            City sydney = new City("Sydney", "NSW", "Australia", "-33.86767", "151.2094");
            cities.Add(sydney);
        }
        #endregion //End of:PopulateCities()



        #region //captureSelectedCitiesAnReturnDistance()
        static string captureSelectedCitiesAndReturnDistance()
        {
            string firstCity = default;
            string secondCity = default;
            string measurementUnit = default;
            int? option1 = null;
            int? option2 = null;
            int? option3 = null;
            bool validFirstCity = default;
            bool validSecondCity = default;
            bool validUnit = default;
            bool inRange = default;
            decimal? resultDecimal = null;
            string result = default;
            string formatResultDecimal = default;



            #region //Capture input for 'firstCity'
            //Capture input for 'secondCity'
            //If 'firstCity' is null then ask user for input
            do 
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Enter first city number: ");
                Console.ForegroundColor = ConsoleColor.Green;
                firstCity = Console.ReadLine();
            }
            while (string.IsNullOrWhiteSpace(firstCity));

            //Validate the 'input' for 'firstCity'
            if (!string.IsNullOrWhiteSpace(firstCity))
            {   

                validFirstCity = int.TryParse(firstCity, out int selectedInt);
                inRange = (selectedInt == 0 || selectedInt == 1 || selectedInt == 2 || selectedInt == 3 || selectedInt == 4 || selectedInt == 5);
                if (validFirstCity == true && inRange == true)
                {
                    option1 = selectedInt;
                }
                else 
                { firstCity = String.Empty; }
            }
            #endregion //End of: Capture input for 'firstCity'

            
            #region //Capture input for 'secondCity'
            //Capture input for 'secondCity'
            if (!string.IsNullOrWhiteSpace(option1.ToString())) 
            {
                //If 'secondCity' is null then ask user for input
                do 
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("Enter second city number: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    secondCity = Console.ReadLine();
                }
                while (string.IsNullOrWhiteSpace(secondCity));

                //Validate the 'input' for 'secondCity'
                if (!string.IsNullOrWhiteSpace(secondCity))
                {
                    validSecondCity = int.TryParse(secondCity, out int selectedInt);
                    inRange = (selectedInt == 0 || selectedInt == 1 || selectedInt == 2 || selectedInt == 3 || selectedInt == 4 || selectedInt == 5);
                    if (validSecondCity == true && inRange == true)
                    {
                        option2 = selectedInt;
                    }
                    else
                    { secondCity = String.Empty; }
                }
            }
            #endregion //End of: Capture input for 'secondCity'

            #region //Capture the 'unit of measurement' for distance
            /* If both cities are valid, then ask the user what units the 'distance' should be measured in. */
            if (validFirstCity == true && validSecondCity == true)
            {
                var unitTypes = Enum.GetValues(typeof(LengthTypes));
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Please select a unit of measurement: ");

                //Print out all menu options
                foreach (var unitIem in unitTypes)
                {
                    Console.WriteLine($"[{unitIem:D}]  {unitIem:G}");
                }
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("Your choice: ");
                Console.ForegroundColor = ConsoleColor.Green;
                measurementUnit = Console.ReadLine();

                //If 'measurementUnit' is null then ask user for input
                while (string.IsNullOrWhiteSpace(measurementUnit))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Please select a unit of measurement: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    measurementUnit = Console.ReadLine();
                };
                            
                

                validUnit = int.TryParse(measurementUnit, out int selectedInt);
                if (validUnit == true && Enum.IsDefined(typeof(LengthTypes), selectedInt) == true)
                {
                    option3 = selectedInt;
                }
            }
            #endregion //End of: Capture the 'unit of measurement' for distance

            #region //Obtain distance()

            if (option1 != null && option2 != null && option3 != null) 
            {
   
                LengthTypes unitLength;
                
                Enum.TryParse(option3.ToString(), true, out unitLength);

                resultDecimal = (decimal)cities[(int)option1].Distance(cities[(int)option2], unitLength);

                formatResultDecimal = Math.Round((double)resultDecimal, 1, MidpointRounding.AwayFromZero).ToString();

                result = $"The distance between {cities[(int)option1].Name} and {cities[(int)option2].Name} is {formatResultDecimal} {unitLength}";
                Console.WriteLine($"{result}");

            }
            return result;
            #endregion

        }
        #endregion //End of: captureSelectedCitiesAndReturnDistance()

        static void Main(string[] args)
        {
            #region //Variables used in Main()
            
            var allOptions = Enum.GetValues(typeof(MenuOptions));
            string choiceString = string.Empty;
            MenuOptions selectedOption;
            MenuOptions quitOption = MenuOptions.Quit;
            int quitVal = (int)quitOption;
            populateCities();
            #endregion

            #region //Capture user's menu-choice and then call appropriate method
            try
            {
                while (!string.Equals(choiceString, quitVal.ToString()))
                {
                    //Present user with options:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Please chose from the following menu options: ");

                    //Print out all menu options
                    foreach (var optionIem in allOptions)
                    {
                        Console.WriteLine($"[{optionIem:D}]  {optionIem:G}");
                    }

                    //Capture selection; change color of output:
                    Console.Write("Your choice: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    choiceString = Console.ReadLine();

                    #region //Verify selected menu-option and call respective method via switch statement
                    //Check once more to ensure that the user has not selected 'Quit'
                    //Then Convert choiceString into a 'MenOptions' type via a variable called 'selectedOption'
                    if ((!string.Equals(choiceString, quitVal.ToString())) && Enum.TryParse(choiceString, true, out selectedOption))
                    { 
                       
                        
                        //Make sure that the next output is in 'correct' color
                        Console.ForegroundColor = ConsoleColor.Magenta;

                        
                        switch (selectedOption)
                        {
                            #region //case MenuOptions.DisplayCities:
                            case MenuOptions.DisplayCities:
                                Console.Write($"\n");
                                City.PrintHeader();
                                foreach (City element in cities) 
                                {
                                    element.Print();
                                }
                                Console.Write($"\n");
                                choiceString = string.Empty;
                                continue;
                            #endregion //MenuOptions.DisplayCities:

                            #region //case MenuOptions.CityDistances:
                            case MenuOptions.CityDistances:
                           
                                int count = 0;
                                StringBuilder cityOption = new StringBuilder();

                                Console.WriteLine("City List: ");
                                foreach (City element in cities)
                                {
                                    cityOption.Append($"{count}. {element.Name.ToString()}");
                                    Console.WriteLine($"   {cityOption}");
                                    count++ ;
                                    cityOption.Clear();
                                }
                                captureSelectedCitiesAndReturnDistance();
                                choiceString = string.Empty;
                                continue;
                             #endregion //case MenuOptions.CityDistances:

                        }
                    }
                    #endregion //End of: Verify menu-option and call respective method via switch statement
                }
                #endregion //End of: Capture user's menu-choice and then call appropriate method
            }
            #region //Catch
            catch (Exception ex)
            {
                //Print all the 'inner' exceptions which where sent to Main() by subsequently called methods
                string padding = " ";
                do 
                {
                    Console.WriteLine($"{padding}{ex.Message} thrown from {ex.TargetSite}");
                } 
                while (ex != null);

            }
            #endregion //Catch
        }
    }
}