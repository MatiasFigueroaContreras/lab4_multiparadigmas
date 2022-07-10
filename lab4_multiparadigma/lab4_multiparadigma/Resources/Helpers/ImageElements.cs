using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_multiparadigma.Resources.Helpers
{
    public class ImageElements
    {
        private static string[] _elements = { "airport_shuttle",
                                            "blender",
                                            "breakfast_dining",
                                            "brunch_dining",
                                            "coffee",
                                            "coffee_maker",
                                            "cooking",
                                            "devices",
                                            "dining",
                                            "dinner_dining",
                                            "directions_bike",
                                            "directions_bus",
                                            "directions_car",
                                            "directions_railway",
                                            "directions_run",
                                            "directions_subway",
                                            "directions_walk",
                                            "flight",
                                            "flight_land",
                                            "flight_takeoff",
                                            "headphones",
                                            "headset_mic",
                                            "hiking",
                                            "icecream",
                                            "liquor",
                                            "local_bar",
                                            "local_cafe",
                                            "local_shipping",
                                            "local_taxi",
                                            "lunch_dining",
                                            "motorcycle",
                                            "nightlife",
                                            "nordic_walking",
                                            "outdoor_grill",
                                            "pedal_bike",
                                            "phone_android",
                                            "phone_iphone",
                                            "ramen_dining",
                                            "smartphone",
                                            "snowmobile",
                                            "snowshoeing",
                                            "sports_bar",
                                            "sports_baseball",
                                            "sports_basketball",
                                            "sports_football",
                                            "sports_gymnastics",
                                            "sports_handball",
                                            "sports_martial_arts",
                                            "sports_rugby",
                                            "sports_soccer",
                                            "sports_volleyball",
                                            "tablet_android",
                                            "tablet_mac",
                                            "tapas",
                                            "train",
                                            "tram",
                                            "two_wheeler"};
        public static List<string> Elements { get { return _elements.ToList(); } }

    }
}
