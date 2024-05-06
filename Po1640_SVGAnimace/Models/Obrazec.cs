using System.Drawing;

namespace Po1640_SVGAnimace.Models
{
    public abstract class Obrazec
    {
        public Obrazec(TypObrazce typObrazce, Color barva, int pozX, int pozY)
        { 
            TypObrazce = typObrazce;
            Barva = barva;
            PozX = pozX;
            PozY = pozY;
        }
        public TypObrazce TypObrazce { get; set; }
        public int PozX { get; set; }
        public int PozY { get; set;}
        public Color Barva { get; set; }

        public void PosunObjekt(int krok, int SvgWidth, int SvgHeight, int MaxRozmer)
        {
            PozX += krok;
            PozY += krok;

            if (PozX > SvgWidth + MaxRozmer)
            {
                PozX = -MaxRozmer;
            }
            if (PozY > SvgHeight + MaxRozmer)
            {
                PozY = -MaxRozmer;
            }
        }

    }


   
}
