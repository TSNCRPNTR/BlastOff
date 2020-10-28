using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 300f;
    [SerializeField] float mainThrust = 350f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelPass;
    Rigidbody rigidBody;
    AudioSource RocketNoise;

    enum State {Alive, Dying, Transcending};
    State state = State.Alive;

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
        if (state == State.Alive){
        //Haha rocket go brrr
        Thrust();
        //rotato banana
        Rotate();
        }
    }

void OnCollisionEnter(Collision collision){
    if(state != State.Alive){return;}
    switch(collision.gameObject.tag){
        case "Friendly":
            break;
        case "Finish":
            state = State.Transcending;
            print("Finish");
            RocketNoise.Stop();
            RocketNoise.PlayOneShot(levelPass);
            Invoke("LoadNextScene", 1f);
            break;
        default:
            state = State.Dying;
            print("Dead");
            Invoke("Die", 1f);
            RocketNoise.Stop();
            RocketNoise.PlayOneShot(death);
            break;
    }
}

private void Die(){
    SceneManager.LoadScene(0);
}
private void LoadNextScene(){
    SceneManager.LoadScene(1);
}
private void Thrust(){
    float frameThrust = mainThrust*Time.deltaTime;
    if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up*frameThrust);
            if(!RocketNoise.isPlaying){
                RocketNoise.PlayOneShot(mainEngine);
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