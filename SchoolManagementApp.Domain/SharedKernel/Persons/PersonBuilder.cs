using System;

namespace SchoolManagementApp.Domain.SharedKernel.Persons
{
    public class PersonBuilder
    {
        public PersonBuilder SetFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }
        public PersonBuilder SetLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        } 
        public PersonBuilder SetGender(Gender gender)
        {
            _gender = gender;
            return this;
        } 
        public PersonBuilder SetPhoneNumber(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
            return this;
        } 
        public PersonBuilder SetStateOfOrigin(string stateOfOrigin)
        {
            _stateOfOrigin = stateOfOrigin;
            return this;
        }
        public PersonBuilder SetLG_Of_Origin(string LG_Of_Origin)
        {
            _LG_Of_Origin = LG_Of_Origin;
            return this;
        }
        public PersonBuilder SetDateOfBirth(DateTimeOffset dateOfBirth)
        {
            _dateOfBirth = dateOfBirth;
            return this;
        }
        public PersonBuilder SetAddress(string city, string street, int house_Number)
        {
            _address = new Address(city, street, house_Number);
            return this;
        }

        public Person Build()
        {
            return new Person()
            {
                Address = _address,
                DateOfBirth = _dateOfBirth,
                FirstName = _firstName,
                LastName = _lastName,
                LG_Of_Origin = _LG_Of_Origin,
                StateOfOrigin = _stateOfOrigin,
                Gender = _gender,
                PhoneNumber = _phoneNumber,
                Created = DateTime.UtcNow,
                CreatedBy="Admin",
            };
        }

        private string _firstName;
        private string _lastName;
        private string _stateOfOrigin;
        private string _LG_Of_Origin;
        private DateTimeOffset _dateOfBirth;
        private Address _address;
        private Gender _gender;
        private string _phoneNumber;
    }
}
