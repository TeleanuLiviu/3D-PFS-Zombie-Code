using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies;
    public int i;
    public int pos;
    private Health _player;
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Health>();
        StartCoroutine(Spawn());

    }

    private IEnumerator Spawn()
    {
        while(!_player.died)
        {
            for (i = 0; i < Enemies.Count; i++)
            {
                yield return new WaitForSeconds(Random.Range(3.0f,7.0f));
                pos = Random.Range(0, 1);
                switch (pos)
                {
                    case 0:
                        Instantiate(Enemies[i], new Vector3(Random.Range(-14, 14), 0, Random.Range(53, -53)), Quaternion.identity);
                        pos++;
                        break;
                    case 1:
                        Instantiate(Enemies[i], new Vector3(Random.Range(-48, 48), 0, Random.Range(-13, 13)), Quaternion.identity);
                        pos++;
                        break;
                    case 2:
                        pos = 0;
                        break;
                }

            }

            if (i >= Enemies.Count)
                i = 0;
        }
      
    }


}
