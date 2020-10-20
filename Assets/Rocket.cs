using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidBody;
    AudioSource RocketNoise;

    bool noiseToggle;
    bool noisePlay;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        RocketNoise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Takes key pressed, does something with it.
        ProcessInput();
    }

    private void ProcessInput(){
        //Haha rocket go brrr
        Thrust();
        //rotato banana
        Rotate();
    }

private void Thrust(){
    if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up);
            if(!RocketNoise.isPlaying){
                RocketNoise.Play();
            }
        }
        
    }

private void Rotate(){
    rigidBody.freezeRotation = true;
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward);
            print("Rotating Left");
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward);
            print("Rotating Right");
        }
    }
    rigidBody.freezeRotation = false;
}

/*
Whoops, way too complicated
    private void ProcessNoise(){
    if(noisePlay == true && noiseToggle == true){
        RocketNoise.Play();
        noiseToggle=false;
    } else if (noisePlay == false && noiseToggle == true){
        RocketNoise.Stop();
        noiseToggle = false();
    }
}
*/