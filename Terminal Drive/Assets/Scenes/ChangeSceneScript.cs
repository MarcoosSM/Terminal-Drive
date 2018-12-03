using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneScript : MonoBehaviour {
	[SerializeField] private string choosenScene;

	public void ChangeScene() {
		SceneManager.LoadScene(choosenScene);
	}

	public void CloseGame(){
		Application.Quit();
	}
}