using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;

namespace Lib
{
    public static class FeatureExtension
    {
        public static bool IsInBounds(this Feature feature, double x, double y)
        {
            switch (feature.Geometry.Type)
            {
                case GeoJSONObjectType.Polygon:
                    var geo = (Polygon) feature.Geometry;
                    return IsVisible(x, y, geo.Coordinates[0].Coordinates.ToList());
                case GeoJSONObjectType.MultiPolygon:
                    foreach (var coordinate in ((MultiPolygon) feature.Geometry).Coordinates)
                        if (IsVisible(x, y, coordinate.Coordinates[0].Coordinates.ToList()))
                            return true;
                    break;
            }

            return false;
        }

        private static bool IsVisible(double x, double y, List<IPosition> points)
        {
            int i, j = points.Count - 1;
            var isVisible = false;
            for (i = 0; i < points.Count; i++)
            {
                if (points[i].Longitude < y && points[j].Longitude >= y || points[j].Longitude < y && points[i].Longitude >= y)
                    if (points[i].Latitude + (y - points[i].Longitude) / (points[j].Longitude - points[i].Longitude) * (points[j].Latitude - points[i].Latitude) < x)
                        isVisible = !isVisible;
                j = i;
            }
            return isVisible;
        }
    }
}