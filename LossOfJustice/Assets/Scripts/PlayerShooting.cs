using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f;
    public PlayerInput input;
    public PlayerMovement player;

    public Camera cam;

    private bool hasShot = false;

    private void Update()
    {
        if(player.fireValue == 1 && player.aimValue == 1 && !hasShot)
        {
            Shoot();
            hasShot = true;
        }
        else if(player.fireValue == 0 || player.aimValue == 0)
        {
            hasShot = false;
        }
    }

    void Shoot()
    {
        AnimateShoot();
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.gameObject.GetComponent<NPCAgent>())
            {
                if(hit.transform.gameObject.GetComponent<NPCAgent>().status == Status.Innocent || hit.transform.gameObject.GetComponent<NPCAgent>().status == Status.Hostage)
                {
                    GameHandler.instance.UpdateHealth(-1);
                }
                else if(hit.transform.gameObject.GetComponent<NPCAgent>().status == Status.Criminal || hit.transform.gameObject.GetComponent<NPCAgent>().status == Status.Holder)
                {
                    hit.transform.gameObject.GetComponent<NPCAgent>().GotCaught();
                    GameHandler.instance.UpdateObjectives(1);
                }
                else if(hit.transform.gameObject.GetComponent<NPCAgent>().status == Status.Snatcher)
                {
                    hit.transform.gameObject.GetComponent<NPCAgent>().GotCaught();
                }
            }
            else if (hit.transform.gameObject.GetComponent<NPCBoss>())
            {
                hit.transform.gameObject.GetComponent<NPCBoss>().TakeDamage(1);
            }
        }
    }

    void AnimateShoot()
    {
        if (player.fireValue == 1)
        {
            switch (player.levelWeapon)
            {
                case 0: // Whistle
                    player.anim.SetTrigger("useWhistle");
                    break;
                case 1: // Pepper Spray
                    player.anim.SetTrigger("usePSpray");
                    break;
                case 2: // Taser
                    player.anim.SetTrigger("useTaser");
                    break;
                case 3: // ??
                    break;
            }
        }
    }
}