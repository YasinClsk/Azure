using Kripto_Odev;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptographyController : ControllerBase
    {

        [HttpPost("odev1")]
        public IActionResult Odev1(List<Polinom> anaPolinom)
        {


            //int count = 1;

            //List<Polinom> anaPolinom = new List<Polinom>();
            //do
            //{
            //    Polinom polinom = new();

            //    Console.Write($"İndirgenemez polinomun {count}. elemanın katsayısını giriniz : ");
            //    polinom.KatSayi = Convert.ToInt32(Console.ReadLine());

            //    Console.Write($"İndirgenemez polinomun {count}. elemanın derecesini giriniz : ");
            //    polinom.Us = Convert.ToInt32(Console.ReadLine());
            //    polinom.Degisken = "x";

            //    anaPolinom.Add(polinom);
            //    count++;

            //    Console.Write("Çıkış yapmak için exit yazınız. Devam etmek için enter tuşuna basınız... ");
            //} while (Console.ReadLine() != "exit");


            int enBuyukDerece = Araclar.EnBuyukDerece(anaPolinom);
            List<Eleman> sonuclar = new List<Eleman>();

            List<int> kritikNoktaPolinomu = new List<int>();

            foreach (var item in anaPolinom)
            {
                if (item.Us == enBuyukDerece)
                    continue;

                kritikNoktaPolinomu.Add(item.Us);
            }

            for (int i = 0; i < Math.Pow(2, enBuyukDerece); i++)
            {
                if (enBuyukDerece > i)
                {
                    Polinom polinom = new(1, i, "a");
                    Eleman eleman = new Eleman((Convert.ToString((int)Math.Pow(2, i), 2)), (int)Math.Pow(2, i), i);
                    eleman.Polinom.Add(polinom);
                    sonuclar.Add(eleman);
                }

                else if (enBuyukDerece == i)
                {

                    List<Polinom> polinomListesi = new List<Polinom>();
                    int tut = 0;
                    foreach (var item in anaPolinom)
                    {
                        if (item.Us == enBuyukDerece)
                            continue;

                        polinomListesi.Add(new Polinom(item.KatSayi, item.Us));
                        tut += (int)Math.Pow(2, item.Us);
                    }
                    Eleman eleman = new Eleman(Convert.ToString(tut, 2), tut, i);
                    eleman.Polinom = polinomListesi;
                    sonuclar.Add(eleman);
                }

                else
                {
                    int temp = 0;

                    var pol1 = sonuclar[i - 1].Polinom;
                    var pol2 = sonuclar[1].Polinom;

                    List<Polinom> polinomList = Araclar.Carp(pol1, pol2);
                    polinomList = Araclar.Kontrol(polinomList, enBuyukDerece, kritikNoktaPolinomu);
                    for (int j = 0; j < polinomList.Count; j++)
                    {
                        if (polinomList[j].KatSayi % 2 != 0)
                            temp += (int)Math.Pow(2, polinomList[j].Us);
                    }

                    Eleman eleman = new Eleman(Convert.ToString(temp, 2), temp, i);
                    eleman.Polinom = polinomList;
                    sonuclar.Add(eleman);
                }
            }


            var response = Araclar.Yazdir(sonuclar);
            List<Polinom> denklem = Araclar.DenklemOlustur(enBuyukDerece);
            sonuclar = Araclar.Temizle(sonuclar);

            Araclar.DenklemiCarp(sonuclar, denklem, enBuyukDerece);

            return Ok(response);
        }
    }
}
