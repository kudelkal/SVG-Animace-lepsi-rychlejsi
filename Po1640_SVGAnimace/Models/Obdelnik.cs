using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace Po1640_SVGAnimace.Models
{
    public class Obdelnik : Ctverec
    {
        public Obdelnik(TypObrazce typObrazce, Color barva, int pozX, int pozY, int stranaA, int stranaB) :
            base(typObrazce, barva, pozX, pozY, stranaA)
        {
            StranaB = stranaB;
        }
        public int StranaB { get; set; }
    }
}
