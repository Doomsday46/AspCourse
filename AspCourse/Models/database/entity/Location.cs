﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCourse.Models
{
    public class Location
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Tournament Tournament { get; set; }

        public long? UserId { get; set; }
        public User User { get; set; }

        public Location()
        {
            
        }

        public override bool Equals(object obj)
        {
            var location = obj as Location;
            return location != null &&
                   Id == location.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
