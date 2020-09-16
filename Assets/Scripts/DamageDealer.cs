﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeStep;
    PlayerController _player;
    DateTime _lastEncounter;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if((DateTime.Now - _lastEncounter).TotalSeconds < 0.02f)
        {
            return;
        }
        _lastEncounter = DateTime.Now;
        _player = info.GetComponent<PlayerController>();
        if(_player != null)
        {
            _player.ChangeHP(-_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D info)
    {
        if(_player == info.GetComponent<PlayerController>())
        {
            _player = null;
        }
    }

    private void Update()
    {
        if(_player!=null && (DateTime.Now - _lastEncounter).TotalSeconds > _timeStep)
        {
            _lastEncounter = DateTime.Now;
            _player.ChangeHP(-_damage);
        }
    }


}
