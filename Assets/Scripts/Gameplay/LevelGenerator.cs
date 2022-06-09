using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : Singleton<LevelGenerator>
{
	[SerializeField] private float leftLimit;
	[SerializeField] private float rightLimit;
	[SerializeField] private int lineCount;

	[Space]
	[SerializeField] private Transform obstacleHolder;
	[SerializeField] private Transform gateHolder;
	[SerializeField] private Finish finish;

	[Space]
	[SerializeField] private UpgradeGate upgradeGatePrefab;
	[SerializeField] private Obstacle obstaclePrefab;
	[SerializeField] private List<GameObject> obstacleModels = new List<GameObject>();

	private float levelLength;
	private int upgradeCount;

	public void GenerateLevel()
	{
		ClearLevel();

		levelLength = finish.transform.position.z;

		float parcel = levelLength / lineCount;
		float currentZ = 0;
		for (int i = 0; i < lineCount; i++)
		{
			currentZ += parcel - upgradeCount;

			if (currentZ > (upgradeCount + 1) * (levelLength / 3) && upgradeCount < 3)
			{
				upgradeCount++;
				Instantiate(upgradeGatePrefab, currentZ * Vector3.forward, Quaternion.identity, gateHolder);
			}
			else
			{
				for (int j = 0; j < 3; j++)
				{
					var obstacle = Instantiate(obstaclePrefab, new Vector3((rightLimit - 1) * (j - 1), 0, currentZ), Quaternion.identity, obstacleHolder);
					obstacle.HitCount = Random.Range(1, 4 + upgradeCount);
					obstacle.Model = obstacleModels[Random.Range(0, obstacleModels.Count)];
					obstacle.ObstacleType = (ObstacleType)Random.Range(0, Enum.GetValues(typeof(ObstacleType)).Length);
					obstacle.Setup();
				}
			}
		}
	}

	private void ClearLevel()
	{
		upgradeCount = 0;
		foreach (Transform child in obstacleHolder)
			Destroy(child.gameObject);

		foreach (Transform child in gateHolder)
			Destroy(child.gameObject);
		// 	for (int i = 0; i < obstacleHolder.childCount; i++)
		// 	Destroy(obstacleHolder.GetChild(0).gameObject);
		// for (int i = 0; i < gateHolder.childCount; i++)
		// 	Destroy(gateHolder.GetChild(0).gameObject);
	}
}