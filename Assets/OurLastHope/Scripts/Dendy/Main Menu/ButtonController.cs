using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;  // Diperlukan untuk interface event

public class ButtonController : MonoBehaviour, IPointerEnterHandler
{
    public Sprite idleSprite;  // Sprite default (idle)
    public Sprite selectedSprite;  // Sprite ketika tombol dipilih

    public GameObject panelToShow;  // Panel yang akan ditampilkan
    public GameObject panelToClose;  // Panel yang akan ditutup
    public string sceneToLoad;  // Nama scene yang akan dimuat

    public AudioClip buttonClickSound;  // AudioClip untuk suara klik tombol
    public AudioClip buttonHoverSound;  // AudioClip untuk suara hover tombol
    private AudioSource audioSource;  // Komponen AudioSource untuk memutar suara

    private Button button;  // Referensi ke komponen Button
    private Image buttonImage;  // Referensi ke komponen Image pada button

    void Start()
    {
        // Mendapatkan referensi ke komponen Button dan Image
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();

        // Mengatur sprite default ke idle
        button.image.sprite = idleSprite;

        // Menambahkan event listener untuk event klik pada tombol
        button.onClick.AddListener(OnButtonClick);

        // Menambahkan komponen AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnButtonClick()
    {
        // Memutar suara klik tombol jika buttonClickSound tidak null
        if (buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        // Mengatur semua tombol kembali ke idle sprite
        ButtonController[] allButtons = FindObjectsOfType<ButtonController>();
        foreach (ButtonController btn in allButtons)
        {
            btn.ResetToIdle();
        }

        // Mengatur sprite tombol yang diklik ke selected sprite
        buttonImage.sprite = selectedSprite;

        // Menampilkan panel yang ditentukan
        if (panelToShow != null)
        {
            panelToShow.SetActive(true);
        }

        // Menutup panel yang ditentukan
        if (panelToClose != null)
        {
            panelToClose.SetActive(false);
        }

        // Memuat scene yang ditentukan
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Memutar suara hover jika buttonHoverSound tidak null
        if (buttonHoverSound != null)
        {
            audioSource.PlayOneShot(buttonHoverSound);
        }
    }

    public void QuitApplication()
    {
        // Fungsi untuk keluar dari aplikasi
        Application.Quit();
    }

    public void ResetToIdle()
    {
        // Mengatur sprite tombol kembali ke idle sprite
        buttonImage.sprite = idleSprite;
    }

    void Update()
    {
        // Menjalankan fungsi apabila tombol Esc ditekan untuk menyembunyikan atau menampilkan panel
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Menyembunyikan panel yang ditampilkan
            if (panelToShow != null)
            {
                panelToShow.SetActive(false);
            }
            // Menampilkan panel yang ditutup
            if (panelToClose != null)
            {
                panelToClose.SetActive(true);
            }

            // Mengatur semua tombol kembali ke idle sprite
            ResetToIdle();
        }
    }
}
