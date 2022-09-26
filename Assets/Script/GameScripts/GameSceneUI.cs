using UnityEngine;
using SaveSystemCore;

namespace GameScripts
{
    public class GameSceneUI : MonoBehaviour
    {
        private void Awake()
        {
            if (!PersistentData.firstRunDone)
            {
                PersistentData.firstRunDone = true;
                SaveSystem.LoadPlayer();
            }
        }
        public void StartTheScene(int sceneIndex)
        {
            
            Loader.Load(sceneIndex);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Music " + PersistentData.gameMusic);
                Debug.Log("Sound " + PersistentData.gameSound);
                foreach (var item in PersistentData.lastTwentyScores)
                {
                    Debug.Log(item);
                }
            }
        }
        public void QuitGame()
        {
            SaveSystem.SavePlayer();
            Application.Quit();
        }

       
    }
}
