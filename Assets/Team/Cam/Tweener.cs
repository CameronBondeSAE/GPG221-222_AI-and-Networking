using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
	    // var tweener = transform.DOLocalMoveY(1f, 1f);
	    // tweener.SetEase(Ease.InQuad);
	    // tweener.OnComplete()

	    transform.DOLocalMoveY(3f, 5f).SetEase(Ease.InOutQuart).OnComplete(ElevatorFinished);

	    GetComponent<Renderer>().material.DOColor(Color.red, 13f);
    }

    private void ElevatorFinished()
    {
	    Debug.Log("DONE!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
