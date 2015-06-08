using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class RotationController : MonoBehaviour {
	public CircleCollider2D currentCircle;
	public int speed = 5;
	private bool isRotating = false;
	public GameObject wheel;
	public float maxSwipeTime; 
	bool couldBeSwipe;
	Vector2 startPos = new Vector2 (0f, 0f);
	float startTime = 0;
	public float comfortZoneVerticalSwipe = 50; // the vertical swipe will have to be inside a 50 pixels horizontal boundary
	public float comfortZoneHorizontalSwipe = 50; // the horizontal swipe will have to be inside a 50 pixels vertical boundary
	public float minSwipeDistance = 14;

	void Start() {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			//capture the mouse position in world coordinates
			Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			//determine in what section the touch occured, and instantiate the appropriate symbol
			RaycastHit2D hit = Physics2D.Raycast(mousePosition, -Vector2.up, 0.46f);
			if (hit.collider != null) {
				currentCircle = (CircleCollider2D)hit.collider;
				wheel.transform.position = currentCircle.transform.position;
			}
		}
		RotateCircle rotateScript = currentCircle.GetComponent<RotateCircle>();
		if (currentCircle != null) {
			if (Input.GetKeyDown(KeyCode.LeftArrow)){
				RotateLeft(rotateScript.getLeafs());
			}
			if (Input.GetKeyDown(KeyCode.RightArrow)){
				RotateRight(rotateScript.getLeafs());
			}
		}

		if (Input.touchCount >0) {
			Touch touch = Input.touches[0];
			
			switch (touch.phase) { //following are 2 cases
				
			case TouchPhase.Began: //here begins the 1st case
				startPos = touch.position;
				startTime = Time.time;
				
				break; //here ends the 1st case
				
			case TouchPhase.Ended: //here begins the 2nd case
				float swipeTime = Time.time - startTime;
				float swipeDist = (touch.position - startPos).magnitude;

				if ((Mathf.Abs(touch.position.x - startPos.x))<comfortZoneVerticalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.y - startPos.y)>0)
				{
					RotateLeft(rotateScript.getLeafs());
				}
				
				if ((Mathf.Abs(touch.position.x - startPos.x))<comfortZoneVerticalSwipe && (swipeTime < maxSwipeTime)&&  (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.y - startPos.y)<0)
				{
					RotateRight(rotateScript.getLeafs());
				}
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.x - startPos.x)<0)
				{
					RotateRight(rotateScript.getLeafs());
				}
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance)&&  Mathf.Sign(touch.position.x - startPos.x)>0)
				{
					RotateLeft(rotateScript.getLeafs());
				}
				break; //here ends the 2nd case
			}
		}
		
	}
	public void RotateLeft(GameObject[] pLeafes) {
		if (!isRotating && pLeafes [0] != null) {
			isRotating = true;
			StartCoroutine (LoopRotation (-60.0f, currentCircle));
		}
	}
	
	public void RotateRight(GameObject[] pLeafes) {
		if (!isRotating && pLeafes [0] != null) {
			isRotating = true;
			StartCoroutine (LoopRotation (60.0f, currentCircle));
		}
	}
	
	IEnumerator LoopRotation(float angle, CircleCollider2D circle)
	{
		float rot = 0f;
		while(true)
		{
			float step = Mathf.Abs(angle) * Time.deltaTime * speed;
			rot += step;
			if (rot < Mathf.Abs(angle)) {
				ActualRotation (angle, step, circle);
			} else {
				step = Mathf.Abs (angle) - (rot - step);
				ActualRotation (angle, step, circle);
				break;
			}
			yield return null;
		}
		isRotating = false;
		if (angle < 0) {
			BroadcastMessage("SwapLeafesLeft", circle.GetComponent<RotateCircle>().getLeafs());
		} else {
			BroadcastMessage("SwapLeafesRight", circle.GetComponent<RotateCircle>().getLeafs());
		}
	}
	
	void ActualRotation (float angle, float step, CircleCollider2D circle)
	{
		if (circle != null && wheel != null) {
			GameObject[] lLeafes = circle.GetComponent<RotateCircle>().getLeafs();
			for (int i = 0; i < lLeafes.Length; i++) {
				lLeafes [i].transform.RotateAround (circle.transform.position, new Vector3 (0, 0, angle < 0 ? 1 : -1), step);
			}
			wheel.transform.RotateAround(circle.transform.position, new Vector3 (0, 0, angle < 0 ? 1 : -1), step);
		}
	}
}
