using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	private static Camera mainCamera;
	public static Camera MainCamera => mainCamera ? mainCamera : mainCamera = Camera.main;

	[Space]
	public ColorScheme ColorScheme;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}
}