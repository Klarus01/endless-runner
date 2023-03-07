using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public GameObject[] cornerPrefabs;
    public List<GameObject> activeRoads = new();

    private int numRoadsOnScreen = 0;
    private int previousPrefabIndex = -1;
    private int cornerNow = 0;
    private int cornerMax = 3;

    private Transform playerTransform;
    private Transform halfOfRoad;

    private GameObject lastPref;
    private float rotate = 0;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        for (int i = 0; i < 5; i++)
        {
            if (i < 2)
            {
                SpawnRoad(0);
            }
            else
            {
                SpawnRoad();
            }
        }

    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToHalf = Vector3.Distance(playerTransform.position, halfOfRoad.position);

        if (numRoadsOnScreen < 11)
        {
            if (distanceToHalf < 8f)
            {
                TryDeleteRoad(activeRoads[0]);
                SpawnRoad();
            }
        }

    }

    private void SpawnRoad(int prefabIndex = -1)
    {
        GameObject road;

        if (prefabIndex != -1)
        {
            road = Instantiate(roadPrefabs[prefabIndex]);
        }
        else if (cornerNow != cornerMax)
        {
            do
            {
                prefabIndex = Random.Range(0, roadPrefabs.Length);
            } while (prefabIndex == previousPrefabIndex);
            road = Instantiate(roadPrefabs[prefabIndex]);
        }
        else
        {
            cornerNow = 0;
            cornerMax = Random.Range(4, 6);
            prefabIndex = Random.Range(0, cornerPrefabs.Length);
            road = Instantiate(cornerPrefabs[prefabIndex]);
        }

        cornerNow++;
        road.transform.SetParent(transform);

        if (lastPref != null)
        {
            if (lastPref.CompareTag("TurnLeft"))
                rotate -= 90f;
            if (lastPref.CompareTag("TurnRight"))
                rotate += +90f;

            road.transform.position = lastPref.transform.GetChild(0).position;
            road.transform.Rotate(0f, rotate, 0f);
        }

        numRoadsOnScreen++;
        previousPrefabIndex = prefabIndex;
        activeRoads.Add(road);
        halfOfRoad = activeRoads[activeRoads.Count / 2].transform;
        lastPref = road;
    }

    private void TryDeleteRoad(GameObject roadToDestroy)
    {
        if (activeRoads.Count >= 10)
        {
            numRoadsOnScreen--;
            activeRoads.RemoveAt(0);
            Destroy(roadToDestroy);
        }
    }
}