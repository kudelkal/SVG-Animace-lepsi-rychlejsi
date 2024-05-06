using System.Drawing;

namespace Po1640_SVGAnimace.Models
{
    public class Ctverec : Obrazec
    {
        public Ctverec(TypObrazce typObrazce, Color barva, int pozX, int pozY,int stranaA) : 
            base(typObrazce, barva, pozX, pozY)
        {
            StranaA = stranaA;
        }
        public int StranaA { get; set; }
    }
}
