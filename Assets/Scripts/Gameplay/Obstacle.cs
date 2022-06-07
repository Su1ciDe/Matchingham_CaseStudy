using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public int HitCount = 1;
	public ObstacleType ObstacleType;
	public GameObject Model;

	[Space]
	[SerializeField] private Transform modelHolder;
	[SerializeField] private TextMeshProUGUI txtHitCount;

	public IEnumerator Setup()
	{
		if (ObstacleType.Equals(ObstacleType.Destroyable))
		{
			txtHitCount.gameObject.SetActive(true);
			txtHitCount.SetText(HitCount.ToString());
		}
		else if (ObstacleType.Equals(ObstacleType.Undestroyable))
		{
			txtHitCount.gameObject.SetActive(false);
		}

		yield return StartCoroutine(DestroyChild());

		GameObject model = null;
		if (Model)
			model = Instantiate(Model, modelHolder);
		if (model)
			model.GetComponentInChildren<MeshRenderer>().material = GameManager.Instance.ColorScheme.ColorSchemeDictionary[ObstacleType];
	}

	private IEnumerator DestroyChild()
	{
		for (int i = 0; i < modelHolder.childCount; i++)
		{
			yield return new WaitForEndOfFrame();
			DestroyImmediate(modelHolder.GetChild(0).gameObject);
		}
	}

	private void OnValidate()
	{
		StartCoroutine(Setup());
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger || !other.attachedRigidbody) return;

		if (other.attachedRigidbody.TryGetComponent(out Player player))
		{
			OnPlayerHit(player);
		}

		if (other.attachedRigidbody.TryGetComponent(out Bullet bullet))
		{
			OnBulletHit(bullet);
		}
	}

	private void OnPlayerHit(Player player)
	{
		player.Die();
	}

	private void OnBulletHit(Bullet bullet)
	{
		bullet.gameObject.SetActive(false);

		if (ObstacleType.Equals(ObstacleType.Destroyable))
		{
			HitCount--;
			txtHitCount.SetText(HitCount.ToString());
			if (HitCount <= 0)
			{
				//TODO: particles
				Destroy(gameObject);
			}
		}
		else if (ObstacleType.Equals(ObstacleType.Undestroyable))
		{
			transform.DOKill();
			transform.DOMove(1 * bullet.transform.forward, .2f).SetEase(Ease.InOutSine);
		}
	}
}