﻿using System;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class CardboardBoxScript : MonoBehaviour
{
	// Token: 0x06001221 RID: 4641 RVA: 0x000809A8 File Offset: 0x0007EBA8
	private void Start()
	{
		Physics.IgnoreCollision(this.Prompt.Yandere.GetComponent<Collider>(), base.GetComponent<Collider>());
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000809C8 File Offset: 0x0007EBC8
	private void Update()
	{
		if (this.Prompt.Circle[0].fillAmount == 0f)
		{
			this.Prompt.MyCollider.enabled = false;
			this.Prompt.Circle[0].fillAmount = 1f;
			base.GetComponent<Rigidbody>().isKinematic = true;
			base.GetComponent<Rigidbody>().useGravity = false;
			this.Prompt.enabled = false;
			this.Prompt.Hide();
			base.transform.parent = this.Prompt.Yandere.Hips;
			base.transform.localPosition = new Vector3(0f, -0.3f, 0.21f);
			base.transform.localEulerAngles = new Vector3(-13.333f, 0f, 0f);
		}
		if (base.transform.parent == this.Prompt.Yandere.Hips)
		{
			base.transform.localEulerAngles = Vector3.zero;
			if (this.Prompt.Yandere.Stance.Current != StanceType.Crawling)
			{
				base.transform.eulerAngles = new Vector3(0f, base.transform.eulerAngles.y, base.transform.eulerAngles.z);
			}
			if (Input.GetButtonDown("RB"))
			{
				this.Prompt.MyCollider.enabled = true;
				base.GetComponent<Rigidbody>().isKinematic = false;
				base.GetComponent<Rigidbody>().useGravity = true;
				base.transform.parent = null;
				this.Prompt.enabled = true;
			}
		}
	}

	// Token: 0x04001560 RID: 5472
	public PromptScript Prompt;
}
