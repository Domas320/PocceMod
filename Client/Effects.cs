﻿using CitizenFX.Core;
using PocceMod.Client.Effect;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PocceMod.Client
{
    public class Effects : BaseScript
    {
        private static readonly List<IEffect> _effects = new List<IEffect>();

        public Effects()
        {
            Tick += Telemetry.Wrap("effects", Update);
        }

        public static async Task<bool> Add(IEffect effect)
        {
            if (Exists(effect.Key))
                return false;

            _effects.Add(effect);
            await effect.Init();
            return true;
        }

        public static bool Exists(string key)
        {
            return _effects.Any(e => e.Key == key);
        }

        public static void Remove(string key)
        {
            foreach (var effect in _effects.Where(e => e.Key == key).ToArray())
            {
                _effects.Remove(effect);
                effect.Clear();
            }
        }

        public static Task<bool> AddEMPEffect(int vehicle) => Add(new EMPEffect(vehicle));

        public static Task<bool> AddWheelFireEffect(int vehicle) => Add(new WheelFireEffect(vehicle));

        public static Task<bool> AddTurboBoostEffect(int vehicle, TurboBoostEffect.Mode mode) => Add(new TurboBoostEffect(vehicle, mode));

        public static void RemoveTurboBoostEffect(int vehicle) => Remove(TurboBoostEffect.GetKeyFrom(vehicle));

        public static Task<bool> AddTurboBrakeEffect(int vehicle) => Add(new TurboBrakeEffect(vehicle));

        public static void RemoveTurboBrakeEffect(int vehicle) => Remove(TurboBrakeEffect.GetKeyFrom(vehicle));

        public static Task<bool> AddHornEffect(int vehicle) => Add(new HornEffect(vehicle));

        public static void RemoveHornEffect(int vehicle) => Remove(HornEffect.GetKeyFrom(vehicle));

        public static Task<bool> AddJesusEffect(int entity) => Add(new JesusEffect(entity));

        public static Task<bool> AddMosesEffect(int entity) => Add(new MosesEffect(entity));

        private static Task Update()
        {
            foreach (var effect in _effects.ToArray())
            {
                effect.Update();

                if (effect.Expired)
                {
                    _effects.Remove(effect);
                    effect.Clear();
                }
            }

            return Delay(100);
        }
    }
}
