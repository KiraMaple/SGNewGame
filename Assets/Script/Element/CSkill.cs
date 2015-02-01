using UnityEngine;
using System;
using System.Collections;
using LuaInterface;

enum SkillDisType
{
	SDT_NEAR,
	SDT_FAR
}

enum SkillRangeType
{
	SRT_SIMPLE,
	SRT_GROUP
}

enum SkillHurtType
{
	SHT_HURT,
	SHT_TREAT,
	SHT_BUFF
}

public class CSkill {

	private SkillDisType m_disType;
	private SkillRangeType m_rangeType;
	private SkillHurtType m_hurtType;
	private int m_nCDGroup = 0;
	private int m_nDamage = 0;
	private int m_nCostSP = 0;
	private int m_hash1 = 0;
	private int m_hash2 = 0;
	private CStateType m_checkType = CStateType.S_NONE;
	private CStateType m_afterType = CStateType.S_NONE;

	private CLifeObj m_life = null;
	private CDTimer m_timer = null;

	public CSkill(CLifeObj life)
	{
		if (life == null)
		{
			Debug.LogError("LifeObj of Skill is null.");
			return;
		}
		m_life = life;
	}

	public bool LoadFromLua(LuaTable skill)
	{
		if (skill["DisType"] == null || skill["DisType"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("DisType of Skill is invalid." + skill["DisType"]);
			return false;
		}
		int type = Convert.ToInt32((double)skill["DisType"]);
		m_disType = (SkillDisType)type;

		if (skill["RangeType"] == null || skill["RangeType"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("RangeType of Skill is invalid.");
			return false;
		}
		type = Convert.ToInt32((double)skill["RangeType"]);
		m_rangeType = (SkillRangeType)type;

		if (skill["HurtType"] == null || skill["HurtType"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("HurtType of Skill is invalid.");
			return false;
		}
		type = Convert.ToInt32((double)skill["HurtType"]);
		m_hurtType = (SkillHurtType)type;

		if (skill["CDGroup"] == null || skill["CDGroup"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("CDGroup of Skill is invalid.");
			return false;
		}
		int id = Convert.ToInt32((double)skill["CDGroup"]);
		m_nCDGroup = id;

		if (skill["Damage"] == null || skill["Damage"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("Damage of Skill is invalid.");
			return false;
		}
		int damage = Convert.ToInt32((double)skill["Damage"]);
		m_nDamage = damage;

		if (skill["CostSP"] == null || skill["CostSP"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("CostSP of Skill is invalid.");
			return false;
		}
		int costSP = Convert.ToInt32((double)skill["CostSP"]);
		m_nCostSP = costSP;

		string animName1 = (string)skill["animation"];
		if (animName1 != null && animName1 != "")
		{
			m_hash1 = Animator.StringToHash(animName1);
		}

		string animName2 = (string)skill["animation"];
		if (animName2 != null && animName2 != "")
		{
			m_hash2 = Animator.StringToHash(animName2);
		}

		if (skill["CheckType"] == null || skill["CheckType"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("CheckType of Skill is invalid.");
			return false;
		}
		type = Convert.ToInt32((double)skill["CheckType"]);
		m_checkType = (CStateType)type;

		if (skill["AfterType"] == null || skill["AfterType"].GetType().ToString() != "System.Double")
		{
			Debug.LogError("AfterType of Skill is invalid.");
			return false;
		}
		type = Convert.ToInt32((double)skill["AfterType"]);
		m_afterType = (CStateType)type;

		m_timer = m_life.GetCDTimerById(m_nCDGroup);
		if (m_timer == null)
		{
			Debug.LogError("Failed to LoadFromFile. CDTimer doesn't exist.");
			return false;
		}

		return true;
	}

	public bool IsCD()
	{
		return m_timer.IsCD();
	}
}

public class CDTimer
{
	private int m_groupId = 0;
	private float m_fCD;
	private float m_deltaTime = 0.0f;

	public CDTimer(int id, float CD)
	{
		m_groupId = id;
		m_fCD = CD;
	}

	public void UpdateCD()
	{
		if (m_deltaTime < m_fCD)
		{
			m_deltaTime += Time.deltaTime;
		}
	}

	public bool IsCD()
	{
		if (m_deltaTime >= m_fCD)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}