using Unity.VisualScripting;
using UnityEngine;

public class MineBase : MonoBehaviour
{
    protected bool isConsumed = false;
    protected bool isTargeted = false;
    
    
    protected float mineAmount = 0;

    public bool IsConsumed => isConsumed;
    public bool IsTargeted => isTargeted;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MineTargeted(bool istargeted)
    {
        isTargeted = istargeted;
    }
}
