using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class DayNightCycle : MonoBehaviour
{
    [Range(0f, 1f)] public float timeOfDay;
    public float timeSpeed;
    public bool aux;
    public List<Tilemap> tilemap;
    public List<GameObject> gameObjects;
    public Color dayColor;
    public Color nightColor;

    public DayNightCycle(float timeOfDay = 0.0f, float timeSpeed = 0.01f, bool aux = true){
        this.timeOfDay = timeOfDay;
        this.timeSpeed = timeSpeed;
        this.tilemap = new List<Tilemap>();
        this.gameObjects = new List<GameObject>();
        this.dayColor = Color.white;
        this.nightColor = new Color(0.1f, 0.1f, 0.4f);
        this.aux = aux;
    }

    void Update()
    {
        if(aux){
            timeOfDay += timeSpeed * Time.deltaTime;
        }else{
            timeOfDay -= timeSpeed * Time.deltaTime;
        }
        if (timeOfDay >= 1.0f)
        {
            aux = false;
        }else if(timeOfDay <= 0)
        {
            aux = true;
        }

        Color currentColor = Color.Lerp(nightColor, dayColor, timeOfDay);

        for(int i=0; i<tilemap.Count;i++){
            tilemap[i].color = currentColor;
            SpriteRenderer sr = gameObjects[i].GetComponent<SpriteRenderer>();
            if(sr != null)
            {
                sr.color = currentColor;
            }
        }

        for(int i=0; i<gameObjects.Count;i++){
            SpriteRenderer sr = gameObjects[i].GetComponent<SpriteRenderer>();
            if(sr != null)
            {
                sr.color = currentColor;
            }
        }
    }

    public bool GetAux(){
        return this.aux;
    }

    public void SetAux(bool aux){
        this.aux = aux;
    }

    public float GetTimeOfDay(){
        return this.timeOfDay;
    }

    public void SetTimeOfDay(float timeOfDay){
        this.timeOfDay = timeOfDay;
    }

    public float GetTimeSpeed(){
        return this.timeSpeed;
    }

    public void SetTimeSpeed(float timeSpeed){
        this.timeSpeed = timeSpeed;
    }


}
