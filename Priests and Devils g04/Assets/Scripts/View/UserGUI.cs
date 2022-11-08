using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;
    private int start;

    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;

        style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;

        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
        start = 0;
    }

    void OnGUI()
    {
        if (start == 1)
        {
            if (status == 1)
            {
                GUI.Label(new Rect(450, 20, 106, 150), "Gameover!", style);
                if (GUI.Button(new Rect(450, 340, 150, 50), "Restart", buttonStyle))
                {
                    status = 0;
                    action.Restart();
                }
            }
            else if (status == 2)
            {
                GUI.Label(new Rect(450, 20, 106, 150), "You win!", style);
                if (GUI.Button(new Rect(450, 340, 150, 50), "Restart", buttonStyle))
                {
                    status = 0;
                    action.Restart();
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(450, 340, 150, 50), "Start", buttonStyle))
            {
                status = 0;
                start = 1;
                action.Restart();
            }
        }
        
    }
}
