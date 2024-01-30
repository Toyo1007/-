using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playerdead : MonoBehaviour
{
    public Playersize getdedflg;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    bool playSE;
    bool sceneReloaded;
    float time;
    Light light;
    [SerializeField] GameObject gameOverUI;

    // Start is called before the first frame update
    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        playSE = false;
        sceneReloaded = false;
        time = 0f;
        light = GameObject.Find("Directional Light").GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Tutorial")
        if (getdedflg.dedflg)
        {
            if (!playSE)
            {
                source.Stop();
                source.volume = 1.0f;
                source.PlayOneShot(clip);
                playSE = true;
                light.intensity = 0f;
                Instantiate(gameOverUI,new Vector3(0f,0f,0f), Quaternion.identity);
                Time.timeScale = 0f;
            }
            if (playSE)
            {
                time += Time.unscaledDeltaTime;
            }

            if (!source.isPlaying || time > 0.3f)
            {
                Time.timeScale = 1f;

                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // MeltFloorÇ…ìñÇΩÇ¡ÇΩÇÁÉäÉXÉ|Å[Éì
        if (collision.gameObject.tag == "MeltFloor")
        {
            if (!playSE)
            {
                source.Stop();
                source.volume = 1.0f;
                source.PlayOneShot(clip);
                playSE = true;
                light.intensity = 0f;
                Instantiate(gameOverUI, new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
            if (playSE)
            {
                time += Time.deltaTime;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
