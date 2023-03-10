// See https://aka.ms/new-console-template for more information

// videos with food for thought
// https://youtu.be/Uwl3-jBNEd4
// https://youtu.be/q9Golcxjpi8

// assumptions
// - rent is cheaper than mortgage + fee (script will still work otherwise but arguably not yield as useful results).
// - house owner amortizes until the loan is paid in full.
// - annual amortization is 2% of initial house loan.
// - comparison stops when house loan is paid. Given interest from investing is larger than house value growth,
//   investing will yield larger capital in the long run.

// Buy parameters
const double houseStartValue = 3000000d;
const double downPaymentHouse = houseStartValue * 0.15;
const double houseValueGrowthMultiplierAnnual = 1.03d;
const double houseLoanInterestMultiplierAnnual = 1.03d;
const double houseFeeMonthly = 4000d;
const double loanInterestDeductionPercent = 25d;
const double loanDeductedInterestMultiplierAnnual = (houseLoanInterestMultiplierAnnual - 1) * (1 - loanInterestDeductionPercent / 100) + 1;

var loanDeductedInterestMultiplierMonthly = Math.Pow(loanDeductedInterestMultiplierAnnual, 1d / 12d);
var houseValueGrowthMultiplierMonthly = Math.Pow(houseValueGrowthMultiplierAnnual, 1d / 12d);
var houseValue = houseStartValue;
var houseLoan = houseStartValue - downPaymentHouse;
var amortizationAnnual = houseLoan * 0.02;
var amortizationMonthly = amortizationAnnual / 12;

// Rent parameters
const double stocksInterestMultiplierAnnual = 1.0657d;
const double rent = 10000;

var stocksInterestMultiplierMonthly = Math.Pow(stocksInterestMultiplierAnnual, 1d / 12d);

// General parameters
const double yearsToSimulate = 40;
const double monthsToSimulate = yearsToSimulate * 12;

var capitalRent = downPaymentHouse;

var monthsLeftToSimulate = monthsToSimulate;
while (monthsLeftToSimulate > 0 && houseLoan > 0)
{
    // interest deduction is in reality made on an annual basis, but we simplify it (in favor of buying).
    var buyExpensesThisMonth = houseFeeMonthly + amortizationMonthly + (loanDeductedInterestMultiplierMonthly - 1) * houseLoan;
    houseLoan -= amortizationMonthly;
    houseValue *= houseValueGrowthMultiplierMonthly;

    var buyRentDifference = buyExpensesThisMonth - rent;
    capitalRent *= stocksInterestMultiplierMonthly;
    if (buyRentDifference > 0)
    {
        capitalRent += buyRentDifference;
    }

    monthsLeftToSimulate--;
}

var houseValueGrowth = houseValue - houseStartValue;

var capitalBuy = houseValue - houseLoan;

Console.WriteLine($"Simulation ran for {Math.Round((monthsToSimulate - monthsLeftToSimulate)/12d, 1)} years.");
Console.WriteLine($"Start capital: {Convert.ToInt32(downPaymentHouse):n0}");
Console.WriteLine($"House value: {Convert.ToInt32(houseValue):n0}");
Console.WriteLine($"House loan: {Convert.ToInt32(houseLoan):n0}");
Console.WriteLine($"House value growth: {Convert.ToInt32(houseValueGrowth):n0}");
Console.WriteLine($"End capital buy: {Convert.ToInt32(capitalBuy):n0}");
Console.WriteLine($"End capital rent: {Convert.ToInt32(capitalRent):n0}");