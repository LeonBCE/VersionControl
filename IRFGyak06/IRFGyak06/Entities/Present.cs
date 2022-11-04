using IRFGyak06.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRFGyak06.Entities
{
    class Present : Toy
    {
        public Color RibbonColor { get; set; }
        public Color BoxColor { get; set; }

        public Present(Color color)
        {
            //BoxColor = new SolidBrush(color);
        }

        protected override void DrawImage(Graphics g)
        {   
            //g.FillRectangle(BoxColor, 0, 0, Width, Height);
        }

    }
}
