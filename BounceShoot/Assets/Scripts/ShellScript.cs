using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellScript : MonoBehaviour {

    public float m_LifeTime = 15;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, m_LifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
