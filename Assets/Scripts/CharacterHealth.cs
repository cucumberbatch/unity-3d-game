using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int characterHealth = 50;
    public GameObject Ragdoll_RayGun;
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8) //номер пули в слоях
        {
            if (gameObject.CompareTag(collision.gameObject.GetComponent<Bullet>().shootedPerson.tag))
            {
                return;
            }
            // ApplyDamage(collision.gameObject.GetComponent<Bullet>().amountOfDamage);
        }
    }

    public void TakeDamage(int amountOfDamage)
    {
        characterHealth -= amountOfDamage;
        print(gameObject.name + ": " + characterHealth);
        if (characterHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Ragdoll_RayGun.SetActive(true);     //спауним труп
        Instantiate(Ragdoll_RayGun, transform.position, transform.rotation);
    }

    
}
