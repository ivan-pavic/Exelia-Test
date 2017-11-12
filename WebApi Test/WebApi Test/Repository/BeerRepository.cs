using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi_Test.Models;

namespace WebApi_Test.Repository
{
    public class BeerRepository
    {
        private const string CacheKey = "BeerStore";

        public BeerRepository()
        {
            var context = HttpContext.Current;

            if (context != null)
            {
                if (context.Cache[CacheKey] == null)
                {
                    List<Beer> beers = new List<Beer>();
                    context.Cache[CacheKey] = beers;
                }
            }
        }



        public List<Beer> GetAll()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                return (List<Beer>)context.Cache[CacheKey];
            }
            return
                null;
        }


        public IEnumerable<Beer> GetByName(string searchParameter)
        {
            List<Beer> beers = GetAll();

            return beers.Where(beer => beer.Name.Contains(searchParameter));
        }

        public bool Add(Beer beer)
        {
            List<Beer> beers = GetAll();

            if (!beers.Any(existingBeer => existingBeer.Name.Equals(beer.Name)))
            {
                beers.Add(beer);
                HttpContext.Current.Cache[CacheKey] = beers;

                return true;
            }

            return false;
        }

        internal Beer Update(Beer beer)
        {
            List<Beer> beers = GetAll();

            Beer existingBeer = beers.Where(b => b.Name.Equals(beer.Name)).Select(b => b).First();

            if (existingBeer != null)
                existingBeer.Rating = beer.Rating;

            return existingBeer;
        }
    }
}