using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

public enum TeamType
{
	TT_PLAYER,
	TT_ENEMY
}

public enum TeamPos
{
	TP_LEADER,
	TP_LEFT,
	TP_RIGHT
}

public class CLifeObj : MonoBehaviour
{
	protected string m_szName = "";
	protected int m_nHPMax = 0;
	protected int m_nSPMax = 0;
	protected float m_fSPSpeed = 0;
	protected int m_nDefense = 0;

	protected string m_path = "";
	protected TeamType m_team = TeamType.TT_PLAYER;
	protected TeamPos m_pos = TeamPos.TP_LEADER;
	protected int m_nHP = 0;
	protected int m_nSP = 0;
	protected Dictionary<int, CDTimer> m_CDTimer = new Dictionary<int,CDTimer>();
	CSkill[] m_SkillList = null;

	protected CAnimationMgr m_animationMgr = null;
	protected CStateMgr m_stateMgr = null;

	public CAnimationMgr AnmationMgr { get { return m_animationMgr; } }
	public CStateMgr StateMgr { get { return m_stateMgr; } }

	public CLifeObj()
	{
	}

	// Use this for initialization
	protected virtual void Start ()
	{
		m_animationMgr = new CAnimationMgr(this);
		m_stateMgr = new CStateMgr(this);
	}

	public bool LoadFromFile(string file)
	{
		LuaState ls = new LuaState();
		TextAsset luaContent = (TextAsset)Resources.Load(file);
		if (luaContent == null)
		{
			Debug.LogError("LifeObj Init Error. Failed to Load File. File:" + file);
			return false;
		}
		ls.DoString(luaContent.text);
		m_path = file;

		LuaTable role = (LuaTable)ls["Role"];
		if (role == null)
		{
			Debug.LogError("LifeObj Init Error. Can not find LifeObj Info in lua. File:" + m_path);
			return false;
		}

		if (role["Name"] == null || role["Name"].GetType().ToString() != "System.String")
		{
			Debug.LogError("LifeObj Init Error. Can not find name in LifeObj Info. File:" + m_path);
			return false;
		}
		string name = (string)role["Name"];
		m_szName = name;

		if (role["HP"] == null || role["HP"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("LifeObj Init Error. Can not find HP in LifeObj Info. File:" + m_path);
			return false;
		}
		int HPMax = Convert.ToInt32((double)role["HP"]);
		m_nHPMax = HPMax;

		if (role["SP"] == null || role["SP"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("LifeObj Init Error. Can not find SP in LifeObj Info. File:" + m_path);
			return false;
		}
		int SPMax = Convert.ToInt32((double)role["SP"]);
		m_nSPMax = SPMax;

		if (role["SPSpeed"] == null || role["SPSpeed"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("LifeObj Init Error. Can not find SPSpeed in LifeObj Info. File:" + m_path);
			return false;
		}
		double SPSpeed = (double)role["SPSpeed"];
		m_fSPSpeed = (float)SPSpeed;

		if (role["Defense"] == null || role["Defense"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("LifeObj Init Error. Can not find defense in LifeObj Info. File:" + m_path);
			return false;
		}
		int defense = Convert.ToInt32((double)role["Defense"]);
		m_nDefense = defense;

		LuaTable CDTimers = (LuaTable)role["CDTimers"];
		if (CDTimers == null || CDTimers.Values.Count == 0)
		{
			Debug.LogError("LifeObj Init Error. Can not find CDTimers in LifeObj Info. File:" + m_path);
			return false;
		}
		int nTimerCnt = CDTimers.Values.Count;
		for (int i = 0; i < nTimerCnt; i++)
		{
			LuaTable timerInfo = (LuaTable)CDTimers[i + 1];
			if (timerInfo == null || timerInfo.Values.Count != 2 || 
				timerInfo["id"] == null || timerInfo["id"].GetType().ToString() != "System.Double" ||
				timerInfo["CD"] == null || timerInfo["CD"].GetType().ToString() != "System.Double")
			{
				Debug.LogError("LifeObj Init Error. TimerInfo[" + i + "] is invalid. File:" + m_path);
				return false;
			}

			int id = Convert.ToInt32((double)timerInfo["id"]);
			double CD = (double)timerInfo["CD"];

			if (id == 0)
			{
				Debug.LogError("LifeObj Init Error. Group ID of TimerInfo[" + i + "] is invalid. File:" + m_path);
				return false;
			}

			CDTimer timer = new CDTimer(id, (float)CD);
			m_CDTimer.Add(id, timer);
		}

		LuaTable SkillsTable = (LuaTable)role["Skill"];
		if (SkillsTable == null || SkillsTable.Values.Count == 0)
		{
			Debug.LogError("LifeObj Init Error. Can not find Skill in LifeObj Info. File:" + m_path);
			return false;
		}
		int nSkillCnt = SkillsTable.Values.Count;
		m_SkillList = new CSkill[nSkillCnt];
		for (int i = 0; i < nSkillCnt; i++)
		{
			LuaTable SkillTable = (LuaTable)SkillsTable[i + 1];
			if (SkillTable == null)
			{
				Debug.LogError("Skill Info is null. Index:" + (i+1) + ", File:" + m_path);
				return false;
			}

			m_SkillList[i] = new CSkill(this);
			if ( m_SkillList[i].LoadFromLua(SkillTable) == false )
			{
				Debug.LogError("Skill Info is invalid. Index:" + (i + 1) + ", File:" + m_path);
				return false;
			}
		}

		return true;
	}
	
	// Update is called once per frame
	protected virtual void Update()
	{
		//m_stateMgr.Update();
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

	public CDTimer GetCDTimerById(int groupId)
	{
		CDTimer timer = null;
		if ( m_CDTimer.TryGetValue(groupId, out timer) )
		{
			return timer;
		}
		return null;
	}

	public void SetTeamType(TeamType type, TeamPos pos)
	{
		m_team = type;
		m_pos = pos;

		int cx = 75;
		int cz = 20;
		int leaderX = -5;
		int leaderZ = 0;
		int leftX = -10;
		int leftZ = 5;
		int rightX = -10;
		int rightZ = -5;

		Vector3 scale = transform.localScale;
		Vector3 position = transform.position;
		if (type == TeamType.TT_PLAYER)
		{
			scale.x = Mathf.Abs(scale.x);
			switch (pos)
			{
				case TeamPos.TP_LEADER:
					{
						position.x = cx + leaderX;
						position.z = cz + leaderZ;
					}
					break;
				case TeamPos.TP_LEFT:
					{
						position.x = cx + leftX;
						position.z = cz + leftZ;
					}
					break;
				case TeamPos.TP_RIGHT:
					{
						position.x = cx + rightX;
						position.z = cz + rightZ;
					}
					break;
				default:
					break;
			}
		}
		else if (type == TeamType.TT_ENEMY)
		{
			scale.x = -Mathf.Abs(scale.x);
			switch (pos)
			{
				case TeamPos.TP_LEADER:
					{
						position.x = cx - leaderX;
						position.z = cz - leaderZ;
					}
					break;
				case TeamPos.TP_LEFT:
					{
						position.x = cx - leftX;
						position.z = cz - leftZ;
					}
					break;
				case TeamPos.TP_RIGHT:
					{
						position.x = cx - rightX;
						position.z = cz - rightZ;
					}
					break;
				default:
					break;
			}
		}
		transform.localScale = scale;
		transform.position = position;
	}

}
