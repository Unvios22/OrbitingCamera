using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] private GameObject targetGameObject;
	private Vector3 targetPos;
    [SerializeField] private Vector3 targetPosOffset;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float orbitDistance;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float panSpeed;

    private void Update() {
	    targetPos = targetGameObject.transform.position + targetPosOffset;
	    if (Input.GetKey(KeyCode.W)) {
		    Orbit(Vector3.up);
	    }
	    if (Input.GetKey(KeyCode.S)){
		    Orbit(Vector3.down);
	    }
	    if (Input.GetKey(KeyCode.A)){
		    Orbit(Vector3.left);
	    }
	    if (Input.GetKey(KeyCode.D)){
		    Orbit(Vector3.right);
	    }
	    Zooming();
	    Panning();
	    ReapplyDistance();
	    transform.LookAt(targetPos,transform.up);
    }

    private void Zooming() {
	    orbitDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
	    orbitDistance = Mathf.Abs(orbitDistance);
	    //-1 to inverse scrolling direction
    }

    private void Panning() {
	    var initialCameraPos = transform.position;
	    if (Input.GetKey(KeyCode.Q)) {
		    transform.Translate(transform.right * -1 * panSpeed * Time.deltaTime);
		    targetPosOffset = transform.position - initialCameraPos;
	    }
	    if (Input.GetKey(KeyCode.E)) {
		    transform.Translate(transform.right * panSpeed * Time.deltaTime);
		    targetPosOffset = transform.position - initialCameraPos;
	    }
    }

    private void Orbit(Vector3 direction) {
	    transform.Translate(direction * orbitSpeed * Time.deltaTime); 
    }

    private void ReapplyDistance() {
	    var position = targetPos;
	    transform.position = (transform.position - position).normalized
	                         * orbitDistance + position;
    }

    private void OnDrawGizmos() {
	    Gizmos.color = Color.red;
	    Gizmos.DrawLine(transform.position, targetPos);
	    Gizmos.DrawSphere(targetPos, 1);
    }
}
