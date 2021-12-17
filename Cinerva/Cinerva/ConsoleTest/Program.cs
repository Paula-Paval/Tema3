using Cinerva.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cinerva.Data.Entities;
using Cinerva.TestConsole.Queries;
using System.Collections.Generic;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var cinerva = new CinervaDbContext();
            var query = new Query();
            query.Cinerva = cinerva;
            query.GetPropertiesFromIasi();
            query.GetClientsThatMadeAReservationAndPaidForIt();
            query.GetUsersWithAdminRole();
            query.GetRoomTypeAndPriceForRoomsAtIntercontinentalRomania();
            query.GetTop5PropertiesInRomaniaByReservationCount();
            query.GetAvaibleHotelsWithPool();
            query.GetTotalNumberOfAvaibleRoomsFromAllPropertiesInRomaniaPriceRangeBetween70And100EuroForThisMonth();
            query.GetTheHighestRatedPropertyThatHasSpaAndRoomsAvailableForTheNextWeekend();
            query.GetTheMonthOfTheYearThatHasTheMostReservationsForAcertainHotel();
            query.FindPropertiesThatHave2DoubleRoomsAvailableForTheNextWeekInAntwerpThatCostBetween100And150Euro();
            query.GetTheNumberOfDistinctGuestsThatHaveMadeBookingsForMayInEachHotelInIasi();
            query.GetLocationsThatWillBeAvaibleNextYearBetweenMayAndSeptember();
            query.GetTop10HotelsInRomaniaThatHadTheHighestEarningsLastYear();
            query.GetAllThePropertiesInSantoriniGreeceThatHaveAtLeast3StarsAndRoomsWithViewOfTheOceanAvailableForAnyDayInAugust2022();
            query.GetTop5AndLast5PropertiesThatHaveTheGreatestDifferenceBetweenTheirRatingAndTheirAverageReviewsRating();
            query.CalculateTheOccupancyRate();            
            query.GetTheLostIncomeFromUnoccupiedRoomsAtTheUnireaHotelForToday();
            query.GetTheMostCommonlyBookedRoomTypeForEachHotelInLondon();
            query.GetAllThePropertiesInDubrovnikCroatiaThatHaveAtLeast4StarsInCustomerReviewsAndAreAvailableForAtLeast7ConsecutiveDaysInTheMonthsOfJune();

        }
    }
}
