using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipper : MonoBehaviour
{
    public void flip()
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.enabled = !renderer.enabled;
    }

}
