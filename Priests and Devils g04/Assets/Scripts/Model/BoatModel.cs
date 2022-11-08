using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatModel
{
    readonly GameObject boat;
    readonly Click click;
    readonly Vector3 fromPosition = new Vector3(3.5F, 0.8F, 0);
    readonly Vector3 toPosition = new Vector3(-3.5F, 0.8F, 0);
    readonly Vector3[] charactersFromPos;
    readonly Vector3[] charactersToPos;
    public float speed = 20;

    int toOrFrom; // to->-1; from->1
    CharacterModel[] characters = new CharacterModel[2];

    public BoatModel()
    {
        toOrFrom = 1;

        charactersFromPos = new Vector3[] { new Vector3(2.5F, 1.7F, 0), new Vector3(4.5F, 1.7F, 0) };
        charactersToPos = new Vector3[] { new Vector3(-4.5F, 1.7F, 0), new Vector3(-2.5F, 1.7F, 0) };

        boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject))) as GameObject;
        boat.transform.position = fromPosition;
        boat.name = "boat";

        click = boat.AddComponent(typeof(Click)) as Click;
    }

    public void DisableClick()
    {
        click.SetStatus(1);
    }

    public Vector3 BoatMoveToPosition()
    {
        if (toOrFrom == -1)
        {
            toOrFrom = 1;
            return fromPosition;
        }
        else
        {
            toOrFrom = -1;
            return toPosition;
        }
    }

    public GameObject getGameObject()
    {
        return boat;
    }

    public int GetEmptyIndex()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 GetEmptyPosition()
    {
        int emptyIndex = GetEmptyIndex();
        if (toOrFrom == -1)
        {
            return charactersToPos[emptyIndex];
        }
        else
        {
            return charactersFromPos[emptyIndex];
        }
    }

    public void GetOnBoat(CharacterModel character)
    {
        int index = GetEmptyIndex();
        characters[index] = character;
    }

    public CharacterModel GetOffBoat(string name)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null && characters[i].GetName() == name)
            {
                CharacterModel charactor = characters[i];
                characters[i] = null;
                return charactor;
            }
        }
        Debug.Log("Cant find passenger in boat: " + name);
        return null;
    }

    public GameObject GetGameobj()
    {
        return boat;
    }

    public int GetToOrFrom()
    { // to->-1; from->1
        return toOrFrom;
    }

    public int[] GetCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] == null)
                continue;
            if (characters[i].GetType() == 0)
            {   // 0->priest, 1->devil
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }

    public void Reset()
    {
   
        if (toOrFrom == -1)
        {
            //Move();
        }
        characters = new CharacterModel[2];
        click.SetStatus(0);
    }
}
