using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public bool isClear;
    private float time = 0;
    private float aftertime = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Reload();
        }

        if (this.isClear == true)
        {
            Clear();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene("Stage0Scene");
        Debug.Log("stage0");
    }

    public void Clear()
    {

        this.aftertime += Time.deltaTime;
        Debug.Log((int)aftertime);

        if (aftertime >= 5)
        {
            SceneManager.LoadScene("Stage1Scene");
            Debug.Log("stage1");
        }
    }

}
