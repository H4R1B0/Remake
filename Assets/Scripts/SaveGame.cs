using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

public class SaveGame : MonoBehaviour
{
    public List<GameObject> UnitCards;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/Player.xml"))
        {
            Debug.Log("파일 없으므로 생성");
            CreateXml();
        }
        else
        {
            Debug.Log("파일 있음");
            LoadXml();
        }
    }

    void CreateXml()
    {
        XmlDocument xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        XmlNode root = xmlDoc.CreateNode(XmlNodeType.Element, "PlayerInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        XmlNode child = xmlDoc.CreateNode(XmlNodeType.Element, "Player", string.Empty);
        root.AppendChild(child);

        //플레이어가 갖고 있는 유닛카드 저장
        XmlElement AnimaCard = xmlDoc.CreateElement("AnimaCard");
        AnimaCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[0]).ToString();
        child.AppendChild(AnimaCard);

        XmlElement BaroqueCard = xmlDoc.CreateElement("BaroqueCard");
        BaroqueCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[1]).ToString();
        child.AppendChild(BaroqueCard);

        XmlElement BattiCard = xmlDoc.CreateElement("BattiCard");
        BattiCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[2]).ToString();
        child.AppendChild(BattiCard);

        XmlElement BeomhoCard = xmlDoc.CreateElement("BeomhoCard");
        BeomhoCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[3]).ToString();
        child.AppendChild(BeomhoCard);

        XmlElement CrusherCard = xmlDoc.CreateElement("CrusherCard");
        CrusherCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[4]).ToString();
        child.AppendChild(CrusherCard);

        XmlElement DestinyCard = xmlDoc.CreateElement("DestinyCard");
        DestinyCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[5]).ToString();
        child.AppendChild(DestinyCard);

        XmlElement DicafrioCard = xmlDoc.CreateElement("DicafrioCard");
        DicafrioCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[6]).ToString();
        child.AppendChild(DicafrioCard);

        XmlElement FennyCard = xmlDoc.CreateElement("FennyCard");
        FennyCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[7]).ToString();
        child.AppendChild(FennyCard);

        XmlElement HadesCard = xmlDoc.CreateElement("HadesCard");
        HadesCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[8]).ToString();
        child.AppendChild(HadesCard);

        XmlElement JenisCard = xmlDoc.CreateElement("JenisCard");
        JenisCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[9]).ToString();
        child.AppendChild(JenisCard);

        XmlElement KelsyCard = xmlDoc.CreateElement("KelsyCard");
        KelsyCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[10]).ToString();
        child.AppendChild(KelsyCard);

        XmlElement KirabeeCard = xmlDoc.CreateElement("KirabeeCard");
        KirabeeCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[11]).ToString();
        child.AppendChild(KirabeeCard);

        XmlElement NanoCard = xmlDoc.CreateElement("NanoCard");
        NanoCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[12]).ToString();
        child.AppendChild(NanoCard);

        XmlElement OrihiruCard = xmlDoc.CreateElement("OrihiruCard");
        OrihiruCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[13]).ToString();
        child.AppendChild(OrihiruCard);

        XmlElement RangCard = xmlDoc.CreateElement("RangCard");
        RangCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[14]).ToString();
        child.AppendChild(RangCard);

        XmlElement RifiCard = xmlDoc.CreateElement("RifiCard");
        RifiCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[15]).ToString();
        child.AppendChild(RifiCard);

        XmlElement SpinpsCard = xmlDoc.CreateElement("SpinpsCard");
        SpinpsCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[16]).ToString();
        child.AppendChild(SpinpsCard);

        XmlElement SquilCard = xmlDoc.CreateElement("SquilCard");
        SquilCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[17]).ToString();
        child.AppendChild(SquilCard);

        XmlElement TombCard = xmlDoc.CreateElement("TombCard");
        TombCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[18]).ToString();
        child.AppendChild(TombCard);

        XmlElement WrightCard = xmlDoc.CreateElement("WrightCard");
        WrightCard.InnerText = Player.instance.UnitCards.Contains(UnitCards[19]).ToString();
        child.AppendChild(WrightCard);

        XmlElement Coin = xmlDoc.CreateElement("Coin");
        Coin.InnerText = Player.instance.Coin.ToString();
        child.AppendChild(Coin);

        XmlElement Crystal = xmlDoc.CreateElement("Crystal");
        Crystal.InnerText = Player.instance.Crystal.ToString();
        child.AppendChild(Crystal);

        XmlElement CallUnitCountMax = xmlDoc.CreateElement("CallUnitCountMax");
        CallUnitCountMax.InnerText = Player.instance.CallUnitCountMax.ToString();
        child.AppendChild(CallUnitCountMax);

        XmlElement CallUnitCountAddPrice = xmlDoc.CreateElement("CallUnitCountAddPrice");
        CallUnitCountAddPrice.InnerText = Player.instance.CallUnitCountAddPrice.ToString();
        child.AppendChild(CallUnitCountAddPrice);

        XmlElement StarPoint = xmlDoc.CreateElement("StarPoint");
        StarPoint.InnerText = Player.instance.StarPoint.ToString();
        child.AppendChild(StarPoint);

        XmlElement StarPointLevel = xmlDoc.CreateElement("StarPointLevel");
        StarPointLevel.InnerText = Player.instance.StarPointLevel.ToString();
        child.AppendChild(StarPointLevel);

        xmlDoc.Save(Application.persistentDataPath + "/Player.xml");

        xmlDoc = new XmlDocument();

        // Xml을 선언한다(xml의 버전과 인코딩 방식을 정해준다.)
        xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes"));

        // 루트 노드 생성
        root = xmlDoc.CreateNode(XmlNodeType.Element, "GameManagerInfo", string.Empty);
        xmlDoc.AppendChild(root);

        // 자식 노드 생성
        child = xmlDoc.CreateNode(XmlNodeType.Element, "GameManager", string.Empty);
        root.AppendChild(child);

        // 자식 노드에 들어갈 속성 생성
        XmlElement Round = xmlDoc.CreateElement("Round");
        Round.InnerText = GameManager.instance.Round.ToString();
        child.AppendChild(Round);

        xmlDoc.Save(Application.persistentDataPath + "/GameManager.xml");
    }

    void LoadXml()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/Player.xml");
        //Debug.Log("asasasa");

        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Player");

        foreach (XmlNode node in nodes)
        {
            for (int i = 0; i < UnitCards.Count; i++)
            {
                if (node.SelectSingleNode(UnitCards[i].name).InnerText == "True" && Player.instance.UnitCards.Contains(UnitCards[i]) == false)
                {
                    Debug.Log(UnitCards[i].name + " 추가");
                    Player.instance.UnitCards.Add(UnitCards[i]);
                }
            }

            Player.instance.Coin = int.Parse(node.SelectSingleNode("Coin").InnerText);
            Player.instance.Crystal = int.Parse(node.SelectSingleNode("Crystal").InnerText);
            Player.instance.CallUnitCountMax = int.Parse(node.SelectSingleNode("CallUnitCountMax").InnerText);
            Player.instance.CallUnitCountAddPrice = int.Parse(node.SelectSingleNode("CallUnitCountAddPrice").InnerText);
            Player.instance.StarPoint = int.Parse(node.SelectSingleNode("StarPoint").InnerText);
            Player.instance.StarPointLevel = int.Parse(node.SelectSingleNode("StarPointLevel").InnerText);
        }

        xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/GameManager.xml");

        nodes = xmlDoc.SelectNodes("GameManagerInfo/GameManager");

        foreach (XmlNode node in nodes)
        {
            GameManager.instance.Round = int.Parse(node.SelectSingleNode("Round").InnerText);
            //Debug.Log(node.SelectSingleNode("Round").InnerText);
        }
    }

    public void SaveOverlapXml()
    {
        Debug.Log("덮어쓰기");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/Player.xml");

        //플레이어
        XmlNodeList nodes = xmlDoc.SelectNodes("PlayerInfo/Player");
        XmlNode player = nodes[0];

        for (int i = 0; i < UnitCards.Count; i++)
        {
            player.SelectSingleNode(UnitCards[i].name).InnerText = Player.instance.UnitCards.Contains(UnitCards[i]).ToString();
        }

        player.SelectSingleNode("Coin").InnerText = Player.instance.Coin.ToString();
        player.SelectSingleNode("Crystal").InnerText = Player.instance.Crystal.ToString();
        player.SelectSingleNode("CallUnitCountMax").InnerText = Player.instance.CallUnitCountMax.ToString();
        player.SelectSingleNode("CallUnitCountAddPrice").InnerText = Player.instance.CallUnitCountAddPrice.ToString();
        player.SelectSingleNode("StarPoint").InnerText = Player.instance.StarPoint.ToString();
        player.SelectSingleNode("StarPointLevel").InnerText = Player.instance.StarPointLevel.ToString();

        xmlDoc.Save(Application.persistentDataPath + "/Player.xml");

        //게임매니저
        xmlDoc = new XmlDocument();
        xmlDoc.Load(Application.persistentDataPath + "/GameManager.xml");
        nodes = xmlDoc.SelectNodes("GameManagerInfo/GameManager");
        XmlNode gamemanager = nodes[0];

        gamemanager.SelectSingleNode("Round").InnerText = GameManager.instance.Round.ToString();
        xmlDoc.Save(Application.persistentDataPath + "/GameManager.xml");
    }
}