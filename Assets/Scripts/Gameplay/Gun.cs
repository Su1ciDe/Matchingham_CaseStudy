using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float FiringRate = 1;
	public GunType GunType;
	public float BulletSpeed = 5;
	public int BulletDistance = 20;

	[Space]
	[SerializeField] private Transform muzzle;

	public bool CanFire { get; set; }

	public void StartFiring()
	{
		CanFire = true;
		Player.Instance.GunController.Aim(.25f);

		StartCoroutine(FireCoroutine());
	}

	public void StopFiring()
	{
		CanFire = false;
		Player.Instance.GunController.Unaim();
		StopCoroutine(FireCoroutine());
	}

	private IEnumerator FireCoroutine()
	{
		var wait = new WaitForSeconds(1 / FiringRate);
		while (CanFire)
		{
			var bullet = ObjectPooler.Instance.Spawn("Bullet", muzzle.position).GetComponent<Bullet>();
			bullet.Fire();

			yield return wait;
		}
	}
}