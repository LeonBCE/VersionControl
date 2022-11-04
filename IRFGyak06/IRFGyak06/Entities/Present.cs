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
        public SolidBrush RibbonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }

        public Present(Color bcolor, Color rcolor)
        {
            BoxColor = new SolidBrush(bcolor);
            RibbonColor = new SolidBrush(rcolor);
        }

        protected override void DrawImage(Graphics g)
        {   
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, Width/2-(Width/5/2), 0, Width/5, Height);
            g.FillRectangle(RibbonColor, 0, Height/2-(Height/5/2) , Width, Height/5);
        }

    }
}
