using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroyGO), 3f);
    }

    // Update is called once per frame
    private void DestroyGO()
    {
        Destroy(gameObject);
    }
}
