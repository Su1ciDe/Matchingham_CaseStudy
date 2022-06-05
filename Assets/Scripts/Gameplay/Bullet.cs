using DG.Tweening;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private void OnEnable()
	{
		transform.DOMove(Player.Instance.transform.forward, Player.Instance.GunController.Gun.bulletSpeed).SetSpeedBased(true).SetEase(Ease.Linear).SetLoops(10, LoopType.Incremental);
	}

	private void OnDisable()
	{
		transform.DOKill();
	}
}