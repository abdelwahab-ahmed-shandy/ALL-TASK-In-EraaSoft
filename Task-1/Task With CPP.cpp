/*
    Islam's Carpet Cleaning Service
    Charges:
        $25 per small
        $35 per large
    Sales tax rate is 6%
    Estimates are valid for 30 days

    Prompt the user for the number of small and large rooms they would like cleaned
    and provide an estimate such as:

Estimate for carpet cleaning service
Number of small carpets: 3 <--
Number of large carpets: 1 <--
Price per small room: $25
Price per large room: $35
Cost : $110
Tax: $6.6
===============================
Total estimate: $116.6
This estimate is valid for 30 days
*/

//Start....

#include <iostream>
using namespace std;

void ReadManycarpet(int &SmallCarpet , int &LargeCarpet)
{
    cout << "Estimate for carpet cleaning service\n";
    do
    {
        cout << "How Many Small Carpets : ";
        cin >> SmallCarpet;
    } while (SmallCarpet <= 0);

    do
    {
        cout << "How Many Large Carpets : ";
        cin >> LargeCarpet;
    } while (LargeCarpet <= 0);
}

int HowCost(int SmallCarpet, int LargeCarpet)
{
    const int PriceSmall = 25;
    const int PriceLarge = 35;
    int Cost = (PriceSmall * SmallCarpet) + (PriceLarge * LargeCarpet);
    return Cost;
}

void PrintTotalEstimate(int Cost)
{
    const float SalesTax = 6.6;
    float TotalEstimate = SalesTax + Cost;
    cout << "Price per small room: $25 \n";
    cout << "Price per large room: $35 \n";
    cout << "Cost : $ " << Cost << endl;
    cout << "Tax: $" << SalesTax << endl;
    cout << "=====================================\n";
    cout << "Total estimate: $" << TotalEstimate << endl;
    cout << "This estimate is valid for 30 days" << endl;
}

int main()
{   
    int SmallCarpet, LargeCarpet;
    ReadManycarpet(SmallCarpet, LargeCarpet);
    PrintTotalEstimate(HowCost(SmallCarpet, LargeCarpet));
    return 0;
}
