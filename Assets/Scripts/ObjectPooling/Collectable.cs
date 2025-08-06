using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private SeasoningStageManager _manager;
    private ObjectPooll _pool;
    private bool _alreadyCounted;

    private void OnEnable()
    {
        //rb.AddForceY(5);
        rb.velocity = Vector3.zero;
        _alreadyCounted = false;
    }

    public void SetPool(ObjectPooll pool, SeasoningStageManager manager)
    {
        _pool = pool;
        _manager = manager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_alreadyCounted) return;

        if (collision.gameObject.tag == "CountZone")
        {
            _alreadyCounted = true;
            _manager.AddPoint();
            StartCoroutine(RemoveObject());
        }
    }

    private IEnumerator RemoveObject()
    {
        var randomSecs = Random.Range(.35f, 4f);

        yield return new WaitForSeconds(randomSecs);
        
        _pool.ReturnObject(this.gameObject);
    }
}
