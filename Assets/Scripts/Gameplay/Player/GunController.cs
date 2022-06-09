using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	private Gun gun;
	public Gun Gun
	{
		get => gun;
		set
		{
			gun = value;
			if (gun.GunType.Equals(GunType.Pistol))
			{
				Player.Instance.Animations.SetBool(AnimationType.Rifle, false);
				Player.Instance.Animations.SetBool(AnimationType.Pistol, true);
			}
			else if (gun.GunType.Equals(GunType.Rifle))
			{
				Player.Instance.Animations.SetBool(AnimationType.Pistol, false);
				Player.Instance.Animations.SetBool(AnimationType.Rifle, true);
			}
		}
	}
	public int CurrentGunIndex { get; set; }

	public List<Gun> Guns = new List<Gun>();

	private void Start()
	{
		Gun = Guns[0];
	}

	public void UpgradeGun()
	{
		if (CurrentGunIndex + 1 >= Guns.Count) return;

		CurrentGunIndex++;
		ChangeGun(CurrentGunIndex);
	}

	public void ChangeGun(int index)
	{
		for (int i = 0; i < Guns.Count; i++)
		{
			Guns[i].StopFiring();
			Guns[i].gameObject.SetActive(false);
		}

		Guns[index].gameObject.SetActive(true);
		Gun = Guns[index];
		Gun.StartFiring();
	}

	public void Aim(float aimTime = 0)
	{
		Player.Instance.Animations.ChangeBodyAimWeight(1, aimTime);
	}

	public void Unaim(float aimTime = 0)
	{
		Player.Instance.Animations.ChangeBodyAimWeight(0, aimTime);
	}
}