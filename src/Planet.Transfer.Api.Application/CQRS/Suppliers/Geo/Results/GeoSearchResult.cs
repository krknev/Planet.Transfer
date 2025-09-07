namespace Planet.Transfer.Api.Application.CQRS.Suppliers.Geo.Results
{
    public class GeoSearchResult
    {
        public List<SearchResult> results { get; set; }
        public SearchQuery query { get; set; }
    }

    public class SearchQuery
    {
        public string text { get; set; }
        public SearchParsed parsed { get; set; }
    }
    public class SearchParsed
    {
        public string housenumber { get; set; }
        public string street { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string expected_type { get; set; }
    }


    public class SearchRank
    {
        public double importance { get; set; }
        public double popularity { get; set; }
        public double confidence { get; set; }
        public int confidence_city_level { get; set; }
        public int confidence_street_level { get; set; }
        public string match_type { get; set; }
    }

    public class SearchResult
    {
        public Datasource datasource { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string city { get; set; }
        public string municipality { get; set; }
        public string postcode { get; set; }
        public string suburb { get; set; }
        public string quarter { get; set; }
        public string street { get; set; }
        public string iso3166_2 { get; set; }
        public double lon { get; set; }
        public double lat { get; set; }
        public string state_code { get; set; }
        public string result_type { get; set; }
        public string formatted { get; set; }
        public string address_line1 { get; set; }
        public string address_line2 { get; set; }
        public Timezone timezone { get; set; }
        public string plus_code { get; set; }
        public string plus_code_short { get; set; }
        public SearchRank rank { get; set; }
        public string place_id { get; set; }
        public Bbox bbox { get; set; }
        public string district { get; set; }
        public string county_code { get; set; }
    }
}
