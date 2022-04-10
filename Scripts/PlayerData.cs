using UnityEngine.SceneManagement;

[System.Serializable]

public class PlayerData
{
    public float maxhealth;
    public float health;
    public float [] positionMain = new float [2];
    public float xArma;
    public float yArma;
    public float xnegativaArma;
    public int score;
    public Scene miescena2;

    public PlayerData(PlayerController player){
        maxhealth = player.vidamaxima;
        health = player.health;
        positionMain[0] = player.transform.position.x;
        positionMain[1] = player.transform.position.y;
        miescena2 = player.miescena;
        score = CoinManager.instance.score;
    }

    public PlayerData(Arma player){
        xArma = player.x;
        yArma = player.y;
        xnegativaArma = player.xNegativa;
    }

    public PlayerData(CoinManager player){
        score = CoinManager.instance.score;
    }     
}
