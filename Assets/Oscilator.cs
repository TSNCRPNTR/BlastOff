using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Oscilator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    // todo remove
    [Range(0,1)][SerializeField]float movementFactor;
    Vector3 startingPos;
    
    bool didReset;

    // Start is called before the first frame update
    void Start(){
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update(){
        //cyclic();
        testCycle();        
    }

    private void cyclic(){  //Too good for the pleb code (Updated it with the video, in case I need to switch back (but I won't))
        if(period <= Mathf.Epsilon) {return;}
        float cycles = Time.time / period;

        const float tau = Mathf.PI * 2;

        float rawSineWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSineWave/2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset;
    }

    private void testCycle(){  //Trying to make it so that it'll go down, and warp back up, then fall again, instead of cycling

        if (movementFactor < 1){
            movementFactor = movementFactor+0.01f;  //We (pseudo) loopin loopin loopin loopin
            Vector3 offset = movementFactor * movementVector;   //Same core movement as the original, but soo much simpler
            transform.position = startingPos + offset;          //
            }
        if (movementFactor >= 1 && didReset == false) {
            didReset = true;
            Invoke("resetCycle", Random.Range(0f,0.5f));
            }
    }

    private void resetCycle(){  //Resets after random interval, looks a lot better than all synced up
        transform.position = startingPos; 
        movementFactor = 0;
        didReset = false;
        transform.Rotate(0f,Random.Range(0f,90f),0f);
    }
}
