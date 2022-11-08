using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandModel
{
    readonly GameObject land;
    readonly Vector3 fromPos = new Vector3(7.5F, 0.5F, 0);
    readonly Vector3 toPos = new Vector3(-7.5F, 0.5F, 0);
    readonly Vector3[] positions;
    readonly int toOrFrom;    // to->-1, from->1


    CharacterModel[] characters;

    public LandModel(string _toOrFrom)
    {
        positions = new Vector3[] {new Vector3(6, 2, -1), new Vector3(7.5F, 2, -1), new Vector3(9, 2, -1),
               new Vector3(6, 2, 1), new Vector3(7.5F, 2, 1), new Vector3(9, 2, 1)};

        characters = new CharacterModel[6];

        if (_toOrFrom == "from")
        {
            land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject))) as GameObject;
            land.transform.position = fromPos;
            land.name = "from";
            toOrFrom = 1;
        }
        else
        {
            land = Object.Instantiate(Resources.Load("Prefabs/Land", typeof(GameObject))) as GameObject;
            land.transform.position = toPos;
            land.name = "to";
            toOrFrom = -1;
        }
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

    public Vector3 GetEmptyPosition()
    {
        Vector3 pos = positions[GetEmptyIndex()];
        pos.x *= toOrFrom;
        return pos;
    }

    public void GetOnLand(CharacterModel character)
    {
        int index = GetEmptyIndex();
        characters[index] = character;
    }

    public CharacterModel GetOffLand(string name)
    {   // 0->priest, 1->devil
        for (int i = 0; i < characters.Length; i++)
        {
            if (characters[i] != null && characters[i].GetName() == name)
            {
                CharacterModel charactor = characters[i];
                characters[i] = null;
                return charactor;
            }
        }
        Debug.Log("cant find passenger on coast: " + name);
        return null;
    }

    public int GetToOrFrom()
    {
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
        characters = new CharacterModel[6];
    }
}
