﻿using MenuAPI;
using PocceMod.Shared;

namespace PocceMod.Client.Menus
{
    [MainMenuInclude]
    public class AircraftHornMenu : Menu
    {
        public AircraftHornMenu() : base("PocceMod", "select horn")
        {
            var horns = (Config.HornList.Length > 0) ? Config.HornList : new string[] { "SIRENS_AIRHORN" };
            foreach (var horn in horns)
            {
                var menuItem = new MenuItem(horn);
                AddMenuItem(menuItem);
            }

            OnItemSelect += (menu, item, index) => SetAircraftHorn(index);
        }

        public static void SetAircraftHorn(int horn)
        {
            if (!Common.EnsurePlayerIsVehicleDriver(out int player, out int vehicle))
                return;

            Vehicles.SetAircraftHorn(vehicle, horn);
        }
    }
}
