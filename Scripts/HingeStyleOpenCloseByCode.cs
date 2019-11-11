using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeStyleOpenCloseByCode : MonoBehaviour
{
    public Transform hatch;
    public Transform dummyOpen;
    public Transform dummyClose;
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isAnimating;

    private bool isOpening;
    private bool isClosing;

    private delegate void PostOpenAnimationDelegate();
    private PostOpenAnimationDelegate postOpenDelegate;

    private delegate void PostCloseAnimationDelegate();
    private PostCloseAnimationDelegate postCloseDelegate;

    private bool permissionCheck = true;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            if (GetComponent<AccessPermissionManager>())
            {
                permissionCheck = GetComponent<AccessPermissionManager>().CheckItemInInventory(other.gameObject);
            }

            if (permissionCheck)
            {
                if (!isAnimating)
                {
                    OpenHatch();
                }
                else
                {
                    if (isClosing)
                    {
                        postCloseDelegate = OpenHatch;
                        postOpenDelegate = null;
                    }

                }
            }
            
                
            
        }
    }
    private void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            if (!isAnimating)
            {
                CloseHatch();
            } 
            else
            {
                if (isOpening)
                {
                    postOpenDelegate = CloseHatch;
                    postCloseDelegate = null;
                }
            }
        }
    }



    public void OpenHatch()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            isOpening = true;
            startRotation = hatch.localRotation;
            targetRotation = dummyOpen.localRotation;
            StartCoroutine(AnimateOpenHatch());
        }
        
    }

    public void CloseHatch()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            isClosing = true;
            startRotation = hatch.localRotation;
            targetRotation = dummyClose.localRotation;
            StartCoroutine(AnimateCloseHatch());
        }  
    }

    private IEnumerator AnimateOpenHatch()
    {
        float timer = 0;
        while (timer<1f)
        {
            hatch.localRotation = Quaternion.Lerp(startRotation, targetRotation, timer);
            timer += 0.01f;
            yield return null;
        }
        isAnimating =false;
        PostOpenOperations();
    }
    private IEnumerator AnimateCloseHatch()
    {
        float timer = 0;
        while (timer < 1f)
        {
            hatch.localRotation = Quaternion.Lerp(startRotation, targetRotation, timer);
            timer += 0.01f;
            yield return null;
        }
        isAnimating = false;
        PostCloseOperations();
    }

    private void PostOpenOperations()
    {
        if (postOpenDelegate != null)
        {
            postOpenDelegate();
            postOpenDelegate = null;
        }

        if (GetComponent<BroadcastMessage>())
        {
            GetComponent<BroadcastMessage>().SendMessageToUI();
        }
        if (GetComponent<LootResource>())
        {
            GetComponent<LootResource>().AddLootToPlayerInventory();
        }
    }
    private void PostCloseOperations()
    {
        if (postCloseDelegate != null)
        {
            postCloseDelegate();
            postCloseDelegate = null;
        }
    }

}
