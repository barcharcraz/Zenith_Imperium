using System;
using Units;
using Units.Infantry;

namespace Units.Buildings
{
	public class Barracks : UnitInfo
	{
		public Barracks ()
		{
			Name = "Barracks";
			Cost.Food = 100;
			Cost.Stone = 50;
			Prefab = UnityEngine.Resources.Load("Buildings/prim_barracks") as UnityEngine.GameObject;
			UnitCommands.Add(new Commands.ProduceUnit<Warrior>());
			Speed = 0;
		}
	}
}

