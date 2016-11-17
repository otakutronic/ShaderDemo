using UnityEngine;
using System.Collections;

public class LevelBuider : MonoBehaviour {
	public const int LEVEL_SIZE = 100;

	public GameObject[] ground;
	public GameObject[] trees;

	void Start () {
		BuildGround ();
		BuildTrees ();
	}

	void BuildGround() {
		for (int i = 0; i < LEVEL_SIZE; i++) {
			for (int j = 0; j < LEVEL_SIZE; j++) {
				Instantiate(ground[Random.Range (0, ground.Length)], new Vector3(i * 3.0f, 0, j * 3.0f), Quaternion.identity);
			}
		}
	}

	void BuildTrees() {
		for (int i = 0; i < LEVEL_SIZE; i++) {
			for (int j = 0; j < LEVEL_SIZE; j++) {
				if ((int)Random.Range (0, 5) == 2) {
					Instantiate (trees [Random.Range (0, trees.Length)], new Vector3 (i * 3.0f, 0.35F, j * 3.0f), Quaternion.identity);
				}
			}
		}
	}
}
