using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int score = 5;
    public ParticleSystem explosionParticle;
    public float hp = 10f;
    
    public void TakeDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            ParticleSystem instance = Instantiate(explosionParticle,transform.position,transform.rotation);
            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            Destroy(instance.gameObject, instance.duration);
            // 프롭을 직접 파괴하고 재생성하는 것이 아닌 잠시 off 상태로 놔둠
            GameManager.instance.AddScore(score);
            gameObject.SetActive(false);
        }
    }
    
}
