using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();
        if(behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die()); //为什么这里用调用协程 而不是直接执行Die()？
    }
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);  //推到敌人
        yield return new WaitForSeconds(1.5f);  //等待1.5s
        Destroy(this.gameObject);
    }
}
