using UnityEngine;

public class FireSpike : MonoBehaviour, IDamageable
{
    public void TakeDamage(int amount, GameObject source = null)
    {
        Debug.Log("FIRE SPIKE: Espinho tirou 1 vida");
    }
}
