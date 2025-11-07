using UnityEngine;

public class FireSpike : MonoBehaviour, IDamageable
{
    public void TakeDamage(int amount, GameObject source = null)
    {
        Debug.Log("Espinho tirou 1 vida");
    }
}
