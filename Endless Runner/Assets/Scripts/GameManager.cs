using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    public Player player;
    public PlayerStats playerStats;

    public int highscore = 0;
    public int coins = 0;
    public int points = 0;
    private float pointsNeededToGetMoveSpeed = 10;
    private readonly float speedUpPlayer = 1.1f;

    protected override void Awake()
    {
        base.Awake();
        Load();
        player.OnDeath += OnPlayerDied;
        InvokeRepeating(nameof(SpawnPoints), 1f, 1f);
    }

    private void OnPlayerDied()
    {
        if (points > highscore)
        {
            highscore = points;
        }
        Destroy(player.gameObject);
        OnSceneTransition(1);
    }

    public void AddCoins()
    {
        coins += playerStats.coinValue;
        SpawnPoints();
    }

    public void OnSceneTransition(int sceneID)
    {
        Save();
        if (sceneID.Equals(0))
        {
            points = 0;
            pointsNeededToGetMoveSpeed = 10;
        }
        SceneManager.LoadScene(sceneID);
    }

    public void SpawnPoints()
    {
        points++;
        if (player = FindObjectOfType<Player>())
        {
            player.pointsText.SetText(points.ToString());
        }
        if (points >= pointsNeededToGetMoveSpeed)
        {
            playerStats.tempMaxMoveSpeed *= speedUpPlayer;
            pointsNeededToGetMoveSpeed += pointsNeededToGetMoveSpeed;
        }
    }

    public void Save()
    {
        SaveObject save = new()
        {
            coins = coins,
            highscore = highscore,
            selectedMaterial = playerStats.selectedMaterial,
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
            playerStats.selectedMaterial = saveObject.selectedMaterial;
        }
    }
}

public class SaveObject
{
    public int coins;
    public int highscore;
    public Material selectedMaterial;
}
