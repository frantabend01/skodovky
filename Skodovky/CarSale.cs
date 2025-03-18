namespace Skodovky
{
    public class CarSale
    {
        public int ID { get; set; }
        public string Nazev { get; set; }
        public DateTime Datum { get; set; }
        public double Cena { get; set; }
        public double DPH { get; set; }

        // Vypočítání DPH
        public double CenaDPH => Cena * (1 + DPH / 100);
    }

}
