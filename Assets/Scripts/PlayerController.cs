using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    private int _currentHP;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentHP = _maxHP;
    }

    public void ChangeHP(int value)
    {
        _currentHP += value;
        Debug.Log("Value = " + value);
        Debug.Log("Current HP = " + _currentHP);
        if(_currentHP>_maxHP)
        {
            _currentHP = _maxHP;
        }
        else if(_currentHP<=0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }
}
