using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.CompareTag("Enemy"))
            {
                var pos = other.transform.position;
                //Debug.LogError("before x is " + pos.x);
                var posX = Mathf.Abs(pos.x) - 1f;
                pos.x = pos.x >= 0 ? -posX : posX;
                //Debug.LogError("x is " + pos.x + "posX is " + posX);
                other.transform.position = pos;
            }
        }
    }
}
