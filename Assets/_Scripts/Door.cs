using UnityEngine;

public class Door : MonoBehaviour
{
    public int keys;
    private void Update()
    {
        keys = Data.keys;
        if (Data.keys >= 1)
        {
            Destroy(gameObject, 0.1f);
        }



    }
}
