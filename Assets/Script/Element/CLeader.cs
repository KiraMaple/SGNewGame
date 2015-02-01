using UnityEngine;
using System.Collections;

public class CLeader : CLifeObj
{

	public CLeader()
		: base()
	{
		
	}

	// Use this for initialization
	protected override void Start()
	{
		base.Start();
		LoadFromFile("Lua/saber.lua");
	}
	
	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}
}
