using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public enum AntigenType
{
    staph = 0,  //éæ¤æ·éç¼??éæ¤æ·éæ¤æ·éæ¤æ·é?
    Strep = 1,    //é¾çèçåä½
    Virus = 2
}
public class StemCell : MonoBehaviour
{
    public Position p;
    public Vector3 target;
    public float speed;     //éæ¤æ·éæ¤æ·éæ¤æ·éç¡è®¹æ·éå«è®¹æ?
    public bool isMove = false;
    public int forward_step = 0;

    private SpriteRenderer sr;
    public List<Sprite> sprites;
    private int curFrame;
    public int FrameCount
    {
        get
        {
            return sprites.Count;
        }
    }
    private float timeDelta;
    private int FPS = 4;


    public Dictionary<AntigenType, byte> antigens = new Dictionary<AntigenType, byte>(); //éæ¤æ·åéæ¤æ·éæ¤æ·ééµç´æ·éæ¤æ·éæ¤æ·
    public byte ATP;         //éæ¤æ·éæ¤æ·
    public byte ATPMax;      //éæ¤æ·éæ¤æ·éæ¤æ·éæ¤æ·
    public byte ATPspeed;   //éæ¤æ·éæ¤æ·éæªé©æ·éå«è®¹æ·
    public int ATPbuffRound;//é¤ç°¬atpgetéï¼å¢?
    public byte extraReward;
    public int rewardRound;


    protected virtual void Awake()
    {
        speed = 3f;
        ATPbuffRound = 0;

        // ç»å®å°spirteç»ä»¶
        sr = GetComponent<SpriteRenderer>();
        if (FrameCount > 0)
        {
            sr.sprite = sprites[0];
        }

        //ä¸ºéæ¤æ·éæ¤æ·éæ¤æ·éæ¤æ·ç§éæ¤æ·éæ¤æ·éæ¤æ·éæ¤æ·éè¡?
        antigens.Add(AntigenType.staph, 2);//2éæ¤æ·éæ¤æ·éæ¤æ·ä¸ºéå¿è¯§æ·éçï½æ·å®éæ¤æ·åºéæ¤æ·ä¸?0
        antigens.Add(AntigenType.Strep, 2);
        antigens.Add(AntigenType.Virus,2);
    }

    public virtual void TurnStart ()
    {
        ATPrecover();
        
    }

    protected virtual void ATPrecover()
    {
        byte addUp=ATPspeed;
        if(ATPbuffRound>0){
            addUp  += 1;
            ATPbuffRound-=1; 
        }
        if (ATP < ATPMax)
        {
            ATP += addUp;
        }
        if(rewardRound>0){
            rewardRound--;
            if(rewardRound == 0){
                ATP+=extraReward;
            }
        }
        if(Board.instance.immuneBuffRound>0){
            ATP+=1;
        }
        if(ATP > ATPMax)
        {
            ATP = ATPMax;
        }
    }

    protected void Update()
    {
        if (isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (target!=null)
        {
            if (transform.position == target)
            {
                isMove = false;
                
            }
        }

        if (FrameCount > 0)
        {
            timeDelta += Time.deltaTime;
            if (timeDelta > 1.0f / FPS)
            {
                timeDelta -= 1.0f / FPS;
                curFrame = (curFrame + 1) % FrameCount;
                sr.sprite = sprites[curFrame];
            }
        }
    }
}
