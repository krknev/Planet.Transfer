namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Bookings.Results
{

    public class MyTransferBookingBuIdResult
    {
        public bool HasMeet { get; set; }
        public int? WaitTime { get; set; }
        public string? MeetAndGreet { get; set; }
        public string? Status { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? CustomerCountry { get; set; }
        public string? CustomerPhone { get; set; }
        public int? Adults { get; set; }
        public int? Childs { get; set; }
        public int? Infants { get; set; }
        public string? SpecialRequirements { get; set; }
        public MyTransferBookedTransfer? BookedTransfer { get; set; }
        public ICollection<MyTransferBookingDetail> BookedDetails { get; set; } = [];
        public MyTransferBookedTotalPrice? TotalPrice { get; set; }
        public int? OrderId { get; set; }
        public string? DriverInfo { get; set; }
        public string? DriverTracking { get; set; }
    }

    public class MyTransferBookedTransfer
    {
        public int? TransportId { get; set; }
        public string? TransportName { get; set; }
        public int? MinPassengers { get; set; }
        public int? MaxPassengers { get; set; }
        public int? Suitcases { get; set; }
        public decimal? Price { get; set; }
        public string? ImageURL { get; set; } // Note: Case-sensitive match for JSON field
    }

    public class MyTransferBookingDetail
    {
        public string? BookingReference { get; set; }
        public string? BookingExternalReference { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public string? Pickup { get; set; }
        public int? TimeToPickup { get; set; }
        public string? PickupType { get; set; }
        public string? Dropoff { get; set; }
        public string? DropoffType { get; set; }
        public string? ExternalTransportLine { get; set; }
        public string? ExternalTransportLocator { get; set; }
        public string? ExternalTransportDate { get; set; }
        public string? ArrivalLine { get; set; }
        public string? ArrivalLocator { get; set; }
        public string? ArrivalPickUpTime { get; set; }
        public string? DepartureLine { get; set; }
        public string? DepartureLocator { get; set; }
        public string? DeparturePickUpTime { get; set; }
        public string? TransportLine { get; set; }
        public string? TransportLocator { get; set; }
        public string? TransportDate { get; set; }
        public ICollection<MyTransferBookedExtra> Extras { get; set; } = [];
    }

    public class MyTransferBookedExtra
    {
        public int? ExtraId { get; set; }
        public string? ExtraName { get; set; }
        public decimal? Amount { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }

    public class MyTransferBookedTotalPrice
    {
        public decimal? TransfersPrice { get; set; }
        public decimal? ExtrasPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Currency { get; set; }
    }
}
