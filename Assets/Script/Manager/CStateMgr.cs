using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public enum CStateType
{
	S_NONE = 0,
	S_STAND,
	S_SKILL,
	S_ATTACKED,
	S_SMALL_FLOAT,
	S_BIG_FLOAT,
	S_LAID
};

public class CStateMgr
{

	private CLifeObj m_life = null;
	private CState m_lastState = null;
	private CState m_state = null;
	private Dictionary<int, CState> m_mapState = new Dictionary<int, CState>();

	public CStateMgr(CLifeObj life)
	{
		m_life = life;
	}

	public CLifeObj role
	{
		get { return m_life; }
	}

	public int StateType
	{
		get { return m_state != null ? m_state.type : 0; }
	}

	public int LastStateType
	{
		get { return m_lastState != null ? m_lastState.type : (int)CStateType.S_NONE; }
	}

	public CState state
	{
		get { return m_state; }
	}

	public void AddState(CState state)
	{
		m_mapState.Add(state.type, state);
	}

	public CState getStateByType(int type)
	{
		CState state = null;
		if (m_mapState.TryGetValue(type, out state))
		{
			return state;
		}
		return null;
	}

	public void Update()
	{
		if (m_state != null)
		{
			m_state.Update();
		}
	}

	public bool ChangeToState(int StateType)
	{
		CState state = null;
		if (!m_mapState.TryGetValue(StateType, out state))
		{
			return false;
		}

		if (m_state != null)
		{
			m_state.AfterAction();
		}
		m_lastState = m_state;
		m_state = state;
		if (m_state.hash != 0)
		{
			m_life.AnmationMgr.Play(m_state.hash);
		}
		m_state.PreAction();
		return true;
	}

}
