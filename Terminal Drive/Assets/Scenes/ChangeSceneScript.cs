using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour {
	[SerializeField] private Object choosenScene;
	[SerializeField] private Button clickedButton;

	public void ChangeScene() {
		SceneManager.LoadScene(choosenScene.name);
		Debug.Log("Scene loaded");
	}
}
