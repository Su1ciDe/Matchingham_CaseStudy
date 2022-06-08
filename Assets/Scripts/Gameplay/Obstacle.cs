using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public bool IsDestroyed { get; set; }
	public int HitCount = 1;
	public ObstacleType ObstacleType;
	public GameObject Model;

	[Space]
	[SerializeField] private Transform modelHolder;
	[SerializeField] private TextMeshProUGUI txtHitCount;

	private List<Rigidbody> fragments;

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
		{
			// Change model colors
			var meshRenderers = model.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer meshRenderer in meshRenderers)
				meshRenderer.material = GameManager.Instance.ColorScheme.ColorSchemeDictionary[ObstacleType];

			// if destructable add rigidbodies to the variable
			fragments = new List<Rigidbody>();
			foreach (Transform child in model.transform)
			{
				if (child.TryGetComponent(out Rigidbody rb))
					fragments.Add(rb);
			}
		}
	}

	private IEnumerator DestroyChild()
	{
		for (int i = 0; i < modelHolder.childCount; i++)
		{
			yield return new WaitForEndOfFrame();
			DestroyImmediate(modelHolder.GetChild(0).gameObject);
		}
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		StartCoroutine(Setup());
	}
#endif

	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger || !other.attachedRigidbody) return;

		if (other.attachedRigidbody.TryGetComponent(out Player player) && !IsDestroyed)
		{
			OnPlayerHit(player);
		}

		if (other.attachedRigidbody.TryGetComponent(out Bullet bullet) && !IsDestroyed)
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
				IsDestroyed = true;
				txtHitCount.gameObject.SetActive(false);
				//TODO: particles

				if (fragments.Count > 0)
				{
					foreach (Rigidbody fragment in fragments)
					{
						fragment.isKinematic = false;
						fragment.AddExplosionForce(400, bullet.transform.position, 25);
						Destroy(fragment.gameObject, 5);
					}
				}
				else
					Destroy(gameObject);
			}
		}
		else if (ObstacleType.Equals(ObstacleType.Undestroyable))
		{
			transform.DOKill();
			transform.DOMove(transform.position + (1 * bullet.transform.forward), .2f).SetEase(Ease.InOutSine);
		}
	}
}