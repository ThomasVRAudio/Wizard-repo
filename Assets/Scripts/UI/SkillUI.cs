using UnityEngine;
using UnityEngine.UI;
using MathFunctions;
using System.Collections.Generic;
using System.Collections;

public class SkillUI : MonoBehaviour
{
    public static SkillUI Instance { get; private set; }

    [SerializeField] private GameObject player;

    #region Spells
    private FireSpell fire;
    private TornadoSpell tornado;
    private AirSpell air;
    private EarthSpell earth;
    private ShieldSpell shield;
    #endregion

    #region images
    [SerializeField] private Image BorderLM;
    [SerializeField] private Image BorderQ;
    [SerializeField] private Image BorderE;
    [SerializeField] private Image BorderR;
    [SerializeField] private Image BorderT;
    #endregion

    #region Rects
    [SerializeField] private RectTransform rectLM;
    Vector3 curPosLM;

    [SerializeField] private RectTransform rectQ;
    Vector3 curPosQ;

    [SerializeField] private RectTransform rectE;
    Vector3 curPosE;

    [SerializeField] private RectTransform rectR;
    Vector3 curPosR;

    [SerializeField] private RectTransform rectT;
    Vector3 curPosT;
    #endregion

    #region skillbox images
    [SerializeField] private Image skillBoxLMImage;
    [SerializeField] private Image skillBoxQImage;
    [SerializeField] private Image skillBoxEImage;
    [SerializeField] private Image skillBoxRImage;
    [SerializeField] private Image skillBoxTImage;
    #endregion

    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;

    public Dictionary<ISpell, bool> hasShoneDictionary = new Dictionary<ISpell, bool>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        #region spells
        fire = player.GetComponent<FireSpell>();
        earth = player.GetComponent<EarthSpell>();
        shield = player.GetComponent<ShieldSpell>();
        air = player.GetComponent <AirSpell>();
        tornado = player.GetComponent<TornadoSpell>();
        #endregion 

        #region rect setup
        curPosLM = SetupPos(rectLM);
        curPosQ = SetupPos(rectQ);
        curPosE = SetupPos(rectE);
        curPosR = SetupPos(rectR);
        curPosT = SetupPos(rectT);
        #endregion
    }

    private Vector3 SetupPos(RectTransform rect) => rect.localPosition + new Vector3(70, 0, 0); 

    private void Update()
    {
        BorderLM.fillAmount = 1 - MathFunc.Remap(0, PlayerStats.Instance.FireTimer, 0, 1, SpellTimer.Instance.GetTime(fire));
        BorderQ.fillAmount = 1 - MathFunc.Remap(0, PlayerStats.Instance.ShieldTimer, 0, 1, SpellTimer.Instance.GetTime(shield));
        BorderE.fillAmount = 1 - MathFunc.Remap(0, PlayerStats.Instance.EarthTimer, 0, 1, SpellTimer.Instance.GetTime(earth));
        BorderR.fillAmount = 1 - MathFunc.Remap(0, PlayerStats.Instance.TornadoTimer, 0, 1, SpellTimer.Instance.GetTime(tornado));
        BorderT.fillAmount = 1 - MathFunc.Remap(0, PlayerStats.Instance.AirTimer, 0, 1, SpellTimer.Instance.GetTime(air));

    }

    public void SetInactiveColor(ISpell spell)
    {
        if (spell.GetType() == typeof(FireSpell))
            skillBoxLMImage.color = inactiveColor;
        else if (spell.GetType() == typeof(EarthSpell))
            skillBoxEImage.color = inactiveColor;
        else if (spell.GetType() == typeof(ShieldSpell))
            skillBoxQImage.color = inactiveColor;
        else if (spell.GetType() == typeof(TornadoSpell))
            skillBoxRImage.color = inactiveColor;
        else if (spell.GetType() == typeof(AirSpell))
            skillBoxTImage.color = inactiveColor;
    }

    public void Shine(ISpell spell)
    {
        if (!hasShoneDictionary.ContainsKey(spell))
            hasShoneDictionary.Add(spell, false);

        if (hasShoneDictionary[spell]) return;

        hasShoneDictionary[spell] = true;


        if(spell.GetType() == typeof(FireSpell))
        {
            StartCoroutine(ShineAnim(rectLM, curPosLM));
            skillBoxLMImage.color = activeColor;
        } else if (spell.GetType() == typeof(EarthSpell))
        {
            StartCoroutine(ShineAnim(rectE, curPosE));
            skillBoxEImage.color = activeColor;

        } else if (spell.GetType() == typeof(ShieldSpell))
        {
            StartCoroutine(ShineAnim(rectQ, curPosQ));
            skillBoxQImage.color = activeColor;

        } else if (spell.GetType() == typeof(TornadoSpell)) 
        {
            StartCoroutine(ShineAnim(rectR, curPosR));
            skillBoxRImage.color = activeColor;

        } else if (spell.GetType() == typeof(AirSpell))
        {
            StartCoroutine(ShineAnim(rectT, curPosT));
            skillBoxTImage.color = activeColor;
        }
    }

    private IEnumerator ShineAnim(RectTransform rect, Vector3 curPos)
    {
        float time = 1;

        while (time > 0)
        {
            float x = Mathf.Lerp(70, -70, time);
            rect.localPosition = curPos + new Vector3(x, 0, 0);
            time -= Time.deltaTime * 2;
            yield return null;
        }
    }
}
