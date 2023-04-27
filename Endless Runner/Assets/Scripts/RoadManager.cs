using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    private int index;
    private int rotate = 0;
    private int zRoad = -10;
    private int xRoad = 0;
    private int previousIndex = -1;
    private int lastCorner = 0;
    private int maxLengthToLastCorner = 3;

    private Transform playerTransform;
    private GameObject road;
    private GameObject lastPref;

    private GameObject HalfwayRoad => activeRoads[activeRoads.Count / 2];
    private List<GameObject> activeRoads = new();

    [SerializeField] private GameObject[] roadPrefabs;
    [SerializeField] private GameObject[] cornerPrefabs;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
        SpawnStartingRoads();
    }

    private void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToHalf = Vector3.Distance(playerTransform.position, HalfwayRoad.transform.position);

        if (activeRoads.Count < 11)
        {
            if (distanceToHalf < 8f)
            {
                DeleteFirstRoad();
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
        if (index != -1)
        {
            road = Instantiate(roadPrefabs[index]);
        }
        else if (lastCorner != maxLengthToLastCorner)
        {
            InstantiateRandomRoad();
        }
        else
        {
            InstantiateCornerRoad();
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

        if (lastPref.GetComponent<Road>().id.Equals(1)) // left
        {
            rotate -= 90;
        }
        else if (lastPref.GetComponent<Road>().id.Equals(2)) // right
        {
            rotate += 90;
        }

        if (rotate.Equals(-360) || rotate.Equals(360))
        {
            rotate = 0;
        }

        activeRoads.Add(road);
        previousIndex = index;
    }

    private void InstantiateRandomRoad()
    {
        do
        {
            index = Random.Range(0, roadPrefabs.Length);
        } while (index == previousIndex);
        road = Instantiate(roadPrefabs[index]);
    }

    private void InstantiateCornerRoad()
    {
        lastCorner = 0;
        maxLengthToLastCorner = Random.Range(4, 6);
        index = Random.Range(0, cornerPrefabs.Length);
        road = Instantiate(cornerPrefabs[index]);
    }

    private void DeleteFirstRoad()
    {
        if (activeRoads.Count < 10)
        {
            return;
        }

        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}