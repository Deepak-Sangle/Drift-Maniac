using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public enum SIDE {Left = -12, Mid = 0, Right=12}
public enum HitX {Left, Mid, Right, None}
public enum HitY {Up, Mid, Down, Low, None}
public enum HitZ {Forward, Mid, Backward, None}

public class CharacterMovement : MonoBehaviour
{
    //VARIABLES
    public float DodgeSpeed=5f;
    public float JumpPower = 5f;
    public float jogtt = 1.2f;
    public float FwdSpeed=10f;
    public float landSpeed=3;
    public float StumbleTolerance = 10f, stumbleTime;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    //REFERNCES
    private Collider CollisionColl;
    private AudioSource footsteps;
    private CharacterController controller;
    private Vector3 moveDirection;
    public Animator anim;
    private CapsuleCollider collider;
    //INITIALIZATION
    public SIDE m_Side = SIDE.Mid;
    public SIDE LastSide;
    private bool left, right, up, down;
    float x,y=0.0f;
    private bool inJump, inRoll;
    private float ColHeight, ColCenterY;
    private bool isPaused = false;
    public bool isDeath = false;
    public GameManager scrpt3;
    
    void Start()
    {
        scrpt3 = FindObjectOfType<GameManager>();
        stumbleTime = StumbleTolerance;
        controller = GetComponent<CharacterController>();
        footsteps = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        collider = GetComponentInChildren<CapsuleCollider>();
        CollisionColl = GetComponentInChildren<Collider>();
        ColHeight = controller.height;
        ColCenterY = controller.center.y;
        transform.position = Vector3.zero;
    }


    void Update()
    {
        //Taking Inputs
        if(isDeath) {
            FindObjectOfType<AudioManager>().StopSound("MainTheme");
            controller.Move(Vector3.down * 500f * Time.deltaTime);
            StartCoroutine(Killed());
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {  
            if(isPaused) {
                scrpt3.PlayResume();
                isPaused = false;
            }
            else{
                isPaused = true;     
                scrpt3.PlayPause();
            }
        }
        left = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) ;
        right = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) ;
        up = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) ;
        down = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
      
        //Right and Left Movement
        if(left && !inRoll){
            if(m_Side == SIDE.Mid){
                LastSide = m_Side;
                m_Side = SIDE.Left;
                // anim.Play("Armature|Dodge-Left");
            }
            else if (m_Side == SIDE.Right){
                LastSide = m_Side;
                m_Side = SIDE.Mid;
                // anim.Play("Armature|Dodge-Left");
            }
            else {
                LastSide = m_Side;
                // PlayStumbleAnimations("Armature|Stumble-Left");
                // StartCoroutine(PlaySideAnimation());
            }
        }
        if(right && !inRoll){
            if(m_Side == SIDE.Mid){
                LastSide = m_Side;
                m_Side = SIDE.Right;
                // anim.Play("Armature|Dodge-Right");
            }
            else if (m_Side == SIDE.Left){
                LastSide = m_Side;
                m_Side = SIDE.Mid;
                // anim.Play("Armature|Dodge-Right");
            }
            else {
                LastSide = m_Side;
                //  PlayStumbleAnimations("Armature|StumbleRight");
                // StartCoroutine(PlaySideAnimation());
            }
        }
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
            isDeath = false;
        }

        stumbleTime = Mathf.MoveTowards(stumbleTime, StumbleTolerance, Time.deltaTime);
        x = Mathf.Lerp(x, (int)m_Side , Time.deltaTime*DodgeSpeed);
        Vector3 moveVector = new Vector3(x-transform.position.x, y*Time.deltaTime, FwdSpeed*Time.deltaTime);
        controller.Move(moveVector);
        Jump();
        Roll();
    }

    public IEnumerator Killed(){
        yield return new WaitForSeconds(1.7f);
        SceneManager.LoadScene(0); 
    }

    public void Jump(){
        if(controller.isGrounded){
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Armature|Falling")){
                // controller.Move(Vector3.forward*(landSpeed));
                PlayAnimations("Armature|Landing");
                inJump = false;
            }
            if(up && !inRoll){
                y=JumpPower;
                PlayAnimations("Armature|Jump");
                inJump = true;
            }
        }
        else{
            y -=JumpPower*2*Time.deltaTime;
            PlayAnimations("Armature|Falling");
        }
    }

    internal float RollCounter;

    public void Roll(){
        RollCounter -=Time.deltaTime;
        if(RollCounter<=0f){
            RollCounter = 0f;
            inRoll = false;
            controller.center = new Vector3(0,ColCenterY,0);
            controller.height = ColHeight;
            collider.center = new Vector3(0,ColCenterY,0);
            collider.height = ColHeight;
        }
        if(down && !inRoll){
            y-=10f;
            RollCounter = 0.8f;
            anim.CrossFadeInFixedTime("Armature|Rolling", 0.1f);
            inRoll = true;
            inJump = false;
            controller.center = new Vector3(0,ColCenterY/2f,0);
            controller.height = ColHeight/2f;
            collider.center = new Vector3(0,ColCenterY/2f,0);
            collider.height = ColHeight/2f;
        }
    }

    public void OnCharacterColliderHit(Collider coll){
        hitX = GetHitX(coll);
        hitY = GetHitY(coll);
        hitZ = GetHitZ(coll);

        if(hitZ == HitZ.Forward && hitX == HitX.Mid){
            if(hitY == HitY.Low){
                PlayStumbleAnimations("Armature|Stumble");
                coll.isTrigger = true;
                StartCoroutine(SetTriggerFalse(coll));
            }
            else if(hitY == HitY.Down) {
                StartCoroutine(PlayDeathAnimations("Armature|Fall-Forward"));
            }
            else {
                StartCoroutine(PlayDeathAnimations("Armature|Fall-Back'"));
            }
        }

        else if (hitZ == HitZ.Mid){
            if(hitX == HitX.Right){
                LastSide = m_Side;
                // PlayStumbleAnimations("Armature|StumbleRight");
            }
            else if(hitX == HitX.Left){
                LastSide = m_Side;
                // PlayStumbleAnimations("Armature|Stumble-Left");
            }
        }
    }

    public IEnumerator SetTriggerFalse(Collider col){
        if(isDeath) yield return null ;
        anim.SetBool("isRun", false);
        yield return new WaitForSeconds(jogtt);
        // anim.CrossFadeInFixedTime("Armature|Jogging", 0.1f);
        // yield return new WaitForSeconds(3f);
        // anim.SetBool("isRun", true);
        // col.isTrigger = false;
    }

    // public IEnumerator PlaySideAnimation(){
        // anim.SetBool("isRun", false);
        // anim.Play("Armature|Jogging");
        // stumbleTime -=3f;
        // yield return new WaitForSeconds(3f);
        // anim.SetBool("isRun", true);

    // }

    private void ResetCollision(){
        hitX = HitX.None;
        hitY = HitY.None;
        hitZ = HitZ.None;
    }
    public void PlayAnimations(string animation){
        if(isDeath) return;
        anim.Play(animation);
    }
    public IEnumerator PlayDeathAnimations(string animation){
        isDeath = true;
        controller.Move(Vector3.down * 500f * Time.deltaTime);
        anim.CrossFadeInFixedTime(animation, 0.10f);
        FindObjectOfType<AudioManager>().PlaySound("GameOver");
        yield return new WaitForSeconds(0.2f);
    }
    public void PlayStumbleAnimations(string animation){
        anim.ForceStateNormalizedTime(0.0f);
        anim.Play(animation);
        // if(stumbleTime < (StumbleTolerance/2f)){
        //     StartCoroutine(PlayDeathAnimations("Armature|Fall-Forward"));
        //     return;
        // }
        stumbleTime -=(StumbleTolerance/2f);
        ResetCollision();
    }

    public HitX GetHitX(Collider coll){
        Bounds controller_bounds = controller.bounds;
        Bounds coll_bounds = coll.bounds;
        float min_x = Mathf.Max(coll_bounds.min.x, controller_bounds.min.x);
        float max_x = Mathf.Min(coll_bounds.max.x, controller_bounds.max.x);
        float avg = (min_x + max_x)/2f - coll_bounds.min.x;
        HitX hit;
        if(avg>coll_bounds.size.x - (controller_bounds.size.x*0.33) )
            hit = HitX.Right;
        else if (avg<(controller_bounds.size.x*0.33))
            hit = HitX.Left;
        else 
            hit = HitX.Mid;

        return hit;
    }

    public HitY GetHitY(Collider coll){
        Bounds controller_bounds = controller.bounds;
        Bounds coll_bounds = coll.bounds;
        float min_y = Mathf.Max(coll_bounds.min.y, controller_bounds.min.y);
        float max_y = Mathf.Min(coll_bounds.max.y, controller_bounds.max.y);
        float avg = ((min_y + max_y)/2f - controller_bounds.min.y) / controller_bounds.size.y;
        HitY hit;
        if (avg<0.10f){
            hit = HitY.Low;
        }
        else if(avg<0.33f)
            hit = HitY.Down;
        else if (avg<0.50f)
            hit = HitY.Mid;
        else 
            hit = HitY.Up;

        return hit;
    }

    public HitZ GetHitZ(Collider coll){
        Bounds controller_bounds = controller.bounds;
        Bounds coll_bounds = coll.bounds;
        float min_z = Mathf.Max(coll_bounds.min.z, controller_bounds.min.z);
        float max_z = Mathf.Min(coll_bounds.max.z, controller_bounds.max.z);
        float avg = ((min_z + max_z)/2f - controller_bounds.min.z) / controller_bounds.size.z;
        HitZ hit;
        if(avg<0.33f)
            hit = HitZ.Backward;
        else if (avg<0.67f)
            hit = HitZ.Mid;
        else 
            hit = HitZ.Forward;

        return hit;
    }
}
