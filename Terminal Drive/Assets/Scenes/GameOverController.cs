using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverController : MonoBehaviour {

	[SerializeField] private Object MenuEscene;
 void Update()
    {
        if (Input.GetKeyDown("space"))
        {
			SceneManager.LoadScene(MenuEscene.name);
        }
    }
}
