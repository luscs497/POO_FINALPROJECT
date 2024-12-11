using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public int Nsct_N;
    public int Nsct_P;
    public int Nsct_K;
    public int Nsct_W;
    //float timeBetweenStages = 20f;
    //

    public Plants(int Nsct_K, int Nsct_N, int Nsct_P, int Nsct_W){
        this.Nsct_N = Nsct_K;
        this.Nsct_N = Nsct_N;
        this.Nsct_N = Nsct_P;
        this.Nsct_N = Nsct_W;
    }

    //public void Vitalidade(){
        //InvokeRepeating("Consume", timeBetweenStages, timeBetweenStages); // Inicia o crescimento
    //}
    /*
    void Consume(){
        currentLand.setN(currentLand.getN() - this.Nsct_N);
        currentLand.setP(currentLand.getP() - this.Nsct_P);
        currentLand.setK(currentLand.getK() - this.Nsct_K);
        currentLand.setK(currentLand.getW() - this.Nsct_W);
    }
    */
}
