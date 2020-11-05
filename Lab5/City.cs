using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public class City
    {
        #region//Auto-properties & traditional properties
        /*Note to self: I made the 'Header' strings 'static' because I want to be able to access them without
        having to reference an instance of type 'City'. In other words, when the 'static' keyword is applied,
        the field can be accessed within the class 'City' and as such, I would not need to create an instance, such
        as 'sanDiego' to then say 'sanDiego.CityHeader' to access the 'CityHeader'
        */

        private decimal _Latitude { get; set; }
        private decimal _Longitude { get; set; }

        public string Name { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }

        public Geolocation Location { get; set; } 

        static private string CityHeader { get; } = "City";
        static private string ProvinceHeader { get; } = "Province";
        static private string CountryHeader { get; } = "Country";
        static private string CoordinatesHeader { get; } = "Coordinates";
        #endregion//End of: Auto-properties & traditional properties


        #region//Constructor(s)
        /*Reminder: Params in the constructor begin with lowercase letters
        //to differentiate them from the auto-properties of the 'City' class */

        #region //Default Constructor
        /* Default Constructor: a parameter-less constructor that can be used when working 
         * with inheritance and serialization.
        */
        public City(){  }
        #endregion

        #region//Constructor1(takes 4 args)
        /*Constructor1 that takes in 'Geolocation' param
        //Currently not being used -  not sure how i could get this version of the constructor to work without
        //directly passsing in the 'latitude' and 'longitude' values*/
        public City(string name, string province, string country, Geolocation location)
        {
            Name = name;
            Province = province;
            Country = country;
            Location = location;

        }
        #endregion //End of: Constructor1(takes 4 args)

        #region //Constructor2(takes 5 args)
        //Constructor-2 that takes in 'latitude' and 'longitude' params and creates 
        //the 'Geolocation' objet inside the constructor
        public City(string name, string province, string country, string latitude, string longitude)
        {
            Name = name;
            Province = province;
            Country = country;

            bool latToDecimal = decimal.TryParse(latitude, out decimal lat);
            bool lngToDecimal = decimal.TryParse(longitude, out decimal lng);

            try
            {
                if (latToDecimal == true && lngToDecimal == true)
                {
                    _Latitude = lat;
                    _Longitude = lng;
                }
                Location = new Geolocation(_Latitude, _Longitude);
            }
            catch(Exception ex) 
            {
                throw new Exception("Exception from constructor City(5 params):", ex);

            }
        }
        #endregion //End of: Constructor2(takes 4 args)

        #endregion //End of: Constructor(s)

        #region // Methods
        //Beginning of: Methods

        #region //Distance() 
        //Distance() 
        public decimal Distance(City city, LengthTypes lengthType = LengthTypes.Miles) 
        {   decimal result = default;

            result = Location.GreatCircleDistance(city.Location, lengthType);        
            
            return result;
        }
        #endregion //End of: Distance()

        #region //Print() 
        //Print() 
        public void Print() 
        {

            Console.WriteLine($"{Name, -15} {Province, -10} {Country, -15} {Location.DMS()}");
        
        }

        #endregion //End of: Print()


        #region //PrintHeader() 
        //PrintHeader() 
        public static void PrintHeader() 
        {
            string line = new string('=', 75);
        
            Console.WriteLine($"{CityHeader,-15} {ProvinceHeader,-10} {CountryHeader,-15} {CoordinatesHeader,-35}");
            Console.WriteLine($"{line}");
        
        }
        #endregion //End of: PrintHeader()

        #endregion //End of: Methods


}
}
