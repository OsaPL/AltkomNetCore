using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.FakeServices.Fakers
{
    public class CustomerFaker: Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true); //throws execption when norule for a prop
            RuleFor(prop => prop.Id, f => f.IndexFaker);
            RuleFor(prop => prop.Name, f => f.Lorem.Word());
            RuleFor(prop => prop.FirstName, f => f.Lorem.Word());
            RuleFor(prop => prop.Birth, f => f.Date.Recent());
            RuleFor(prop => prop.Gender, f => f.PickRandom<Gender>());
            //RuleFor(prop => prop.Gender, f => (Gender)f.Random.Int(0,2));
            Ignore(prop => prop.Devices);
        }
    }
}
