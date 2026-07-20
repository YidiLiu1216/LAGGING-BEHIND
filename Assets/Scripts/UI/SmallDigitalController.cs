using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmallDigitalController : MonoBehaviour
{

    public Sprite[] smalldigits = new Sprite[10];
    public List<Image> activeImages = new List<Image>();

    public bool useLeadingZeros = false;
    public int minDigits = 1;

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
            img.sprite = smalldigits[d];
            img.enabled = true;
        }

        // Disable any leftover active images if string shrank (handled in EnsureDigits)
    }
    void EnsureDigits(int length)
    {
        if (length == 2)
        {
            activeImages[2].gameObject.SetActive(false);
        }
        else if (length == 1)
        {
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
