using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public TapToStart TapToStartScreen => tapToStartScreen ? tapToStartScreen : tapToStartScreen = GetComponentInChildren<TapToStart>(true);
	private TapToStart tapToStartScreen;
	
	public LevelUI LevelUI => levelUI ? levelUI : levelUI = GetComponentInChildren<LevelUI>(true);
	private LevelUI levelUI;
	
	private void Awake()
	{
		TapToStartScreen.SetActiveUI(true);
		
		//TODO: Tutorial
	}
	
	private void OnEnable()
	{
		LevelManager.OnLevelLoad += OnLevelLoad;
		LevelManager.OnLevelSuccess += OnLevelSuccess;
		LevelManager.OnLevelFail += OnLevelFail;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelLoad -= OnLevelLoad;
		LevelManager.OnLevelSuccess -= OnLevelSuccess;
		LevelManager.OnLevelFail -= OnLevelFail;
	}

	private void OnLevelLoad()
	{
		TapToStartScreen.SetActiveUI(true);
	}

	private void OnLevelSuccess()
	{
		//LevelSuccessScreen.SetActive(true);
	}

	private void OnLevelFail()
	{
		//LevelFailScreen.SetActive(true);
	}
}
