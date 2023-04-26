namespace Test.Odev1;

public class Polinom
{
    public Polinom(int katSayi, int us, string degisken)
    {
        KatSayi = katSayi;
        Us = us;
        Degisken = degisken;
    }

    public Polinom(int katSayi, int us)
    {
        KatSayi = katSayi;
        Us = us;
    }

    public Polinom() { }
    public int KatSayi { get; set; }
    public int Us { get; set; }
    public String? Degisken { get; set; }
}