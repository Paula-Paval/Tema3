create table Roles(Id int identity(1,1),
			Name nvarchar(50) Not Null,
			primary key(Id));

create table Users(Id int identity(1,1),
			FirstName nvarchar(200) Not Null,
			LastName nvarchar(200) Not Null,			
			Email nvarchar(100) Not Null,
			Password char(100) Not Null,
			Phone char(25),
			IsDeleted bit default 0,
			IsBanned bit default 0,
			RoleId int,
			primary key(Id),
			foreign key(RoleId) references Roles(Id));

create table Countries(Id int identity(1,1),
				Name nvarchar(100) Not Null,
				primary key(Id));
				
create table Cities(Id int identity(1,1),
			Name nvarchar(100) Not Null,
			CountryId int,
			primary key(Id),
			foreign key (CountryId) references Countries(Id));

create table PropertyTypes(Id int identity(1,1),
					Type nvarchar(50) not null
					primary key(Id));

create table Properties( Id int identity(1,1),
			Name nvarchar(250) not null,
			Rating decimal(2, 1) not null,
			Description nvarchar(MAX) not null,
			Address nvarchar(200) not null,
			Phone char(25) not null,
			TotalRooms int not null,
			NumberOfDaysForRefunds int not null,
			CityId int,
			AdministratorId int,
			PropetyTypeId int
			primary key (Id),
			foreign key (CityId) references Cities(Id),
			foreign key(AdministratorId) references Users(Id),
			foreign key(PropetyTypeId) references PropertyTypes(Id));

create table PropertyImages( Id int identity(1,1),
				ImageUrl nvarchar(2500) not null,
				PropetyId int,
				foreign key (PropetyId) references Properties(Id));

create table GeneralFeatures(Id int identity(1,1), 
				Name nvarchar(500) not null,
				IconUrl nvarchar(500) not null,
				primary key(Id));
create table PropertyFacilities(PropertyId int,
						GeneralFeatureId int,
						primary key(PropertyId, GeneralFeatureId),
						foreign key(PropertyId) references Properties(Id),
						foreign key(GeneralFeatureId) references GeneralFeatures(Id));

create table RoomCategories( Id int identity(1,1),
				Name varchar(100) not null,
				BedsCount int not null,
				Description nvarchar(500),
				PricePerNight decimal (20, 2) not null,
				primary key(Id));
create table Rooms(Id int identity(1,1),
				RoomCategory int not null,
				PropertyId int,
				primary key(Id),
				foreign key(PropertyId) references Properties(Id),
				foreign key(RoomCategory) references RoomCategories(Id));
				
				
create table RoomFeatures(Id int identity(1,1),  
				Name nvarchar(500) not null,
				IconUrl nvarchar(500) not null,
				primary key(Id));

create table RoomFacilities(RoomId int,
						RoomFeatureId int,
						primary key(RoomId, RoomFeatureId),
						foreign key(RoomId) references Rooms(Id),
						foreign key(RoomFeatureId) references RoomFeatures(Id));

create table Reviews(UserId int,
		PropertyId int,
		Description nvarchar(200),
		Rating int not null,
		ReviewDate date not null,
		primary key(UserId, PropertyId),
		foreign key(UserId) references Users(Id),
		foreign key(PropertyId) references Properties(Id));

create table Reservations(Id int identity(1,1),
			UserId int,
			CheckIndate date not null,
			CheckOutdate date not null,
			Price decimal(20, 2) not null,
			PayedStatus bit not null,
			PaymentMethod varchar(20) not null,
			CancelDate date,
			primary key(Id),
			foreign key(UserId) references Users(Id));

create table RoomReservations(Id int identity(1,1),
			RoomId int,
			ReservationId int,
			primary key(Id),
			foreign key(RoomId) references Rooms(Id),
			foreign key(ReservationId) references Reservations(Id));
				

