using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleRaycaster : MonoBehaviour
{
    [SerializeField] LayerMask _grappleLayer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestGrapple();
        }
    }

    void TestGrapple()
    {
        // test cast
        float detectRadius = 3;
        float castDistance = 20;
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, detectRadius, transform.forward, castDistance, _grappleLayer);
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log("Grapple point name: " + hits[i].transform.gameObject.name);
        }
    }
}
