using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenominationCalculator
{
    /// <summary>
    /// Currency Change Calculator
    /// </summary>
    public class CurrencyChangeCalculator
    {
        List<int> denominationsList = new List<int>() { 1, 2, 5, 10, 20, 50, 100 };
        List<double> listOfChanges = new List<double>();

        /// <summary>
        /// Get Changes to calculate denomications
        /// </summary>
        /// <param name="productPrice">product price</param>
        /// <param name="givenAmount">given amount</param>
        /// <returns>List of denomination model</returns>
        /// <exception cref="Exception">exceptions</exception>
        public List<DenominationModel> GetChanges(double productPrice, double givenAmount)
        {
            // Throw exception when given amount is less than product price
            if (givenAmount < productPrice) throw new Exception("Amount should be more than product price");

            // Throw exception when product price is zero
            if (productPrice <= 0) throw new Exception("Product price should not be zero");

            List<DenominationModel> denominations = new List<DenominationModel>();
            double changeToReturn = Math.Round(givenAmount - productPrice, 2);

            // Calculate the denominations for the change
            double remainingChange = changeToReturn;
            while (remainingChange > 0)
            {
                double lastDenominationFound = CalculateChange(denominationsList, remainingChange);
                remainingChange = remainingChange - lastDenominationFound;
                this.listOfChanges.Add(lastDenominationFound);
            }

            // Compare the change with sum of denominations to be given
            double totalChange = this.listOfChanges.Sum();
            if (changeToReturn == totalChange)
            {
                // Order by large denomination first
                List<double> orderedList = listOfChanges.OrderByDescending(x => x).ToList();

                // Group by denominations with count
                var groupChanges = orderedList.GroupBy(x => x);

                // Build the output
                StringBuilder outputBuilder = new StringBuilder();
                groupChanges.ToList().ForEach(x => {
                    double denomination = x.Key >= 1 ? x.Key : Math.Round(((x.Key * 100) / 1),2);
                    denominations.Add(new DenominationModel()
                    {
                        Denomination = Convert.ToInt32(denomination),
                        Count = x.Count(),
                        DenominationWithSymbol = x.Key >= 1 ? String.Format("£{0}", denomination) : String.Format("{0}p", denomination)
                    });
                });
            }
            return denominations;
        }


        /// <summary>
        /// Calculate Change
        /// </summary>
        /// <param name="denominationsList">Predefined denomination list</param>
        /// <param name="change">Change</param>
        /// <returns>returns change</returns>
        private double CalculateChange(List<int> denominationsList, double change)
        {
            int denominationIndex = 0;
            double lastDenominationFound = 0;
            denominationsList.ForEach((denomination) =>
            {
                if (change > denomination)
                {
                    lastDenominationFound = denomination;
                }

                denominationIndex++;
            });

            if (lastDenominationFound == 0)
            {
                lastDenominationFound = change;
            }

            return lastDenominationFound;
        }
    }
}
