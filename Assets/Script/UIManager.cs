using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text ammoText;
    public BombShooter shooter;

    void Update()
    {
        string ammoDisplay = "Bomb: " + shooter.currentAmmo + "/5";

        if (!shooter.canReload)
        {
            ammoDisplay += "\n<color=yellow>Reloading...</color>";
        }

        ammoText.text = ammoDisplay;
    }
}