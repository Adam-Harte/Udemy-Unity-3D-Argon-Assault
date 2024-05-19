using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1f);
    }
}
