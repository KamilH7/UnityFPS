using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityFPS.ShootingSystem;

namespace UnityFPS.PlayerSystem.HUD
{
    public class UIGunSlot : MonoBehaviour
    {
        public IGunData AssingedGunData { get; private set; }

        [field: Header("References")]
        [field: SerializeField]
        private TMP_Text GunNameText{ get; set; }
        [field: SerializeField]
        private Image GunImage { get; set; }

        [field: Header("Settings")]
        [field: SerializeField]
        private float SelectedScale { get; set; }
        [field: SerializeField]
        private float UnselectedScale { get; set; }

        public void InitializeGunData(IGunData gunData)
		{
            AssingedGunData = gunData;
            GunNameText.text = gunData.GunName;
            GunImage.sprite = gunData.GunIcon;
        }

        [Button]
        public void SelectSlot()
		{
            SetGunImageScale(SelectedScale);
        }

        [Button]
        public void UnselectSlot()
        {
            SetGunImageScale(UnselectedScale);
        }

        private void SetGunImageScale(float scale)
		{
            GunImage.transform.localScale = Vector3.one * scale;
        }
    }
}