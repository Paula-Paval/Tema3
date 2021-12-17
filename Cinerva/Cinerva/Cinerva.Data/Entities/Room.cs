﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomCategoryId { get; set; }
        public RoomCategory RoomCategory { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public IList<RoomFeature> RoomFeatures { get; set; }
        public IList<RoomFacility> RoomFacilities { get; set; }
        public IList<Reservation> Reservations { get; set; }
        public IList<RoomReservation> RoomReservations { get; set; }
    }
}
