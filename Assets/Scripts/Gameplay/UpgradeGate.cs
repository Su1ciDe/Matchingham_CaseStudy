using UnityEngine;

public class UpgradeGate : Gate
{
	protected override void OnEnterGate(GunController gunController)
	{
		gunController.UpgradeGun();
	}
}
