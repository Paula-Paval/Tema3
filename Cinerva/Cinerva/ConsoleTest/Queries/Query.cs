using Cinerva.Data;
using Cinerva.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.TestConsole.Queries
{
    public class Query
    {
        public CinervaDbContext Cinerva { get; set; }

        public void GetPropertiesFromIasi()
        {
            Console.WriteLine("2:");

            var q2 = Cinerva.Properties
                .Where(p => p.City.Name == "Iasi")
                .Select(p => new { p.Name, p.Description, p.Address, p.TotalRooms })
                .ToList();

            foreach (var item in q2)
            {
                Console.WriteLine($"{item.Name} {item.Address}, {item.TotalRooms}");
            }
        }

        public void GetClientsThatMadeAReservationAndPaidForIt()
        {
            Console.WriteLine("3:");

            var q3 = Cinerva.Reservations
                .Where(r => r.PayedStatus == true)
                .Select(r => new {Client=r.User.FirstName+" "+r.User.LastName, r.User.LastName, r.User.Email, r.User.Phone })
                .ToList();

            foreach (var item in q3)
            {
                Console.WriteLine($"{item.Client}, {item.Email}");
            }
        }
        public void GetUsersWithAdminRole()
        {
            Console.WriteLine("4:");

            var q4 = Cinerva.Properties
                .Where(p => p.City.Name == "Brasov" && p.PropertyType.Type == "Guest house")
                .Select(p => new { p.User.FirstName, p.User.LastName })
                .OrderBy(s=>s.FirstName).ThenBy(s=>s.LastName)
                .ToList();

            foreach (var item in q4)
            {
                Console.WriteLine($"{item.FirstName} {item.LastName}");
            }
        }

        public void GetRoomTypeAndPriceForRoomsAtIntercontinentalRomania()
        {
            Console.WriteLine("5:");

            var q5 = Cinerva.Rooms
                .Where(r => r.Property.Name == "Intercontinental" && r.Property.City.Country.Name == "Romania")
                .Select(r => new { r.RoomCategory.Name, r.RoomCategory.PricePerNight }).Distinct()
                .ToList();

            foreach (var item in q5)
            {
                Console.WriteLine($"{item.Name}->>>{item.PricePerNight}");
            }
        }

        public void GetTop5PropertiesInRomaniaByReservationCount()
        {
            Console.WriteLine("6:");
            var startOfNovember = new DateTime(2021, 11, 01);
            var endOfNovember = new DateTime(2021, 11, 30);

            var q6 = Cinerva.RoomReservations
                    .Where(r => r.Room.Property.City.Country.Name == "Romania" && r.Reservation.CheckIndate >= startOfNovember && r.Reservation.CheckIndate <= endOfNovember)
                    .GroupBy(r => r.Room.Property.Name)
                    .Select(g => new { PropertyName = g.Key, TotalReservations = g.Count() })
                    .OrderByDescending(g => g.TotalReservations)
                    .ToList();

            foreach (var item in q6)
            {
                Console.WriteLine($"{item.PropertyName}->{item.TotalReservations}");
            }
        }
        public void GetAvaibleHotelsWithPool()
        {
            Console.WriteLine("7:");

            var q7 = Cinerva.Rooms
                        .Where(r => (r.Reservations.Count == 0 || r.Reservations.Count(d => d.CheckIndate > DateTime.Now && d.CheckOutdate < DateTime.Now) > 0)
                        && r.Property.GeneralFeatures.Count(x=>x.Name== "Swimming pool/ Jacuzzi")>0)
                       .GroupBy(r => r.Property.Name)
                       .Select(g => new { PropertyName = g.Key })
                       .ToList();

            foreach (var item in q7)
            {
                Console.WriteLine($"{item.PropertyName}");
            }
        }

        public void GetTotalNumberOfAvaibleRoomsFromAllPropertiesInRomaniaPriceRangeBetween70And100EuroForThisMonth()
        {
            Console.WriteLine($"8:");

            var startOfDecember = new DateTime(2021, 12, 01);
            var endOfDecember = new DateTime(2021, 12, 31);

            var q8 = Cinerva.Rooms
                        .Where(r => (r.RoomReservations.Count == 0
                        || r.Reservations.Count(d => d.CheckIndate < startOfDecember && d.CheckOutdate >= startOfDecember && d.CheckOutdate <= endOfDecember && (31 - (d.CheckOutdate.Day - startOfDecember.Day)) > 0) > 0
                        || r.Reservations.Count(d => d.CheckIndate >= startOfDecember && d.CheckOutdate <= endOfDecember && d.CheckOutdate >= startOfDecember && (31 - (d.CheckOutdate.Day - d.CheckIndate.Day)) > 0) > 0
                        || r.Reservations.Count(d => d.CheckIndate >= startOfDecember && d.CheckIndate <= endOfDecember && d.CheckOutdate > endOfDecember && (31 - (endOfDecember.Day - d.CheckIndate.Day)) > 0) > 0
                        || r.Reservations.Count(d => (d.CheckIndate < startOfDecember && d.CheckOutdate < startOfDecember) || d.CheckIndate > endOfDecember) > 0)
                        && (r.RoomCategory.PricePerNight >= 70 && r.RoomCategory.PricePerNight <= 100))
                        .GroupBy(r => r.Property.Name)
                        .Select(g => new { g.Key, AvaibleRooms = g.Count() })
                        .ToList();

            foreach (var item in q8)
            {
                Console.WriteLine($"{item.Key}, {item.AvaibleRooms}");
            }
        }
        public void GetTheHighestRatedPropertyThatHasSpaAndRoomsAvailableForTheNextWeekend()
        {
            var startOfNextWeekend = new DateTime(2021, 12, 04);
            var endOfNextWeekend = new DateTime(2021, 12, 05);

            Console.WriteLine("9:");

            var q9 = Cinerva.Rooms
                    .Where(r => (r.Reservations.Count == 0
                    || r.Reservations.Count(d => d.CheckIndate < startOfNextWeekend && d.CheckOutdate >= startOfNextWeekend && d.CheckOutdate <= endOfNextWeekend && (2 - (d.CheckOutdate.Day - startOfNextWeekend.Day)) > 0) > 0
                    || r.Reservations.Count(d => d.CheckIndate >= startOfNextWeekend && d.CheckIndate <= endOfNextWeekend && d.CheckOutdate >= startOfNextWeekend && d.CheckOutdate <= endOfNextWeekend && (2 - (d.CheckOutdate.Day - d.CheckIndate.Day)) > 0) > 0
                    || r.Reservations.Count(d => d.CheckIndate >= startOfNextWeekend && d.CheckIndate <= endOfNextWeekend && d.CheckOutdate > endOfNextWeekend && (2 - (endOfNextWeekend.Day - d.CheckIndate.Day)) > 0) > 0
                    || r.Reservations.Count(d => (d.CheckIndate < startOfNextWeekend && d.CheckOutdate < startOfNextWeekend) || d.CheckIndate > startOfNextWeekend) > 0)
                    && (r.Property.GeneralFeatures.Count(g => g.Name == "Swimming pool/ Jacuzzi") > 0))
                    .GroupBy(r => new { r.Property.Name, Rating = r.Property.Rating })
                    .Select(g => new { g.Key.Name, g.Key.Rating })
                    .OrderByDescending(g => g.Rating)
                    .FirstOrDefault();

            Console.WriteLine($"{q9.Name}, {q9.Rating}");
        }
        public void GetTheMonthOfTheYearThatHasTheMostReservationsForAcertainHotel()
        {
            Console.WriteLine("10:");

            var q10 = Cinerva.RoomReservations
                                            .Where(r => r.Room.Property.Name == "Intercontinental")
                                            .GroupBy(r => r.Reservation.CheckIndate.Month)
                                            .Select(g => new { Month = g.Key, Count = g.Count() })
                                            .OrderByDescending(g => g.Count)
                                            .FirstOrDefault();
           
            Console.WriteLine($"{q10.Month}->{q10.Count}");
        }
        public void FindPropertiesThatHave2DoubleRoomsAvailableForTheNextWeekInAntwerpThatCostBetween100And150Euro()
        {
            Console.WriteLine("11:"); 

            var startOfNextWeek = new DateTime(2021, 12, 5);
            var endOfNextWeek = new DateTime(2021, 12, 18);

            var q11 = Cinerva.Rooms
                    .Where(r => (r.Reservations.Count == 0
                    || r.Reservations.Count(d => d.CheckIndate < startOfNextWeek && d.CheckOutdate >= startOfNextWeek && d.CheckOutdate <= endOfNextWeek && (14 - (d.CheckOutdate.Day - startOfNextWeek.Day)) > 0) > 0
                    || r.Reservations.Count(d => d.CheckIndate >= startOfNextWeek && d.CheckIndate <= endOfNextWeek && d.CheckOutdate >= startOfNextWeek && d.CheckOutdate <= endOfNextWeek && (14 - (d.CheckOutdate.Day - d.CheckIndate.Day)) > 0) > 0
                    || r.Reservations.Count(d => d.CheckIndate >= startOfNextWeek && d.CheckIndate <= endOfNextWeek && d.CheckOutdate > endOfNextWeek && (14 - (endOfNextWeek.Day - d.CheckIndate.Day)) > 0) > 0
                    || r.Reservations.Count(d => (d.CheckIndate < startOfNextWeek && d.CheckOutdate < startOfNextWeek) || d.CheckIndate > startOfNextWeek) > 0)
                    && r.RoomCategory.Name == "Double" && r.Property.City.Name == "Antwerp" && (r.RoomCategory.PricePerNight >= 100 && r.RoomCategory.PricePerNight <= 150))
                    .GroupBy(r => r.Property.Name)
                    .Select(g => new { Name = g.Key })
                    .ToList();

            foreach (var item in q11)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
        public void GetTheNumberOfDistinctGuestsThatHaveMadeBookingsForMayInEachHotelInIasi()
        {

            Console.WriteLine("12:");

            var q12 = Cinerva.RoomReservations
                .Where(r => r.Room.Property.City.Name == "Iasi" && r.Reservation.CheckIndate.ToString().Contains("2021-05"))
                .GroupBy(r => new { r.Room.Property.Name })
                .Select(g => new { g.Key.Name, TotalUsers = g.Count() })
                .ToList();

            foreach (var item in q12)
            {
                Console.WriteLine($"{item.Name}->{item.TotalUsers}");
            }
        }
        public void GetLocationsThatWillBeAvaibleNextYearBetweenMayAndSeptember()
        {
            Console.WriteLine("13:");

            var startOfMay = new DateTime(2022, 05, 01);
            var endOfAugust = new DateTime(2022, 08, 31);

            var q13 = Cinerva.Rooms
                .Where(r => r.Reservations.Count == 0
                || r.Reservations.Count(d => d.CheckIndate < startOfMay && d.CheckOutdate >= startOfMay && d.CheckOutdate <= endOfAugust && (123 - (d.CheckOutdate.Day - startOfMay.Day)) > 0) > 0
                || r.Reservations.Count(d => d.CheckIndate >= startOfMay && d.CheckIndate <= endOfAugust && d.CheckOutdate <= endOfAugust && 123 - (d.CheckOutdate.Day - d.CheckIndate.Day) > 0) > 0
                || r.Reservations.Count(d => d.CheckIndate >= startOfMay && d.CheckIndate <= endOfAugust && d.CheckOutdate > endOfAugust && 123 - (endOfAugust.Day - d.CheckIndate.Day) > 0) > 0
                || r.Reservations.Count(d => (d.CheckIndate < startOfMay && d.CheckOutdate <= startOfMay) || d.CheckIndate > endOfAugust) > 0)
                .GroupBy(r => new { r.Property.Name, r.Property.Description, r.Property.Address })
                .Select(g => new { g.Key.Name, g.Key.Description, g.Key.Address })
                .ToList();

            foreach (var item in q13)
            {
                Console.WriteLine($"{item.Name}, {item.Address}");
            }

        }
        public void GetTop10HotelsInRomaniaThatHadTheHighestEarningsLastYear()
        {

            Console.WriteLine("14:");

            var startOf2021 = new DateTime(2021, 01, 01);
            var endOf2021 = new DateTime(2021, 12, 31);
            var q14 = Cinerva.RoomReservations
                            .Where(r => r.Reservation.CheckIndate >= startOf2021 && r.Reservation.CheckIndate <= endOf2021
                                    && r.Room.Property.City.Country.Name == "Romania")
                            .GroupBy(r => new { Name = r.Room.Property.Name, CityName = r.Room.Property.City.Name, Rating = r.Room.Property.Rating }, r => new { r.Reservation.Price })
                            .Select(g => new { g.Key.Name, g.Key.CityName, g.Key.Rating, Total = g.Sum(x => x.Price) })
                            .OrderByDescending(g => g.Total)
                            .Take(10)
                            .ToList();

            foreach (var item in q14)
            {
                Console.WriteLine($"{item.Name}, {item.CityName}, {item.Rating}, {item.Total}");
            }
        }
        public void GetAllThePropertiesInSantoriniGreeceThatHaveAtLeast3StarsAndRoomsWithViewOfTheOceanAvailableForAnyDayInAugust2022()
        {
            Console.WriteLine("15:");

            var startOfAugust = new DateTime(2022, 08, 01);
            var endOfAugust = new DateTime(2022, 08, 31);

            var q15 = Cinerva.Rooms
                    .Where(r => (r.RoomReservations.Count == 0
                        || r.Reservations.Count(d => d.CheckIndate < startOfAugust && d.CheckOutdate >= startOfAugust && d.CheckOutdate <= endOfAugust && (31 - (d.CheckOutdate.Day - startOfAugust.Day)) > 0) > 0
                        || r.Reservations.Count(d => d.CheckIndate >= startOfAugust && d.CheckOutdate <= endOfAugust && d.CheckOutdate >= startOfAugust && (31 - (d.CheckOutdate.Day - d.CheckIndate.Day)) > 0) > 0
                        || r.Reservations.Count(d => d.CheckIndate >= startOfAugust && d.CheckIndate <= endOfAugust && d.CheckOutdate > endOfAugust && (31 - (endOfAugust.Day - d.CheckIndate.Day)) > 0) > 0
                        || r.Reservations.Count(d => (d.CheckIndate < startOfAugust && d.CheckOutdate < startOfAugust) || d.CheckIndate > endOfAugust) > 0)
                        && (r.Property.Rating >= 3 && r.Property.GeneralFeatures.Count(g => g.Name == "Ocean view") > 0 && r.Property.City.Name == "Santorini" && r.Property.City.Country.Name == "Greece")
                        )
                    .GroupBy(r => new { r.Property.Name })
                    .Select(g => new { Name = g.Key.Name })
                    .ToList();

            foreach (var item in q15)
            {
                Console.WriteLine($"{item.Name}");
            }
        }
        public void GetTop5AndLast5PropertiesThatHaveTheGreatestDifferenceBetweenTheirRatingAndTheirAverageReviewsRating()
        {
            Console.WriteLine("16:");
            var top5 = Cinerva.Reviews
                .GroupBy(r => new { Name = r.Property.Name, Difference = r.Property.Rating - r.Rating })
                .Select(g => new { g.Key.Name, g.Key.Difference })
               .OrderBy(s => s.Difference)
                .Take(2);

            var last5 = Cinerva.Reviews
                .GroupBy(r => new { Name = r.Property.Name, Difference = r.Property.Rating - r.Rating })
                .Select(g => new { g.Key.Name, g.Key.Difference })
                .OrderByDescending(s => s.Difference)
                .Take(2);

            var q16 = top5.Concat(last5).ToList();

            foreach (var item in q16)
            {
                Console.WriteLine($"{item.Name} {item.Difference}");
            }
        }
        public void CalculateTheOccupancyRate()
        {
            Console.WriteLine("17:");

            var listOdDays = new List<DateTime>();

            for (int i = 1; i <= 30; i++)
            {
                listOdDays.Add(new DateTime(2021, 11, i));
            }

            for (int i = 0; i < 30; i++)
            {
                var q17 = Cinerva.RoomReservations
                 .Where(r => r.Reservation.CheckIndate <= listOdDays.ElementAt(i) && r.Reservation.CheckOutdate >= listOdDays.ElementAt(i)
                            && r.Room.Property.Name == "Intercontinental")
                 .GroupBy(r => new { r.Room.Property.TotalRooms })
                 .Select(g => new
                 {
                     Percent = ((double)g.Count() / g.Key.TotalRooms) * 100,
                     Color = (((double)g.Count() / g.Key.TotalRooms) * 100) < 50 ? "Green"
                                            : (((double)g.Count() / g.Key.TotalRooms) * 100) > 50 && (((double)g.Count() / g.Key.TotalRooms) * 100) < 90 ? "Yellow"
                                            : "Red",
                     TotalRooms = g.Key.TotalRooms
                 })
                 .ToList();

                foreach (var item in q17)
                {
                    Console.WriteLine($"{item.Color}, {item.Percent}, {item.TotalRooms}");
                }

            }
        }
        public void GetTheLostIncomeFromUnoccupiedRoomsAtTheUnireaHotelForToday()
        {

            Console.WriteLine("18:");

            var q18 = Cinerva.Rooms
                .Where(r => (r.Reservations.Count == 0
                || r.Reservations.Count(d => (d.CheckIndate < DateTime.Now && d.CheckOutdate < DateTime.Now) || (d.CheckIndate > DateTime.Now)) > 0)
                && r.Property.Name == "Unirea")
                .Sum(r => r.RoomCategory.PricePerNight);

            Console.WriteLine($"Sum is:{q18}");           
        }

        public void GetTheMostCommonlyBookedRoomTypeForEachHotelInLondon()
        {
            Console.WriteLine($"19:");

            var q19 = Cinerva.Rooms
                .Where(r => r.Property.City.Name == "London" && r.Reservations.Count > 0)
                .GroupBy(r => new { Type = r.RoomCategory.Name, r.Property.Name })
                .Select(g => new { g.Key.Type, g.Key.Name, Number = g.Count() })
                .Where(s => s.Number == Cinerva.Rooms
                                 .Where(r => r.Property.City.Name == "London" && r.Reservations.Count > 0)
                                 .GroupBy(r => new { Type = r.RoomCategory.Name, r.Property.Name })
                                 .Select(g => new { g.Key.Type, g.Key.Name, Number = g.Count() })
                                  .Max(g => g.Number))
                .SingleOrDefault();

           Console.WriteLine($"{q19.Type}");
        }

        public void GetAllThePropertiesInDubrovnikCroatiaThatHaveAtLeast4StarsInCustomerReviewsAndAreAvailableForAtLeast7ConsecutiveDaysInTheMonthsOfJune()
        {

            Console.WriteLine("20:");

            var startOfJune = new DateTime(2022, 06, 01);
            var endOfJune = new DateTime(2022, 06, 30);

            var q20 = Cinerva.Rooms
                .Where(r => (r.Reservations.Count == 0
                || r.Reservations.Count(d => d.CheckIndate >= startOfJune && d.CheckIndate <= endOfJune && d.CheckOutdate >= startOfJune && d.CheckOutdate <= endOfJune && (d.CheckIndate.Day - startOfJune.Day >= 7 || endOfJune.Day - d.CheckOutdate.Day >= 7)) > 0
                || r.Reservations.Count(d => d.CheckIndate < startOfJune && d.CheckOutdate >= startOfJune && d.CheckOutdate <= endOfJune && (endOfJune.Day - d.CheckOutdate.Day >= 7)) > 0
                || r.Reservations.Count(d => d.CheckIndate >= startOfJune && d.CheckIndate <= endOfJune && d.CheckOutdate > endOfJune && (d.CheckIndate.Day - startOfJune.Day >= 7)) > 0
                || r.Reservations.Count(d => (d.CheckIndate < startOfJune && d.CheckOutdate < startOfJune) || d.CheckIndate > endOfJune) > 0
                )
                && r.Property.Name == "Dubrovnik"
                && r.Property.City.Name == "Croatia")
                .Select(r => new { Property = r.Property.Name })
                .ToList();

            foreach (var item in q20)
            {
                Console.WriteLine($"{item.Property}");
            }
        }
    }
}