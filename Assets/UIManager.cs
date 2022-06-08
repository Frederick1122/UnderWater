using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    #endregion
    [SerializeField] private Joystick _moveJoystick;



    public Joystick GetJoystick()
    {
        if ((object)_moveJoystick != null) return _moveJoystick;
        _moveJoystick = FindObjectOfType<Joystick>();
        
        try
        {
            if ((object)_moveJoystick != null) return _moveJoystick;
        }
        catch
        {
            Debug.LogError("UIManager not found Joystick. Check this");
        }
        
        return null;
    }
}
