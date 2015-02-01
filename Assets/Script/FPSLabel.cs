using UnityEngine;
using System.Collections;
using System;

public class FPSLabel : MonoBehaviour {

	public float width = 50.0f;
	public float height = 10.0f;
	private UILabel m_label;
	private int fps = 0;
	private float time = 0.0f;


	// Use this for initialization
	void Start()
	{
		CInputMgr.GetInstance();

		int UIWidth, UIHeight;
		CGameMgr.GetInstance().GetUISize(out UIWidth, out UIHeight);
		int labelWidth, labelHeight;
		m_label = transform.GetComponent<UILabel>();
		labelWidth = Convert.ToInt32(m_label.localSize.x);
		labelHeight = Convert.ToInt32(m_label.localSize.y);
		this.transform.localPosition = new Vector3(-UIWidth * 0.5f + labelWidth * 0.5f + width, UIHeight * 0.5f - labelHeight * 0.5f - height, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (time > 1.0f)
		{
			m_label.text = "FPS: " + fps;
			time = 0.0f;
			fps = 0;
		}
		else
		{
			time += Time.deltaTime;
			fps++;
		}
	}
}
