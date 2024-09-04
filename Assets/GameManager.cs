using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// �V�[���̏��
    /// </summary>
    enum SCENE_STATE
    {
        TITLE = 0,   // �^�C�g��
        EXPLAIN = 1, // ����
        PLAY = 2,    // �v���C
        RESULT,      // ����
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �N���b�N������A�V�[���J��
    /// </summary>
    public void OnClickNextScene()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        if(buildIndex == (int)SCENE_STATE.RESULT)
            SceneManager.LoadSceneAsync(0);
        else
            SceneManager.LoadSceneAsync(buildIndex + 1);
    }
}
