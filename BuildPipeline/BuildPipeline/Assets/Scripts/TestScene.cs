using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackRefactory
{
    public class TestScene : MonoBehaviour
    {
        public string Scene;

        public void LoadScene()
        {
            SceneManager.LoadScene(Scene);
        }
    }
}


