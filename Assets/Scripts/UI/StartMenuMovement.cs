using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMovement : MonoBehaviour
{
    public Transform[] path;
    public float speedAmount;
    public float distanceTolerance;

    Transform nextPos;
    int i = 0;
    Vector3 movementAmount;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if(path.Length > 0)
            nextPos = path[i];
    }

    // Update is called once per frame
    void Update()
    {
        //select next position to reach
        if(Vector3.Distance(transform.position, nextPos.position) < distanceTolerance)
        {
            nextPos = path[getNextPos()];
        }

        //rotate spaceship
        transform.LookAt(nextPos);


        //move spaceship
        movementAmount = Vector3.Lerp(transform.position, nextPos.position, speedAmount * Time.deltaTime);
        rb.MovePosition(movementAmount);
    }


    int getNextPos()
    {
        if (i >= path.Length - 1)
        {
            i = 0;
        }
        else
        {
            i++;
        }

        Debug.Log(i);
        return i;
    }
}
