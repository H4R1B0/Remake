using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSynergy : MonoBehaviour
{
    private List<GameObject> Memels; //�޸� ����Ʈ
    private List<GameObject> Beasts; //��Ʈ ����Ʈ
    private List<GameObject> Raptors; //���� ����Ʈ
    private List<GameObject> Insects; //�μ�Ʈ ����Ʈ
    private List<GameObject> Shells; //�� ����Ʈ
    private List<GameObject> Fishs; //�ǽ� ����Ʈ
    private List<GameObject> Birds; //���� ����Ʈ
    private List<GameObject> Fossils; //ȭ�� ����Ʈ

    private List<GameObject> Warriors; //���� ����Ʈ
    private List<GameObject> Wizards; //������ ����Ʈ
    private List<GameObject> Assassins; //�ϻ��� ����Ʈ
    private List<GameObject> Gunners; //��� ����Ʈ
    private List<GameObject> Mecanics; //��ī�� ����Ʈ
    private List<GameObject> Guardians; //��ȣ�� ����Ʈ

    private void Start()
    {
        //����Ʈ �ʱ�ȭ
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

    //�ó��� ����
    public void SynergyApply()
    {
        //���� �ó���
        MemelSynergy(); //�޸� �ó���
        BeastSynergy(); //��Ʈ �ó���
        RaptorSynergy(); //���� �ó���
        InsectSynergy(); //�μ�Ʈ �ó���
        ShellSynergy(); //�� �ó���
        FishSynergy(); //�ǽ� �ó���
        BirdSynergy(); //���� �ó���
        FossilSynergy(); //ȭ�� �ó���

        WarriorSynergy(); //���� �ó���
        WizardSynergy(); //������ �ó���

        //��ȯ�� ���ֵ� �ó��� ����(ü��, ���ݷ�)
        List<GameObject> units = new List<GameObject>(GameObject.FindGameObjectsWithTag("Unit"));
        foreach(GameObject unit in units)
        {
            unit.GetComponent<Unit>().SetHealthSynergy();
            unit.GetComponent<Unit>().SetPowerSynergy();
        }

        //ġ��Ÿ�� ����
        for (int i = 0; i < Birds.Count; i++)
        {
            //���� �ó��� ȿ�� ��ŭ ġ��Ÿ�� ����
            Birds[i].GetComponent<Unit>().SetCriticalRateSynergy();
        }

        //ȸ���� ����
        for (int i = 0; i < Fishs.Count; i++)
        {
            //�ǽ� �ó��� ȿ�� ��ŭ ȸ���� ����
            Fishs[i].GetComponent<Unit>().SetAvoidRateSynergy();
        }
    }

    //�޸� �ó���
    private void MemelSynergy()
    {
        if (Memels.Count >= 4)
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //ü�� 200 �߰�
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 200;
            }
        }
        else if (Memels.Count >= 2)
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //ü�� 100 �߰�
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 100;
            }
        }
        else
        {
            for (int i = 0; i < Memels.Count; i++)
            {
                //ü�� �״��
                Memels[i].GetComponent<Unit>().MemelSynergyHP = 0;
            }
        }
    }

    //��Ʈ �ó���
    private void BeastSynergy()
    {
        if (Beasts.Count >= 4)
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //Ÿ���� ������ �ִ� ü���� 10% ȸ��
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 10;
            }
        }
        else if (Beasts.Count >= 2)
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //Ÿ���� ������ �ִ� ü���� 5% ȸ��
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 5;
            }
        }
        else
        {
            for (int i = 0; i < Beasts.Count; i++)
            {
                //Ÿ���� ������ 0 ȸ��
                Beasts[i].GetComponent<Unit>().BeastSynergyHealHPPercent = 0;
            }
        }
    }

    //���� �ó���
    private void RaptorSynergy()
    {
        if (Raptors.Count >= 4)
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //���ʸ��� ü���� 10 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 10;
                //���ʸ��� ������ 2 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 2;
            }
        }
        else if (Raptors.Count >= 2)
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //���ʸ��� ü���� 5 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 5;
                //���ʸ��� ������ 1 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 1;
            }
        }
        else
        {
            for (int i = 0; i < Raptors.Count; i++)
            {
                //���ʸ��� ü���� 0 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealHP = 0;
                //���ʸ��� ������ 0 ȸ��
                Raptors[i].GetComponent<Unit>().RaptorSynergyHealMana = 0;
            }
        }
    }

    //�μ�Ʈ �ó���
    private void InsectSynergy()
    {
        if (Insects.Count >= 4)
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //��ų�� �ʿ��� ���� 20 ����
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 20;
            }
        }
        else if (Insects.Count >= 2)
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //��ų�� �ʿ��� ���� 10 ����
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 10;
            }
        }
        else
        {
            for (int i = 0; i < Insects.Count; i++)
            {
                //��ų�� �ʿ��� ���� 0 ����
                Insects[i].GetComponent<Unit>().InsectSynergyReducedMana = 0;
            }
        }
    }

    //�� �ó���
    private void ShellSynergy()
    {
        if (Shells.Count >= 4)
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //���ط� 20% ����
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 20;
            }
        }
        else if (Shells.Count >= 2)
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //���ط� 10% ����
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 10;
            }
        }
        else
        {
            for (int i = 0; i < Shells.Count; i++)
            {
                //���ط� �״��
                Shells[i].GetComponent<Unit>().ShellSynergyReducedDamagePercent = 0;
            }
        }
    }

    //�ǽ� �ó���
    private void FishSynergy()
    {
        if (Fishs.Count >= 4)
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //ȸ���� 20 ����
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 20;
            }
        }
        else if (Fishs.Count >= 2)
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //ȸ���� 10 ����
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 10;
            }
        }
        else
        {
            for (int i = 0; i < Fishs.Count; i++)
            {
                //ȸ���� 0 ����
                Fishs[i].GetComponent<Unit>().FishSynergyAvoid = 0;
            }
        }
    }

    //���� �ó���
    private void BirdSynergy()
    {
        if (Birds.Count >= 4)
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //ũ��Ƽ�� Ȯ�� 20 ����
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 20;
            }
        }
        else if (Birds.Count >= 2)
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //ũ��Ƽ�� Ȯ�� 10 ����
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 10;
            }
        }
        else
        {
            for (int i = 0; i < Birds.Count; i++)
            {
                //ũ��Ƽ�� Ȯ�� 0 ����
                Birds[i].GetComponent<Unit>().BirdSynergyCritical = 0;
            }
        }
    }

    //ȭ�� �ó���
    private void FossilSynergy()
    {
        if (Fossils.Count >= 4)
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //���ݷ� 20 �߰�
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 20;
                //ü�� 80 �߰�
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 80;
            }
        }
        else if (Fossils.Count >= 2)
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //���ݷ� 10 �߰�
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 10;
                //ü�� 40 �߰�
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 40;
            }
        }
        else
        {
            for (int i = 0; i < Fossils.Count; i++)
            {
                //���ݷ�, ü�� �״��
                Fossils[i].GetComponent<Unit>().FossilSynergyPower = 0;
                Fossils[i].GetComponent<Unit>().FossilSynergyHP = 0;
            }
        }
    }

    //���� �ó���
    private void WarriorSynergy()
    {
        if (Warriors.Count >= 5)
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //4��°���� �߰� ����
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = true;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 4;
            }
        }
        else if (Warriors.Count >= 3)
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //6��°���� �߰� ����
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = true;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 6;
            }
        }
        else
        {
            for (int i = 0; i < Warriors.Count; i++)
            {
                //�߰� ���� ����
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackBool = false;
                Warriors[i].GetComponent<Unit>().WarriorSynergyExtraAttackCountMax = 0;
            }
        }
    }

    //������ �ó���
    private void WizardSynergy()
    {
        if (Wizards.Count >= 5)
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //ü�� 100% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 100;
                //���ݷ� 100% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 100;
            }
        }
        else if (Wizards.Count >= 3)
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //ü�� 50% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 50;
                //���ݷ� 50% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 50;
            }
        }
        else
        {
            for (int i = 0; i < Wizards.Count; i++)
            {
                //ü�� 0% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyHPPercent = 0;
                //���ݷ� 0% ����
                Wizards[i].GetComponent<Unit>().WizardSynergyPowerPercent = 0;
            }
        }
    }

    //���� �߰�
    public void TribeAdd(string tribe, GameObject unit)
    {
        Debug.Log(tribe + "���� �߰�");
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

    //���� ����
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

    //���� �߰�
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

    //���� ����
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
