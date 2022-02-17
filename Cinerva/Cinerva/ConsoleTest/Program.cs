using Cinerva.Data;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Cinerva.Data.Entities;
using Cinerva.TestConsole.Queries;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

           // var cinerva = new CinervaDbContext();
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var cString= config.GetConnectionString("Cinerva");
            var host = Host.CreateDefaultBuilder().ConfigureServices((x, y) => {
                y.AddDbContext<CinervaDbContext>(a => a.UseSqlServer(cString));
                y.AddScoped<IQuery, Query>();
                })
            .Build();
            var query = host.Services.GetRequiredService<IQuery>();
           
            //query.GetPropertiesFromIasi();
            //query.GetClientsThatMadeAReservationAndPaidForIt();
            //query.GetUsersWithAdminRole();
            //query.GetRoomTypeAndPriceForRoomsAtIntercontinentalRomania();
            //query.GetTop5PropertiesInRomaniaByReservationCount();
           // query.GetAvaibleHotelsWithPool();
            //query.GetTotalNumberOfAvaibleRoomsFromAllPropertiesInRomaniaPriceRangeBetween70And100EuroForThisMonth();
            //query.GetTheHighestRatedPropertyThatHasSpaAndRoomsAvailableForTheNextWeekend();
            //query.GetTheMonthOfTheYearThatHasTheMostReservationsForAcertainHotel();
            //query.FindPropertiesThatHave2DoubleRoomsAvailableForTheNextWeekInAntwerpThatCostBetween100And150Euro();
            //query.GetTheNumberOfDistinctGuestsThatHaveMadeBookingsForMayInEachHotelInIasi();
            //query.GetLocationsThatWillBeAvaibleNextYearBetweenMayAndSeptember();
            //query.GetTop10HotelsInRomaniaThatHadTheHighestEarningsLastYear();
            //query.GetAllThePropertiesInSantoriniGreeceThatHaveAtLeast3StarsAndRoomsWithViewOfTheOceanAvailableForAnyDayInAugust2022();
            //query.GetTop5AndLast5PropertiesThatHaveTheGreatestDifferenceBetweenTheirRatingAndTheirAverageReviewsRating();
            //query.CalculateTheOccupancyRate();
            //query.GetTheLostIncomeFromUnoccupiedRoomsAtTheUnireaHotelForToday();
           // query.GetTheMostCommonlyBookedRoomTypeForEachHotelInLondon();
            query.GetAllThePropertiesInDubrovnikCroatiaThatHaveAtLeast4StarsInCustomerReviewsAndAreAvailableForAtLeast7ConsecutiveDaysInTheMonthsOfJune();

        }
    }
}
