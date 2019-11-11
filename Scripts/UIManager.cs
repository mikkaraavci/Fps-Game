using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text messageText;
    public Transform inventoryPanel;
    public Transform gridStylePanelContentArea;
    public Transform listStylePanelContentArea;
    private float progressbarWidth;
    public Image progressBar;
    private bool isCoroutineActive;
    public Image crossHair;


    private List<GameObject> gridViewPool = new List<GameObject>();
    private List<GameObject> listViewPool = new List<GameObject>();

    private GameObject player;

    private void Start()
    {
        LockCursor();
        progressbarWidth = progressBar.rectTransform.rect.width;
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.gameObject.activeSelf)
                CloseInventoryPanel();
            else
                OpenInventoryPanel();
        }
        if(player.GetComponent<PlayerMovement>().isDead)
            {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ReloadScene();
            }
        }

        if (player.GetComponent<PlayerMovement>().weaponDummy.transform.childCount > 0)
        {
            SetCrossHairVisibilty(true);
        }
        else
        {
            SetCrossHairVisibilty(false);
        }


    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OpenInventoryPanel()
    {
        inventoryPanel.gameObject.SetActive(true);
        UnlockCursor();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        for (int i=0; i<playerInventory.inventory.Count; i++)
        {
            if (gridViewPool.Count==0)
            {
                GameObject item = Instantiate(Resources.Load("GridStyleUnit_Button"), gridStylePanelContentArea) as GameObject;
                item.transform.Find("Text").GetComponent<Text>().text = playerInventory.inventory[i];
            }
            else
            {
                GameObject objFromPool = gridViewPool[0];
                objFromPool.SetActive(true);
                objFromPool.transform.Find("Text").GetComponent<Text>().text = playerInventory.inventory[i];
                gridViewPool.RemoveAt(0);
            }
        }

        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        for (int i = 0; i < playerInventory.inventory.Count; i++)
        {
            if (listViewPool.Count == 0)
            {
                GameObject item = Instantiate(Resources.Load("ListStyleUnit_Button"), listStylePanelContentArea) as GameObject;
                item.transform.Find("Text").GetComponent<Text>().text = playerInventory.inventory[i];
            }
            else
            {
                GameObject objFromPool = listViewPool[0];
                objFromPool.SetActive(true);
                objFromPool.transform.Find("Text").GetComponent<Text>().text = playerInventory.inventory[i];
                listViewPool.RemoveAt(0);
            }   
        }
    }
    private void CloseInventoryPanel()
    {
        inventoryPanel.gameObject.SetActive(false);
        LockCursor();
        for (int i=0; i<gridStylePanelContentArea.childCount; i++)
        {
            //Destroy(gridStylePanelContentArea.GetChild(i).gameObject);
            GameObject theObject = gridStylePanelContentArea.GetChild(i).gameObject;
            theObject.SetActive(false);
            gridViewPool.Add(theObject);
        }
        for (int i = 0; i < listStylePanelContentArea.childCount; i++)
        {
            //Destroy(listStylePanelContentArea.GetChild(i).gameObject);
            GameObject theObject = listStylePanelContentArea.GetChild(i).gameObject;
            theObject.SetActive(false);
            listViewPool.Add(theObject);
        }
    }




    public void SetMessageText(string aMessage)
    {
        messageText.text = aMessage;
        if (!isCoroutineActive)
        {
            isCoroutineActive = true;
            StartCoroutine(ClearTextAfter5Seconds());
        }
        
    }

    private IEnumerator ClearTextAfter5Seconds()
    {
        yield return new WaitForSeconds(5f);
        messageText.text = "";
        isCoroutineActive = false;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void SetProgressbar(float aRatio)
    {
        progressBar.rectTransform.sizeDelta = new Vector2(progressbarWidth * aRatio, progressBar.rectTransform.rect.height);
    }

    public void SetCrossHairVisibilty(bool isVisible)
    {
        crossHair.gameObject.SetActive(isVisible);
    }
}
