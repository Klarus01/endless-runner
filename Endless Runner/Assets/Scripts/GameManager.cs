using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public Material selectedMaterial;

    public static GameManager instance;

    public int points = 0;
    public int highscore = 0;
    public int coins = 100;
    public float moveSpeed = 10f;
    public float pointsNeededToGetMoveSpeed = 10;
    private readonly float speedUpPlayer = 1.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        InvokeRepeating(nameof(SpawnPoints), 1f, 1f);
    }

    public void Save()
    {
        SaveObject save = new()
        {
            coins = coins,
            highscore = highscore,
            selectedMaterial = selectedMaterial,
        };
        string json = JsonUtility.ToJson(save);

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }


    private void Load()
    {
        if (File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            coins = saveObject.coins;
            highscore = saveObject.highscore;
            selectedMaterial = saveObject.selectedMaterial;
        }
    }

    public void SpawnPoints()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            return;

        points++;
        player.pointsText.SetText(points.ToString());
        if (points > pointsNeededToGetMoveSpeed)
        {
            moveSpeed *= speedUpPlayer;
            pointsNeededToGetMoveSpeed += pointsNeededToGetMoveSpeed;
        }
    }
}

public class SaveObject
{
    public int coins;
    public int highscore;
    public Material selectedMaterial;
    public GameObject[] colorUnlocked;
}
