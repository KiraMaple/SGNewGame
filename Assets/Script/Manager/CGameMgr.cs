using UnityEngine;
using System.Collections;

public class CGameMgr {

	private static CGameMgr m_instance = null;

	public static CGameMgr GetInstance()
	{
		if (m_instance == null)
		{
			m_instance = new CGameMgr();
		}
		return m_instance;
	}

	private int m_nPlayerCnt = 0;
	private int m_nEnemyCnt = 0;
	private CLifeObj[] m_playerTeam = null;
	private CLifeObj[] m_enemyTeam = null;

	private CGameMgr()
	{
		CreateLifeObj();
	}

	public void GetUISize(out int width, out int height)
	{
		UIRoot root = GameObject.FindObjectOfType<UIRoot>();
		if (root != null)
		{
			float s = (float)root.activeHeight / Screen.height;
			height =  Mathf.CeilToInt(Screen.height * s);
			width = Mathf.CeilToInt(Screen.width * s);
		}
		else
		{
			width = 0;
			height = 0;
		}
	}

	private void CreateLifeObj()
	{
		m_nPlayerCnt = 1;
		m_playerTeam = new CLifeObj[1];
		m_playerTeam[0] = Resources.Load("Element/Character/PlayerLeader") as CLifeObj;
		m_playerTeam[0].LoadFromFile("Lua/Character/saber.lua");
		m_playerTeam[0].SetTeamType(TeamType.TT_PLAYER, TeamPos.TP_LEADER);

		m_nEnemyCnt = 1;
		m_enemyTeam = new CLifeObj[1];
		m_enemyTeam[0] = Resources.Load("Element/Character/PlayerLeader") as CLifeObj;
		m_enemyTeam[0].LoadFromFile("Lua/Character/axer.lua");
		m_enemyTeam[0].SetTeamType(TeamType.TT_ENEMY, TeamPos.TP_LEADER);
	}

	public int PlayerCnt { get { return m_nPlayerCnt; } }
	public int EnemyCnt { get { return m_nEnemyCnt; } }
	public CLifeObj GetPlayerByIndex(int index)
	{
		if (index < m_nPlayerCnt)
		{
			return m_playerTeam[index];
		}
		return null;
	}

	public CLifeObj GetEnemyByIndex(int index)
	{
		if (index < m_nEnemyCnt)
		{
			return m_enemyTeam[index];
		}
		return null;
	}

}
