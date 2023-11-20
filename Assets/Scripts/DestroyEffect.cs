using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroyEffect", lifeTime);
    }

    void destroyEffect()
    {
        Destroy(gameObject);
    }

}
