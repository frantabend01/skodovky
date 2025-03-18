using System.Xml.Linq;
using Skodovky;

public class CarSalesProcessor
{
    private string xmlFilePath;

    //constructor
    public CarSalesProcessor(string filePath)
    {
        xmlFilePath = filePath;
    }

    public List<CarSale> LoadSales()
    {
        List<CarSale> sales = new List<CarSale>();

        try
        {
            XDocument doc = XDocument.Load(xmlFilePath);
            sales = doc.Descendants("item")
                .Select(car => new CarSale
                {
                    ID = int.Parse(car.Element("id")?.Value),
                    Nazev = car.Element("nazev")?.Value,
                    Datum = DateTime.Parse(car.Element("datum")?.Value),
                    Cena = double.Parse(car.Element("cena")?.Value),
                    DPH = double.Parse(car.Element("DPH")?.Value),
                    //CenaSDPH = double.Parse(car.Element("cena")?.Value) * (1 + double.Parse(car.Element("DPH")?.Value) / 100)
                })
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading XML: " + ex.Message);
        }

        return sales;
    }
}
