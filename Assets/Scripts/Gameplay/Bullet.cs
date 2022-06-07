using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public void Fire()
	{
		transform.DOMove(transform.position + (Player.Instance.GunController.Gun.BulletDistance * Player.Instance.transform.forward),
				Player.Instance.GunController.Gun.BulletDistance / Player.Instance.GunController.Gun.BulletSpeed).SetEase(Ease.Linear)
			.OnComplete(() => gameObject.SetActive(false));
	}

	private void OnDisable()
	{
		transform.DOKill();
	}
}