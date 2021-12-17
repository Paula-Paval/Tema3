--1
ALTER TABLE Properties 
ADD ZipCode varchar(10);
UPDATE Properties
SET ZipCode='80911'
WHERE Id=5;
UPDATE Properties
SET ZipCode='44035'
WHERE Id=6;
UPDATE Properties
SET ZipCode='74820'
WHERE Id=7;
UPDATE Properties
SET ZipCode='27703'
WHERE Id=8;
UPDATE Properties
SET ZipCode='700028'
WHERE Id=10;
--2
SELECT P.Name, Description, Address, TotalRooms
FROM Properties P
INNER JOIN Cities C	ON P.CityId=C.Id  
	WHERE C.Name='Iasi';
---3
SELECT  CONCAT(U.FirstName, ' ', U.LastName) as Name, U.Email, U.Phone
FROM Users U
	INNER JOIN Reservations R	ON R.UserId=U.Id
	WHERE R.PayedStatus=1;
--4
SELECT U.FirstName, U.LastName
FROM Users U 
	INNER JOIN Properties P	ON P.AdministratorId=U.Id
	INNER JOIN PropertyTypes T ON T.Id=P.PropetyTypeId		
	INNER JOIN Cities C ON C.Id=P.CityId
	WHERE T.Type='Guest house' and C.Name='Brasov'
	ORDER By U.FirstName, U.LastName;
--5
SELECT DISTINCT Ro.Name, Ro.PricePerNight
FROM  RoomCategories Ro
	INNER JOIN Rooms R ON R.RoomCategory=Ro.Id
	INNER JOIN Properties P ON P.Id=R.PropertyId
	INNER JOIN Cities  C ON C.Id=P.CityId
	INNER JOIN Countries  Co ON Co.Id=C.CountryId
	WHERE P.Name='Intercontinental' and Co.Name='Romania';
--6
SELECT  P.Name, count(Re.Id)
FROM Properties P
	INNER JOIN Cities C ON C.Id=P.CityId
	INNER JOIN Countries  Co ON Co.Id=C.CountryId
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	INNER JOIN  RoomReservations Res ON Res.RoomId=R.Id
	INNER JOIN Reservations	Re ON Re.Id=Res.ReservationId
	WHERE Re.CheckIndate>='2021-11-01'and Re.CheckIndate<='2021-11-30' and Co.Name='Romania'
	GROUP BY P.Name
	ORDER BY count(Re.Id) desc;
--7
SELECT P.Name
FROM Properties P
   JOIN  Rooms  R	ON R.PropertyId=P.Id
   LEFT JOIN  RoomReservations Res  ON Res.RoomId=R.Id
   LEFT JOIN Reservations  Re ON Re.Id=Res.ReservationId
   INNER JOIN PropertyFacilities F ON P.Id=F.PropertyId
   INNER JOIN GeneralFeatures G ON G.Id=F.GeneralFeatureId
   WHERE G.Name='Swimming pool/ Jacuzzi'and ((GETDATE()<CheckIndate or GETDATE()>CheckOutdate) or (CheckIndate is null  and CheckOutdate is null))
   GROUP BY P.Name;

--8
SELECT P.Name, count(R.Id) as"Avaible"
FROM Properties P
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	INNER JOIN RoomCategories  Rc ON R.RoomCategory=Rc.Id
	LEFT JOIN RoomReservations Res  ON R.Id=Res.RoomId
	LEFT JOIN Reservations  Re ON Re.Id=Res.ReservationId
	WHERE Rc.PricePerNight between 70 and 100 and
		((CheckIndate<'2021-12-01' and CheckOutdate >='2021-12-01' and CheckOutdate<='2021-12-31' and(31-DATEDIFF(day,'2021-12-01', CheckOutdate))>0)
		or(CheckIndate>='2021-12-01'and CheckIndate <='2021-12-31' and CheckOutdate >='2021-12-01' and CheckOutdate<='2021-12-31' and(31-DATEDIFF(day,CheckIndate, CheckOutdate))>0)
		or(CheckIndate>='2021-12-01'and CheckIndate <='2021-12-31' and CheckOutdate>'2021-12-31' and (31-DATEDIFF(day,CheckIndate, '2021-12-31'))>0)
	    or((CheckIndate<'2021-12-01' and CheckOutdate <'2021-12-01') or CheckIndate>'2021-12-31')
		or(CheckIndate is null  and CheckOutdate is null))
	 GROUP BY  P.Name

--9

SELECT TOP(1) P.Name, P.Rating
FROM Properties P
	INNER JOIN PropertyFacilities Pf ON P.Id=Pf.PropertyId
	INNER JOIN GeneralFeatures G ON G.Id=PF.GeneralFeatureId
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	LEFT JOIN RoomReservations Res  ON R.Id=Res.RoomId
	LEFT JOIN Reservations Re ON Re.Id=Res.ReservationId
	WHERE G.Name='Swimming pool/ Jacuzzi' and 
	((CheckIndate<'2021-12-04' and CheckOutdate>='2021-12-04' and CheckOutdate<='2021-12-05' and 2-DATEDIFF(day,  '2021-12-04', CheckOutdate)>0)
	or(CheckIndate>='2021-12-04' and CheckIndate<='2021-12-05'and CheckOutdate>='2021-12-04' and CheckOutdate<='2021-12-05' and 2-DATEDIFF(day, CheckIndate, CheckOutdate)>0)
	or (CheckIndate>='2021-12-04' and CheckIndate<='2021-12-05'and CheckOutdate>'2021-12-05' and -DATEDIFF(day,CheckIndate, '2021-12-05')>0)
	or((CheckIndate<'2021-12-04' and CheckOutdate <'2021-12-04') or CheckIndate>'2021-12-05')
	or (CheckIndate is null  and CheckOutdate is null))	
	GROUP BY P.Name, P.Rating
	ORDER BY P.Rating desc
	
--10 
SELECT TOP(1) MONTH(Re.CheckIndate) as"Month", count(Re.Id) 
FROM Reservations Re	
	INNER JOIN RoomReservations Res ON Res.ReservationId=Re.Id
	INNER JOIN Rooms R ON R.Id=Res.RoomId
	INNER JOIN Properties P ON P.Id=R.PropertyId 
	WHERE P.Name='Intercontinental' 
	GROUP BY MONTH(Re.CheckIndate)
	ORDER BY count(Re.Id) desc 
--11

SELECT P.Name
FROM Properties P
	INNER JOIN Rooms R ON P.Id=R.PropertyId
	INNER JOIN RoomCategories C ON R.RoomCategory=C.Id
	LEFT JOIN RoomReservations Res  ON Res.RoomId=R.Id
	LEFT JOIN Reservations Re  ON Re.Id=Res.ReservationId
	INNER JOIN Cities ON Cities.Id=P.CityId
	WHERE C.Name='Double'  and  Cities.Name='Antwerp' and C.PricePerNight between 100 and 150 
	and ((CheckIndate<'2021-12-06' and CheckOutdate>='2021-12-06' and CheckOutdate<='2021-12-19' and 14-DATEDIFF(day,  '2021-12-06', CheckOutdate)>0)
	or(CheckIndate>='2021-12-06' and CheckIndate<='2021-12-19'and CheckOutdate>='2021-12-06' and CheckOutdate<='2021-12-19' and 14-DATEDIFF(day, CheckIndate, CheckOutdate)>0)
	or (CheckIndate>='2021-12-06' and CheckIndate<='2021-12-19'and CheckOutdate>'2021-12-19' and 14-DATEDIFF(day,CheckIndate, '2021-12-19')>0)
	or((CheckIndate<'2021-12-06' and CheckOutdate <'2021-12-06') or CheckIndate>'2021-12-19')
	or (CheckIndate is null  and CheckOutdate is null))	
	GROUP BY P.Name, P.TotalRooms
	
--12 
SELECT count(distinct U.Id) as "Number of distinct guests", P.Name
FROM Users U
	INNER JOIN Reservations R ON R.UserId=U.Id
	INNER JOIN RoomReservations Rez On Rez.ReservationId=R.Id
	INNER JOIN Rooms Ro ON Ro.Id=Rez.RoomId
	INNER JOIN Properties P ON P.Id=Ro.PropertyId
	INNER JOIN Cities C ON C.Id=P.CityId
	WHERE C.Name='Iasi' and R.CheckIndate LIKE '2021-05-%'
	GROUP BY P.Name

--13 

SELECT P.Name, P.Description, P.Address
FROM Properties P
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	LEFT JOIN RoomReservations Ro ON Ro.RoomId=R.Id
	LEFT JOIN Reservations Re ON Re.Id=Ro.ReservationId
	WHERE  ( (CheckIndate is null  and CheckOutdate is null)
			or ((CheckIndate<'2022-05-01' and CheckOutdate>='2022-05-01' and CheckOutdate<='2022-08-31')and 123-(DATEDIFF(day,'2022-05-01', CheckOutdate))>0)
			or((CheckIndate>='2022-05-01' and CheckIndate<='2022-08-31' and CheckOutdate<='2022-08-31') and 123-(DATEDIFF(day, CheckIndate, CheckOutdate))>0)
			or((CheckIndate>='2022-05-01' and CheckIndate<='2022-08-31' and CheckOutdate>'2022-08-31') and 123-(DATEDIFF(day, CheckIndate, '2022-08-31'))>0)
			or((CheckIndate<'2022-05-01' and CheckOutdate <'2022-05-01') or CheckIndate>'2022-08-31'))
	GROUP BY P.Name, P.Description, P.Address
	

--14
SELECT TOP(10) P.Name, C.Name, P.Rating
FROM Properties P
	INNER JOIN Cities C ON C.Id=P.CityId
	INNER JOIN Rooms  R ON R.PropertyId=P.Id
	INNER JOIN RoomReservations  Res ON Res.RoomId=R.Id
	INNER JOIN Reservations Re ON Re.Id=Res.ReservationId
	WHERE CheckIndate>='2021-01-01' and CheckIndate<='2021-12-31'
	GROUP BY P.Name, C.Name, P.Rating
	ORDER BY sum(Re.Price) desc


--15 
SELECT P.Name
FROM Properties P
	INNER JOIN Cities C ON C.Id=P.CityId
	INNER JOIN Countries Co ON Co.Id=C.CountryId
	INNER JOIN PropertyFacilities F ON F.PropertyId=P.Id
	INNER JOIN GeneralFeatures G ON G.Id=F.GeneralFeatureId
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	LEFT JOIN RoomReservations Ro ON Ro.RoomId=R.Id
	LEFT JOIN Reservations Re ON Re.Id=Ro.ReservationId
	WHERE ((CheckIndate is null  and CheckOutdate is null)
			or ((CheckIndate<'2022-08-01' and CheckOutdate>='2022-08-01' and CheckOutdate<='2022-08-31')and 31-(DATEDIFF(day,'2022-08-01', CheckOutdate))>0)
			or((CheckIndate>='2022-08-01' and CheckIndate<='2022-08-31' and CheckOutdate<='2022-08-31') and 31-(DATEDIFF(day, CheckIndate, CheckOutdate))>0)
			or((CheckIndate>='2022-08-01' and CheckIndate<='2022-08-31' and CheckOutdate>'2022-08-31') and 31-(DATEDIFF(day, CheckIndate, '2022-08-31'))>0)
			or((CheckIndate<'2022-08-01' and CheckOutdate <'2022-08-01') or CheckIndate>'2022-08-31')
		  )
	and P.Rating>=3 and G.Name='Ocean view' and Co.Name='Greece' and C.Name='Santorini'
	GROUP BY P.Name

--16

SELECT Name,  "Difference"
FROM (SELECT TOP (2) P.Name, (P.Rating- AVG(R.Rating)) as "Difference"
		FROM Properties P
				INNER JOIN Reviews R ON R.PropertyId=P.Id
				GROUP BY P.Name, P.Rating, R.Rating
				ORDER BY "Difference") last5
UNION ALL
SELECT Name,  "Difference"
FROM (SELECT TOP (2) P.Name, (P.Rating- AVG(R.Rating)) as "Difference"
		FROM Properties P
				INNER JOIN Reviews R ON R.PropertyId=P.Id
				GROUP BY P.Name, P.Rating, R.Rating
				ORDER BY "Difference" desc) top5


--17
create table days( Day date);
select * from days;

select D.Day, ((cast(count(R.Id) as decimal(3,2))/P.TotalRooms)*100) as "percent" ,
	case
		when ((cast(count(R.Id) as decimal(3,2))/P.TotalRooms)*100)<50  then  'Green'
		when ((cast(count(R.Id) as decimal(3,2))/P.TotalRooms)*100)between 50 and 90  then  'Yellow'
		when ((cast(count(R.Id) as decimal(3,2))/P.TotalRooms)*100) >90 then 'Red'
	end
	as "Color", P.TotalRooms 
	FROM days D, Reservations R 
		INNER JOIN RoomReservations Rez ON  Rez.ReservationId=R.Id
		INNER JOIN Rooms Ro ON Ro.Id=Rez.RoomId
		INNER JOIN Properties P ON P.Id=Ro.PropertyId
		WHERE D.Day>=CheckIndate and D.Day<=CheckOutdate 	
		and P.Name='Intercontinental'
		GROUP BY P.TotalRooms, D.Day


--18
SELECT sum(C.PricePerNight) as "Lost income from unoccupied rooms"
FROM RoomCategories C
	INNER JOIN Rooms R ON R.RoomCategory=C.Id
	INNER JOIN Properties P ON P.Id=R.PropertyId
	LEFT JOIN  RoomReservations Re ON Re.RoomId=R.Id
	LEFT JOIN Reservations Rez ON Rez.Id=Re.ReservationId
	WHERE ((CheckIndate<GETDATE() and CheckOutdate<GETDATE() )or CheckIndate>GETDATE() or CheckIndate is null or CheckOutdate is null) and P.Name='Unirea';

--19
SELECT "type"
FROM (SELECT T.Name as "type", P.Name, count(T.Id) as nr 
	FROM RoomCategories T
	INNER JOIN Rooms R ON R.RoomCategory=T.Id
	INNER JOIN Properties P ON P.Id=R.PropertyId
	INNER JOIN Cities C ON C.Id=P.CityId
	INNER JOIN RoomReservations Re ON Re.RoomId=R.Id
	WHERE C.Name='London'
	GROUP BY  T.Name, P.Name) x
	WHERE nr=(select max(nr) from (SELECT T.Name as "type", P.Name, count(T.Id) as nr 
										FROM RoomCategories T
											INNER JOIN Rooms R ON R.RoomCategory=T.Id
											INNER JOIN Properties P ON P.Id=R.PropertyId
											INNER JOIN Cities C ON C.Id=P.CityId
											INNER JOIN RoomReservations Re ON Re.RoomId=R.Id
											WHERE C.Name='Iasi'
											GROUP BY  T.Name, P.Name) as x);
--20

SELECT P.Name
FROM Properties P
	INNER JOIN Cities C ON C.Id=P.CityId
	INNER JOIN Countries Co ON Co.Id=C.CountryId
	INNER JOIN Reviews Rev ON Rev.PropertyId=P.Id
	INNER JOIN Rooms R ON R.PropertyId=P.Id
	LEFT JOIN RoomReservations Re ON Re.RoomId=R.Id
	LEFT JOIN Reservations Rez ON Rez.Id=Re.ReservationId
	WHERE Rev.Rating>=3 and (((CheckIndate>='2022-06-01' and CheckIndate<='2022-06-30' and CheckOutdate>='2022-06-01'and CheckOutdate<='2022-06-30' and  (DATEDIFF(day,'2022-06-01', CheckIndate)>=7 or DATEDIFF(day,CheckOutdate, '2022-06-30')>=7)) or 
								(CheckIndate<'2022-06-01'and CheckOutdate>='2022-06-01' and CheckOutdate<='2022-06-30' and   DATEDIFF(day,CheckOutdate, '2022-06-30')>=7) or
								(CheckIndate>='2022-06-01' and CheckIndate<='2022-06-30' and CheckOutdate>'2022-06-30' and   DATEDIFF(day,'2022-06-01', CheckIndate)>=7) or
								(CheckIndate<'2022-06-01' and CheckOutdate <'2022-06-01') or CheckIndate>'2022-06-30')or
								(CheckIndate is null and CheckOutdate is null)) 
								and C.Name='Dubrovnik' and Co.Name='Croatia';




	