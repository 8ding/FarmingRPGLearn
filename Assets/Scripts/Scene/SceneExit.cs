using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneExit : MonoBehaviour
{
    [SerializeField] private SceneName sceneNameGoto = SceneName.Scene1_Farm;
    [SerializeField] private Vector3 scenePositionGoto = new Vector3();


    private void OnTriggerStay2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            //  Calculate players new position

            float xPosition = Mathf.Approximately(scenePositionGoto.x, 0f) ? player.transform.position.x : scenePositionGoto.x;

            float yPosition = Mathf.Approximately(scenePositionGoto.y, 0f) ? player.transform.position.y : scenePositionGoto.y;

            float zPosition = 0f;

            // Teleport to new scene
            SceneControllerManager.Instance.FadeAndLoadScene(sceneNameGoto.ToString(), new Vector3(xPosition, yPosition, zPosition));

        }

    }
}
