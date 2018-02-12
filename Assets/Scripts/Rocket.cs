using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f; // make this allow to be changed in the unity editor
    [SerializeField] float mainThurst = 100f;
    [SerializeField] AudioClip rocketSound;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip sucess;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;
    Rigidbody rigidbody;

    AudioSource audioSource;

    enum State {ALIVE, DEAD}
    State state = State.ALIVE;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.ALIVE)
        {
            Thrust();
            Rotate();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.ALIVE)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Win":
                audioSource.Stop();
                audioSource.PlayOneShot(sucess);
                successParticles.Play();
                break;
            case "Friendly":
                break;
            default:
                print("Dead");
                state = State.DEAD; 
                audioSource.Stop();
                audioSource.PlayOneShot(death);
                deathParticles.Play();
                Invoke("LoadScene", 1f);
                break;
        }
    } 

    private void LoadScene() {
        SceneManager.LoadScene(0);
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThurst);
            if (!audioSource.isPlaying)
            {          
                audioSource.PlayOneShot(rocketSound);

            }
            engineParticles.Play();
        }
        else
        {
            audioSource.Stop();
            engineParticles.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

       
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.left * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.right * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // resume physics control of rotation
    }
}
