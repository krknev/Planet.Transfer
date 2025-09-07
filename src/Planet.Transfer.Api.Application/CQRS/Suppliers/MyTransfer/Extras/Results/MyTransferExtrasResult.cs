namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Extras.Results
{
    public class MyTransferExtrasResult
    {
        public ICollection<MyTransfersExtra> Extra { get; set; } = [];
    }
    public class MyTransfersExtra
    {
        public int? id { get; set; }
        public string? Name { get; set; }
        public string? NameDE { get; set; }
        public string? NameEN { get; set; }
        public string? NameES { get; set; }
        public string? NameFR { get; set; }
    }
}
