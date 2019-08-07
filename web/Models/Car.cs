using System;

namespace DontParkHere.Models
{
    public class Car {
        public Guid Id { get; set; } 
        public string Identifier { get; set; }
        public string ZoneId { get; set; }
    }
}