using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text ammoText;
    public BombShooter shooter;

    void Update()
    {
        ammoText.text = "Bomb: " + shooter.currentAmmo + "/5";
    }
}