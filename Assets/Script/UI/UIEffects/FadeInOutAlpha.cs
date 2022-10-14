using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutAlpha : MonoBehaviour
{   
    bool fadeIn = true, fadeOut = false;
    public float fadeSpeed = 0.02f;
     float GoTo;
	Color colr;

    void Start()
    {
    //    Debug.Log("nameShow" + this.name);
        FadeIn(256);
    }


	void Update ()
    {
        if (fadeIn)
		{
          //  if (this.tag == "Image")
            {
                colr = this.GetComponent<Image>().color;
            }
            //else if (this.tag == "Text")
            //    colr = this.GetComponent<Text>().color;
            if (colr.a < GoTo) colr.a += fadeSpeed;
            else fadeIn = false;
		//	if (this.tag == "Image")
				this.GetComponent<Image> ().color = colr;
			//else if (this.tag == "Text")
			//	this.GetComponent<Text> ().color = colr;
        }

        if (fadeOut)
        {
		//	if (this.tag == "Image")
             colr = this.GetComponent<Image>().color;
			//else if (this.tag == "Text")
			//	colr = this.GetComponent<Text> ().color;
            if (colr.a > 0.2f) colr.a -= fadeSpeed;
            else
            {
                fadeOut = false;
                this.gameObject.SetActive(false);
            } 
			//if (this.tag == "Image")
            this.GetComponent<Image>().color = colr;
			//else if(this.tag == "Text")
			//	this.GetComponent<Text> ().color = colr;
          
        }
    }

    public void FadeIn(float gotoVal)
    {
		this.gameObject.SetActive (true);

        this.GoTo = gotoVal;
		//if (this.tag == "Image")
        colr = this.GetComponent<Image>().color;
		//else if (this.tag == "Text")
		//	colr = this.GetComponent<Text> ().color;
        colr.a = 0;
		//if (this.tag == "Image")
        this.GetComponent<Image>().color = colr;
		//else if(this.tag == "Text")
		//	this.GetComponent<Text> ().color = colr;
        fadeIn = true;
    }

    public void FadeOut()
    {
		fadeIn = false;
        fadeOut = true;
    }
}
