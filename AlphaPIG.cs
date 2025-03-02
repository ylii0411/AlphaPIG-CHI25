using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NICER_Unity_API;

public class AlphaPIG : MonoBehaviour
{
    [Header("AlphaPIG Parameters")]
    public float fatigueThreshold;
    public float decayRateAlpha;
    public float decayRateBeta = 0.005f;

    [Header("Input Devices Parameters")]
    public float AlphaPIGManipulationVariable;
    public float manipulationMax; // Theta_1
    public float manipulationMin; // Theta_0

    private float manipulationVariable_Default;
    private float realTimeFatigue;

    [Header("Fatigue Measurements by NICER")]
    public NICER_API nicerModel;
    public string gender;
    private double fatigueLevel;
    private int targetFrameRate = 60;

    private Transform rightShoulder;
    private Transform rightElbow;
    private Transform rightWrist;
    private Transform rightHand;

    public bool transitionSmoothing;

    public bool sessionStart;
    private bool sessionOn = false;
    private float startTime;

    private float alpha = 0;
    private float beta = 1f;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }

    void Update()
    {
        // Get motion capture from Meta Movement Packages
        rightHand = GameObject.Find("Character/Bones/RightHandThumbMeta").transform;
        rightWrist = GameObject.Find("Character/Bones/RightHandWrist").transform;
        rightElbow = GameObject.Find("Character/Bones/RightArmLower").transform;
        rightShoulder = GameObject.Find("Character/Bones/RightArmUpper/RightArmUpperTwist1").transform;


        if (sessionStart == true && sessionOn == false)
        {
            sessionOn = true;
            startTime = Time.time;
        }

        RunRealTimeNicerRight(Time.deltaTime, Time.time - startTime + Time.deltaTime);

        if (sessionStart == false && sessionOn == true)
        {
            sessionOn = false;
        }

        if (sessionOn)
        {
            float fatigueDelta = realTimeFatigue - fatigueThreshold;

            if (fatigueDelta > 0)
            {
                if (beta > 0f)
                {
                    beta -= decayRateBeta;
                }

                alpha = 1 - Mathf.Exp(-decayRateAlpha * fatigueDelta);
            }
            else
            {
                if (beta < 1f)
                {
                    beta += decayRateBeta;
                }

                alpha = 0;
            }

            AlphaPIGManipulationVariable = manipulationMin + alpha * (manipulationMax - manipulationMin);

            if (transitionSmoothing is true)
            {
                AlphaPIGManipulationVariable = beta * manipulationVariable_Default + (1 - beta) * AlphaPIGManipulationVariable;
            }
        }
    }

    public void SetInteractionTechnique(float interactionVariable)
    {
        manipulationVariable_Default = interactionVariable;
    }

    public void SetFatigueModel(float f)
    {
        realTimeFatigue = f;
    }

    //********************************** Get Fatigue Prediction from NICER Model ************************************
    
    private void RunRealTimeNicerRight(float dt, float tt)
    {
        double[] results = nicerModel.generatePrediction(rightHand, rightWrist, rightElbow, rightShoulder, gender, dt, tt);
        fatigueLevel = results[1];
        SetFatigueModel((float)fatigueLevel);
    }
}
