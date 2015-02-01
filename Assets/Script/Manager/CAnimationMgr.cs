using UnityEngine;
using System.Collections;

public class CAnimationMgr {

	private CLifeObj m_life = null;
	private Animator m_animator = null;

	public CAnimationMgr(CLifeObj life)
	{
		if (life == null)
		{
			Debug.LogError("LifeObj is invalid. Role:" + m_life.transform.name);
			return;
		}
		m_life = life;

		//m_animator = m_life.transform.Find("Animation").GetComponent<Animator>();
		//if (m_animator == null)
		//{
		//	Debug.LogError("Role Animator is not found. Role:" + m_life.transform.name);
		//	return;
		//}
	}

	public void Play(string name)
	{
		m_animator.Play(name);
	}

	public void Play(int hash)
	{
		m_animator.Play(hash);
	}

	public bool isPlaying
	{
		get
		{
			AnimatorStateInfo info = m_animator.GetCurrentAnimatorStateInfo(0);
			if (info.normalizedTime >= 1.0f)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
	}

}