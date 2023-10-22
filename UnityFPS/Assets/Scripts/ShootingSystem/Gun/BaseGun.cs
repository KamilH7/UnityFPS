using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFPS.DamageSystem;
using UnityFPS.Tools.ReactiveVariable;

namespace UnityFPS.ShootingSystem
{
    public abstract class BaseGun : SerializedMonoBehaviour, IGunData
    {
        public IObservableVariable<int> CurrentLoadedAmmoObservable { get => CurrentLoadedAmmo; }
        public IObservableVariable<int> CurrentReserveAmmoObservable { get => CurrentReserveAmmo; }

        [field: Header("Base References")]
        [field: SerializeField]
        protected BaseBullet BulletPrefab { get; set; }
        [field: SerializeField]
        protected ShootingPoint BulletShootingPoint { get; set; }

        [field: Header("Base Settings")]
        [field: OdinSerialize]
        public Dictionary<MaterialType, float> DamageByMaterialsMap { get; protected set; }
        [field: SerializeField]
        public string GunName { get; protected set; }
        [field: SerializeField]
        public Sprite GunIcon { get; protected set; }
        [field: SerializeField]
        protected int MagazineSize { get; set; }
        [field: SerializeField]
        protected int MaxReserveAmmo { get;  set; }
        [field: SerializeField]
        protected float MinTimeBetweenShots { get; set; }

        protected ReactiveVariable<int> CurrentLoadedAmmo { get; set; } = new ReactiveVariable<int>();
        protected ReactiveVariable<int> CurrentReserveAmmo { get; set; } = new ReactiveVariable<int>();
        protected bool IsShootingOnCooldown { get; set; } = false;
        protected WaitForSeconds ShootCooldownYieldInstruction { get; set; }

        public virtual void Initialize()
		{
            CurrentLoadedAmmo.Value = MagazineSize;
            CurrentReserveAmmo.Value = MaxReserveAmmo;

            ShootCooldownYieldInstruction = new WaitForSeconds(MinTimeBetweenShots);
        }

        public virtual void ShootInputStarted()
		{

        }

        public virtual void ShootInputStopped()
		{

		}

        public virtual void RealoadInput()
		{

		}

        protected virtual void OnEnable()
        {
            IsShootingOnCooldown = false;
        }

        protected virtual void OnDisable()
        {

        }

        protected virtual bool CanShoot()
		{
            return IsShootingOnCooldown == false;
        }

        protected virtual void ApplyShootCooldown()
		{
            StartCoroutine(ShootCooldownCoroutine());
        }

        protected virtual IEnumerator ShootCooldownCoroutine()
        {
            IsShootingOnCooldown = true;
            yield return ShootCooldownYieldInstruction;
            IsShootingOnCooldown = false;
        }

        protected virtual void InstantiateBullet(float power)
		{
            Instantiate(BulletPrefab).Initialize(DamageByMaterialsMap, BulletShootingPoint, power);
        }

        protected virtual void ApplyGunShake(float topRotation)
		{
            Quaternion initialRotation = transform.localRotation;

            Tween rotateUpTween = RotateTowards(initialRotation * Quaternion.Euler(-Vector3.right * topRotation), MinTimeBetweenShots * 0.2f);
            rotateUpTween.onComplete += () => RotateTowards(initialRotation, MinTimeBetweenShots * 0.8f);
        }

        protected Tween RotateTowards(Quaternion rotation, float rotateTime)
		{
            return transform.DOLocalRotateQuaternion(rotation, rotateTime);
        }
    }
}   