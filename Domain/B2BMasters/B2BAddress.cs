using System;
using Domain.B2BMasters.Args;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class B2BAddress : Entity<long>
    {
        protected B2BAddress()
        {
            //For ORM
        }

        private B2BAddress(B2BAddressType addressType, string address, string firstName, string lastName, string email,
            string city, string district, string countryName, string creatorEmail, string phone, string fullName,
            bool? freeTradeZone)
        {
            AddressType = addressType;
            Address = address;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            City = city;
            District = district;
            CountryName = countryName;
            CreatorEmail = creatorEmail;
            Phone = phone;
            FullName = fullName;
            FreeTradeZone = freeTradeZone;
        }

        public B2BAddressType AddressType { get; protected set; }
        public string? Address { get; protected set; }
        public long B2BMasterId { get; protected set; }
        public string? FirstName { get; protected set; }
        public string? LastName { get; protected set; }
        public string? Email { get; protected set; }
        public string? City { get; protected set; }
        public string? District { get; protected set; }
        public string? CountryName { get; protected set; }
        public string? CreatorEmail { get; protected set; }
        public string? Phone { get; protected set; }
        public string? FullName { get; protected set; }
        public bool? FreeTradeZone { get; protected set; }

        public static B2BAddress Create(CreateB2BAddressArg arg) => new B2BAddress(
            addressType: arg.AddressType,
            address: arg.Address,
            firstName: arg.FirstName,
            lastName: arg.LastName,
            email: arg.Email,
            city: arg.City,
            district: arg.District,
            countryName: arg.CountryName,
            creatorEmail: arg.Email,
            phone: arg.Phone,
            fullName: arg.FullName,
            freeTradeZone: arg.FreeTradeZone
        );
    }
}