using UnityEngine;

public class Spaceship : MonoBehaviour
{

    #region Variables
    public float EnginePower = 10f;
    public float TurnPower = 10f;

    [Header("Health")]
    public int HealthMax = 3;
    public int HealthCurrent;

    [Header("Bullets")]
    public GameObject BulletPrefab;
    public float BulletSpeed = 100f;
    public float FiringRate = 0.33f;
    private float fireTimer = 0f;

    [Header("Sound")]
    public SoundPlayer HitSounds;
    public SoundPlayer DieSounds;

    private Rigidbody2D rb2D;

    #endregion

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        HealthCurrent = HealthMax;
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        ApplyThrust(vertical);
        ApplyTorque(horizontal);
        UpdateFiring();
    }

    private void UpdateFiring()
    {
        bool isFiring = Input.GetButton("Fire1");
        fireTimer -= Time.deltaTime;    //Decrement the timer

        if (isFiring == true && fireTimer <= 0)
        {
            FireBullet();
            fireTimer = FiringRate;
        }
    }

    private void ApplyThrust(float amount)
    {
        //Debug.Log("Thrust amount is " + amount);
        Vector2 thrust = transform.up * EnginePower * Time.deltaTime * amount;
        rb2D.AddForce(thrust);
    }

    private void ApplyTorque(float amount)
    {
        //Debug.Log("Torque amount is " + amount);
        float torque = amount * TurnPower * Time.deltaTime;
        rb2D.AddTorque(-torque);
    }

    public void TakeDamage(int damage)
    {
        //Reduce the current health by the damage
        HealthCurrent = HealthCurrent - damage;
        //HealthCurrent -= damage; another way of writing the above

        HitSounds.PlayRandomSound();

        //If current health is zero, then Explode
        if(HealthCurrent <= 0)
        {
            Explode();
        }
    }

    public void Explode()
    {
        DieSounds.PlaySound(false);
        //Destroy the ship, end the game
        Debug.Log("Game Over");
        Destroy(gameObject);
    }

    public void FireBullet()
    {
        //Create a new bullet at the spaceships position and rotation
        GameObject bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        //Find the bullets rigidbody component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        //Create a force to push the bullet 'up' from the spaceships facing direction
        Vector2 force = transform.up * BulletSpeed;
        rb.AddForce(force);

    }

}
