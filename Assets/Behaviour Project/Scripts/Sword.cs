using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Sword : MonoBehaviour
{
    public int damage;
    public LayerMask target;
    private void OnCollisionEnter(Collision collision)
    {
        // Have to bit convert the layer to compare to layermask
        if ((1 << collision.gameObject.layer) == target.value)
        {
            // Reduce the health of the hit target, if possible
            if (collision.gameObject.TryGetComponent<Health>(out Health hitTargetHP))
            {
                hitTargetHP.TakeDamage(damage);
            }
        }
    }
}
