using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace utcAltkomDevices.Models
{
    public class Customer : Base
    {
        public string FirstName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
        public DateTime Birth { get; set; }
        public ICollection<Device> Devices { get; set; }
    }
    public enum Gender { Male = 0, Female = 1, NA = 2 }
}
