using SchoolManagementApp.Domain.SharedKernel.Persons;
using Shared.Domain.Entities;
using System;

namespace SchoolManagementApp.Domain.SharedKernel
{
    public class Person:BaseEntity<int>
    {
        internal Person() { }

        public virtual int GetAge()
        {
            var age = DateTimeOffset.UtcNow.Subtract(DateOfBirth);
            return int.Parse(age.ToString());
        }

        public virtual void UpdateAddress(string city, string street, string house_Number)
        {
            Address = new Address(city, street, house_Number);
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string StateOfOrigin { get; set; }
        public virtual string LG_Of_Origin { get; set; }
        public virtual DateTimeOffset DateOfBirth { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual Address Address { get; protected internal set; }
    }
}
