using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] roadPrefabs;
    [SerializeField]
    private GameObject[] cornerPrefabs;
    private List<GameObject> activeRoads = new();

    private int rotate = 0;
    private int zRoad = -10;
    private int xRoad = 0;
    private int previousIndex = -1;
    private int lastCorner = 0;
    private int maxLengthToLastCorner = 3;

    private Transform playerTransform;
    private Transform halfOfRoad;
    private GameObject lastPref;

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

        if (activeRoads.Count < 11)
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
        for (int i = 0; i < 6; i++)
        {
            if (i < 3)
            {
                SpawnRoad(0);
            }
            else
            {
                SpawnRoad();
            }
        }
    }

    private void SpawnRoad(int index = -1)
    {
        GameObject road;

        if (index != -1)
        {
            road = Instantiate(roadPrefabs[index]);
        }
        else if (lastCorner != maxLengthToLastCorner)
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
        lastPref = road;

        if (rotate.Equals(-90) || rotate.Equals(270))
        {
            xRoad -= 10;
        }
        else if (rotate.Equals(90) || rotate.Equals(-270))
        {
            xRoad += 10;
        }
        else if (rotate.Equals(180) || rotate.Equals(-180))
        {
            zRoad -= 10;
        }
        else
        {
            zRoad += 10;
        }

        Vector3 roadPlace = new(xRoad, 0f, zRoad);
        road.transform.position = roadPlace;
        road.transform.Rotate(0, rotate, 0);

        if (lastPref.CompareTag("TurnLeft"))
        {
            rotate -= 90;
        }
        else if (lastPref.CompareTag("TurnRight"))
        {
            rotate += 90;
        }

        if (rotate.Equals(-360) || rotate.Equals(360))
        {
            rotate = 0;
        }

        activeRoads.Add(road);
        previousIndex = index;
        halfOfRoad = activeRoads[activeRoads.Count / 2].transform;
    }

    private void TryDeleteRoad(GameObject roadToDestroy)
    {
        if (activeRoads.Count >= 10)
        {
            activeRoads.RemoveAt(0);
            Destroy(roadToDestroy);
        }
    }
}