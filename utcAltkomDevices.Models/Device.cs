using System;

namespace utcAltkomDevices.Models
{
    public class Device : Base
    {
        public string Firmware { get; set; }
        public bool Active { get; set; }
        public Customer Owner { get; set; }

        public override string ToString()
        {
            return string.Join(" ", "Id:", Id, "Name:", Name, "Firmware:", Firmware, "Active:", Active);
        }
    }
}
