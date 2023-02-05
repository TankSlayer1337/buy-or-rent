// See https://aka.ms/new-console-template for more information

// videos with food for thought
// https://youtu.be/Uwl3-jBNEd4
// https://youtu.be/q9Golcxjpi8

// Buy parameters
const double downPaymentHouse = 300000d;
const double houseStartValue = 2000000d;
const double houseAnnualValueGrowth = 1.03d;
const double houseLoanInterestMultiplier = 1.03d;
const double houseMonthlyFee = 4000d;
const double interestDeductionPercent = 25d;
const double houseLoanDeductedInterestMultiplier = (houseLoanInterestMultiplier - 1) * (1 - interestDeductionPercent / 100) + 1;
const double monthlyAmortization = 5000d;

var monthlyLoanInterestMultiplier = Math.Pow(houseLoanDeductedInterestMultiplier, 1d / 12d);
var monthlyHouseValueGrowth = Math.Pow(houseAnnualValueGrowth, 1d / 12d);
var houseValue = houseStartValue;
var houseLoan = houseStartValue - downPaymentHouse;

// Rent parameters
const double stocksInterestMultiplier = 1.0657d;
const double rent = 8000;

var monthlyStocksInterestMultiplier = Math.Pow(stocksInterestMultiplier, 1d / 12d);

// General parameters
const double yearsToSimulate = 30;
const double monthsToSimulate = yearsToSimulate * 12;


var capitalBuy = downPaymentHouse;
var capitalRent = downPaymentHouse;

var monthsLeftToSimulate = monthsToSimulate;
while (monthsLeftToSimulate > 0 && houseLoan > 0)
{
    capitalBuy += monthlyAmortization;
    houseLoan -= monthlyAmortization;
    houseValue *= monthlyHouseValueGrowth;

    var buyExpensesThisMonth = houseMonthlyFee + monthlyAmortization + (monthlyLoanInterestMultiplier - 1) * houseLoan;
    var buyRentDifference = buyExpensesThisMonth - rent;
    capitalRent *= monthlyStocksInterestMultiplier;
    if (buyRentDifference > 0)
    {
        capitalRent += buyRentDifference;
    }

    monthsLeftToSimulate--;
}

var houseValueGrowth = houseValue - houseStartValue;

capitalBuy += houseValueGrowth;

Console.WriteLine($"Simulation ran for {Math.Round((monthsToSimulate - monthsLeftToSimulate)/12d, 1)} years.");
Console.WriteLine($"Start capital: {Convert.ToInt32(downPaymentHouse):n0}");
Console.WriteLine($"House value growth: {Convert.ToInt32(houseValueGrowth):n0}");
Console.WriteLine($"End capital buy: {Convert.ToInt32(capitalBuy):n0}");
Console.WriteLine($"End capital rent: {Convert.ToInt32(capitalRent):n0}");