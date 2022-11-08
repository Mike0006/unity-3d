using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Judge
{
    public LandModel fromLand;
    public LandModel toLand;
    public BoatModel boat;

    public Judge(LandModel _fromLand, LandModel _toLand, BoatModel _boat)
    {
        fromLand = _fromLand;
        toLand = _toLand;
        boat = _boat;
    }

    public  int isOver()
    {

        int fromPriest = 0;
        int fromDevil = 0;
        int toPriest = 0;
        int toDevil = 0;

        int[] fromCount = fromLand.GetCharacterNum();
        fromPriest += fromCount[0];
        fromDevil += fromCount[1];

        int[] toCount = toLand.GetCharacterNum();
        toPriest += toCount[0];
        toDevil += toCount[1];

        if (toPriest + toDevil == 6)      // 赢了
            return 2;

        int[] boatCount = boat.GetCharacterNum();
        if (boat.GetToOrFrom() == -1)
        {
            toPriest += boatCount[0];
            toDevil += boatCount[1];
        }
        else
        {
            fromPriest += boatCount[0];
            fromDevil += boatCount[1];
        }
        if (fromPriest < fromDevil && fromPriest > 0) //输了
        {
            return 1;
        }
        if (toPriest < toDevil && toPriest > 0)
        {
            return 1;
        }
        return 0;			// 游戏未结束
    }
}
