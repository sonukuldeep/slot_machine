using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameScripts
{

    public static class Loader
    {
        /*
        public enum Scene
        {
            MainScene,
        }
        */

        private static Action onLoaderCallback;

        public static void Load(int scene)
        {

            onLoaderCallback = () => { SceneManager.LoadScene(scene); }; // set loader callback action to load target scene
            
            SceneManager.LoadScene(1); // load loading scene 
        }

        public static void LoaderCallback() //trigger after first update. Executes the loader callback action which will load target scene
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }
}
