using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 350f;
    [SerializeField] float mainThrust = 350f;
    [SerializeField] float levelLoadDelay = 2f;


    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip levelPass;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem levelPassParticles;

    Rigidbody rigidBody;
    AudioSource RocketNoise;

    bool isTransitioning = false;

    bool canCollide=true;

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
        if(Debug.isDebugBuild){
        ProcessDebug();
        }
    }

    private void ProcessInput(){
        if (!isTransitioning){
        //Haha rocket go brrr
        Thrust();
        //rotato banana
        Rotate();
        }
    }

    void OnCollisionEnter(Collision collision){     //Detects when we run into something
        if(isTransitioning || canCollide == false){return;}
        switch(collision.gameObject.tag){
            case "Friendly":    //If it's a friendly object, do nothing
                break;
            case "Finish":      //If it's the finish, finish the level
                FinishTheLevel();
                break;
            default:            //If it's anything else (obstacles) just kill the player
                EndTheLevel();
                break;
        }
    }
    private void Thrust(){
        float frameThrust = mainThrust*Time.deltaTime;
        if(Input.GetKey(KeyCode.Space)){
            rigidBody.AddRelativeForce(Vector3.up*frameThrust);
            if(!RocketNoise.isPlaying){
                RocketNoise.PlayOneShot(mainEngine);
            }
            mainEngineParticles.Play();
        } else {
            RocketNoise.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void Rotate(){
        rigidBody.angularVelocity = Vector3.zero;   //Freezes rotation, makes for easier control
        checkDirection();
    } 

    private void checkDirection(){
        float frameRotation = rcsThrust*Time.deltaTime; //help rotate faster, with more control.
        if(Input.GetKey(KeyCode.A)){
                transform.Rotate(Vector3.forward*frameRotation);
            }else if(Input.GetKey(KeyCode.D)){
                transform.Rotate(-Vector3.forward*frameRotation);
            }
    }
    private void FinishTheLevel(){
        isTransitioning = true;
        RocketNoise.Stop();
        RocketNoise.PlayOneShot(levelPass);
        levelPassParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void EndTheLevel(){
        isTransitioning = true;
        RocketNoise.Stop();
        RocketNoise.PlayOneShot(death);
        deathParticles.Play();
        Invoke("Die", levelLoadDelay);
    }

    private void Die(){
        SceneManager.LoadScene(0);  //Resets to the first level
    }
    private void LoadNextScene(){
        //SceneManager.LoadScene(1);  //Loads next scene 
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int nextLevel = -1;
        if(currentLevel == 4){ //Hard coding in the level max, cause I'm not adding any more, and it's super buggy
            nextLevel = 0;
        } else {
            nextLevel = currentLevel+1;
        }
        SceneManager.LoadScene(nextLevel);
    }

    private void ProcessDebug(){
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextScene();
        } else if (Input.GetKeyDown(KeyCode.C)){
            if (canCollide==false){
                canCollide = true;
            } else {
                canCollide = false;
            }
        }
    }
}