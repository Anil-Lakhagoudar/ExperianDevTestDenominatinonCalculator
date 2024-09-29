// See https://aka.ms/new-console-template for more information
using DenominationCalculator;
using System.Text;

Console.WriteLine("Change calculator");

/// <summary>
/// global variables
/// </summary>
List<int> denominationsList = new List<int>() { 1, 2, 5, 10, 20, 50, 100 };
List<double> listOfChanges = new List<double>();
double productPrice = 0;
double given = 0;

try
{
    Console.WriteLine("Enter product price: ");
    productPrice = Convert.ToDouble(Console.ReadLine());

    Console.WriteLine("Enter given amount: ");
    given = Convert.ToDouble(Console.ReadLine());

    // Calculate change to return
    double changeToReturn = Math.Round(given - productPrice, 2);
    if(changeToReturn >= 0)
    {
        Console.WriteLine("Amount to be returned - {0}", changeToReturn);
    }

    CurrencyChangeCalculator currencyChangeCalculator = new CurrencyChangeCalculator();
    List<DenominationModel> denominations = currencyChangeCalculator.GetChanges(productPrice, given);

    if (denominations.Count() > 0)
    {
        // Build the output
        StringBuilder outputBuilder = new StringBuilder();
        outputBuilder.AppendLine("Your change is:");
        denominations.ToList().ForEach(x => {

            outputBuilder.AppendLine(String.Format("{0} x {1}", x.Count, x.DenominationWithSymbol));
        });

        Console.WriteLine(outputBuilder.ToString());
    }
    else
    {
        Console.WriteLine("Calculation need to re run");
    }
}catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.ReadKey();






