using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private RawImage background;
    [SerializeField] private GameObject XboxController;
    [SerializeField] private GameObject leftStick;
    [SerializeField] private GameObject rightStick;
    [SerializeField] private GameObject LeftTrigger;
    [SerializeField] private GameObject LeftShoulder;
    [SerializeField] private GameObject RightTrigger;
    [SerializeField] private RawImage RightShoulder;

    [SerializeField] private GameObject collider1;
    [SerializeField] private GameObject collider2;
    [SerializeField] private GameObject collider3;
    [SerializeField] private GameObject collider4;

    [SerializeField] private TextMeshProUGUI one;
    [SerializeField] private TextMeshProUGUI two;
    [SerializeField] private TextMeshProUGUI three;
    [SerializeField] public TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI WonText;
    [SerializeField] private TextMeshProUGUI LostText;
    
    [SerializeField] private List<GameObject> targets = new List<GameObject>();
    
    private float elapsedTime = 0f;  
    public bool EndOfTutorial = false;
    
    // private float TimeBeforeTutorial = 5f;
    // private float TimeBetweenActions = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        background.enabled = false;
        leftStick.SetActive(false);
        rightStick.SetActive(false);
        LeftTrigger.SetActive(false);
        LeftShoulder.SetActive(false);
        RightTrigger.SetActive(false);
        RightShoulder.enabled = false;
        XboxController.SetActive(false);
        StartCoroutine(TutoMove());
        one.enabled = false;
        two.enabled = false;
        three.enabled = false;
        WonText.enabled = false;
        LostText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EndOfTutorial)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
        CheckVictory();

        if (elapsedTime >= 60)
        {
            LostText.enabled = true;
        }
    }
    
    public IEnumerator TutoMove()
    {
        yield return new WaitForSeconds(3);
        background.enabled = true;
        XboxController.SetActive(true);
        leftStick.SetActive(true);
        yield return new WaitForSeconds(5);
        leftStick.SetActive(false);
        rightStick.SetActive(true);
        yield return new WaitForSeconds(5);
        rightStick.SetActive(false);
        LeftTrigger.SetActive(true);
        yield return new WaitForSeconds(5);
        LeftTrigger.SetActive(false);
        RightTrigger.SetActive(true);
        yield return new WaitForSeconds(5);
        RightTrigger.SetActive(false);
        LeftShoulder.SetActive(true);
        yield return new WaitForSeconds(5);
        LeftShoulder.SetActive(false);
        background.enabled = false;
        XboxController.SetActive(false);
        
        one.enabled = true;
        yield return new WaitForSeconds(1);
        one.enabled = false;
        two.enabled = true;
        yield return new WaitForSeconds(1);
        two.enabled = false;
        three.enabled = true;
        yield return new WaitForSeconds(1);
        three.enabled = false;
        
        collider1.gameObject.SetActive(false);
        collider2.gameObject.SetActive(false);
        collider3.gameObject.SetActive(false);
        collider4.gameObject.SetActive(false);
        
        EndOfTutorial = true;
    }
    
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }

    void CheckVictory()
    {
        
        if (targets.Count == 0)
        {
            WonText.enabled = true;
        }
    }
    
    public void RemoveTarget(GameObject target)
    {
        if (targets.Contains(target))
        {
            targets.Remove(target);
            Destroy(target);
        }
    }
}
