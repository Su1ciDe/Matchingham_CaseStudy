using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public Gun Gun { get; set; }
	public int CurrentGunIndex { get; set; }

	public List<Gun> Guns = new List<Gun>();

	private Player player => Player.Instance;
	
	public void UpgradeGun()
	{
		if (CurrentGunIndex + 1 >= Guns.Count) return;

		CurrentGunIndex++;
		ChangeGun(CurrentGunIndex);
	}

	public void ChangeGun(int index, bool startFiring = true)
	{
		for (int i = 0; i < Guns.Count; i++)
		{
			Guns[i].StopFiring();
			Guns[i].gameObject.SetActive(false);
		}

		Guns[index].gameObject.SetActive(true);
		Gun = Guns[index];
		if (Gun.GunType.Equals(GunType.Pistol))
		{
			player.Animations.SetBool(AnimationType.Rifle, false);
			player.Animations.SetBool(AnimationType.Pistol, true);
		}
		else if (Gun.GunType.Equals(GunType.Rifle))
		{
			player.Animations.SetBool(AnimationType.Pistol, false);
			player.Animations.SetBool(AnimationType.Rifle, true);
		}
		if (startFiring)
			Gun.StartFiring();
	}

	public void Aim(float aimTime = 0)
	{
		player.Animations.ChangeBodyAimWeight(1, aimTime);
	}

	public void Unaim(float aimTime = 0)
	{
		player.Animations.ChangeBodyAimWeight(0, aimTime);
	}
}