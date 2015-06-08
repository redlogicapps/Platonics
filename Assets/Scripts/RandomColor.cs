using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer> ().color = GlobalVariables.GetNextColor ();
	}

}
