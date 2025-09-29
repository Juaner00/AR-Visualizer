using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Onboarding goal to be achieved as part of the <see cref="GoalManager"/>.
/// </summary>
public struct Goal
{
    /// <summary>
    /// Goal state this goal represents.
    /// </summary>
    public GoalManager.OnboardingGoals CurrentGoal;

    /// <summary>
    /// This denotes whether a goal has been completed.
    /// </summary>
    public bool Completed;

    /// <summary>
    /// Creates a new Goal with the specified <see cref="GoalManager.OnboardingGoals"/>.
    /// </summary>
    /// <param name="goal">The <see cref="GoalManager.OnboardingGoals"/> state to assign to this Goal.</param>
    public Goal(GoalManager.OnboardingGoals goal)
    {
        CurrentGoal = goal;
        Completed = false;
    }
}

/// <summary>
/// The GoalManager cycles through a list of Goals, each representing
/// an <see cref="GoalManager.OnboardingGoals"/> state to be completed by the user.
/// </summary>
public class GoalManager : MonoBehaviour
{
    /// <summary>
    /// State representation for the onboarding goals for the GoalManager.
    /// </summary>
    public enum OnboardingGoals
    {
        /// <summary>
        /// Current empty scene
        /// </summary>
        Empty,

        /// <summary>
        /// Show movement hints
        /// </summary>
        Hints,

        /// <summary>
        /// Show scale and rotate hints
        /// </summary>
        Scale
    }

    /// <summary>
    /// Individual step instructions to show as part of a goal.
    /// </summary>
    [Serializable]
    public class Step
    {
        /// <summary>
        /// The GameObject to enable and show the user in order to complete the goal.
        /// </summary>
        [SerializeField]
        public GameObject stepObject;

        /// <summary>
        /// The text to display on the button shown in the step instructions.
        /// </summary>
        [SerializeField]
        public string buttonText;

        /// <summary>
        /// This indicates whether to show an additional button to skip the current goal/step.
        /// </summary>
        [SerializeField]
        public bool includeSkipButton;
    }

    [Tooltip("List of Goals/Steps to complete as part of the user onboarding.")]
    [SerializeField]
    List<Step> m_StepList = new List<Step>();

    [SerializeField] private GameObject blackout;
    
    /// <summary>
    /// List of Goals/Steps to complete as part of the user onboarding.
    /// </summary>
    public List<Step> stepList
    {
        get => m_StepList;
        set => m_StepList = value;
    }
    
    const int k_NumberOfSurfacesTappedToCompleteGoal = 1;

    Queue<Goal> m_OnboardingGoals;
    Coroutine m_CurrentCoroutine;
    Goal m_CurrentGoal;
    bool m_AllGoalsFinished;
    int m_SurfacesTapped;
    int m_CurrentGoalIndex = 0;
    private bool isFirstTime = true;
    private bool waiting = false;
    
    void Update()
    {
        if (waiting)
            return;
        
        if (Pointer.current != null && Pointer.current.press.wasPressedThisFrame && !m_AllGoalsFinished && (m_CurrentGoal.CurrentGoal == OnboardingGoals.Hints || m_CurrentGoal.CurrentGoal == OnboardingGoals.Scale))
        {
            if (m_CurrentCoroutine != null)
            {
                StopCoroutine(m_CurrentCoroutine);
            }
            CompleteGoalAndWait();
        }
    }

    void CompleteGoalAndWait()
    {
        m_CurrentGoal.Completed = true;
        m_CurrentGoalIndex++;

        StartCoroutine(NextGoal());
    }
    
    private IEnumerator NextGoal()
    {
        waiting = true;
        
        if (m_OnboardingGoals.Count > 0 && m_CurrentGoalIndex < m_StepList.Count && !m_AllGoalsFinished)
        {
            m_CurrentGoal = m_OnboardingGoals.Dequeue();
            m_StepList[m_CurrentGoalIndex - 1].stepObject.SetActive(false);
            
            yield return new WaitForSeconds(6);
                
            m_StepList[m_CurrentGoalIndex].stepObject.SetActive(true);
              
            waiting = false;
        }
        else
        {
            m_StepList[m_CurrentGoalIndex - 1].stepObject.SetActive(false);
            m_AllGoalsFinished = true;
        }
    }
    
    /// <summary>
    /// Triggers a restart of the onboarding/coaching process.
    /// </summary>
    public void StartCoaching()
    {
        if (!isFirstTime)
            return;
        
        isFirstTime = false;
        
        blackout.SetActive(true);
        
        if (m_OnboardingGoals != null)
        {
            m_OnboardingGoals.Clear();
        }

        m_OnboardingGoals = new Queue<Goal>();

        if (!m_AllGoalsFinished)
        {
            var scaleGoal = new Goal(OnboardingGoals.Scale);
            m_OnboardingGoals.Enqueue(scaleGoal);
        }

        int startingStep = m_AllGoalsFinished ? 1 : 0;

        var scaleHintsGoal = new Goal(OnboardingGoals.Scale);
        var rotateHintsGoal = new Goal(OnboardingGoals.Hints);

        m_OnboardingGoals.Enqueue(scaleHintsGoal);
        m_OnboardingGoals.Enqueue(rotateHintsGoal);

        m_CurrentGoal = m_OnboardingGoals.Dequeue();
        m_AllGoalsFinished = false;
        m_CurrentGoalIndex = startingStep;

        for (int i = startingStep; i < m_StepList.Count; i++)
        {
            if (i == startingStep)
            {
                m_StepList[i].stepObject.SetActive(true);
            }
            else
            {
                m_StepList[i].stepObject.SetActive(false);
            }
        }

    }
}
