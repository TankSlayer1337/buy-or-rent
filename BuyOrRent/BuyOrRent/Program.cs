// See https://aka.ms/new-console-template for more information

// videos with food for thought
// https://youtu.be/Uwl3-jBNEd4
// https://youtu.be/q9Golcxjpi8

// Buy parameters
var downPaymentHouse = 300000d;
var houseStartValue = 2000000d;
var houseAnnualValueGrowth = 1.03d;
var houseLoanInterestMultiplier = 1.03d;
var houseMonthlyFee = 4000d;
var interestDeductionPercent = 0.25d;
var houseLoanDeductedInterestMultiplier = (houseLoanInterestMultiplier - 1) * (1 - interestDeductionPercent) + 1;
var monthlyAmortization = 5000d;

var monthlyLoanInterestMultiplier = Math.Pow(houseLoanDeductedInterestMultiplier, 1d / 12d);
var monthlyHouseValueGrowth = Math.Pow(houseAnnualValueGrowth, 1d / 12d);
var houseValue = houseStartValue;
var houseLoan = houseStartValue - downPaymentHouse;

// Rent parameters
var stocksInterestMultiplier = 1.0657d;
var rent = 8000;

var monthlyStocksInterestMultiplier = Math.Pow(stocksInterestMultiplier, 1d / 12d);

// General parameters
var yearsToSimulate = 30;
var monthsToSimulate = yearsToSimulate * 12;


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