using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestures : MonoBehaviour {

	public GameObject player;
	public Camera cam;
	private PlayerMovement pm;
	private PlayerAttack pa;
	private float doubleTapMin,time,prevTime;
	private bool currentTouch,prevTouch;
	private short prevDir,currentDir;

	// Use this for initialization
	void Start () {
		pm = player.GetComponent<PlayerMovement> ();
		pa = player.GetComponent<PlayerAttack> ();
		width = cam.pixelWidth;
		doubleTapMin = .23f;
		currentTouch = false;
		prevTouch = false;
	}

	// Update is called once per frame
	void Update () {
		if (pm.movable ()) {
			int index = Input.touches.Length;
			if (index > 0) {
				currentTouch = true;
				Vector2 pos = cam.ScreenToWorldPoint(Input.touches [0].position);

				prevDir = currentDir;
				if (pos.x > player.transform.position.x)
					currentDir = 1;
				else if (pos.x < player.transform.position.x)
					currentDir = -1;
				else
					currentDir = 0;
				prevTime = time;
				time = 0;
			} else
				currentTouch = false;

			if(currentTouch && !prevTouch && prevDir == currentDir && prevTime<=doubleTapMin)
				dash ();
			else 
				move (currentTouch);
		}
		time += Time.deltaTime;
		prevTouch = currentTouch;
	}

	private void move(bool touched) {
		if (!touched)
			pm.move (0);
		else
			pm.move (currentDir);
	}

	private void dash(){
		pa.attack (currentDir);
	}

}
