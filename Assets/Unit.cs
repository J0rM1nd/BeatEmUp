using UnityEngine;
using UnityEngine.SceneManagement;

public class Unit : MonoBehaviour
{
    private UnitStats stats;
    private float energy = 1f;
    private float currentSpeed;
    private Animator Animator;
    private CharacterController Controller;
    private GameController GameController;
    bool punch = false;
    bool death = false;
    bool stuned = false;
    private float damageMultiplyer = 1f;
    
    private float _hp;
    private float hp
    {
        get => _hp;
        set
        {
            _hp = value;
            if (stats.tag == UnitTag.Player)
                if (value > 0)
                    GameController.ShowHp(Mathf.RoundToInt(value));
                else
                    GameController.ShowHp(0);
        }
    }



    public Vector3 direction;
    public void Punch() => punch = true;
    public void ApplyDamage(float damage)
    {
        if (death)
            return;
        hp -= damage;
        if (hp <= 0)
        {
            Death();
            return;
        }
        Animator.SetTrigger("Stuned");
    }

    private void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        Controller = gameObject.GetComponent<CharacterController>();
        Animator.SetBool("Fight", false);
    }
    private void Update()
    {
        if (death)
        {
            transform.position += 0.1f * Time.deltaTime * Vector3.down;
            if (transform.position.y < -0.3f)
                if (stats.tag == UnitTag.Player)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                else
                    Destroy(gameObject);
            return;
        }
        Fight();
        Moovement();
    }
    public void Init(UnitStats stats, GameController gameController)
    {
        this.stats = stats;
        _hp = stats.hp;
        GameController = gameController;
    }
    private void Moovement()
    {
        if (direction.magnitude < 0.1f)
        {
            currentSpeed = 0f;
            Animator.SetFloat("CurrentSpeed", currentSpeed);
            return;
        }
        if (direction.magnitude > 1f)
            direction.Normalize();
        currentSpeed = stats.speed;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * stats.rotationSpeed);
        if (Animator.GetBool("Fight"))
            direction *= 0.5f;
        Controller.Move(stats.speed * Time.deltaTime * direction);
        Animator.SetFloat("CurrentSpeed", currentSpeed);
    }
    private void Fight()
    {
        energy += Time.deltaTime * stats.energyRegen;
        if (energy > 1f)
            energy = 1f;
        Animator.SetFloat("Random", Random.value);
        if (punch)
        {
            Animator.SetBool("Fight", true);
            Animator.SetTrigger("Punch");
            if (energy > 0.6f)
            {
                energy -= 0.6f;
                damageMultiplyer = 1.2f;
                Animator.SetInteger("PunchEnergy", 1);
            }
            else if (energy > 0.3f)
            {
                energy -= 0.3f;
                damageMultiplyer = 1f;
                Animator.SetInteger("PunchEnergy", 0);
            }
            else
            {
                energy = 0f;
                damageMultiplyer = 0.8f;
                Animator.SetInteger("PunchEnergy", -1);
            }
        }
        punch = false;
    }
    public void EndPunch()
    {
        if (stuned)
            return;
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward, out RaycastHit hit, stats.hitDistance))
        {
            Unit target = hit.transform.GetComponent<Unit>();
            if (target != null)
                target.ApplyDamage(damageMultiplyer*stats.damage);
        }
    }
    private void Death()
    {
        Animator.SetTrigger("Death");
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        death = true;
        GameController.SpawnEnemy();
    }
}