using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenComponentHealth : HealthClass {

    private void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.AddComponent<ChildrenComponentHealth>();
        }
    }

    public override void TakeDamage(float amount)
    {
        transform.parent.GetComponent<HealthClass>().TakeDamage(amount);
    }
}
