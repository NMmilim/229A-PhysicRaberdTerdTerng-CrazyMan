using UnityEngine;
public class CameraFollow : MonoBehaviour 
{ 
    public Transform target; 
    public float height = 10f; 
    // ความสูงกล้อง
    public float smoothSpeed = 5f; 
    
    void LateUpdate() 
        { if (target == null) return; 
        
        // กล้องอยู่เหนือ player ตลอด
        Vector3 desiredPosition = new Vector3( target.position.x, height, target.position.z ); 
        // ลื่น
        transform.position = Vector3.Lerp( transform.position, desiredPosition, smoothSpeed * Time.deltaTime ); 
        // มองลงตรงๆ transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        }  
}