using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSynergy : MonoBehaviour
{
    private List<GameObject> Memels; //메멀 리스트
    private List<GameObject> Beasts; //비스트 리스트
    private List<GameObject> Raptors; //랩터 리스트
    private List<GameObject> Insects; //인섹트 리스트
    private List<GameObject> Shells; //쉘 리스트
    private List<GameObject> Fishs; //피쉬 리스트
    private List<GameObject> Birds; //버드 리스트
    private List<GameObject> Fossils; //화석 리스트

    private List<GameObject> Warriors; //전사 리스트
    private List<GameObject> Wizards; //마법사 리스트
    private List<GameObject> Assassins; //암살자 리스트
    private List<GameObject> Gunners; //사수 리스트
    private List<GameObject> Mecanics; //메카닉 리스트
    private List<GameObject> Guardians; //수호자 리스트

    private void Start()
    {
        //리스트 초기화
        Memels = new List<GameObject>();
        Beasts = new List<GameObject>();
        Raptors = new List<GameObject>();
        Insects = new List<GameObject>();
        Shells = new List<GameObject>();
        Fishs = new List<GameObject>();
        Birds = new List<GameObject>();
        Fossils = new List<GameObject>();

        Warriors = new List<GameObject>();
        Wizards = new List<GameObject>();
        Assassins = new List<GameObject>();
        Gunners = new List<GameObject>();
        Mecanics = new List<GameObject>();
        Guardians = new List<GameObject>();
    }

    //시너지 적용
    public void SynergyApply()
    {
        //종족 시너지
        MemelSynergy(); //메멀 시너지
        BeastSynergy(); //비스트 시너지
        RaptorSynergy(); //랩터 시너지
        InsectSynergy(); //인섹트 시너지
        ShellSynergy(); //쉘 시너지
        FishSynergy(); //피쉬 시너지
        BirdSynergy(); //버드 시너지
        FossilSynergy(); //화석 시너지

        WarriorSynergy(); //전사 시너지
        WizardSynergy(); //마법사 시너지

        //소환된 유닛들 시너지 적용(체력, 공격력)
        List<GameObject> units = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        foreach(GameObject unit in units)
        {
            unit.GetComponent<Unit>().SetHealthSynergy();
            unit.GetComponent<Unit>().SetPowerSynergy();
        }

        //치명타율 증가
        for (int i = 0; i < Birds.Count; i++)
        {
            //버드 시너지 효과 만큼 치명타율 증가
            Birds[i].GetComponent<Unit>().SetCriticalRateSynergy();
        }

        //회피율 증가
        for (int i = 0; i < Fishs.Count; i++)
        {
            //피쉬 시너지 효과 만큼 회피율 증가
            Fishs[i].GetComponent<Unit>().SetAvoidRateSynergy();
        }
    }

    //메멀 시너지
    private void MemelSynergy()
    {
        if (Memels.Count >= 4)
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //체력 200 추가
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 200;
            }
        }
        else if (Memels.Count >= 2)
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //체력 100 추가
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 100;
            }
        }
        else
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //체력 그대로
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 0;
            }
        }
    }

    //비스트 시너지
    private void BeastSynergy()
    {
        if (Beasts.Count >= 4)
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //타겟이 죽으면 최대 체력의 10% 회복
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 10;
            }
        }
        else if (Beasts.Count >= 2)
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //타겟이 죽으면 최대 체력의 5% 회복
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 5;
            }
        }
        else
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //타겟이 죽으면 0 회복
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 0;
            }
        }
    }

    //랩터 시너지
    private void RaptorSynergy()
    {
        if (Raptors.Count >= 4)
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //매초마다 체력을 10 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 10;
                //매초마다 마나를 2 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 2;
            }
        }
        else if (Raptors.Count >= 2)
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //매초마다 체력을 5 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 5;
                //매초마다 마나를 1 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 1;
            }
        }
        else
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //매초마다 체력을 0 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 0;
                //매초마다 마나를 0 회복
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 0;
            }
        }
    }

    //인섹트 시너지
    private void InsectSynergy()
    {
        if (Insects.Count >= 4)
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //스킬에 필요한 마나 20 감소
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 20;
            }
        }
        else if (Insects.Count >= 2)
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //스킬에 필요한 마나 10 감소
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 10;
            }
        }
        else
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //스킬에 필요한 마나 0 감소
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 0;
            }
        }
    }

    //쉘 시너지
    private void ShellSynergy()
    {
        if (Shells.Count >= 4)
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //피해량 20% 감소
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 20;
            }
        }
        else if (Shells.Count >= 2)
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //피해량 10% 감소
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 10;
            }
        }
        else
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //피해량 그대로
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 0;
            }
        }
    }

    //피쉬 시너지
    private void FishSynergy()
    {
        if (Fishs.Count >= 4)
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //회피율 20 증가
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 20;
            }
        }
        else if (Fishs.Count >= 2)
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //회피율 10 증가
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 10;
            }
        }
        else
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //회피율 0 증가
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 0;
            }
        }
    }

    //버드 시너지
    private void BirdSynergy()
    {
        if (Birds.Count >= 4)
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //크리티컬 확률 20 증가
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 20;
            }
        }
        else if (Birds.Count >= 2)
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //크리티컬 확률 10 증가
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 10;
            }
        }
        else
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //크리티컬 확률 0 증가
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 0;
            }
        }
    }

    //화석 시너지
    private void FossilSynergy()
    {
        if (Fossils.Count >= 4)
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //공격력 20 추가
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 20;
                //체력 80 추가
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 80;
            }
        }
        else if (Fossils.Count >= 2)
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //공격력 10 추가
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 10;
                //체력 40 추가
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 40;
            }
        }
        else
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //공격력, 체력 그대로
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 0;
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 0;
            }
        }
    }

    //전사 시너지
    private void WarriorSynergy()
    {
        if (Warriors.Count >= 5)
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //4번째마다 추가 공격
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = true;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 4;
            }
        }
        else if (Warriors.Count >= 3)
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //6번째마다 추가 공격
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = true;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 6;
            }
        }
        else
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //추가 공격 없음
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = false;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 0;
            }
        }
    }

    //마법사 시너지
    private void WizardSynergy()
    {
        if (Wizards.Count >= 5)
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //체력 100% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 100;
                //공격력 100% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 100;
            }
        }
        else if (Wizards.Count >= 3)
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //체력 50% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 50;
                //공격력 50% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 50;
            }
        }
        else
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //체력 0% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 0;
                //공격력 0% 증가
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 0;
            }
        }
    }

    //종족 추가
    public void TribeAdd(string tribe, GameObject unit)
    {
        Debug.Log(tribe + "종족 추가");
        if (tribe == "Memel")
        {
            Memels.Add(unit);
        }
        else if (tribe == "Beast")
        {
            Beasts.Add(unit);
        }
        else if (tribe == "Raptor")
        {
            Raptors.Add(unit);
        }
        else if (tribe == "Insect")
        {
            Insects.Add(unit);
        }
        else if (tribe == "Shell")
        {
            Shells.Add(unit);
        }
        else if (tribe == "Fish")
        {
            Fishs.Add(unit);
        }
        else if (tribe == "Bird")
        {
            Birds.Add(unit);
        }
        else if (tribe == "Fossil")
        {
            Fossils.Add(unit);
        }
    }

    //종족 제거
    public void TribeDelete(string tribe, GameObject unit)
    {
        if (tribe == "Memel")
        {
            Memels.Remove(unit);
        }
        else if (tribe == "Beast")
        {
            Beasts.Remove(unit);
        }
        else if (tribe == "Raptor")
        {
            Raptors.Remove(unit);
        }
        else if (tribe == "Insect")
        {
            Insects.Remove(unit);
        }
        else if (tribe == "Shell")
        {
            Shells.Remove(unit);
        }
        else if (tribe == "Fish")
        {
            Fishs.Remove(unit);
        }
        else if (tribe == "Bird")
        {
            Birds.Remove(unit);
        }
        else if (tribe == "Fossil")
        {
            Fossils.Remove(unit);
        }
    }

    //직업 추가
    public void JobAdd(string job, GameObject unit)
    {
        if (job == "Warrior")
        {
            Warriors.Add(unit);
        }
        else if (job == "Wizard")
        {
            Wizards.Add(unit);
        }
        else if (job == "Assassin")
        {
            Assassins.Add(unit);
        }
        else if (job == "Gunner")
        {
            Gunners.Add(unit);
        }
        else if (job == "Mecanic")
        {
            Mecanics.Add(unit);
        }
        else if (job == "Guardian")
        {
            Guardians.Add(unit);
        }
    }

    //직업 제거
    public void JobDelete(string job, GameObject unit)
    {
        if (job == "Warrior")
        {
            Warriors.Remove(unit);
        }
        else if (job == "Wizard")
        {
            Wizards.Remove(unit);
        }
        else if (job == "Assassin")
        {
            Assassins.Remove(unit);
        }
        else if (job == "Gunner")
        {
            Gunners.Remove(unit);
        }
        else if (job == "Mecanic")
        {
            Mecanics.Remove(unit);
        }
        else if (job == "Guardian")
        {
            Guardians.Remove(unit);
        }
    }
}
