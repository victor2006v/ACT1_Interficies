using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CatController : MonoBehaviour
{
    public static CatController Instance { get; private set; }

    private Animator animator;
    [HideInInspector] public UnityEvent onHit = new UnityEvent();
    [HideInInspector] public UnityEvent onHeal = new UnityEvent();
    [HideInInspector] public UnityEvent onDie = new UnityEvent();
    [HideInInspector] public UnityEvent onRevive = new UnityEvent();

    [SerializeField] private HealthController healthController;

    public enum States{ 
        Idle,
        Hit,
        Heal,
        Die,
        Revive
    }
    private States currentState = States.Idle;
    private void Awake() {
        if (Instance == null) { 
            Instance = this;
        } else
            Destroy(this.gameObject);
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
        else
            return;
    }
    private void Update() {
        switch (currentState){ 
            case States.Idle:
                Idle();
                break;
            case States.Hit:
                Hit(); 
                break;
            case States.Heal:
                Heal();
                break;
            case States.Die:
                Die(); 
                break;
            case States.Revive:
                Revive(); 
                break;
        }
    }
    public void SetState(States newState) {
        currentState = newState;
    }
    private void Idle(){ 
        
    }
    //Button Actions
    private void Heal(){
        onHeal.Invoke();

    }
    private void Revive(){
        animator.SetBool("Die", false);
        animator.SetBool("revive", true);
        onRevive.Invoke();
    }

    private void Die(){
        animator.SetBool("revive", false);
        animator.SetBool("Die", true);
        onDie.Invoke();
    }

    private void Hit(){
        animator.SetTrigger("Hit");
        
        onHit.Invoke();
        if (healthController.GetCurrentHealth() <= 0)
        {
            Die();
        }
    }
}
