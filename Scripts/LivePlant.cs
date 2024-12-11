using UnityEngine;

public class LivePlant : MonoBehaviour
{
    public Sprite[] growthStagesMilho;
    public Sprite[] growthStagesCafe;
    public Sprite[] growthStagesCana;
    public Sprite[] growthStagesSoja;
    public Sprite[] growthStages;
    public Item1 fruitMilho;
    public Item1 fruitCafe;
    public Item1 fruitCana;
    public Item1 fruitSoja;
    public Item1 fruit;
    public Sprite spriteOrigin;
    float timeBetweenStages = 5f;
    private int currentStage = 0; 
    private SpriteRenderer spriteRenderer;
    public bool isPlayerInRange;
    int qtd_N = 10;
    int qtd_P = 10;
    int qtd_K = 10;
    int qtd_W = 10;
    int aux = 0;
    Plants p_milho = new Plants(1,3,2,3);
    Plants p_cafe = new Plants(2,1,3,2);
    Plants p_cana = new Plants(2,1,2,3);
    Plants p_soja = new Plants(2,3,2,1);
    Plants choosen;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Harvest()
    {
        if (currentStage == growthStages.Length - 1) // Último estágio (pronto para colher)
        {
            Transform player = GameObject.Find("Human").transform;

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float randomDistance = Random.Range(0.1f, 0.5f);
            Vector2 spawnPosition = (Vector2)player.position + randomDirection * randomDistance;

            int fruits = Random.Range(2, 5);
            for(int i=0; i<=fruits; i++){
                GameObject dropItemPrefab = Instantiate(fruit.prefab, spawnPosition, Quaternion.identity);
                dropItemPrefab.GetComponent<SpriteRenderer>().sprite = fruit.sprite;
                dropItemPrefab.GetComponent<PickUpItem>().item = fruit;
            }
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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void OnMouseDown()
    {
        Harvest(); // Chama o método de colheita quando o jogador clica na planta
    }

    void Update()
    {
        if(isPlayerInRange){
            if(Input.GetKeyDown(KeyCode.P)){
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
            }else if(Input.GetKeyDown(KeyCode.M) && Inventory1.Singleton.hasItem("milho", 1))
            {
                Debug.Log("plantou milho");
                this.choosen = p_milho;
                this.fruit = fruitMilho;
                this.growthStages = growthStagesMilho;
                plant();
            }else if(Input.GetKeyDown(KeyCode.L) && Inventory1.Singleton.hasItem("cafe", 1))
            {
                Debug.Log("plantou cafe");
                this.choosen = p_cafe;
                this.fruit = fruitCafe;
                this.growthStages = growthStagesCafe;
                plant();
            }else if(Input.GetKeyDown(KeyCode.N) && Inventory1.Singleton.hasItem("cana", 1))
            {
                Debug.Log("plantou cana");
                this.choosen = p_cana;
                this.fruit = fruitCana;
                this.growthStages = growthStagesCana;
                plant();
            }else if(Input.GetKeyDown(KeyCode.O) && Inventory1.Singleton.hasItem("soja", 1))
            {
                Debug.Log("plantou soja");
                this.choosen = p_soja;
                this.fruit = fruitSoja;
                this.growthStages = growthStagesSoja;
                plant();
            }
            else if(Input.GetKeyDown(KeyCode.Q)){
                Destroy(gameObject);
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
        Plants p1 = this.choosen;
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
    }


    //Implementar: sistema de colheita, sistema de adubar, de plantação somente com a semente
    //quando tiver uma planta na pllantação, definir a tag dela como outra tag
}

