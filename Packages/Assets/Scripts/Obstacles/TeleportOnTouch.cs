using UnityEngine;
using UnityEngine.SceneManagement; 

public class TeleportOnTouch : MonoBehaviour
{
    public string sceneName; 

    private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Player"))
        {
           
            SceneManager.LoadScene(sceneName);
        }
    }
}
