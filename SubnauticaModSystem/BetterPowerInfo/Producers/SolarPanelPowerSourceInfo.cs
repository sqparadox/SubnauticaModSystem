﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace BetterPowerInfo.Producers
{
	class SolarPanelPowerSourceInfo : PowerSourceInfoBase
	{
		protected static MethodInfo SolarPanel_GetRechargeScalar;

		private SolarPanel panel;

		public SolarPanelPowerSourceInfo(PowerSource source) : base(source)
		{
			panel = source.gameObject.GetComponent<SolarPanel>();
		}

		protected override float GetPowerProductionPerMinute()
		{
			float rechargeScalar = GetRechargeScalar();
			return rechargeScalar * 0.25f * 5.0f * 60;
		}

		protected override string GetPowerSourceDisplayText()
		{
			string name = base.GetPowerSourceDisplayText();
			float rechargeScalar = GetRechargeScalar();
			return string.Format("{0} ({1}%)", name, Mathf.RoundToInt(rechargeScalar * 100));
		}

		protected float GetRechargeScalar()
		{
			if (SolarPanel_GetRechargeScalar == null)
			{
				SolarPanel_GetRechargeScalar = typeof(SolarPanel).GetMethod("GetRechargeScalar", BindingFlags.NonPublic | BindingFlags.Instance);
			}

			return (float)SolarPanel_GetRechargeScalar.Invoke(panel, new object[] { });
		}
	}
}
