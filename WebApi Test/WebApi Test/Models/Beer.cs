using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Test.Models
{
    public class Beer
    {
        public String Name { get; set; }

        private List<int> Ratings = new List<int>();


        public int? Rating
        {
            get
            {
                if (Ratings.Count > 0)
                    return Convert.ToInt16(Ratings.Average());
                else
                    return null;
            }
            set
            {
                if (value != null)
                    this.Ratings.Add(value.Value);
            }
        }

        public static bool IsValid(Beer beer)
        {
            return beer != null
                && !String.IsNullOrEmpty(beer.Name)
                && (beer.Rating == null
                    || (beer.Rating >= 1 && beer.Rating <= 5));
        }
    }
}