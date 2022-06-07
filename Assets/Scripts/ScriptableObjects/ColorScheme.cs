using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorScheme", menuName = "ColorScheme", order = 1)]
public class ColorScheme : ScriptableObject
{
	[System.Serializable]
	public struct ColorValue
	{
		public ObstacleType ObstacleType;
		public Material Material;
	}

	[SerializeField] private List<ColorValue> colorValues = new List<ColorValue>();
	public Dictionary<ObstacleType, Material> ColorSchemeDictionary = new Dictionary<ObstacleType, Material>();

	private void OnEnable()
	{
		foreach (ColorValue colorValue in colorValues)
			ColorSchemeDictionary.Add(colorValue.ObstacleType, colorValue.Material);
	}
}