using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanPanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Digit sprites")]
    public Sprite[] digits = new Sprite[10];
    //public Sprite[] smalldigits = new Sprite[10];

    [Header("Formatting")]
    public bool useLeadingZeros = false;
    public int minDigits = 1;    
    public List<Image> activeImages = new List<Image>();
    [Header("Show and Hide")]
    public GameObject target;
    public GameObject targetBackground;
    public float animationTime = 1.0f;
    public Vector2 showPos = new Vector2();
    Vector2 orignalPos;
    float animationTimer=0.0f;
    RectTransform rectransform;
    [Header("Small Digit Count")]
    public List<string> types=new List<string>();
    public List<SmallDigitalController> citizenDigitalByType = new List<SmallDigitalController>();
    public List<SmallDigitalController> reactiontionHistDigitalByType = new List<SmallDigitalController>();
    public List<SmallDigitalController> enemyDisposalDigitalByType = new List<SmallDigitalController>();
    //public Dictionary<string, SmallDigitalController> KillsDigitalByType = new Dictionary<string, SmallDigitalController>();
    //public Dictionary<string, SmallDigitalController> LoveMinistrayDigitalByType = new Dictionary<string, SmallDigitalController>();
   // public Dictionary<string, SmallDigitalController> GarbageDisposalDigitalByType = new Dictionary<string, SmallDigitalController>();

    public static SanPanelControl instance;
    bool isAnimating;
    bool isPoping;
    
    private void Awake()
    {
        if (instance != null) { Destroy(this); } else { instance = this; }
        rectransform = target.GetComponent<RectTransform>();
        orignalPos = rectransform.anchoredPosition;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!isAnimating)
        {
            animationTimer = 0f;
            isAnimating = true;
            isPoping = !isPoping;
            targetBackground.SetActive(!targetBackground.activeInHierarchy);
            updatecitizenDigital();
            updateReactionayDigital();
            updateEnemyDigital();
            rectransform.anchoredPosition = showPos;
        }
        if (isAnimating) {
            animationTimer += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(animationTimer / animationTime);

            float eased = UIAnimationCurve.EaseInOutExpo(t);
            if (isPoping)
            {
                rectransform.anchoredPosition = Vector2.LerpUnclamped(orignalPos, showPos, eased);
            }
            else {
                rectransform.anchoredPosition = Vector2.LerpUnclamped(showPos, orignalPos, eased);
            }

            if (t >= 1) isAnimating = false;
        }
    }
  
    public void updatecitizenDigital() {
        string s1 = types[0];
        string s2 = types[1];
        int id1= types.IndexOf(s1);
        int id2 = types.IndexOf(s2);

        //SmallDigitalController controller1= KillsDigitalByType[0];
        int count1 = PlayerData.instance.LoveMinistryByType[s1] + PlayerData.instance.LoveMinistryByType[s2];
        int count2 = PlayerData.instance.KillsByType[s1]+PlayerData.instance.KillsByType[s2];
        int count3 = PlayerData.instance.GarbageDisposalByType[s1] + PlayerData.instance.GarbageDisposalByType[s2];
        //controller.SetValue(count);
        citizenDigitalByType[0].SetValue(count1);
        citizenDigitalByType[1].SetValue(count2);
        citizenDigitalByType[2].SetValue(count3);
    }
    public void updateReactionayDigital()
    {
        string s = types[3];

        int count1 = PlayerData.instance.LoveMinistryByType[s];
        int count2 = PlayerData.instance.KillsByType[s];
        int count3 = PlayerData.instance.GarbageDisposalByType[s];
        //controller.SetValue(count);
        reactiontionHistDigitalByType[0].SetValue(count1);
        reactiontionHistDigitalByType[1].SetValue(count2);
        reactiontionHistDigitalByType[2].SetValue(count3);
    }
    public void updateEnemyDigital() {
        enemyDisposalDigitalByType[0].SetValue(0);
        enemyDisposalDigitalByType[1].SetValue(0);
        enemyDisposalDigitalByType[2].SetValue(0);
    }

    public void SetValue(int value)
    {
        if (value < 0) value = 0; // clamp

        string s = useLeadingZeros ? value.ToString(new string('0', Mathf.Max(minDigits, 1)))
                                   : value.ToString();

        // Ensure we have enough active digit Images
        EnsureDigits(s.Length);

        // Assign sprites
        for (int i = 0; i < s.Length; i++)
        {
            int d = s[i] - '0';
            if (d < 0 || d > 9) d = 0;

            var img = activeImages[i];
            img.sprite = digits[d];
            img.enabled = true;
        }

        // Disable any leftover active images if string shrank (handled in EnsureDigits)
    }
    public void ShowPanel() {
        animationTimer = 0f;
        isAnimating = true;
        isPoping = !isPoping;
        targetBackground.SetActive(!targetBackground.activeInHierarchy);
        updatecitizenDigital();
        updateReactionayDigital();
        updateEnemyDigital();
        rectransform.anchoredPosition = showPos;
    }
    void EnsureDigits(int length) {
        if (length == 2)
        {
            activeImages[2].gameObject.SetActive(false);
        }
        else if (length == 1) {
            activeImages[2].gameObject.SetActive(false);
            activeImages[1].gameObject.SetActive(false);
        }
        else
        {
            activeImages[0].gameObject.SetActive(true);
            activeImages[2].gameObject.SetActive(true);
        }
    }
    
}
