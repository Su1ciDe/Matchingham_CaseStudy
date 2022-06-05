using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float FiringRate = 1;
	public GunType GunType;
	public float bulletSpeed = 1;

	[SerializeField] private Transform muzzle;

	public bool CanFire { get; set; } = true;

	public void StartFiring()
	{
		StartCoroutine(FireCoroutine());
	}

	private IEnumerator FireCoroutine()
	{
		var wait = new WaitForSeconds(1 / FiringRate);
		while (CanFire)
		{
			var bullet = ObjectPooler.Instance.Spawn("Bullet", muzzle.position);

			yield return wait;
		}
	}
}
