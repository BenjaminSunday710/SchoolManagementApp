using Shared.Domain;
using System.Collections.Generic;

namespace SchoolManagementApp.Domain.SharedKernel
{
    public class Address : ValueObject<Address>
    {
        protected Address() { }
        public Address(string city, string street, string house_Number)
        {
            City = city;
            Street = street;
            House_Number = house_Number;
        }

        public virtual string City { get; }
        public virtual string Street { get; }
        public virtual string House_Number { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return Street;
            yield return House_Number;
        }
    }
}
