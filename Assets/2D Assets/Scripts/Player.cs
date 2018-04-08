using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    // Variables - public or private identifyer, data type (int, floats, bool, strings).
    // Every variable has a NAME and optional value assigned.

	public bool tripleShotBoost = false;

    [SerializeField]
	private GameObject _laserPrefab;
    // fireRate is 0.25f seconds.
    // canFireNext -- has the required amount of time passed?
    // Time.time -- how long the game has been running.
	[SerializeField]
	private GameObject _tripleShotPrefab;
    [SerializeField]
	private float _fireRate = 0.20f;
    private float _canFireNext = 0.0f;
	
	[SerializeField]
	private float _speed = 5.0f;

    private void Start ()	{
	// current position = new position
	// position, rotation, and scale are all example of vector3 (object type).
		transform.position = new Vector3(0, 0, 0);
	}
	
	private void Update () 	{
    // Update is called once per frame.
        MovementHandler();
	// if space key is pressed or left mouse button is clicked, spawn laser at player position.
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
			FireHandler();
		}
	}

	private void FireHandler() {

	// if triple shot booster is collected, fire 3 lasers, else shoot 1 laser.

		if (Time.time > _canFireNext) {
    // Quaternion.identity -- the default rotation value.
			if (tripleShotBoost == true) {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
			}
            else {
				Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.70f, 0), Quaternion.identity);
			}
        _canFireNext = Time.time + _fireRate; // this condition is no longer true for 0.20f!
        }
    }

	private void MovementHandler() {
	// horizontalInput and verticalInput are the keypresses that are assigned by default as a, d, w, s and the arrow keys.
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
	// Vector3.right moves the object once per frame, which is about 60m per second!
	// Multiplying this by Time.deltaTime slows it down to roughly 1m per second.
	// Multiplying this by 5 speeds it up a little bit to make it smooth.
		transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
	// if player on the x axis is > 0, set player position on the y axis to 0.
		if (transform.position.y >= 0) {
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y <= -4.0f) {
			transform.position = new Vector3(transform.position.x, -4.0f, 0);
		}
	// if player on the x axis is >= 9.0, set player position to -9.5.
		if (transform.position.x >= 9.0f){
			transform.position = new Vector3(-9.0f, transform.position.y, 0);
		}
		else if (transform.position.x <= -9.0f) {
			transform.position = new Vector3(9.0f, transform.position.y, 0);
		}
	}
}
