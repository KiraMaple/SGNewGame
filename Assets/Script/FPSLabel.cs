using UnityEngine;
using System.Collections;

public class FPSLabel : MonoBehaviour {

	public float width = 50.0f;
	public float height = 30.0f;
	private UILabel m_label;
	private int fps = 0;
	private float time = 0.0f;


	// Use this for initialization
	void Start()
	{
		m_label = transform.GetComponent<UILabel>();
		this.transform.localPosition = new Vector3(-Screen.width * 0.5f + width, Screen.height * 0.5f - height, 0);
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
