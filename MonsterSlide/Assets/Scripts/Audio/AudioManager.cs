using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Audio鳴らす基底クラス
/// </summary>
public class AudioManager : SingletonMonoBehavior<AudioManager>{

	/// <summary>
	/// BGMリスト
	/// </summary>
	public List<AudioClip> audioList;

	/// <summary>
	/// Audiosourceリスト
	/// </summary>
	private List<AudioSource> audioSources;

	/// <summary>
	/// AudioClipDictionary
	/// </summary>
	private Dictionary<string, AudioClip> audioDict = null;

	/// <summary>
	/// AudioSourceDictionary
	/// </summary>
	private Dictionary<string, AudioSource> sourceDict = null;

	public void Awake()
	{
		DontDestroyOnLoad(this);

		if (this != Instance) { Destroy(this); return; }
		DontDestroyOnLoad(this.gameObject);

		if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled)) { this.gameObject.AddComponent<AudioListener>(); }
		audioSources = new List<AudioSource>();
		audioDict = new Dictionary<string, AudioClip>();
		sourceDict = new Dictionary<string, AudioSource>();
		Action<Dictionary<string, AudioClip>, AudioClip> addClipdict = (dict, c) =>
		{
			if (!dict.ContainsKey(c.name)) { dict.Add(c.name, c); }
		};
		audioList.ForEach(audio => addClipdict(audioDict, audio));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Audio名に対応した音声を鳴らす
	/// </summary>
	/// <param name="audioName"></param>
	public void PlayAudio(string audioName)
	{
		// 使われていないAudioSourceがあれば使うし，なければ追加して鳴らす
		if (!audioDict.ContainsKey(audioName)) { return; }
		AudioSource source = audioSources.FirstOrDefault(s => !s.isPlaying);
		if (source == null)
		{
			source = gameObject.AddComponent<AudioSource>();
			audioSources.Add(source);
		}
		source.clip = audioDict[audioName];
		source.Play();
		if (!sourceDict.ContainsKey(audioName)) { sourceDict.Add(audioName, source); }
	}

	/// <summary>
	/// Audio名に対応したaudioclipを取得する
	/// </summary>
	/// <param name="audioName"></param>
	/// <returns></returns>
	public AudioSource GetAudioClip(string audioName) { return sourceDict[audioName]; }

	/// <summary>
	/// Audioを止める
	/// </summary>
	public void StopAudio() { audioSources.ForEach(s => s.Stop()); }
}
