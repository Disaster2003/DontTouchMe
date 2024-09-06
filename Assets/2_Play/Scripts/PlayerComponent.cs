using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerComponent : MonoBehaviour
{
    public static PlayerComponent instance;

    [SerializeField] float hp;
    private float hpMax;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hpMax = hp;
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���I�[�o�[
        if(hp <= 0)
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(buildIndex + 1);
        }
    }

    /// <summary>
    /// HP�o�[�̔䗦���擾����
    /// </summary>
    public float GetHPBarRatio()
    {
        return hp / hpMax;
    }
}
