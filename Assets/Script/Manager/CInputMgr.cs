using UnityEngine;
using System.Collections;

public class CInputMgr
{

	private static CInputMgr m_instance = null;

	public static CInputMgr GetInstance()
	{
		if (m_instance == null)
		{
			m_instance = new CInputMgr();
		}
		return m_instance;
	}

	private CInputMgr()
	{
		GameObject leftButton1 = GameObject.Find("UI Root/Panel/LeftButton1");
		UIEventListener.Get(leftButton1).onClick = LeftButton1OnClick;

		GameObject leftButton2 = GameObject.Find("UI Root/Panel/LeftButton2");
		UIEventListener.Get(leftButton2).onClick = LeftButton2OnClick;

		GameObject rightButton1 = GameObject.Find("UI Root/Panel/RightButton1");
		UIEventListener.Get(rightButton1).onClick = RightButton1OnClick;

		GameObject rightButton2 = GameObject.Find("UI Root/Panel/RightButton2");
		UIEventListener.Get(rightButton2).onClick = RightButton2OnClick;

		GameObject leaderButton1 = GameObject.Find("UI Root/Panel/LeaderButton1");
		UIEventListener.Get(leaderButton1).onClick = LeaderButton1OnClick;

		GameObject leaderButton2 = GameObject.Find("UI Root/Panel/LeaderButton2");
		UIEventListener.Get(leaderButton2).onClick = LeaderButton2OnClick;

		GameObject leaderButton3 = GameObject.Find("UI Root/Panel/LeaderButton3");
		UIEventListener.Get(leaderButton3).onClick = LeaderButton3OnClick;

		GameObject leaderButtonACE = GameObject.Find("UI Root/Panel/LeaderButtonACE");
		UIEventListener.Get(leaderButtonACE).onClick = LeaderButtonACEOnClick;
	}

	private void LeftButton1OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void LeftButton2OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void RightButton1OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void RightButton2OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void LeaderButton1OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void LeaderButton2OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void LeaderButton3OnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

	private void LeaderButtonACEOnClick(GameObject button)
	{
		Debug.Log("NGUI button name :" + button.name);
	}

}
