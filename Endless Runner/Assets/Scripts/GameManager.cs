using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    public static GameManager instance;

    public int points = 0;
    public int highscore = 0;
    public int coins = 0;
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

    public void SpawnPoints()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
            return;

        points++;
        player.pointsText.SetText(points.ToString());
        if (points > pointsNeededToGetMoveSpeed)
        {
            player.moveSpeed *= speedUpPlayer;
            pointsNeededToGetMoveSpeed += pointsNeededToGetMoveSpeed;
        }
    }
}
