using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour
{
	public PLANT_TYPE plantType;

	public void DestroyPlant(int time = 0)
	{
		Destroy(gameObject, time);
	}
}

public enum PLANT_TYPE
{
    Attract,
    Repel
}
