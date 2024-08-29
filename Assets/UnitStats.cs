public enum UnitTag
{
    Player,
    Enemy
}
public struct UnitStats
{
    public UnitTag tag;
    public float speed;
    public float rotationSpeed;
    public float energyRegen;
    public float hp;
    public float damage;
    public float hitDistance;
    public static UnitStats Player
    {
        get => new UnitStats(UnitTag.Player, 3f, 10f, 0.5f, 100f, 30f, 1.5f);
    }
    public static UnitStats Enemy
    {
        get => new UnitStats(UnitTag.Enemy, 1.5f, 5f, 0.5f, 50f, 15f, 1.2f);
    }
    public UnitStats(UnitTag tag, float speed, float rotationSpeed, float energyRegen, float hp, float damage, float hitDistance)
    {
        this.tag = tag;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.energyRegen = energyRegen;
        this.hp = hp;
        this.damage = damage;
        this.hitDistance = hitDistance;
    }
}