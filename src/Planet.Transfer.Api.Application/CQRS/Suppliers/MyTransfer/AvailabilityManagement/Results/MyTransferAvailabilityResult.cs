
namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.AvailabilityManagement.Results
{
    public class MyTransferAvailabilityResult
    {

        public string SessionId { get; set; } = string.Empty;
        public double DestinationLat { get; set; } = 0.0;
        public double DestinationLng { get; set; } = 0.0;
        public string FromDate { get; set; } = string.Empty;
        public DateTime? ToDate { get; set; } = null;
        public int Adults { get; set; } = 0;
        public int Childs { get; set; } = 0;
        public int Infants { get; set; } = 0;
        public bool OneWay { get; set; } = true;
        public bool ToAirport { get; set; } = true;
        public string PickupType { get; set; } = string.Empty;
        public string DropoffType { get; set; } = string.Empty;
        public ICollection<TransferPriceList> TransferPriceList { get; set; } = [];
    }

    public class TransferPriceList
    {
        public string TransferId { get; set; } = string.Empty;
        public int TransportId { get; set; }
        public string TransportName { get; set; } = string.Empty;
        public int MinPassengers { get; set; }
        public int MaxPassengers { get; set; }
        public int Suitcases { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal AppliedPromoDiscount { get; set; }
        public bool HasMeet { get; set; }
        public int WaitTime { get; set; }
        public ICollection<TransferExtra> Extras { get; set; } = [];
    }

    public class TransferExtra
    {
        public int ExtraId { get; set; }
        public string ExtraName { get; set; } = string.Empty;
        public int MinUnits { get; set; }
        public int MaxUnits { get; set; }
        public decimal Price { get; set; }
    }

}
