using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;
using Toolbox;
using UnityEngine.UI;
using Unity.VisualScripting;

//This script handles running a specific status effect (single) ASSUMING it passed all the checks in order to apply an effect.
public class StatusEffectsRunner : MonoBehaviour
{
    //Variables Section
    public StatusEffectsScriptObj scriptableObjReference;
    public Image statusEffectImage; //The actual image component, NOT the visible sprite
    GameObject targetToApply;
    AtlasAnimator atlasAnimatorRef;
    ScriptMachine scriptMachineRef;
    StatusEffectsCustomFunctionality customFunctionalityRef;

    public void InitializeEffect(StatusEffectsScriptObj scriptableObjReference, GameObject target)
    {
        
        
        this.scriptableObjReference = scriptableObjReference;
        this.targetToApply = target;
        if(scriptableObjReference.isAnimatedActiveIcon == true)
        {
            if (atlasAnimatorRef == null)
            {
                atlasAnimatorRef = GetComponent<AtlasAnimator>();
            }
            atlasAnimatorRef.ReassignAnimation(scriptableObjReference.animatedActiveIcon, scriptableObjReference.activeIconTexture, scriptableObjReference.animationSpeed);
        }
        else
        {
            statusEffectImage.sprite = scriptableObjReference.activeIcon;
        }
    }

    public void InitializePermanentImmunitiy(StatusEffectsScriptObj scriptObjRef)
    {
        scriptableObjReference = scriptObjRef;
        statusEffectImage.sprite = scriptableObjReference.permanentImmuneIcon;
    }

    public void OnEnable() //Passed the if check, this function handles the running of the status effect
    {
        ApplyStatusEffect(0, null);
        //TODO
        //Add callback for when status effect finishes
    }

    private void ApplyStatusEffect(float duration, UnityAction callback)
    {
        if (atlasAnimatorRef && scriptableObjReference.isAnimatedActiveIcon == true)
        {
            atlasAnimatorRef.enabled = true;
        }
        if (scriptableObjReference.useNormalScripting == false)
        {
            if (scriptMachineRef == null)
            {
                scriptMachineRef = GetComponent<ScriptMachine>();
            }
            scriptMachineRef.graphData = scriptableObjReference.customFunctionality_Visual.graph.CreateData();
            scriptMachineRef.enabled = true;
        }
        else
        {
            if (this.GetComponent(scriptableObjReference.customFunctionality_Script.GetType()) is StatusEffectsCustomFunctionality customFunc)
            {
                customFunctionalityRef = customFunc;
            }
            else
            {
                customFunctionalityRef = this.gameObject.AddComponent(scriptableObjReference.customFunctionality_Script.GetType()) as StatusEffectsCustomFunctionality;
            }
            customFunctionalityRef.enabled = true;
        }
        StartCoroutine(DelayCallback(duration, callback));
    }

    public void OnDisable()
    {
        StartImmunity(0, null);
    }
    
    public void StartImmunity(float duration, UnityAction callback)
    {
        if (atlasAnimatorRef != null)
        {
            atlasAnimatorRef.enabled = false;
        }
        if (scriptMachineRef != null)
        {
            scriptMachineRef.enabled = false;
        }
        if (customFunctionalityRef != null)
        {
            customFunctionalityRef.enabled = false;
        }
        //Show immunity: change image
        StartCoroutine(DelayCallback(duration, callback));
        statusEffectImage.sprite = scriptableObjReference.immuneIcon;
    }

    public IEnumerator DelayCallback(float duration, UnityAction callback)
    {
        yield return new WaitForSeconds(duration);
        if (callback != null)
        {
            callback.Invoke();
        }
    }
    
}