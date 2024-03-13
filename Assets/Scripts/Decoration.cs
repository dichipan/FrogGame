using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Object.Destroy(this.gameObject);
    }

}
