using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour {
	[SerializeField] private Object choosenScene;

	public void ChangeScene() {
		SceneManager.LoadScene(choosenScene.name);
	}

	public void CloseGame(){
		Application.Quit();
	}
}