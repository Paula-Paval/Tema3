using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.TestConsole.Queries
{
    public interface  IQuery
    {
        public void GetPropertiesFromIasi();
        public void GetClientsThatMadeAReservationAndPaidForIt();
        public void GetUsersWithAdminRole();
        public void GetRoomTypeAndPriceForRoomsAtIntercontinentalRomania();
        public void GetTop5PropertiesInRomaniaByReservationCount();
        public void GetAvaibleHotelsWithPool();
        public void GetTotalNumberOfAvaibleRoomsFromAllPropertiesInRomaniaPriceRangeBetween70And100EuroForThisMonth();
        public void GetTheHighestRatedPropertyThatHasSpaAndRoomsAvailableForTheNextWeekend();
        public void GetTheMonthOfTheYearThatHasTheMostReservationsForAcertainHotel();
        public void FindPropertiesThatHave2DoubleRoomsAvailableForTheNextWeekInAntwerpThatCostBetween100And150Euro();
        public void GetTheNumberOfDistinctGuestsThatHaveMadeBookingsForMayInEachHotelInIasi();
        public void GetLocationsThatWillBeAvaibleNextYearBetweenMayAndSeptember();
        public void GetTop10HotelsInRomaniaThatHadTheHighestEarningsLastYear();
        public void GetAllThePropertiesInSantoriniGreeceThatHaveAtLeast3StarsAndRoomsWithViewOfTheOceanAvailableForAnyDayInAugust2022();
        public void GetTop5AndLast5PropertiesThatHaveTheGreatestDifferenceBetweenTheirRatingAndTheirAverageReviewsRating();
        public void CalculateTheOccupancyRate();
        public void GetTheLostIncomeFromUnoccupiedRoomsAtTheUnireaHotelForToday();
        public void GetTheMostCommonlyBookedRoomTypeForEachHotelInLondon();
        public void GetAllThePropertiesInDubrovnikCroatiaThatHaveAtLeast4StarsInCustomerReviewsAndAreAvailableForAtLeast7ConsecutiveDaysInTheMonthsOfJune();
    }
}
