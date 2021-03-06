﻿using System;
using UnityEngine;

// Token: 0x0200010B RID: 267
[ExecuteInEditMode]
[AddComponentMenu("Camera Filter Pack/AAA/Blood_Hit")]
public class CameraFilterPack_AAA_Blood_Hit : MonoBehaviour
{
	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00060E74 File Offset: 0x0005F074
	private Material material
	{
		get
		{
			if (this.SCMaterial == null)
			{
				this.SCMaterial = new Material(this.SCShader);
				this.SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.SCMaterial;
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00060EA8 File Offset: 0x0005F0A8
	private void Start()
	{
		this.Texture2 = (Resources.Load("CameraFilterPack_AAA_Blood_Hit1") as Texture2D);
		this.SCShader = Shader.Find("CameraFilterPack/AAA_Blood_Hit");
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00060EE0 File Offset: 0x0005F0E0
	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (this.SCShader != null)
		{
			this.TimeX += Time.deltaTime;
			if (this.TimeX > 100f)
			{
				this.TimeX = 0f;
			}
			this.material.SetFloat("_TimeX", this.TimeX);
			this.material.SetFloat("_Value", this.LightReflect);
			this.material.SetFloat("_Value2", Mathf.Clamp(this.Hit_Left, 0f, 1f));
			this.material.SetFloat("_Value3", Mathf.Clamp(this.Hit_Up, 0f, 1f));
			this.material.SetFloat("_Value4", Mathf.Clamp(this.Hit_Right, 0f, 1f));
			this.material.SetFloat("_Value5", Mathf.Clamp(this.Hit_Down, 0f, 1f));
			this.material.SetFloat("_Value6", Mathf.Clamp(this.Blood_Hit_Left, 0f, 1f));
			this.material.SetFloat("_Value7", Mathf.Clamp(this.Blood_Hit_Up, 0f, 1f));
			this.material.SetFloat("_Value8", Mathf.Clamp(this.Blood_Hit_Right, 0f, 1f));
			this.material.SetFloat("_Value9", Mathf.Clamp(this.Blood_Hit_Down, 0f, 1f));
			this.material.SetFloat("_Value10", Mathf.Clamp(this.Hit_Full, 0f, 1f));
			this.material.SetFloat("_Value11", Mathf.Clamp(this.Blood_Hit_Full_1, 0f, 1f));
			this.material.SetFloat("_Value12", Mathf.Clamp(this.Blood_Hit_Full_2, 0f, 1f));
			this.material.SetFloat("_Value13", Mathf.Clamp(this.Blood_Hit_Full_3, 0f, 1f));
			this.material.SetTexture("_MainTex2", this.Texture2);
			Graphics.Blit(sourceTexture, destTexture, this.material);
			return;
		}
		Graphics.Blit(sourceTexture, destTexture);
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00002ACE File Offset: 0x00000CCE
	private void Update()
	{
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x0006113B File Offset: 0x0005F33B
	private void OnDisable()
	{
		if (this.SCMaterial)
		{
			UnityEngine.Object.DestroyImmediate(this.SCMaterial);
		}
	}

	// Token: 0x04000D82 RID: 3458
	public Shader SCShader;

	// Token: 0x04000D83 RID: 3459
	private float TimeX = 1f;

	// Token: 0x04000D84 RID: 3460
	[Range(0f, 1f)]
	public float Hit_Left = 1f;

	// Token: 0x04000D85 RID: 3461
	[Range(0f, 1f)]
	public float Hit_Up;

	// Token: 0x04000D86 RID: 3462
	[Range(0f, 1f)]
	public float Hit_Right;

	// Token: 0x04000D87 RID: 3463
	[Range(0f, 1f)]
	public float Hit_Down;

	// Token: 0x04000D88 RID: 3464
	[Range(0f, 1f)]
	public float Blood_Hit_Left;

	// Token: 0x04000D89 RID: 3465
	[Range(0f, 1f)]
	public float Blood_Hit_Up;

	// Token: 0x04000D8A RID: 3466
	[Range(0f, 1f)]
	public float Blood_Hit_Right;

	// Token: 0x04000D8B RID: 3467
	[Range(0f, 1f)]
	public float Blood_Hit_Down;

	// Token: 0x04000D8C RID: 3468
	[Range(0f, 1f)]
	public float Hit_Full;

	// Token: 0x04000D8D RID: 3469
	[Range(0f, 1f)]
	public float Blood_Hit_Full_1;

	// Token: 0x04000D8E RID: 3470
	[Range(0f, 1f)]
	public float Blood_Hit_Full_2;

	// Token: 0x04000D8F RID: 3471
	[Range(0f, 1f)]
	public float Blood_Hit_Full_3;

	// Token: 0x04000D90 RID: 3472
	[Range(0f, 1f)]
	public float LightReflect = 0.5f;

	// Token: 0x04000D91 RID: 3473
	private Material SCMaterial;

	// Token: 0x04000D92 RID: 3474
	private Texture2D Texture2;
}
