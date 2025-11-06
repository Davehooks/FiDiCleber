using UnityEngine;

public class A : MonoBehaviour, ITest
{
    public void PrintTeste() => Debug.Log("Sou o A");
}
