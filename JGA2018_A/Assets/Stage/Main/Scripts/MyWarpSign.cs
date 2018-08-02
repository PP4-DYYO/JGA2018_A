using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyWarpSign : MonoBehaviour {

    Light m_spotLight;

	// Use this for initialization
	void Start () {
        m_spotLight = gameObject.GetComponent<Light>();
    }
	
	// Update is called once per frame
	void Update () {
        m_spotLight.spotAngle += 3.0f;
        m_spotLight.intensity -= 0.2f;
    }
}
