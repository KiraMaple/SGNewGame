using UnityEngine;
using System.Collections;

public class CLifeObj : MonoBehaviour {

	public enum TeamType
	{
		TT_NONE,
		TT_PLAYER,
		TT_ENEMY
	}


	protected TeamType m_teamId = TeamType.TT_NONE;
	protected int m_nHP = 0;
	protected int m_nSP = 0;
	protected int m_nHPMax = 0;
	protected int m_nSPMax = 0;
	protected int m_nSPSpeed = 0;
	protected int m_nStrength = 0;
	protected int m_nDefense = 0;
	//CSkill[] m_AttackList = null;
	//CSKill[] m_SkillList = null;

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

	protected virtual void UpdateState()
	{

	}

	protected virtual void UpdateAttack()
	{

	}

	protected virtual void UpdateSkill()
	{

	}

	protected virtual void UpdateHurt()
	{

	}

}
