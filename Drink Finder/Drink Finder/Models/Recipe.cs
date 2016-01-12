using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drink_Finder.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Alcohol { get; set; }
        public string Mixers { get; set; }
        public string Directions { get; set; }
        public string Glass { get; set; }
    }
}