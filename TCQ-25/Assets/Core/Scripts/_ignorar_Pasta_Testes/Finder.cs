using UnityEngine;

public class Finder : MonoBehaviour
{
    void Start()
    {
        ITest testA = GameObject.Find("A").GetComponent<ITest>();
        ITest testB = GameObject.Find("B").GetComponent<ITest>();

        testA.PrintTeste();
        testB.PrintTeste();
    }

    void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        GetComponent<IDamageable>()?.TakeDamage(1);
    }
}
}
