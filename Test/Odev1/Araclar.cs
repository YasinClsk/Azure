using System.Text;

namespace Test.Odev1
{
    public class Araclar
    {
        public static int EnBuyukDerece(List<Polinom> anaPolinom)
        {
            int enBuyukDerece = 0;

            foreach (var item in anaPolinom)
            {
                enBuyukDerece = item.Us > enBuyukDerece ? item.Us : enBuyukDerece;
            }

            return enBuyukDerece;
        }

        public static string Yazdir(List<Eleman> sonuclar)
        {
            var metin = new StringBuilder();
            foreach (var sonuc in sonuclar)
            {
                StringBuilder polinomMetni = new StringBuilder();
                foreach (var polinom in sonuc.Polinom)
                {
                    if (polinom.KatSayi != 0)
                        polinomMetni.Append($"{polinom.KatSayi}x^{polinom.Us} + ");
                }

                Console.WriteLine($"x^{sonuc.Us} = {polinomMetni.ToString()} | {sonuc.BinaryCount} | {sonuc.Count}");
                metin.AppendLine($"x^{sonuc.Us} = {polinomMetni.ToString()} | {sonuc.BinaryCount} | {sonuc.Count}");
            }

            return metin.ToString();
        }

        public static List<Polinom> Carp(List<Polinom> pol1, List<Polinom> pol2)
        {
            List<Polinom> polinomSonuc = new List<Polinom>();
            for (int j = 0; j < pol1.Count; j++)
            {
                for (int k = 0; k < pol2.Count; k++)
                {
                    if (polinomSonuc.Any(x => x.Us == pol1[j].Us + pol2[k].Us))
                    {
                        polinomSonuc.FirstOrDefault(x => x.Us == pol1[j].Us + pol2[k].Us).KatSayi += pol1[j].KatSayi % 2 * pol2[k].KatSayi % 2;
                        continue;
                    }

                    polinomSonuc.Add(new Polinom(pol1[j].KatSayi % 2 * pol2[k].KatSayi % 2, pol1[j].Us + pol2[k].Us));
                }
            }

            return polinomSonuc;
        }

        public static List<Polinom> Kontrol(List<Polinom> pol1, int enYuksekDerece, List<int> sayilar)
        {

            for (int i = 0; i < pol1.Count; i++)
            {
                if (pol1[i].Us >= enYuksekDerece)
                {
                    foreach (var item in sayilar)
                    {
                        if (pol1.Any(x => x.Us == item))
                            pol1.FirstOrDefault(x => x.Us == item).KatSayi += pol1[i].KatSayi;
                        else
                            pol1.Add(new Polinom(pol1[i].KatSayi, item));
                    }

                    pol1.Remove(pol1[i]);
                }
            }
            return pol1;
        }

        public static List<Eleman> Temizle(List<Eleman> elemanlar)
        {
            for (int i = 0; i < elemanlar.Count; i++)
            {
                for (int j = 0; j < elemanlar[i].Polinom.Count; j++)
                {
                    if (elemanlar[i].Polinom[j].KatSayi != 1)
                        elemanlar[i].Polinom.Remove(elemanlar[i].Polinom[j]);
                }
            }

            return elemanlar;
        }

        public static List<Polinom> DenklemOlustur(int enBuyukDerece)
        {
            List<Polinom> polinom = new List<Polinom>();

            for (int i = 0; i < enBuyukDerece; i++)
            {
                polinom.Add(new Polinom(1, i, $"x{i}"));
            }
            return polinom;
        }
        // a^2 * (x3*a^3 + x2*a^2 + x1*a^1 + x0) 
        public static void DenklemiCarp(List<Eleman> sonuclar, List<Polinom> denklemler, int enBuyukDerece)
        {
            List<List<Polinom>> denklemList = new List<List<Polinom>>();

            foreach (var sonuc in sonuclar)
            {
                List<Polinom> denklemPolinom = new List<Polinom>();
                foreach (var polinom in sonuc.Polinom)
                {
                    foreach (var denklem in denklemler)
                    {
                        if (polinom.Us + denklem.Us >= enBuyukDerece)
                        {
                            foreach (var pol in sonuclar[polinom.Us + denklem.Us].Polinom)
                            {
                                denklemPolinom.Add(new Polinom(1, pol.Us, denklem.Degisken));
                            }
                        }

                        else
                        {
                            denklemPolinom.Add(new Polinom(1, polinom.Us + denklem.Us, $"{denklem.Degisken}"));
                        }
                    }
                }
                denklemList.Add(denklemPolinom);
            }

            int ss = 0;
            foreach (var item in denklemList.Take(denklemList.Count-1))
            {
                MatrixOlustur(enBuyukDerece, item, sonuclar[ss].Count);
                ss++;
            }

            Console.WriteLine();
        }

        public static void MatrixOlustur(int enBuyukDerece, List<Polinom> denklemList, int hex)
        {
            int[,] matrix = new int[enBuyukDerece, enBuyukDerece];

            for (int i = 0; i < enBuyukDerece; i++)
            {
                for (int j = 0; j < denklemList.Count; j++)
                {
                    if (denklemList[j].Degisken == $"x{i}")
                    {
                        matrix[denklemList[j].Us, i] += denklemList[j].KatSayi;
                        matrix[denklemList[j].Us, i] = matrix[denklemList[j].Us, i] % 2;
                    }
                }
            }
            Console.WriteLine($"--------------------- {hex} -----------------------");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.WriteLine();
            }
        }
    }
}
