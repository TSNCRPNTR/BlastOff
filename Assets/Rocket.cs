using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 300f;
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

void OnCollisionEnter(Collision collision){
    switch(collision.gameObject.tag){
        case "Friendly":
            print("ok");
            break;
        case "Fuel":
            print("Fuel");
            break;
        default:
            print("Dead");
            break;
    }
}

private void Thrust(){
    float frameThrust = mainThrust*Time.deltaTime;
    if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up*frameThrust);
            if(!RocketNoise.isPlaying){
                RocketNoise.Play();
            }
        } else {
            RocketNoise.Stop();
        }
        
    }

private void Rotate(){
    rigidBody.freezeRotation = true;    //Freezes rotation, makes for easier control
    float frameRotation = rcsThrust*Time.deltaTime; //help rotate faster, with more control.
        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(Vector3.forward*frameRotation);
        }else if(Input.GetKey(KeyCode.D)){
            transform.Rotate(-Vector3.forward*frameRotation);
        }
    rigidBody.freezeRotation = false;
    }
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