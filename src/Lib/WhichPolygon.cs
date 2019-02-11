using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GeoJSON.Net.Feature;
using Newtonsoft.Json;

namespace Lib
{
    public static class WhichPolygon
    {
        private static List<Feature> _features;

        static WhichPolygon()
        {
            if (_features == null)
                _features = LoadGeoJsonFeatures();
        }

        public static string Find(double px, double py)
        {
            foreach (var feature in _features)
                if (feature.IsInBounds(px, py))
                    return feature.Properties["name"].ToString();
            return null;
        }
        
        private static List<Feature> LoadGeoJsonFeatures()
        {
            var assembly = typeof(WhichPolygon).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Lib.tr-city-geojson.json");
            if (stream == null)
                throw new Exception("GeoJson data file cannot open");
            
            try
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var geoStr = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<FeatureCollection>(geoStr).Features;
                }    
            }
            catch (Exception)
            {
                throw new Exception("GeoJson data file cannot read");
            }
        }
    }
}
