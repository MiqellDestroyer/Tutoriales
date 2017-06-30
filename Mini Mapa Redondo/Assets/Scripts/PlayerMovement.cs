using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    public float rotSpeed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical"); //OBTIENE LA DIRECCION HACIA LA CUAL SE MUEVE EL JUGADOR

        movement *= Time.deltaTime * speed; // MULTIPLICA LA DIRECCION POR LA VELOCIDAD.

        rb.MovePosition(transform.position + movement) ; //MUEVE AL JUGADOR

        float rotY = Input.GetAxis("Mouse X"); // OBTIENE EL EJE QUE REPRESENTA EL MOVIMIENT EN X DEL MOUSE

        gameObject.transform.Rotate(0, rotY * rotSpeed, 0); // ROTA AL JUGADOR
    }
}
