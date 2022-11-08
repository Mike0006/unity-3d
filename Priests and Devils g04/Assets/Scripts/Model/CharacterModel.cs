using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    readonly GameObject character;
    readonly int characterType; // 0->priest, 1->devil
    readonly  Click click;

    bool _isOnBoat;
    LandModel land;


    public CharacterModel(string type)
    {
        //从预制中生成实例
        if (type == "priest")
        {
            character = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject))) as GameObject;
            characterType = 0;
        }
        else
        {
            character = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject))) as GameObject;
            characterType = 1;
        }
        //增加两个脚本在牧师/魔鬼上
        click = character.AddComponent(typeof(Click)) as Click;
        click.SetModel(this);
    }

    public void DisableClick()
    {
        click.SetStatus(1);
    }

    public void SetName(string name)
    {
        character.name = name;
    }

    public void SetPosition(Vector3 pos)
    {
        character.transform.position = pos;
    }

    public new int GetType()
    {   // 0->priest, 1->devil
        return characterType;
    }

    public string GetName()
    {
        return character.name;
    }

    public void GetOnBoat(BoatModel boat)
    {
        land = null;
        character.transform.parent = boat.GetGameobj().transform;
        _isOnBoat = true;
    }

    public void GetOnLand(LandModel landToGetOn)
    {
        land = landToGetOn;
        character.transform.parent = null;
        _isOnBoat = false;
    }

    public bool IsOnBoat()
    {
        return _isOnBoat;
    }

    public LandModel GetLandModel()
    {
        return land;
    }

    public void Reset()
    {
        land = (SSDirector.GetInstance().CurrentScenceController as FirstController).fromLand;
        GetOnLand(land);
        SetPosition(land.GetEmptyPosition());
        land.GetOnLand(this);
        click.SetStatus(0);
    }

    public GameObject getGameObject()
    {
        return character;
    }
}
