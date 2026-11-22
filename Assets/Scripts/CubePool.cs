using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    public GameObject cubePrefab;
    public AudioSource song;
    public GameObject rain;

    private GameObject[] cubes = new GameObject[43];
    private float[] returnIn = new float[43];
    private float nextSpawn;

    private bool isSpawning = false;

    void Start()
    {
        //Prepare the cubes
        for(int i = 0; i < cubes.Length; i++)
        {
            cubes[i] = Instantiate(cubePrefab, transform);
            cubes[i].SetActive(false);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        //Return. Deactivate the cubes
        for( int i = 0; i < cubes.Length; i++)
        {
            if(cubes[i].activeSelf && Time.time >= returnIn[i])
            {
                cubes[i].SetActive(false);
            }
        }

        //Spawn
        if (isSpawning && Time.time >= nextSpawn)
        {
            Spawn();
            nextSpawn = Time.time + 1f;

        }

        ToggleSpawning();

    }

  

    public void ToggleSpawning()
    {
        // Flip the boolean (True becomes False, False becomes True)
        isSpawning = !isSpawning;
        if (song != null)
        {
            if (isSpawning)
            {
                song.Play(); // Starts or restarts the music
                rain.SetActive(true);
            }
            else
            {
                song.Pause();
                rain.SetActive(false);
            }
        }
    }

    void Spawn()
    {
        for( int i = 0; i < cubes.Length; i++)
        {
            if(!cubes[i].activeSelf)
            {
                cubes[i].transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(1f, 6f), Random.Range(-1f, 10f));

                // Reset velocity and angular velocity before re-enabling
                if (cubes[i].TryGetComponent<Rigidbody>(out Rigidbody rb))
                {
                    rb.linearVelocity = Vector3.zero; 
                    rb.angularVelocity = Vector3.zero;
                }

                cubes[i].SetActive(true);
                returnIn[i] = Time.time + 5f;
                return;
            }
        }
    }
}
