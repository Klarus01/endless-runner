using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public GameObject[] cornerPrefabs;
    public List<GameObject> activeRoads = new();

    private int numRoadsOnScreen = 0;
    private int previousIndex = -1;
    private int lastCorner = 0;
    private int maxLengthToLastCorner = 3;
    private int index;

    private Transform playerTransform;
    private Transform halfOfRoad;

    private GameObject lastPref;
    private float rotate = 0;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        SpawnStartingRoads();
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

    private void SpawnStartingRoads()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRoad();
        }
    }

    private void SpawnRoad()
    {
        GameObject road;

        if (lastCorner != maxLengthToLastCorner)
        {
            do
            {
                index = Random.Range(0, roadPrefabs.Length);
            } while (index == previousIndex);
            road = Instantiate(roadPrefabs[index]);
        }
        else
        {
            lastCorner = 0;
            maxLengthToLastCorner = Random.Range(4, 6);
            index = Random.Range(0, cornerPrefabs.Length);
            road = Instantiate(cornerPrefabs[index]);
        }

        lastCorner++;
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

        lastPref = road;
        numRoadsOnScreen++;
        activeRoads.Add(road);
        previousIndex = index;
        halfOfRoad = activeRoads[activeRoads.Count / 2].transform;
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