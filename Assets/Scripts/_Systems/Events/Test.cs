using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerMove(Vector2 pointerPosition)
    {
        Debug.Log(pointerPosition);
    }

    public void TestMe()
    {
        Debug.Log("Test");
	}
}
