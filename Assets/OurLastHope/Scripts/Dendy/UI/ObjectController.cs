using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private GameObject objectToToggleF1; // Assign objek yang akan dibuka/tutup dengan F1
    [SerializeField] private GameObject objectToToggleTab; // Assign objek yang akan dibuka/tutup dengan Tab
    [SerializeField] private Animator tabObjectAnimator; // Animator untuk objek yang dikendalikan oleh Tab

    private bool canUseF1 = true;
    private bool canUseTab = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && canUseF1)
        {
            ToggleObject(objectToToggleF1);
            canUseTab = false; // Disable Tab usage
        }

        if (Input.GetKeyDown(KeyCode.Tab) && canUseTab)
        {
            TriggerCloseAnimationAndToggle(objectToToggleTab);
        }

        // Reset condition when object is closed
        if (objectToToggleF1 != null && !objectToToggleF1.activeSelf)
        {
            canUseTab = true; // Enable Tab usage again
        }

        if (objectToToggleTab != null && !objectToToggleTab.activeSelf)
        {
            canUseF1 = true; // Enable F1 usage again
        }
    }

    private void ToggleObject(GameObject objectToToggle)
    {
        if (objectToToggle != null)
        {
            bool isActive = objectToToggle.activeSelf;
            objectToToggle.SetActive(!isActive);
            Debug.Log($"Object Toggled: {(isActive ? "Closed" : "Opened")}");
        }
    }

    private void TriggerCloseAnimationAndToggle(GameObject objectToToggle)
    {
        if (objectToToggle != null && tabObjectAnimator != null)
        {
            bool isActive = objectToToggle.activeSelf;

            if (isActive)
            {
                // Jika objek aktif, jalankan animasi penutupan
                tabObjectAnimator.SetBool("isClosing", true);
                StartCoroutine(CloseAfterAnimation(objectToToggle));
            }
            else
            {
                // Jika objek tidak aktif, langsung buka
                objectToToggle.SetActive(true);
                Debug.Log("Object Opened");
            }
        }
    }

    private IEnumerator CloseAfterAnimation(GameObject objectToToggle)
    {
        // Tunggu sampai animasi penutupan selesai
        yield return new WaitForSeconds(tabObjectAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Sembunyikan objek setelah animasi selesai
        objectToToggle.SetActive(false);
        tabObjectAnimator.SetBool("isClosing", false);
        Debug.Log("Object Closed after animation");
    }
}
