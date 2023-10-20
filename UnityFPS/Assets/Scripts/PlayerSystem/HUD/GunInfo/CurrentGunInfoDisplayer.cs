using TMPro;
using UnityEngine;
using UnityFPS.ShootingSystem;

namespace UnityFPS.PlayerSystem.HUD
{
    public class CurrentGunInfoDisplayer : MonoBehaviour
    {
        [field: Header("References")]       
        [field: SerializeField]
        private TMP_Text GunNameText;
        [field: SerializeField]
        private TMP_Text LoadedAmmoText;
        [field: SerializeField]
        private TMP_Text ReserveAmmoText;

        private bool IsDisabled { get; set; }
        private IGunData AssignedGunData { get; set; }

        public void SetGunData(IGunData gunData)
		{
            if (AssignedGunData != null && gunData != AssignedGunData)
            {
                DetachFromEvents(AssignedGunData);
            }

            if (gunData == null)
			{
                if(IsDisabled == false)
				{
                    DisableGunInfo();
                }

                return;
            }
            else if(IsDisabled == true)
			{
                EnableGunInfo();
			}

            AttachToEvents(gunData);

            AssignedGunData = gunData;

            RefreshCurrentData();
        }

        private void RefreshCurrentData()
		{
            SetLoadedAmmoText(AssignedGunData.CurrentLoadedAmmoObservable.Value);
            SetReserveAmmoText(AssignedGunData.CurrentReserveAmmoObservable.Value);
            SetGunNameText(AssignedGunData.GunName);
        }

		private void AttachToEvents(IGunData gunData)
		{
            gunData.CurrentLoadedAmmoObservable.OnValueChanged += SetLoadedAmmoText;
            gunData.CurrentReserveAmmoObservable.OnValueChanged += SetReserveAmmoText;
        }

        private void DetachFromEvents(IGunData gunData)
        {
            gunData.CurrentLoadedAmmoObservable.OnValueChanged -= SetLoadedAmmoText;
            gunData.CurrentReserveAmmoObservable.OnValueChanged -= SetReserveAmmoText;
        }

        private void SetGunNameText(string gunName)
        {
            GunNameText.text = gunName;
        }

        private void SetLoadedAmmoText(int loadedAmmo)
		{
            LoadedAmmoText.text = loadedAmmo.ToString();
        }

        private void SetReserveAmmoText(int reserveAmmo)
		{
            ReserveAmmoText.text = reserveAmmo.ToString();
        }

        private void DisableGunInfo()
		{
            IsDisabled = true;
            gameObject.SetActive(false);
        }

        private void EnableGunInfo()
        {
            IsDisabled = false;
            gameObject.SetActive(true);
        }
    }
}