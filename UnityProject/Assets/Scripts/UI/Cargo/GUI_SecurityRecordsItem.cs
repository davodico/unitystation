﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SecurityRecords item is used inside EntriesPage.
/// It's elements are hardcoded strings so don't rename anything inside gameobject.
/// </summary>
public class GUI_SecurityRecordsItem : DynamicEntry
{
	private GUI_SecurityRecords securityRecordsTab;
	private SecurityRecord securityRecord;

	public void ReInit(SecurityRecord record, GUI_SecurityRecords recordsTab)
	{
		if (record == null)
		{
			Logger.Log("SecurityRecordItem: no record found, not doing init", Category.NetUI);
			return;
		}
		securityRecord = record;
		securityRecordsTab = recordsTab;
		foreach ( var element in Elements )
		{
			string nameBeforeIndex = element.name.Split('~')[0];
			switch (nameBeforeIndex)
			{
				case "RecordNameText":
					element.SetValue = record.EntryName;
					break;
				case "RecordIDText":
					element.SetValue = record.ID;
					break;
				case "RecordRankText":
					element.SetValue = record.Rank;
					break;
				case "RecordFingerprintsText":
					element.SetValue = record.Fingerprints;
					break;
				case "RecordStatusText":
					element.SetValue = record.Status.ToString();
					break;
				case "RecordBG":
					element.SetValue = GetStatusColor(record.Status);
					break;
			}
		}
	}

	private string GetStatusColor(SecurityStatus status)
	{
		switch (status)
		{
			case SecurityStatus.None:
				return "424142";
			case SecurityStatus.Arrest:
				return "C10000";
			case SecurityStatus.Parole:
				return "F57211";
		}
		return ("424142");
	}

	public void OpenRecord()
	{
		securityRecordsTab.OpenRecord(securityRecord);
	}
}
