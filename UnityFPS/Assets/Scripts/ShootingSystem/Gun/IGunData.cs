using System.Collections.Generic;
using UnityEngine;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.ShootingSystem
{
    public interface IGunData
    {
        public string GunName { get; }
        public Sprite GunIcon { get; }
        public IObservableVariable<int> CurrentLoadedAmmoObservable { get; }
        public IObservableVariable<int> CurrentReserveAmmoObservable { get; }
    }
}
