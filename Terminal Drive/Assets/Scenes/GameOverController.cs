using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    [SerializeField] private string MenuEscene;

    void Update() {
        if (Input.GetKeyDown("space")) {
            SceneManager.LoadScene(MenuEscene);
        }
    }
}