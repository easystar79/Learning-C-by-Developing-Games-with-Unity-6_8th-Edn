using UnityEngine;

public class Destroyable<T> : MonoBehaviour where T : MonoBehaviour
{
    public float OnscreenDelay = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay);
    }
}
