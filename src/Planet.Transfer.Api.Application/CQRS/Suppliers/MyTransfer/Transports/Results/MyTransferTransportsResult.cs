namespace Planet.Transfer.Api.Application.CQRS.Suppliers.MyTransfer.Transports.Results
{
    public class MyTransferTransportsResult
    {
        public ICollection<MyTransferTransport> Transports { get; set; } = [];
    }

    public class MyTransferTransport
    {

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Phone { get; set; }
        public int? Delay { get; set; }
        //public int? CategoryId { get; set; }
        //public int? Max_people { get; set; }
        //public int? Min_people { get; set; }
        //public int? SubcategoryId { get; set; }
        //public int? Max_suitcases { get; set; }
        //public string? DescriptionDE { get; set; }
        //public string? DescriptionEN { get; set; }
        //public string? DescriptionES { get; set; }
        //public string? DescriptionFR { get; set; }
        //public string? ImageURL { get; set; }
        //public string? NameDE { get; set; }
        //public string? NameEN { get; set; }
        //public string? NameES { get; set; }
        //public string? NameFR { get; set; }
    }
}
