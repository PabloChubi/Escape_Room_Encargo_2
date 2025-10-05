using UnityEngine;

public class Key : MonoBehaviour
{
    public int keys;
    private void Update()
    {
        keys = Data.keys;
    }
    private void OnTriggerEnter(Collider other)
    {
        Data.keys += 1;
        Destroy(gameObject, 0.1f);
    }
}
