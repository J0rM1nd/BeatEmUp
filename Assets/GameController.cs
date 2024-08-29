using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject enemy1Prefab;
    [SerializeField]
    private GameObject enemy2Prefab;
    [SerializeField]
    private GameObject enemy3Prefab;
    [SerializeField]
    private GameObject Joystick;
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private GameObject HpText;

    private float spawnTimer = 0f;
    private float spawnRate = 20f;
    private TextMeshProUGUI PlayerHp;
    public void ShowHp(int hp) => PlayerHp.text = hp.ToString();
    private void Awake()
    {
        PlayerHp = HpText.GetComponent<TextMeshProUGUI>();
        GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        go.name = "Player";
        go.GetComponent<Unit>().Init(UnitStats.Player, this);
        go.GetComponent<Player>().Init(Joystick.GetComponent<bl_Joystick>(), Button.GetComponent<Button>());
    }
    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnRate;
        }
    }
    public void SpawnEnemy()
    {
        GameObject go;
        if (Random.value < 1f / 3f)
            go = Instantiate(enemy1Prefab, RandomPosition(), Quaternion.identity);
        else if (Random.value < 2f / 3f)
            go = Instantiate(enemy2Prefab, RandomPosition(), Quaternion.identity);
        else
            go = Instantiate(enemy3Prefab, RandomPosition(), Quaternion.identity);
        go.GetComponent<Unit>().Init(UnitStats.Enemy, this);
    }
    private static Vector3 RandomPosition()
    {
        Vector3 position = new Vector3();
        position.z = Random.value < 0.5f ? 15f : -15f;
        position.x = Random.value * 8.5f;
        return position;
    }
}