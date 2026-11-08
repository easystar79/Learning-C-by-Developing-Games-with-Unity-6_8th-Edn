using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Item collected!");
        }
    }
}
