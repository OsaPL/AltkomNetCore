using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.FakeServices
{
    public class DeviceFaker : Faker<Device>
    {
        public DeviceFaker()
        {
            StrictMode(true); //throws execption when norule for a prop
            RuleFor(prop => prop.Id, f => f.IndexFaker);
            RuleFor(prop => prop.Name, f => f.Lorem.Word());
            RuleFor(prop => prop.Firmware, f => f.System.Version().ToString());
            RuleFor(prop => prop.Active, f => f.Random.Bool(0.8f)); //80% chance for true
            Ignore(prop => prop.Owner);
        }
    }
}
