using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : LivingEntity
{
    protected int baseHP; //기본 체력
    protected int roundHP; //라운드당 추가되는 체력
    protected int basePower; //기본 공격력
    protected int roundPower; //라운드당 추가되는 공격력
}