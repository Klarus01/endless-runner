using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject[] roadPrefabs;
    public GameObject[] cornerPrefabs;

    private int numRoadsOnScreen = 0;
    private int previousPrefabIndex = -1;
    private int cornerNow = 0;
    private int cornerMax = 3;

    public List<GameObject> activeRoads = new();

    private Transform playerTransform;
    private Transform halfOfRoad;

    private GameObject lastPref;
    private float rotate = 0;

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
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

        if (numRoadsOnScreen < 21)
        {

            if (playerTransform.position.z > 0 || playerTransform.position.x > 0)
            {
                if (playerTransform.position.z + 3 > halfOfRoad.position.z && playerTransform.position.z - 3 < halfOfRoad.position.z || playerTransform.position.x + 3 > halfOfRoad.position.x && playerTransform.position.x - 3 < halfOfRoad.position.x)
                {
                    DeleteRoad();
                    SpawnRoad();
                }
            }
            else
            {
                if (playerTransform.position.z - 3 < halfOfRoad.position.z && playerTransform.position.z + 3 > halfOfRoad.position.z || playerTransform.position.x - 3 < halfOfRoad.position.x && playerTransform.position.x + 3 > halfOfRoad.position.x)
                {
                    DeleteRoad();
                    SpawnRoad();
                }
            }
        }
        
    }

    private void SpawnRoad(int prefabIndex = -1)
    {
        GameObject road;

        if (prefabIndex != -1)
        {
            prefabIndex = 0;
            road = Instantiate(roadPrefabs[prefabIndex]) as GameObject;
        }
        else if (cornerNow != cornerMax)
        {
            do
            {
                prefabIndex = Random.Range(0, roadPrefabs.Length);
            } while (prefabIndex == previousPrefabIndex);
            road = Instantiate(roadPrefabs[prefabIndex]) as GameObject;
        }
        else
        {
            cornerNow = 0;
            cornerMax = Random.Range(4, 6);
            prefabIndex = Random.Range(0, cornerPrefabs.Length);
            road = Instantiate(cornerPrefabs[prefabIndex]) as GameObject;
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

    private void DeleteRoad()
    {
        if(activeRoads.Count >= 20)
        {
            numRoadsOnScreen--;
            GameObject roadToDestroy = activeRoads[0];
            activeRoads.RemoveAt(0);
            Destroy(roadToDestroy);
        }
    }
}