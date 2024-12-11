using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f; // Velocidade da suavização
    public Vector3 offSet = new Vector3(0, 0, -10); // Offset para a posição da câmera

    private void FixedUpdate()
    {
        // Posição desejada da câmera (posição do player + offset)
        Vector3 desiredPosition = GetPlayer().position + GetOffSet();

        // Suaviza o movimento da câmera usando Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, GetSmoothSpeed());

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }

    public Transform GetPlayer(){
        return this.player;
    }

    public void SetPlayer(Transform newPlayer){
        this.player = newPlayer;
    }

    public float GetSmoothSpeed(){
        return this.smoothSpeed;
    }

    public Vector3 GetOffSet(){
        return this.offSet;
    }

    public void SetOffSet(Vector3 newOffSet){
        this.offSet = newOffSet;
    }
}
