using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CopyText : MonoBehaviour
{
    private TMP_Text CopiedToText;
    public TMP_Text CopiedText;

    void Awake()
    {
        CopiedToText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(CopiedToText !=null && CopiedText != null) CopiedToText.text = CopiedText.text;
    }

}
