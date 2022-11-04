using IRFGyak06.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace IRFGyak06.Entities
{
    public class PresentFactory : IToyFactory
    {
        public Color BoxColor { get; set; }
        public Color RibbonColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(BoxColor, RibbonColor);
        }
    }
}
