using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	public Gun Gun { get; set; }
	public int CurrentGunIndex { get; set; }

	public List<Gun> Guns = new List<Gun>();

	private void Awake()
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
			if (i.Equals(index))
			{
				Guns[i].gameObject.SetActive(true);
				Gun = Guns[i];
				Gun.StartFiring();
			}
			else
			{
				Guns[i].StopFiring();
				Guns[i].gameObject.SetActive(false);
			}
		}
	}
}