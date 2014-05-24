/*============================================================================== 
 * Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc. All Rights Reserved. 
 * ==============================================================================*/

using UnityEngine;
using System.Collections;

public class CloudRecoSceneManager : MonoBehaviour {
    
    #region PRIVATE_MEMBER_VARIABLES
    private CameraDevice.FocusMode mFocusMode;
    #endregion PRIVATE_MEMBER_VARIABLES
    
    void Start () 
    {
        InputController.BackButtonTapped += OnBackButtonTapped;
        InputController.SingleTapped += OnSingleTapped;
        
        //Enable continuous autofocus on start;
        SetFocusModeToContinuousAuto();
    }
    
    void Update () 
    {
        InputController.UpdateInput();
    }
    
    
    private void OnBackButtonTapped()
    {
        Application.LoadLevel("Vuforia-1-About");
    }
    
    //Setting focus mode to triggerauto unsets the continuous autofocus mode. So, we invoke continuous autofocus right after.
    private void OnSingleTapped()
    {
        StartCoroutine(SetFocusModeToTriggerAuto());
    }
    
    private IEnumerator SetFocusModeToTriggerAuto()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO)) {
              mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO;
        }
        
        Debug.Log("Focus Mode Changed To " + mFocusMode);
        
        yield return new WaitForSeconds(1.0f);
        
        SetFocusModeToContinuousAuto();
        
    }
    
    private void SetFocusModeToContinuousAuto()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO)) {
            mFocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
        }
        
        Debug.Log("Focus Mode Changed To " + mFocusMode);
    }
    
}