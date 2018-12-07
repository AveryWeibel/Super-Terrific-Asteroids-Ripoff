using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour {

    public Transform[] points;
    public float changeThreshold;
    public float speed;

    private int curPoint = 0;
		
	void Update () {
        if (Vector3.Distance(transform.position, points[curPoint].position) > changeThreshold)
        {
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 0, transform.position.z), points[curPoint].position, speed);
        }
        else {
            if (curPoint < points.Length - 1)
            {
                curPoint++;
            }
            else {
                curPoint = 0;
            }
        }
    }
}
