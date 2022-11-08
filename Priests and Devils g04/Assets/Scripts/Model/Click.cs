using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    IUserAction action;
    CharacterModel character;
    private int status;

    public void SetStatus(int _status)
    {
        status = _status;
    }

    public void SetModel(CharacterModel _character)
    {
        character = _character;
    }

    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }

    void OnMouseDown()
    {
        if (status == 0)
        {
            if (gameObject.name == "boat")
            {
                action.MoveBoat();
            }
            else
            {
                action.MoveCharacter(character);
            }
        }
    }
        
}