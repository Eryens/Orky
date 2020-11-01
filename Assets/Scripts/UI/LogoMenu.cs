using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Save.instance.LoadOptions();
        StartCoroutine(LoadMainMenu());
	}

    private IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(3);
        LevelChangerScript.instance.FadeToLevel(1);
    }
}
