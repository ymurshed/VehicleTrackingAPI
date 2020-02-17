using GraphQL;
using GraphQL.Language.AST;
using GraphQL.Types;
using MongoDB.Driver.GeoJsonObjectModel;

namespace VehicleTracker.Contracts.GraphQL
{
    // Custom Scalar Type
    public class LocationType : ScalarGraphType
    {
        public LocationType()
        {
            Name = "Location";
        }

        public override object Serialize(object value)
        {
            var serializedValue = ValueConverter.ConvertTo(value, typeof(GeoJsonPoint<GeoJson2DGeographicCoordinates>));
            return ParseValue(serializedValue);
        }

        public override object ParseValue(object value)
        {
            if (value is GeoJsonPoint<GeoJson2DGeographicCoordinates> geoJsonPoint)
            {
                var geoLocation = new GeoLocation
                {
                    Type = geoJsonPoint.Type.ToString(),
                    Latitude = geoJsonPoint.Coordinates.Latitude,
                    Longitude = geoJsonPoint.Coordinates.Longitude
                };
                return geoLocation;
            }
            return null;
        }

        public override object ParseLiteral(IValue value)
        {
            return value is GeoJsonPoint<GeoJson2DGeographicCoordinates> geoJsonPoint
                ? ParseValue(geoJsonPoint)
                : value is StringValue stringValue
                ? ParseValue(stringValue.Value)
                : null;
        }
    }

    public class GeoLocation
    {
        public string Type { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
