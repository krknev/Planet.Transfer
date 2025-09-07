namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Results
{

    public class Timezone
    {
        public string name { get; set; }
        public string offset_STD { get; set; }
        public int offset_STD_seconds { get; set; }
        public string offset_DST { get; set; }
        public int offset_DST_seconds { get; set; }
        public string abbreviation_STD { get; set; }
        public string abbreviation_DST { get; set; }
    }

    public class Bbox
    {
        public double lon1 { get; set; }
        public double lat1 { get; set; }
        public double lon2 { get; set; }
        public double lat2 { get; set; }
    }

    public class Datasource
    {
        public string sourcename { get; set; }
        public string attribution { get; set; }
        public string license { get; set; }
        public string url { get; set; }
    }
}
