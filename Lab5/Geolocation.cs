using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    public struct Geolocation
    {
        #region //Fields of struct 'Geolocation'
        //Define the fields 'Longitude' and 'Latitude' of the struct 'Geolocation'
        //Reminder: a fields name is written in Pascal-casing to differentiate the
        //fields from the parameters used in the constructor-methods
        public readonly decimal Longitude, Latitude;
        #endregion //End of: Fields of struct 'Geolocation'


        #region //Constructors
        #region//Signature for first constructor
        public Geolocation(decimal latitude, decimal longitude) 
        {
            #region //Local variables
            //validLati will evaluate to 'true' if latitude is between -90 degrees and 90 degrees
            bool validLat = ((decimal)-90 <= latitude && latitude <= (decimal)90);
            //validLati will evaluate to 'true' if longitude is between -180 degrees and 180 degrees
            bool validLng = ((decimal)-180 <= latitude && latitude <= (decimal)180);
            #endregion //Local variables

            #region // Beginning of 'try{}' block
            try
            {
                //If 'latitude' and 'longitude' are valid, then assign these values to their respective fields
                if (validLat == true && validLng == true)
                {
                    //Assign the value of param 'latitude' to the field 'Latitude'
                    Latitude = latitude;
                    //Assign the value of param 'longitude' to the field 'Longitude'
                    Longitude = longitude;
                }
                else //If either 'latitude' or 'longitude' are invalid, jump to the 'catch' block
                {
                    throw new ArgumentOutOfRangeException("Exception from Geolocation():");
                }
            }
            #endregion // End of 'try{}' block

            #region //Beginning of 'catch{}' block
            catch (Exception ex) 
            {
                throw new ArgumentOutOfRangeException("Exception from Geolocation():", ex);

            }
            #endregion //End of: 'catch{}' block
        }
        #endregion//End of: Signature for first constructor

        #region//Signature for second constructor
        public Geolocation(Geolocation otherPoint) 
        {
            Latitude = otherPoint.Latitude;
            Longitude = otherPoint.Longitude;
        }
        #endregion //End of: Signature for second constructor

        #endregion //End of: Constructors

        #region //Methods
        #region //Method: DMS() 
        public string DMS() 
        {
            #region //Variables for DMS()
            string latCoordinate = string.Empty;
            string latDirection = string.Empty;
            string lngCoordinate = string.Empty;
            string lngDirection = string.Empty;
            string coordinates = string.Empty;

            int degrees = default;
            int minutes = default;
            int seconds = default;
            decimal minutesAndSeconds = default;
            #endregion //End of: Variables for DMS()

            #region //Calculate the DMS-coordinate of 'latitude'

            if (String.IsNullOrEmpty(latCoordinate))
            {
                //Calculate the 'minutesAndSeconds' by subtracting 'hours' from 'Latitude'; the remainder represents 'minutesAndSeconds'
                degrees = (int)Latitude;
                minutesAndSeconds = (Latitude - degrees) * 60; //Subtracts 'hours' to get the remainder which is stored in 'minutesAndSeconds'

                //Truncate 'minutesAndSeconds' to get the whole-number value for 'minutes'
                minutes = (int)(minutesAndSeconds);

                //Subtract the 'minutes' from 'minutesAndSeconds' and then multiply by 60 to get the decimal value of 'seconds'
                //Then cast the decimal value of 'seconds' as an (int) to get the whole-number value for 'seconds'
                seconds = (int)((minutesAndSeconds - minutes) * 60);

                //I feel uncomfortable simplyifying this further because I hate using 'the short hand ternary operators(spellcheck bc I'm not sure that's the correct term....)'
                if (degrees > 0)
                {
                    latDirection = "N";
                }
                else
                {
                    latDirection = "S";

                    //Then get the absolute-value of all variables
                    degrees = Math.Abs(degrees);
                    minutes = Math.Abs(minutes);
                    seconds = Math.Abs(seconds);
                }

                /*The unicodes represent:   \u00B0 for 'degree symbol'
                                            \u0027 for 'apostrophe symbol'
                                            \u0022 for 'quote symbol'
                 */
                latCoordinate = $"{degrees}\u00B0{minutes}\u0027{seconds}\u0022{latDirection}";
            }
            #endregion //Calculate the DMS-coordinate of 'latitude'

            #region //Calculate the DMS-coordinate of 'longitude' only if 'latCoordinate' has already been calculated
            if (!String.IsNullOrEmpty(latCoordinate))
            {
                //Calculate the 'minutesAndSeconds' by subtracting 'hours' from 'Latitude'; the remainder represents 'minutesAndSeconds'
                minutesAndSeconds = default;
                degrees = (int)Longitude;
                minutesAndSeconds = (Longitude - degrees) * 60; //Subtracts 'hours' to get the remainder which is stored in 'minutesAndSeconds'

                //Truncate 'minutesAndSeconds' to get the whole-number value for 'minutes'
                minutes = (int)(minutesAndSeconds);

                //Subtract the 'minutes' from 'minutesAndSeconds' and then multiply by 60 to get the decimal value of 'seconds'
                //Then cast the decimal value of 'seconds' as an (int) to get the whole-number value for 'seconds'
                seconds = (int)((minutesAndSeconds - minutes) * 60);

                //I feel uncomfortable simplyifying this further because I hate using 'the short hand ternary'
                if (degrees > 0)
                {
                    lngDirection = "E";
                }
                else
                {
                    lngDirection = "W";

                    //Then get the absolute-value of all variables
                    degrees = Math.Abs(degrees);
                    minutes = Math.Abs(minutes);
                    seconds = Math.Abs(seconds);
                }
                
                lngCoordinate = $"{degrees}\u00B0{minutes}\u0027{seconds}\u0022{lngDirection}";
            }
            #endregion //Calculate the DMS-coordinate of 'latitude'

            //Package the coordinates and send them back to caller
            coordinates = $"{latCoordinate} {lngCoordinate}";

            return coordinates;
        }

        #endregion //End of: DMS()

        #region //Method: GreatCircleDistance() 
        public decimal GreatCircleDistance(Geolocation other, LengthTypes lengthType = LengthTypes.Meters) 
        {
            #region //Variables
            double lat1, lat2, lng1, lng2;
            double latDiff, lngDiff;
            double sin2Lat, sin2Lng;
            double a, arcLength;
            decimal? distance = null ;
            decimal? convertedDistance = null;
            const decimal EarthRadius = 6371000;
            const decimal KilometersConversionRatio = 0.001M;
            const decimal FeetConversionRatio = 3.2808399M;
            const decimal MilesConversionRatio = 0.00062137119M;
            #endregion


            #region//Step1
            /*Convert from degrees to radians*/
            lng1 = ToRadians((double)Longitude); //The 'x' of city
            lat1 = ToRadians((double)Latitude); //The 'y' of city
           
            lng2 = ToRadians((double)other.Longitude); //The 'x' of otherCity
            lat2 = ToRadians((double)other.Latitude); //The 'y' of otheCity
            #endregion//End of: Step1

            #region//Step2
            /*Calculate for 'latDiff' and 'lngDiff' */
            latDiff = lat1 - lat2;
            lngDiff = lng1 - lng2;
            #endregion//End of: Step2


            #region//Step3
            /*Calculate for sin^2() portion of formula*/
            sin2Lat = Math.Pow(Math.Sin(latDiff / 2), 2);
            sin2Lng = Math.Pow(Math.Sin(lngDiff / 2), 2);
          
            #endregion//End of: Step3

            #region//Step4
            /*Calculate 'a' where 'a' is the result of the rest of the terms under the radical*/
            a = sin2Lat + Math.Cos(lat1) * (Math.Cos(lat2)) * sin2Lng;
            #endregion//End of: Step4

            #region//Step5
            /*Calculate for 'arcLength'*/
            arcLength = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            #endregion//End of: Step5

            #region//Step6
            /*Calculate for 'distance'*/
            distance = (decimal)arcLength * (decimal)EarthRadius;
            #endregion//End of: Step6

            #region//Step7
            if (lengthType == LengthTypes.Meters)
            {
                return (decimal)distance;
            }
            else 
            {
                switch (lengthType) 
                {
                    case LengthTypes.Kilometers:
                        convertedDistance = (decimal)distance * KilometersConversionRatio;
                        break;

                    case LengthTypes.Feet:
                        convertedDistance = (decimal)distance * FeetConversionRatio;
                        break;
                    case LengthTypes.Miles:
                        convertedDistance = (decimal)distance * MilesConversionRatio;
                        break;
                }
                if(!String.IsNullOrWhiteSpace(convertedDistance.ToString()))
                distance = (decimal)convertedDistance;
                //Console.WriteLine($"distance: {distance}");
                return (decimal)distance;
            }
            #endregion//End of: Step7

        }
        #endregion //End of: GreatCircleDistance() 

        #region //Method: ToRadians()
        private static double ToRadians(double degreeVal)
        {
            return (degreeVal * (Math.PI / 180.0));
        }
        #endregion //End of: ToRadians()


        #endregion //End of: Methods

    }
}
