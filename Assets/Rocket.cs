using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Takes key pressed, does something with it.
        ProcessInput();
    }

//Two seperate if's are messy, but it works better than else-if's for rotating and thrusting at the same time.
//Space would take priority, would only let you do one thing at a time (if we did else-if).
//^-^
    private void ProcessInput(){
        if(Input.GetKey(KeyCode.Space)){
            //haha rocket go brrrrr
            rigidBody.AddRelativeForce(Vector3.Up);
        }
        if(Input.GetKey(KeyCode.A)){
            print("Rotating Left");
        }else if(Input.GetKey(KeyCode.D)){
            print("Rotating Right");
        }
    }
}
