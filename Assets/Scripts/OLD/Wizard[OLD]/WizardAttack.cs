using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : Wizard
{
    public ISpell spell;
    public Transform spellSpawnPos;

    private bool hasCast = false;

    // Update is called once per frame
    void Update()
    {
        if (hasCast)
            return;

        if (Input.GetKeyDown(KeyCode.Q)) SpellCast(GetComponent<FireSpell>());
        if (Input.GetKeyDown(KeyCode.E)) SpellCast(GetComponent<EarthSpell>());
        if (Input.GetKeyDown(KeyCode.R)) SpellCast(GetComponent<FireSpell>());
        if (Input.GetKeyDown(KeyCode.T)) SpellCast(GetComponent<EarthSpell>());
    }

    void SpellCast(ISpell SpellType)
    {
        spell = SpellType;
        hasCast = true;
        speed = 0;

        spell.Cast(spellSpawnPos, animator);
    }

    void OnEndSpell(object sender, EventArgs e)
    {
        hasCast = false;
        speed = startSpeed;
    }
} 