namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Destinations.Results
{
    public class MyTransferDestinationsResult
    {
        public ICollection<MyTransferDestination> Destiantions { get; set; } = [];
    }
    public class MyTransferDestination
    {
        public int? Delay { get; set; }
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Type { get; set; }
    }
}
