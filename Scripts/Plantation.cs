using UnityEngine;

public class Plantation : MonoBehaviour
{
    public Sprite[] growthStages;
    public Sprite spriteOrigin;
    public Sprite deathSprite;
    float timeBetweenStages = 5f;
    private int currentStage = 0; 
    private SpriteRenderer spriteRenderer;
    public bool isPlayerInRange;
    public bool isAlive = true;
    public bool hasPlantNext;
    int qtd_N = 10;
    int qtd_P = 10;
    int qtd_K = 10;
    int qtd_W = 10;
    int aux = 0;
    int sickness = 0;
    Plants p1 = new Plants(2,2,2,2);
    Plants p_milho = new Plants(2,2,2,2);
    Plants p_cafe = new Plants(2,2,2,2);
    Plants p_cana = new Plants(2,2,2,2);
    Plants p_soja = new Plants(2,2,2,2);


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    public void Harvest()
    {
        if (currentStage == growthStages.Length - 1) // Último estágio (pronto para colher)
        {
            Debug.Log("Colhido com sucesso!");
            ResetPlant(); // Reinicia o crescimento
        }
        else
        {
            Debug.Log("Removido antes de amadurecer.");
            ResetPlant(); // Opcional: também pode reiniciar a planta aqui
        }
    }

    void ResetPlant()
    {
        currentStage = 0;
        spriteRenderer.sprite = growthStages[currentStage]; // Volta ao estágio inicial
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }else if (other.CompareTag("Plantation")){
            hasPlantNext = true;
        }
    }

    void OnMouseDown()
    {
        Harvest(); // Chama o método de colheita quando o jogador clica na planta
    }

    void Update()
    {
        if(isAlive)
        {
            Debug.Log("A planta tá vivinha");
            if(isPlayerInRange){
                if(Input.GetKeyDown(KeyCode.O)){
                    plant();
                }else if(Input.GetKeyDown(KeyCode.P)){
                    CancelInvoke("Grow");
                    spriteRenderer.sprite = spriteOrigin;
                }else if(Input.GetKeyDown(KeyCode.U)){
                    if(Inventory1.Singleton.getInventoryItems().Contains("nitrogenio")){
                        //to do
                        setN(getN() + 10);
                    }
                    if(Inventory1.Singleton.getInventoryItems().Contains("potassio")){
                        //to do
                        setP(getP() + 10);
                    }
                        if(Inventory1.Singleton.getInventoryItems().Contains("fosforo")){
                        //to do
                        setK(getK() + 10);
                    }
                }else if(Input.GetButtonDown("Fire1") && Inventory1.Singleton.getUsing("regador")){
                    setW(getW() + 10);
                }else if(Input.GetKeyDown(KeyCode.Q)){
                    Destroy(gameObject);
                }
            }
        }else
        {
            Debug.Log("A planta tá morta");
            if(hasPlantNext)
            {
                isAlive = true;
                spriteRenderer.sprite = spriteOrigin;
                Inventory1.Singleton.money += 5;
            }
        }
    }

    public void plant(){
        //p1.Vitalidade();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = growthStages[currentStage]; // Define o sprite inicial
        InvokeRepeating("Grow", timeBetweenStages, timeBetweenStages); // Inicia o crescimento

    }

    void Grow()
    {
        this.aux = this.aux + 2;
        if (currentStage < growthStages.Length - 1)
        {
            currentStage++;
            spriteRenderer.sprite = growthStages[currentStage]; // Atualiza o sprite
        }
        if(aux == 4){
            Debug.Log("diminuindo");
            setN(getN() - p1.Nsct_N);
            setP(getP() - p1.Nsct_P);
            setK(getK() - p1.Nsct_K);
            setW(getW() - p1.Nsct_W);
            aux = 0;
        }
        if(getN() <= 0 || getK() <= 0 || getP() <= 0 || getW() <= 0){
            CancelInvoke("Grow");
            spriteRenderer.sprite = spriteOrigin;
            //death();
            Destroy(gameObject);
        }
        
    }
    
    public int getN(){
        return this.qtd_N;
    }

    public void setN(int new_QtdN){
        this.qtd_N = new_QtdN;
    }

    public int getP(){
        return this.qtd_P;
    }

    public void setP(int new_QtdP){
        this.qtd_P = new_QtdP;
    }

    public int getK(){
        return this.qtd_K;
    }

    public void setK(int new_QtdK){
        this.qtd_K = new_QtdK;
    }

    public int getW(){
        return this.qtd_W;
    }

    public void setW(int new_QtdW){
        this.qtd_W = new_QtdW;
    } //oxe

    public int getSickness()
    {
        return this.sickness;
    }

    //Implementar: sistema de colheita, sistema de adubar, de plantação somente com a semente
}

