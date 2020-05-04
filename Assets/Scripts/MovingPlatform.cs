using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField]
    private Transform _targetA, _targetB;
    [SerializeField]
    private float speed = 5;
    private bool _moveRight = true;
    
    void Update() {
        if (_moveRight) {
            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, Time.deltaTime * speed);
            if (transform.position == _targetB.position) {
                _moveRight = false;
            }
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, Time.deltaTime * speed);
            if (transform.position == _targetA.position) {
                _moveRight = true;
            }
        }

        {
            
        }
    }
}