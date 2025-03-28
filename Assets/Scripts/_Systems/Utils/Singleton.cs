//Copyright(C) 2025 Jo�o V�tor Demaria Ven�ncio under GNU AGPL. Refer to README.md for more information.
using UnityEngine;

/// <summary>
/// Abstract Singleton class to be inherited by any MonoBehaviour that should follow the Singleton pattern.
/// </summary>
/// <typeparam name="T">The type of the Singleton class inheriting from this base class.</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
	[Header("Singleton")]
	[SerializeField] protected bool _dontDestroyOnLoad = false; // Flag to determine if the Singleton instance should persist across scenes.
	private static T _instance; // Static instance of the Singleton.


	public static T Instance { get => _instance; } // Public accessor for the Singleton instance.



	private void Awake()
	{
		SetupSingleton();
	}



	/// <summary>
	/// Sets up the Singleton instance. Destroys the object if an instance already exists.
	/// </summary>
	protected void SetupSingleton()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
			return;
		}
		else
		{
			_instance = this as T;
		}

		if (_dontDestroyOnLoad)
		{
			DontDestroyOnLoad(this);
		}
	}
}
