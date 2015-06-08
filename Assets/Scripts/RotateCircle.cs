using UnityEngine;
using System.Collections;

public class RotateCircle : MonoBehaviour {
	public GameObject[] leafes = new GameObject[6];
	
	public GameObject[] getLeafs() {
		return leafes;
	}

	void OnDrawGizmos () {
		Gizmos.DrawWireSphere (this.GetComponent<CircleCollider2D> ().transform.position, this.GetComponent<CircleCollider2D> ().radius);
	}

	void SwapLeafesLeft(GameObject[] pLeafes) {
		if (checkIfNotSame (pLeafes)) {
			//test if any leafes match this leaf array received as parameter
			for (int i = 0; i < pLeafes.Length; i++) {
				for (int j = 0; j < leafes.Length; j++) {
					if (pLeafes [i] == leafes [j]) {
						//we have a match and we need to replace the leafs between the circles
						leafes [j] = pLeafes [(i + 1) % 6];
						return;
					}
				}
			}
		}
	}

	void SwapLeafesRight(GameObject[] pLeafes) {
		if (checkIfNotSame(pLeafes)) {
			//test if any leafes match this leaf array received as parameter
			for (int i = 0; i < pLeafes.Length; i++) {
				for (int j = 0; j < leafes.Length; j++) {
					if (pLeafes[i] == leafes[j]) {
						//we have a match and we need to replace the leafs between the circles
						leafes[j] = pLeafes[(i + 5) % 6];
						return;
					}
				}
			}
		}
	}

	bool checkIfNotSame(GameObject[] pLeafes) {
		for (int i = 0; i < pLeafes.Length; i++) {
			if (pLeafes [i] != leafes [i])
				return true;
		}
		return false;
	}
}
