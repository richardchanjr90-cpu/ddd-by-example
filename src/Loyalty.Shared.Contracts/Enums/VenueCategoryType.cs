using System;

namespace Loyalty.Shared.Contracts.Enums
{
    [Flags]
    public enum VenueCategoryType : ulong
    {
        Cafe = 1,
        CoffeeShop = 2,
        Bar = 4,
        Restaurant = 8,
        SnackBar = 16,
        SeasonalCafe = 32,
        SportsBar = 64,
        IceCreamCafe = 128,
        Bistro = 256,
        InternetCafe = 512,
        KidsCafe = 1024,
        DiningRoom = 2048,
        FastFood = 4096,
        StreetFood = 8192,
        ArtBar = 16384,
        Pizzeria = 32768,
        Pub = 65536‬,
        Shawarma = 131072,
        Gastrobar = 262144‬,
        Bakery = 524288,
        Burgers = 1048576,
        Sushi = 2097152,
        CoffeePoint = 4194304‬,
        FoodTruck = 8388608,
        Grillbar = 16777216‬,
        Coworking = 33554432‬,
        Pancake = 67108864‬,
    }
}