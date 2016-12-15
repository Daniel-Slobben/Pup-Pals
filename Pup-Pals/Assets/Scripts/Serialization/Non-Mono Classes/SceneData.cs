using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveGame {

    public int savegameName = 10;
	public List<SceneObject> sceneObjects = new List<SceneObject>();

	public SaveGame() {

	}

	public SaveGame(int saveGameNumber, List<SceneObject> list) {
		savegameName = saveGameNumber;
		sceneObjects = list;
	}
}
