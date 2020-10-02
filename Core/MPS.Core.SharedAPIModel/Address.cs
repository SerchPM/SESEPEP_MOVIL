using Newtonsoft.Json;
using Sysne.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel
{
    public partial class Response
    {
        [JsonProperty("authenticationResultCode")]
        public string AuthenticationResultCode { get; set; }

        [JsonProperty("brandLogoUri")]
        public Uri BrandLogoUri { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("resourceSets")]
        public List<ResourceSet> ResourceSets { get; set; }

        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }

        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        [JsonProperty("traceId")]
        public string TraceId { get; set; }
    }

    public partial class ResourceSet
    {
        [JsonProperty("estimatedTotal")]
        public long EstimatedTotal { get; set; }

        [JsonProperty("resources")]
        public List<Resource> Resources { get; set; }
    }

    public partial class Resource : ObservableObject
    {
        string type;
        [JsonProperty("__type")]
        public string Type { get => type; set => Set(ref type, value); }

        List<double> bbox;
        [JsonProperty("bbox")]
        public List<double> Bbox { get => bbox; set => Set(ref bbox, value); }

        string name;
        [JsonProperty("name")]
        public string Name { get => name; set => Set(ref name, value); }

        Point point;
        [JsonProperty("point")]
        public Point Point { get => point; set => Set(ref point, value); }

        Address address;
        [JsonProperty("address")]
        public Address Address { get => address; set => Set(ref address, value); }

        string confidence;
        [JsonProperty("confidence")]
        public string Confidence { get => confidence; set => Set(ref confidence, value); }

        string entityType;
        [JsonProperty("entityType")]
        public string EntityType { get => entityType; set => Set(ref entityType, value); }

        List<GeocodePoint> geocodePoints;
        [JsonProperty("geocodePoints")]
        public List<GeocodePoint> GeocodePoints { get => geocodePoints; set => Set(ref geocodePoints, value); }

        List<string> matchCodes;
        [JsonProperty("matchCodes")]
        public List<string> MatchCodes { get => matchCodes; set => Set(ref matchCodes, value); }
    }

    public partial class Address
    {
        [JsonProperty("countryRegion")]
        public string CountryRegion { get; set; }

        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }
    }

    public partial class GeocodePoint
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonProperty("calculationMethod")]
        public string CalculationMethod { get; set; }

        [JsonProperty("usageTypes")]
        public List<string> UsageTypes { get; set; }
    }

    public partial class Point
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public List<double> Coordinates { get; set; }
    }
}
