using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityFPS.ShootingSystem;

namespace UnityFPS.PlayerSystem.HUD
{
    public class GunSlotManager : SerializedMonoBehaviour
    {
        [OdinSerialize]
        private Dictionary<GunSlotNumber, UIGunSlot> GunSlotMap { get; set; }

        private UIGunSlot CurrentSlot { get; set; }

        public void InjectGunData(IEnumerable<(GunSlotNumber, IGunData)> gunDatas)
		{
            UnselectAllSlots();

            foreach ((GunSlotNumber slot, IGunData data) in gunDatas)
			{
                GunSlotMap[slot].InitializeGunData(data);
            }
		}

        public void SelectSlot(GunSlotNumber gunData)
		{
            UIGunSlot targetSlot = GunSlotMap[gunData];

            if (IsNewSlot(targetSlot))
			{
                UnselectCurrentSlot();
                SelecNewSlot(targetSlot);
            }
        }

        private void UnselectCurrentSlot()
		{
            if (CurrentSlot != null)
            {
                CurrentSlot.UnselectSlot();
            }
        }

        private void SelecNewSlot(UIGunSlot newSlot)
        {
            newSlot.SelectSlot();
            CurrentSlot = newSlot;
        }

        private bool IsNewSlot(UIGunSlot slot)
		{
            return slot != CurrentSlot;
        }

        private void UnselectAllSlots()
		{
            foreach(KeyValuePair<GunSlotNumber, UIGunSlot> slotNumberBySlot in GunSlotMap)
			{
                slotNumberBySlot.Value.UnselectSlot();
            }
		}
    }
}