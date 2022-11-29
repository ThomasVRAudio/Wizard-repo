using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellTimer : MonoBehaviour
{
    public static SpellTimer Instance;
    public static Dictionary<ISpell, float> spellTimeOutDictionary = new Dictionary<ISpell, float>();


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public void StartTimer(ISpell spell, float time)
    {
        if (!spellTimeOutDictionary.ContainsKey(spell))
        {
            spellTimeOutDictionary.Add(spell, time);
        } else if (spellTimeOutDictionary[spell] <= 0)
        {
            spellTimeOutDictionary[spell] = time;
            SkillUI.Instance.hasShoneDictionary[spell] = false;
        }
    }

    public float GetTime(ISpell spell)
    {
        if (!spellTimeOutDictionary.ContainsKey(spell)) return 0;

        return spellTimeOutDictionary[spell];
    }

    private void Update()
    {
        foreach (ISpell key in spellTimeOutDictionary.Keys.ToList())
        {
            if (spellTimeOutDictionary[key] <= 0)
            {
                SkillUI.Instance.Shine(key);
                continue;
            }

            spellTimeOutDictionary[key] -= Time.deltaTime;
        }
    }
}

