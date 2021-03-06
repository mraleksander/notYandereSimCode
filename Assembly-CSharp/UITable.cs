﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
[AddComponentMenu("NGUI/Interaction/Table")]
public class UITable : UIWidgetContainer
{
	// Token: 0x17000051 RID: 81
	// (set) Token: 0x06000313 RID: 787 RVA: 0x0001E655 File Offset: 0x0001C855
	public bool repositionNow
	{
		set
		{
			if (value)
			{
				this.mReposition = true;
				base.enabled = true;
			}
		}
	}

	// Token: 0x06000314 RID: 788 RVA: 0x0001E668 File Offset: 0x0001C868
	public List<Transform> GetChildList()
	{
		Transform transform = base.transform;
		List<Transform> list = new List<Transform>();
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			if (!this.hideInactive || (child && NGUITools.GetActive(child.gameObject)))
			{
				list.Add(child);
			}
		}
		if (this.sorting != UITable.Sorting.None)
		{
			if (this.sorting == UITable.Sorting.Alphabetic)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortByName));
			}
			else if (this.sorting == UITable.Sorting.Horizontal)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortHorizontal));
			}
			else if (this.sorting == UITable.Sorting.Vertical)
			{
				list.Sort(new Comparison<Transform>(UIGrid.SortVertical));
			}
			else if (this.onCustomSort != null)
			{
				list.Sort(this.onCustomSort);
			}
			else
			{
				this.Sort(list);
			}
		}
		return list;
	}

	// Token: 0x06000315 RID: 789 RVA: 0x0001E73C File Offset: 0x0001C93C
	protected virtual void Sort(List<Transform> list)
	{
		list.Sort(new Comparison<Transform>(UIGrid.SortByName));
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0001E750 File Offset: 0x0001C950
	protected virtual void Start()
	{
		this.Init();
		this.Reposition();
		base.enabled = false;
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0001E765 File Offset: 0x0001C965
	protected virtual void Init()
	{
		this.mInitDone = true;
		this.mPanel = NGUITools.FindInParents<UIPanel>(base.gameObject);
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0001E77F File Offset: 0x0001C97F
	protected virtual void LateUpdate()
	{
		if (this.mReposition)
		{
			this.Reposition();
		}
		base.enabled = false;
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0001E796 File Offset: 0x0001C996
	private void OnValidate()
	{
		if (!Application.isPlaying && NGUITools.GetActive(this))
		{
			this.Reposition();
		}
	}

	// Token: 0x0600031A RID: 794 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
	protected void RepositionVariableSize(List<Transform> children)
	{
		float num = 0f;
		float num2 = 0f;
		object obj = (this.columns > 0) ? (children.Count / this.columns + 1) : 1;
		int num3 = (this.columns > 0) ? this.columns : children.Count;
		object obj2 = obj;
		Bounds[,] array = new Bounds[obj2, num3];
		Bounds[] array2 = new Bounds[num3];
		Bounds[] array3 = new Bounds[obj2];
		int num4 = 0;
		int num5 = 0;
		int i = 0;
		int count = children.Count;
		while (i < count)
		{
			Transform transform = children[i];
			Bounds bounds = NGUIMath.CalculateRelativeWidgetBounds(transform, !this.hideInactive);
			Vector3 localScale = transform.localScale;
			bounds.min = Vector3.Scale(bounds.min, localScale);
			bounds.max = Vector3.Scale(bounds.max, localScale);
			array[num5, num4] = bounds;
			array2[num4].Encapsulate(bounds);
			array3[num5].Encapsulate(bounds);
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
			}
			i++;
		}
		num4 = 0;
		num5 = 0;
		Vector2 pivotOffset = NGUIMath.GetPivotOffset(this.cellAlignment);
		int j = 0;
		int count2 = children.Count;
		while (j < count2)
		{
			Transform transform2 = children[j];
			Bounds bounds2 = array[num5, num4];
			Bounds bounds3 = array2[num4];
			Bounds bounds4 = array3[num5];
			Vector3 localPosition = transform2.localPosition;
			localPosition.x = num + bounds2.extents.x - bounds2.center.x;
			localPosition.x -= Mathf.Lerp(0f, bounds2.max.x - bounds2.min.x - bounds3.max.x + bounds3.min.x, pivotOffset.x) - this.padding.x;
			if (this.direction == UITable.Direction.Down)
			{
				localPosition.y = -num2 - bounds2.extents.y - bounds2.center.y;
				localPosition.y += Mathf.Lerp(bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, 0f, pivotOffset.y) - this.padding.y;
			}
			else
			{
				localPosition.y = num2 + bounds2.extents.y - bounds2.center.y;
				localPosition.y -= Mathf.Lerp(0f, bounds2.max.y - bounds2.min.y - bounds4.max.y + bounds4.min.y, pivotOffset.y) - this.padding.y;
			}
			num += bounds3.size.x + this.padding.x * 2f;
			transform2.localPosition = localPosition;
			if (++num4 >= this.columns && this.columns > 0)
			{
				num4 = 0;
				num5++;
				num = 0f;
				num2 += bounds4.size.y + this.padding.y * 2f;
			}
			j++;
		}
		if (this.pivot != UIWidget.Pivot.TopLeft)
		{
			pivotOffset = NGUIMath.GetPivotOffset(this.pivot);
			Bounds bounds5 = NGUIMath.CalculateRelativeWidgetBounds(base.transform);
			float num6 = Mathf.Lerp(0f, bounds5.size.x, pivotOffset.x);
			float num7 = Mathf.Lerp(-bounds5.size.y, 0f, pivotOffset.y);
			Transform transform3 = base.transform;
			for (int k = 0; k < transform3.childCount; k++)
			{
				Transform child = transform3.GetChild(k);
				SpringPosition component = child.GetComponent<SpringPosition>();
				if (component != null)
				{
					component.enabled = false;
					SpringPosition springPosition = component;
					springPosition.target.x = springPosition.target.x - num6;
					SpringPosition springPosition2 = component;
					springPosition2.target.y = springPosition2.target.y - num7;
					component.enabled = true;
				}
				else
				{
					Vector3 localPosition2 = child.localPosition;
					localPosition2.x -= num6;
					localPosition2.y -= num7;
					child.localPosition = localPosition2;
				}
			}
		}
	}

	// Token: 0x0600031B RID: 795 RVA: 0x0001EC30 File Offset: 0x0001CE30
	[ContextMenu("Execute")]
	public virtual void Reposition()
	{
		if (Application.isPlaying && !this.mInitDone && NGUITools.GetActive(this))
		{
			this.Init();
		}
		this.mReposition = false;
		Transform transform = base.transform;
		List<Transform> childList = this.GetChildList();
		if (childList.Count > 0)
		{
			this.RepositionVariableSize(childList);
		}
		if (this.keepWithinPanel && this.mPanel != null)
		{
			this.mPanel.ConstrainTargetToBounds(transform, true);
			UIScrollView component = this.mPanel.GetComponent<UIScrollView>();
			if (component != null)
			{
				component.UpdateScrollbars(true);
			}
		}
		if (this.onReposition != null)
		{
			this.onReposition();
		}
	}

	// Token: 0x0400045A RID: 1114
	public int columns;

	// Token: 0x0400045B RID: 1115
	public UITable.Direction direction;

	// Token: 0x0400045C RID: 1116
	public UITable.Sorting sorting;

	// Token: 0x0400045D RID: 1117
	public UIWidget.Pivot pivot;

	// Token: 0x0400045E RID: 1118
	public UIWidget.Pivot cellAlignment;

	// Token: 0x0400045F RID: 1119
	public bool hideInactive = true;

	// Token: 0x04000460 RID: 1120
	public bool keepWithinPanel;

	// Token: 0x04000461 RID: 1121
	public Vector2 padding = Vector2.zero;

	// Token: 0x04000462 RID: 1122
	public UITable.OnReposition onReposition;

	// Token: 0x04000463 RID: 1123
	public Comparison<Transform> onCustomSort;

	// Token: 0x04000464 RID: 1124
	protected UIPanel mPanel;

	// Token: 0x04000465 RID: 1125
	protected bool mInitDone;

	// Token: 0x04000466 RID: 1126
	protected bool mReposition;

	// Token: 0x02000640 RID: 1600
	// (Invoke) Token: 0x06002ACD RID: 10957
	public delegate void OnReposition();

	// Token: 0x02000641 RID: 1601
	[DoNotObfuscateNGUI]
	public enum Direction
	{
		// Token: 0x040045C1 RID: 17857
		Down,
		// Token: 0x040045C2 RID: 17858
		Up
	}

	// Token: 0x02000642 RID: 1602
	[DoNotObfuscateNGUI]
	public enum Sorting
	{
		// Token: 0x040045C4 RID: 17860
		None,
		// Token: 0x040045C5 RID: 17861
		Alphabetic,
		// Token: 0x040045C6 RID: 17862
		Horizontal,
		// Token: 0x040045C7 RID: 17863
		Vertical,
		// Token: 0x040045C8 RID: 17864
		Custom
	}
}
