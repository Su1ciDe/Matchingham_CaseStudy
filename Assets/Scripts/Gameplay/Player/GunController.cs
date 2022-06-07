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
		for (int i = 0; i < Guns.Count; i++)
			Guns[i].gameObject.SetActive(false);
		Gun = Guns[Mathf.Clamp(CurrentGunIndex++, 0, Guns.Count)];
		Guns[CurrentGunIndex].gameObject.SetActive(true);
	}
}