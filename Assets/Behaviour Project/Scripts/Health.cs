using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool canTakeDamage = true;
    public float hurtTimer;
    public Color hurtColorOne;
    public Color hurtColorTwo;

    private float timer;
    private Color regularColor;
    private void Start()
    {
        
    }
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0 && canTakeDamage)
        {
            currentHealth -= damage;
            canTakeDamage = false;
            timer = 0f;
            regularColor = GetComponent<MeshRenderer>().material.color;
            StartCoroutine(Hurt());
        }
    }
    public void Restore()
    {
        currentHealth = maxHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    private IEnumerator Hurt()
    {
        Material materialColor = GetComponent<MeshRenderer>().materials[0];
        while (timer < hurtTimer)
        {
            materialColor.color = hurtColorOne;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);

            materialColor.color = hurtColorTwo;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);

            materialColor.color = regularColor;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        materialColor.color = regularColor;
        canTakeDamage = true;
    }
}
