﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinerva.Data.Entities
{
    public class RoomFacility
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int RoomFeatureId { get; set; }
        public RoomFeature RoomFeature { get; set; }
    }
}
