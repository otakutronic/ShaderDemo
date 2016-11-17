using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OldEffect : MonoBehaviour 
{
	
	#region Variables
	public Shader oldShader;
	
	public float OldEffectAmount = 1.0f;
	public float contrast = 3.0f;
	public float distortion = 0.2f;
	public float cubicDistortion = 0.6f;
	public float scale = 0.8f;
	
	public Color sepiaColor = Color.white;
	public Texture2D stainTexture;
	public float stainAmount = 1.0f;
	
	public Texture2D scratchesTexture;
	public float scratchesYSpeed = 10.0f;
	public float scratchesXSpeed = 10.0f;
	
	public Texture2D dustTexture;
	public float dustYSpeed = 10.0f;
	public float dustXSpeed = 10.0f;
	
	private Material curMaterial;
	private float randomValue;
	#endregion
	
	#region Properties
	Material material
	{
		get
		{
			if(curMaterial == null)
			{
				curMaterial = new Material(oldShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}
	#endregion
	
	void Start()
	{
		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		
		if(!oldShader && !oldShader.isSupported)
		{
			enabled = false;
		}
	}
	
	void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(oldShader != null)
		{	
			material.SetColor("_SepiaColor", sepiaColor);
			material.SetFloat("_StainAmount", stainAmount);
			material.SetFloat("_EffectAmount", OldEffectAmount);
			material.SetFloat("_Contrast", contrast);
			material.SetFloat("_cubicDistortion", cubicDistortion);
			material.SetFloat("_distortion", distortion);
			material.SetFloat("_scale",scale);
			
			if(stainTexture)
			{
				material.SetTexture("_StainTex", stainTexture);
			}
			
			if(scratchesTexture)
			{
				material.SetTexture("_ScratchesTex", scratchesTexture);
				material.SetFloat("_ScratchesYSpeed", scratchesYSpeed);
				material.SetFloat("_ScratchesXSpeed", scratchesXSpeed);
			}
			
			if(dustTexture)
			{
				material.SetTexture("_DustTex", dustTexture);
				material.SetFloat("_dustYSpeed", dustYSpeed);
				material.SetFloat("_dustXSpeed", dustXSpeed);
				material.SetFloat("_RandomValue", randomValue);
			}
			
			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
	
	void Update()
	{
		stainAmount = Mathf.Clamp01(stainAmount);
		OldEffectAmount = Mathf.Clamp(OldEffectAmount, 0f, 1.5f);
		randomValue = Random.Range(-1f,1f);
		contrast = Mathf.Clamp(contrast, 0f, 4f);
		distortion = Mathf.Clamp(distortion, -1f,1f);
		cubicDistortion = Mathf.Clamp(cubicDistortion, -1f, 1f);
		scale = Mathf.Clamp(scale, 0f, 1f);
	}
	
	void OnDisable()
	{
		if(curMaterial)
		{
			DestroyImmediate(curMaterial);
		}
	}
}
