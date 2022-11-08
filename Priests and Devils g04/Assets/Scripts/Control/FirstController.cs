using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    readonly Vector3 waterPos = new Vector3(0, 0, 0);
    public LandModel fromLand;            
    public LandModel toLand;              
    public BoatModel boat;
    public Judge judge;
    private CharacterModel[] characters;
    private UserGUI userGUI;

    public MySceneActionManager actionManager;   //动作管理

    // Start is called before the first frame update
    void Start()
    {
        SSDirector director = SSDirector.GetInstance();      
        director.CurrentScenceController = this;             
        userGUI = gameObject.AddComponent<UserGUI>() as UserGUI;  
        actionManager = gameObject.AddComponent<MySceneActionManager>() as MySceneActionManager; 
        characters = new CharacterModel[6];
        LoadResources();                                     
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].DisableClick();
        }
        judge = new Judge(fromLand, toLand, boat);
    }

    public void LoadResources()              //布景
    {
        GameObject water = Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject))) as GameObject;
        water.transform.position = waterPos;
        water.name = "water";

        fromLand = new LandModel("from");
        toLand = new LandModel("to");
        boat = new BoatModel();

        for (int i = 0; i < 3; i++)
        {
            CharacterModel newOne = new CharacterModel("priest");
            newOne.SetName("priest" + i);
            newOne.SetPosition(fromLand.GetEmptyPosition());
            newOne.GetOnLand(fromLand);
            fromLand.GetOnLand(newOne);

            characters[i] = newOne;
        }

        for (int i = 0; i < 3; i++)
        {
            CharacterModel newOne = new CharacterModel("devil");
            newOne.SetName("devil" + i);
            newOne.SetPosition(fromLand.GetEmptyPosition());
            newOne.GetOnLand(fromLand);
            fromLand.GetOnLand(newOne);

            characters[i + 3] = newOne;
        }
    }

    public void MoveBoat()                  //移动船
    {
        if (boat.IsEmpty())
            return;
        //boat.Move();
        actionManager.moveBoat(boat.getGameObject(), boat.BoatMoveToPosition(), boat.speed);
        userGUI.status = isOver();
        if (isOver() != 0)
        {
            boat.DisableClick();
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].DisableClick();
            }
        }
    }

    private int isOver()
    {
        return judge.isOver();
    }

    public void MoveCharacter(CharacterModel character)    //移动角色
    {
        if (character.IsOnBoat())
        {
            LandModel whichLand;
            if (boat.GetToOrFrom() == -1)
            { // to->-1; from->1
                whichLand = toLand;
            }
            else
            {
                whichLand = fromLand;
            }

            boat.GetOffBoat(character.GetName());
            Vector3 dest = whichLand.GetEmptyPosition();                                         
            Vector3 middle = new Vector3(character.getGameObject().transform.position.x, dest.y, dest.z);  
            actionManager.moveRole(character.getGameObject(), middle, dest, boat.speed); 
            character.GetOnLand(whichLand);
            whichLand.GetOnLand(character);

            if (boat.IsEmpty() && boat.GetToOrFrom() == -1)
                actionManager.moveBoat(boat.getGameObject(), boat.BoatMoveToPosition(), boat.speed);

        }
        else
        {                                   
            LandModel whichLand = character.GetLandModel(); // character在land上

            if (boat.GetEmptyIndex() == -1)
            {       
                return;
            }

            if (whichLand.GetToOrFrom() != boat.GetToOrFrom())   
                return;

            whichLand.GetOffLand(character.GetName());
            Vector3 dest = boat.GetEmptyPosition();                                       
            Vector3 middle = new Vector3(dest.x, character.getGameObject().transform.position.y, dest.z);  
            actionManager.moveRole(character.getGameObject(), middle, dest, boat.speed);  
            character.GetOnBoat(boat);
            boat.GetOnBoat(character);
        }
        userGUI.status = isOver();
        if (isOver() != 0)
        {
            boat.DisableClick();
            for (int i = 0; i < characters.Length; i++)
            {
                characters[i].DisableClick();
            }
        }
    }

    public void Restart()
    {
        boat.Reset(); 
        if (boat.GetToOrFrom() == -1)
        {
            actionManager.moveBoat(boat.getGameObject(), boat.BoatMoveToPosition(), boat.speed);
        }
        fromLand.Reset();
        toLand.Reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Reset();
        }
    }
}
