using UnityEngine;

public class Player : Singleton<Player>
{
	public PlayerController PlayerController { get; private set; }
	public PlayerMovement PlayerMovement { get; private set; }
	public GunController GunController { get; private set; }
	public AnimationController Animations { get; private set; }

	private void Awake()
	{
		PlayerMovement = GetComponent<PlayerMovement>();
		PlayerController = GetComponent<PlayerController>();
		GunController = GetComponent<GunController>();
		Animations = GetComponentInChildren<AnimationController>();

		GunController.Unaim();
	}

	private void OnEnable()
	{
		LevelManager.OnLevelLoad += OnLevelLoaded;
		LevelManager.OnLevelStart += OnLevelStarted;
		LevelManager.OnLevelSuccess += OnLevelSuccess;
		LevelManager.OnLevelFail += OnLevelFailed;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelLoad -= OnLevelLoaded;
		LevelManager.OnLevelStart -= OnLevelStarted;
		LevelManager.OnLevelSuccess -= OnLevelSuccess;
		LevelManager.OnLevelFail -= OnLevelFailed;
	}

	private void OnLevelLoaded()
	{
		transform.position = Vector3.zero;
		GunController.CurrentGunIndex = 0;
		GunController.ChangeGun(0);
		GunController.Unaim();
	}

	private void OnLevelStarted()
	{
		PlayerController.CanControl = true;
		PlayerMovement.CanMove = true;
		GunController.Gun.StartFiring();
		GunController.Aim(.25f);
	}

	private void OnLevelSuccess()
	{
		PlayerController.CanControl = false;
		PlayerMovement.CanMove = false;
		GunController.Gun.StopFiring();
	}

	private void OnLevelFailed()
	{
		PlayerController.CanControl = false;
		PlayerMovement.CanMove = false;
		GunController.Gun.StopFiring();
	}

	public void Finish()
	{
		LevelManager.Instance.GameSuccess();
		GunController.Unaim(.25f);
	}

	public void Die()
	{
		GunController.Unaim();
		Animations.SetTrigger(AnimationType.Die);
		LevelManager.Instance.GameFail();
	}
}