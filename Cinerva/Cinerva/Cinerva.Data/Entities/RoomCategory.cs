﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class RoomCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BedsCount { get; set; }
        public string Description { get; set; }
        public decimal PricePerNight { get; set; }
        public IList<Room> Rooms { get; set; }
    }
}
