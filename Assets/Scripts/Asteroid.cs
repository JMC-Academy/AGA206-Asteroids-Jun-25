using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int CollisionDamage = 1;


    public void OnCollisionEnter2D(Collision2D collision)
    {
        Spaceship ship = collision.gameObject.GetComponent<Spaceship>();
        if(ship != null)
        {
            ship.TakeDamage(CollisionDamage);
        }
    }
}
