using UnityEngine;

public abstract class Gate : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out GunController gunController))
		{
			OnEnterGate(gunController);
		}
	}

	protected abstract void OnEnterGate(GunController gunController);
}