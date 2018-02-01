using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f; // make this allow to be changed in the unity editor
    [SerializeField] float mainThurst = 100f;
    Rigidbody rigidbody;
    AudioSource rocketThrust;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rocketThrust = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("YOU ARE ALIVE FOOL");
                break;
            case "Fuel":
                print("FUEL");
                break;
            default:
                print("YOU ARE DEAD FOOL");
                break;
        }
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * mainThurst);
            if (!rocketThrust.isPlaying)
            {
                rocketThrust.Play();
            }
        }
        else
        {
            rocketThrust.Stop();
        }
    }

    private void Rotate()
    {
        rigidbody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

       
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down * rotationThisFrame);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotationThisFrame);
        }

        rigidbody.freezeRotation = false; // resume physics control of rotation
    }
}
