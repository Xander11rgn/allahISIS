using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class INI
    {
        public int id { get; set; }
        public int formSizeX { get; set; }
        public int formSizeY { get; set; }
        public int maxSizeX { get; set; }
        public int maxSizeY { get; set; }
        public int minSizeX { get; set; }
        public int minSizeY { get; set; }
        public int locationX { get; set; }
        public int locationY { get; set; }
        public double opacity { get; set; }
        public string projectTitle { get; set; }
    }
}