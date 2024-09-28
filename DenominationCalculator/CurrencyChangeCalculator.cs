using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenominationCalculator
{
    public class CurrencyChangeCalculator
    {
        List<int> denominationsList = new List<int>() { 1, 2, 5, 10, 20, 50, 100 };
        List<double> listOfChanges = new List<double>();

        public List<DenominationModel> GetChanges(double productPrice, double givenAmount)
        {
            List<DenominationModel> denominations = new List<DenominationModel>();
            double changeToReturn = givenAmount - productPrice;

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
                    double denomination = x.Key >= 1 ? x.Key : ((x.Key * 100) / 1);
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
        /// calculate change method
        /// </summary>
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
