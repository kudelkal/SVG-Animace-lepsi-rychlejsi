using Newtonsoft.Json;
using Po1640_SVGAnimace.Models;
using System.Drawing;


namespace Po1640_SVGAnimace.Pages
{
    public partial class SVG
    {
        public SVG() 
        {
            Task.Run(NastaveniNacist);
        }
        // bud pres konstruktor nebo pres protected override oninic...
        public int SvgWidth { get; set; } = 1600;
        public int SvgHeight { get; set; } = 800;
        public int MinRozmer { get; set; } = 20;
        public int MaxRozmer { get; set; } = 100;
        public bool AnimaceBezi { get; set; } = false;
        public List<Models.Obrazec> SvgObrazceList { get; set; } = new List<Models.Obrazec>();

        private Random rnd = new Random();
        private int typObrazceCount => Enum.GetNames(typeof(Models.TypObrazce)).Length;
        private void PridatObrazec(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
        {
            Models.TypObrazce typObrazce = (TypObrazce)rnd.Next(typObrazceCount);
            Color barva= Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            int rozmer = rnd.Next(MinRozmer, MaxRozmer);
            Obrazec? obrazec = null;
            switch (typObrazce)
            {
                case TypObrazce.ctverec:
                    obrazec = new Models.Ctverec(typObrazce, barva, rnd.Next(SvgWidth - rozmer), rnd.Next(SvgHeight - rozmer), rozmer);
                    break;
                case TypObrazce.obdelnik:
                    int rozmerB = rnd.Next(MinRozmer, MaxRozmer);
                    obrazec = new Models.Obdelnik(typObrazce, barva, rnd.Next(SvgWidth - rozmer),
                        rnd.Next(SvgHeight - rozmerB), rozmer, rozmerB);
                    break;
                case TypObrazce.kruh:
                    var polomer = rozmer / 2;
                    obrazec = new Models.Kruh(typObrazce, barva, rnd.Next(SvgWidth - polomer), rnd.Next(SvgHeight - polomer), polomer);
                    break;
            }
            if(obrazec != null)
                SvgObrazceList.Add(obrazec);

        }

        private void OdebratObrazec()
        {
            if (SvgObrazceList.Any())
            SvgObrazceList.RemoveAt(SvgObrazceList.Count - 1);
        }

        private async Task NastaveniUlozit()
        {
            string json = JsonConvert.SerializeObject(SvgObrazceList, new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
            await localStorage.SetItemAsync("data", json);
        }

        private async Task NastaveniNacist()
        {
            var jsonData = await localStorage.GetItemAsync<string>("data");
            if (jsonData != null)
            {
                List<Models.Obrazec> ? geometrie = JsonConvert.DeserializeObject<List<Models.Obrazec>>(jsonData, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
                if (geometrie != null)
                {
                    SvgObrazceList.AddRange(geometrie);
                    StateHasChanged();
                }
                
            }
                      
        }

        private async Task NastaveniSmazat()
        {
            await localStorage.RemoveItemAsync("data");

        }

        private async Task AnimaceSpustit()
        {
            AnimaceBezi = true;
            do
            {
                foreach (var item in SvgObrazceList)
                {
                    item.PosunObjekt(1, SvgWidth, SvgHeight, MaxRozmer);
                }
                StateHasChanged();
                await Task.Delay(10);
            } while (AnimaceBezi);
        }

        private void AnimaceStop()
        {
            AnimaceBezi = false;
        }
    }
}
