using UnityEngine;

public class CameraController : MonoBehaviour{
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Vector3 targetPosOffset;
    [SerializeField] private float orbitSpeed;
    [SerializeField] private float orbitDistance;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float panSpeed;
    
    private Vector3 targetPos;

    private void Update() {
	    Orbiting();
	    Zooming();
	    Panning();
	    ReapplyDistance();
	    transform.LookAt(targetPos,transform.up);
    }

    private void Orbiting() {
	    if (Input.GetKey(KeyCode.W)) {
		    OrbitInDirection(Vector3.up);
	    }
	    else if (Input.GetKey(KeyCode.S)) {
		    OrbitInDirection(Vector3.down);
	    }
	    if (Input.GetKey(KeyCode.A)) {
		    OrbitInDirection(Vector3.left);
	    }
	    else if (Input.GetKey(KeyCode.D)) {
		    OrbitInDirection(Vector3.right);
	    }
    }
    
    private void OrbitInDirection(Vector3 direction) {
	    transform.Translate(orbitSpeed * Time.deltaTime * direction); 
    }

    private void Zooming() {
	    orbitDistance += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * -1;
	    //-1 to inverse scrolling direction
	    orbitDistance = Mathf.Abs(orbitDistance);
	    //to avoid orbitDistance going < 0
	    
    }

    private void Panning() {
	    var initialCameraPos = transform.position;
	    if (Input.GetKey(KeyCode.Q)) {
		    transform.Translate(-1 * panSpeed * Time.deltaTime * transform.right);
		    //multiplying transform.right * -1 because transform.left is unavailable for some strange reason
	    }
	    else if (Input.GetKey(KeyCode.E)) {
		    transform.Translate(panSpeed * Time.deltaTime * transform.right);
	    }
	    targetPosOffset += transform.position - initialCameraPos;
	    targetPos = targetGameObject.transform.position + targetPosOffset;
    }



    private void ReapplyDistance() {
	    transform.position = (transform.position - targetPos).normalized
	                         * orbitDistance + targetPos;
    }

    private void OnDrawGizmos() {
	    Gizmos.color = Color.red;
	    Gizmos.DrawLine(transform.position, targetPos);
	    Gizmos.DrawSphere(targetPos, 1);
    }
}
