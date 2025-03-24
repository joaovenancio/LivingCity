//Copyright(C) 2025 Joao Vitor Demaria Venancio under GNU AGPL. Refer to README.md for more information.
using System;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
	private MeshRenderer _prefabMeshRenderer;
	private MeshFilter _prefabMeshFilter;
	private GameObject _previewObject;
	//private bool  = true;
	[Header("References")]
    [SerializeField] private GameObject _prefab;
	[SerializeField] private Material _previewMaterial;
    [Header("Optional Reference")]
    [SerializeField] private GameObject _previewObjectHolder;

	[Header("Variables")]
	[SerializeField] private Vector3 _locationToShow;
	[SerializeField] private bool _isPreviewing = true;

	public bool IsPreviewing { 
		get => _isPreviewing;
		set {
			if (value) ShowObject();
			else HideObject();

			_isPreviewing = value;
		}
	}
	public Vector3 LocationToShow { get => _locationToShow; set => _locationToShow = value; }
	public GameObject Prefab { get => _prefab; }


	private void Awake()
	{
        SetupFields();
        SetupPreviewObject();
	}

	void Update()
	{
		MovePreview();
	}



	private void SetupPreviewObject()
    {
		_previewObject = new GameObject("Preview of " + _prefab.name);

        _previewObject.transform.localPosition = Vector3.zero;
		_previewObject.transform.SetParent(_previewObjectHolder.transform);

		MeshFilter previewObjectMeshFilter = _previewObject.AddComponent<MeshFilter>();
		MeshRenderer previewObjectMeshRenderer = _previewObject.AddComponent<MeshRenderer>();

		_previewObject.SetActive(false);

        SetupPreviewObjectRender(previewObjectMeshFilter, previewObjectMeshRenderer);
	}

    private void SetupPreviewObjectRender(MeshFilter meshFilter, MeshRenderer meshRenderer)
    {
		meshFilter.sharedMesh = _prefabMeshFilter.sharedMesh;
        
        int qtyMaterialsInPrefab = _prefabMeshRenderer.sharedMaterials.Length;
		Material[] materials = new Material[qtyMaterialsInPrefab];

		for (int i = 0; i < qtyMaterialsInPrefab; i++)
        {
            materials[i] = _previewMaterial;
		}

		meshRenderer.materials = materials;

		_previewObject.transform.localScale = _prefabMeshFilter.gameObject.transform.localScale;
	}

	private void SetupFields()
    {
        if (!_previewObjectHolder) _previewObjectHolder = this.gameObject;

		if (!_prefab) _prefab = gameObject;
        if (!_prefab)
        {
            Debug.LogError("PreviewObject: Prefab not set.", this);
            return;
		}

        _prefabMeshFilter = _prefab.GetComponent<MeshFilter>();
		if (!_prefabMeshFilter) _prefabMeshFilter = _prefab.GetComponentInChildren<MeshFilter>();
        if (!_prefabMeshFilter)
        {
            Debug.LogError("PreviewObject: MeshFilter not found in the prefab.", this);
            return;
        }

		_prefabMeshRenderer = _prefab.GetComponent<MeshRenderer>();
		if (!_prefabMeshRenderer) _prefabMeshRenderer = _prefab.GetComponentInChildren<MeshRenderer>();
		if (!_prefabMeshRenderer)
		{
			Debug.LogError("PreviewObject: MeshRenderer not found in the prefab.", this);
			return;
		}
	}

	void Start()
    {
        
    }

    

    private void MovePreview()
    {
		if (!_isPreviewing) return;

		_previewObject.transform.position = _locationToShow;
	}

    public void ShowObject()
    {
		_previewObject.SetActive(true);
    }

    public void HideObject()
    {
        _previewObject.transform.localPosition = Vector3.zero;
		_previewObject.SetActive(false);
	}
}
