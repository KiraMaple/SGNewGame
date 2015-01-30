using UnityEngine;
using System.Collections;

public class CLifeObj : MonoBehaviour {

	protected CAnimationMgr m_animationMgr = null;
	protected CStateMgr m_stateMgr = null;

	public CAnimationMgr AnmationMgr { get { return m_animationMgr; } }
	public CStateMgr StateMgr { get { return m_stateMgr; } }

	// Use this for initialization
	protected virtual void Start ()
	{
		m_animationMgr = new CAnimationMgr(this);
		m_stateMgr = new CStateMgr(this);
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		m_stateMgr.Update();
	}
}
