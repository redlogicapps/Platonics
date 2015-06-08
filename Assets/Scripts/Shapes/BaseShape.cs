using UnityEngine;
using System.Collections;

public abstract class BaseShape {

	protected int difficulty;
	protected Color[] colors;

	public abstract bool CheckIfMatched();
}
