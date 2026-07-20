using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject targetBackground;
    public GameObject pasuedMenu;
    public float animationTime = 1.0f;
    public Vector2 showPos = new Vector2();
    Vector2 orignalPos;
    float animationTimer = 0.0f;
    RectTransform rectransform;
    bool isAnimating;
    bool isPoping;
    bool isPaused;
    public bool pausedAfterAnime;
    private void Awake()
    {
       
    }
    void Start()
    {
        rectransform = target.GetComponent<RectTransform>();
        orignalPos = rectransform.anchoredPosition;
        pausedAfterAnime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimating) {
            animationTimer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(animationTimer / animationTime);

            float eased = UIAnimationCurve.EaseInOutExpo(t);
            float easedbackground = UIAnimationCurve.EaseInOutSine(t);
            if (isPoping)
            {
                rectransform.anchoredPosition = Vector2.LerpUnclamped(orignalPos, showPos, eased);
                targetBackground.GetComponent<Image>().color = new Color(1.0f,1.0f,1.0f, Mathf.LerpUnclamped(0f, 1f, easedbackground));

            }
            else
            {
               
                rectransform.anchoredPosition = Vector2.LerpUnclamped(showPos, orignalPos, eased);
                targetBackground.GetComponent<Image>().color = new Color(1.0f, 1.0f, 1.0f, Mathf.LerpUnclamped(1f, 0f, easedbackground));
            }
            Color c = targetBackground.GetComponent<Image>().color;
            c.a = isPoping ? eased : 1f - eased;
            targetBackground.GetComponent<Image>().color = c;
            if (t >= 1) { isAnimating = false;animationTimer=0; if (!isPoping) { pasuedMenu.SetActive(false); } }
            /*
            if (t >= 1) {
                if (pausedAfterAnime) { SetPaused(true); pausedAfterAnime = false; }
                else { pasuedMenu.SetActive(false); }
                isAnimating = false; t = 0;
            }
            */
        }
    }
    public void PauseAndPopMenu() {
        if (isAnimating) return;

    }
    public void StartPopMenu() {
        //Debug.Log("click!");
        if (isAnimating) return;
        isAnimating = true;
        
        isPoping = true;
        Time.timeScale = 0;
        AudioController.instance.pauseBackgroundMusic();
    }
    public void EndPopMenu()
    {
        //Debug.Log("click!");
        
        isAnimating = true;
        isPoping = false;
        SetPaused(false);
        AudioController.instance.unpauseBackgroundMusic();
    }
    public void setPauseAfterPlayAnime() {
        pausedAfterAnime = true;
    }
    public void SetPaused(bool paused)
    {
        isPaused = paused;
        //if (paused == true) {
        //    isPaused = paused;
        //}
        // Freeze game time

        Time.timeScale = paused ? 0f : 1f;
        
    }
}
