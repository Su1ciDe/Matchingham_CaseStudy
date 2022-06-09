using UnityEngine;

public class UpgradeGate : Gate
{
	private bool hasEntered;

	protected override void OnEnterGate(GunController gunController)
	{
		if (hasEntered) return;
		
		gunController.UpgradeGun();
		hasEntered = true;
	}
}