using System.Collections;
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
    
    
    [SerializeField] public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;  
    
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
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }
    
    public IEnumerator TutoMove()
    {
        yield return new WaitForSeconds(3);
        background.enabled = true;
        XboxController.SetActive(true);
        leftStick.SetActive(true);
        yield return new WaitForSeconds(10);
        leftStick.SetActive(false);
        rightStick.SetActive(true);
        yield return new WaitForSeconds(10);
        rightStick.SetActive(false);
        LeftTrigger.SetActive(true);
        yield return new WaitForSeconds(10);
        LeftTrigger.SetActive(false);
        RightTrigger.SetActive(true);
        yield return new WaitForSeconds(10);
        RightTrigger.SetActive(false);
        LeftShoulder.SetActive(true);
        yield return new WaitForSeconds(10);
        LeftShoulder.SetActive(false);
        background.enabled = false;
        XboxController.SetActive(false);
    }
    
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        timerText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}
