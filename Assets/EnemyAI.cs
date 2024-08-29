using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    Unit unit;
    void Start()
    {
        player = GameObject.Find("Player");
        unit = GetComponent<Unit>();
    }
    void Update()
    {
        Vector3 v = player.transform.position - transform.position;
        v.y = 0f;
        unit.direction = v;
        if (unit.direction.magnitude < 1f)
            unit.Punch();
    }
}