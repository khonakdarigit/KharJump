using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Donky : MonoBehaviour
{
    public static Donky instance;
    float horizantal;
    public Rigidbody2D DoodleRigid;
    public enum DonkeyStaus
    {
        Normal,
        InSimpleJump,
        InCoilJump,
        InRocketFly,
        InPolingJump,
        OnBlckHole
    };



    public DonkeyStaus staus;
    private Animator anim;

    public Sprite
        sp_Donky,
        sp_DonkyJump,
        sp_RoketDonkey;



    public AudioSource
        DropSound,
        JumpWithCoil,
        JumpWithPoling,
        AS_SlapingLeg;


    public float forcejump;


    private bool isCoroutineExecuting;

    public Vector3 MovePositionPoint { get; private set; }

    void Start()
    {
        if (instance == null) { instance = this; }

        isCoroutineExecuting = false;

        staus = DonkeyStaus.Normal;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            horizantal = Input.acceleration.x;
        }


    }

    void FixedUpdate()
    {
        DoodleRigid.velocity = new Vector2(Input.acceleration.x * 15f, DoodleRigid.velocity.y);

        App.instance.ChangeTxtScore((int)DoodleRigid.position.y * 3);

        if (staus == DonkeyStaus.OnBlckHole)
        {
            float dist = Vector3.Distance(DoodleRigid.transform.position, MovePositionPoint);
            if (dist > 0.1f)
            {
                Vector2 newPosition = Vector2.MoveTowards(DoodleRigid.transform.position, MovePositionPoint, Time.deltaTime * 5f);
                DoodleRigid.MovePosition(newPosition);
            }
            else
            {
                DoodleRigid.bodyType = RigidbodyType2D.Static;
                staus = DonkeyStaus.Normal;
            }



        }
    }

    public void toJumpShape()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sp_DonkyJump;

    }
    public void toNormalShape()
    {

        if (staus == DonkeyStaus.Normal)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sp_Donky;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (staus != DonkeyStaus.InRocketFly)
        {
            var platform = collision.collider.GetComponent<Platform>();
            var BrackPlatform = collision.collider.GetComponent<BrackPlatform>();
            var LeftRightPlatform = collision.collider.GetComponent<LeftRightPlatform>();
            var Explosion_Platfrom = collision.collider.GetComponent<ExplosionPlatfrom>();
            var LeftRightExploid_Platform = collision.collider.GetComponent<LeftRightExploidPlatform>();
            var JumpHide_Platform = collision.collider.GetComponent<JumpHidePlatform>();
            var LeftRightJumpHide_Platform = collision.collider.GetComponent<LeftRightJumpHidePlatform>();

            var coil = collision.collider.GetComponent<Per_Coil>();
            var roket = collision.collider.GetComponent<Roket>();
            var poling = collision.collider.GetComponent<Per_Poling>();
            var oneEyeMonester = collision.collider.GetComponent<OneEyeMonester>();
            var FourEyeMonester = collision.collider.GetComponent<ForEyeMonester>();
            var beeMonester = collision.collider.GetComponent<BeeMonester>();

            // On platform
            if (platform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // On Brack platform
            else if (BrackPlatform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    BrackPlatform.Brack();

                }
            }
            // On LeftRightPlatform
            if (LeftRightPlatform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // On Explosion_Platfrom
            if (Explosion_Platfrom != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // On Left Right Explosion_Platfrom
            if (LeftRightExploid_Platform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // On Jump Hide platform
            if (JumpHide_Platform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    JumpHide_Platform.Delete();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // On Left Right Jump Hide platform
            if (LeftRightJumpHide_Platform != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump;
                    gameObject.GetComponent<AudioSource>().Play();
                    LeftRightJumpHide_Platform.Delete();
                    toJumpShape();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // on coil
            else if (coil != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InCoilJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump * 2;
                    toJumpShape();
                    JumpWithCoil.Play();
                    coil.toOpenShape();

                    StartCoroutine(App.instance.ExecuteAfterTime(0.6f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            // on rocket
            else if (roket != null && staus != DonkeyStaus.InRocketFly)
            {
                staus = DonkeyStaus.InRocketFly;
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = sp_RoketDonkey;

                roket.transform.SetParent(gameObject.transform);
                roket.transform.position = gameObject.transform.position;

                roket.StartFier();
                // Start Lunching
                DoodleRigid.velocity = Vector2.up * forcejump * 10;


                StartCoroutine(ExecuteAfterTime(6f, () =>
                {
                    roket.StopFier();
                    gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                    staus = DonkeyStaus.Normal;
                    toNormalShape();

                }));
            }
            // on Poling
            else if (poling != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InPolingJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump * 2.5f;
                    toJumpShape();
                    poling.toOpenShape();
                    anim.SetTrigger("rote");
                    JumpWithPoling.Play();

                    StartCoroutine(App.instance.ExecuteAfterTime(0.6f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));


                }
            }
            else if (oneEyeMonester != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump * 1.5f;
                    AS_SlapingLeg.Play();
                    toJumpShape();
                    oneEyeMonester.Drop();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));
                }
            }
            else if (FourEyeMonester != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump * 1.5f;
                    AS_SlapingLeg.Play();
                    toJumpShape();
                    FourEyeMonester.Drop();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));
                }
            }
            else if (beeMonester != null)
            {
                if (collision.relativeVelocity.y > 0)
                {
                    staus = DonkeyStaus.InSimpleJump;
                    App.instance.ChangeStatus(staus.ToString());
                    DoodleRigid.velocity = Vector2.up * forcejump * 1.5f;
                    AS_SlapingLeg.Play();
                    toJumpShape();
                    beeMonester.Drop();
                    StartCoroutine(App.instance.ExecuteAfterTime(0.4f, () =>
                    {
                        staus = DonkeyStaus.Normal;
                        toNormalShape();
                    }));
                }
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //only exectue OnPlayerEnter if the player collides with this token.
        var deadZone = collision.GetComponent<DeadZone>();
        if (deadZone != null)
        {
            if (!App.instance.IsGameOver)
            {
                DropSound.Play();
                gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
                App.instance.GameOver();
            }
        }

    }

    internal void onBlcakHole(Vector3 position)
    {

        GetComponent<Animator>().SetTrigger("OnBlackHole");

        staus = DonkeyStaus.OnBlckHole;

        MovePositionPoint = position;
    }
    internal void Drop()
    {
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
    public IEnumerator ExecuteAfterTime(float time, Action task)
    {
        if (isCoroutineExecuting)
            yield break;
        isCoroutineExecuting = true;
        yield return new WaitForSeconds(time);
        task();
        isCoroutineExecuting = false;
    }

}
