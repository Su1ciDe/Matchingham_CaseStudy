using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
	public static int CurrentLevel
	{
		get => PlayerPrefs.GetInt("CurrentLevel", 1);
		set
		{
			PlayerPrefs.SetInt("CurrentLevel", value);
			UIManager.Instance.LevelUI.ChangeLevelText(value);
		}
	}

	public SceneAsset TutorialLevel, MainLevel;

	public static event UnityAction OnLevelLoad;
	public static event UnityAction OnLevelStart;
	public static event UnityAction OnLevelSuccess;
	public static event UnityAction OnLevelFail;

	private AsyncOperation loadLevel;

	private void Awake()
	{
		DontDestroyOnLoad(this);
		LoadLevel();
	}

	private void Start()
	{
		// LoadLevel();
	}

	public void LoadLevel()
	{
		// if (CurrentLevel.Equals(1))
		// {
		// 	if (!SceneManager.GetActiveScene().name.Equals(TutorialLevel.name))
		// 		SceneManager.LoadScene(TutorialLevel.name);
		// }
		// else
		// {
		// 	if (!SceneManager.GetActiveScene().name.Equals(MainLevel.name))
		// 	{
		// 		SceneManager.LoadScene(MainLevel.name);
		// 		// return;
		// 	}
		//
		// 	LevelGenerator.Instance.GenerateLevel();
		// }
		//
		// Debug.Log("alo " + SceneManager.GetActiveScene().name);
		// OnLevelLoad?.Invoke();
		// Debug.Log("alo2");

		StartCoroutine(LoadLevelCoroutine());
	}

	private IEnumerator LoadLevelCoroutine()
	{
		if (CurrentLevel.Equals(1))
		{
			if (!SceneManager.GetActiveScene().name.Equals(TutorialLevel.name))
				loadLevel = SceneManager.LoadSceneAsync(TutorialLevel.name);
		}
		else
		{
			if (!SceneManager.GetActiveScene().name.Equals(MainLevel.name))
			{
				loadLevel = SceneManager.LoadSceneAsync(MainLevel.name);
			}

			Debug.Log("hop");
			yield return new WaitUntil(() => loadLevel.isDone);

			LevelGenerator.Instance.GenerateLevel();
		}

		yield return new WaitUntil(() => loadLevel.isDone);
		Debug.Log("alo");
		OnLevelLoad?.Invoke();
		Debug.Log("alo2");
	}

	public void StartLevel()
	{
		OnLevelStart?.Invoke();
	}

	public void GameSuccess()
	{
		CurrentLevel++;
		OnLevelSuccess?.Invoke();
	}

	public void GameFail()
	{
		OnLevelFail?.Invoke();
	}
}