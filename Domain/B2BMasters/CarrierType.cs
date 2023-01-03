using System.Runtime.Serialization;
using Domain.Common;

namespace Domain.B2BMasters
{
    public class CarrierType : Enumeration<string>
    {
        private static string OwnVehicle => "Self Vehicle";
        public CarrierType() : base(OwnVehicle)
        { }

        

        public static readonly CarrierType SelfCarrier = new CarrierType(OwnVehicle);
        private CarrierType(string value) : base(value) { }
    }
}