using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
  
    bool isGrounded;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;
    private Vector3 velocity;
    [Header ("Movement Parameters")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;
    
    private Vector3 initScale;
    private bool movingLeft;
    

private void DirectionChange(){
        movingLeft = !movingLeft;
}
    private void Awake(){
        initScale = enemy.localScale;
    }

    private void MoveInDirection(int direction){
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime*direction*speed, enemy.position.y, enemy.position.z);
    }
    void Start(){
        
    }

    void Update(){
        if (movingLeft){
        
            if(enemy.position.x >= leftEdge.position.x)
                 MoveInDirection(-1);
            else{
                DirectionChange();
            }
        }
        else{
         
            if(enemy.position.x <= rightEdge.position.x)
                 MoveInDirection(1);
            else{
                DirectionChange();
            }
            
        }
    }
}
       
    
    