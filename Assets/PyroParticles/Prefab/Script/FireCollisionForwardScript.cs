using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace DigitalRuby.PyroParticles
{
    public interface ICollisionHandler
    {
        void HandleCollision(GameObject obj, Collision c);
    }

    /// <summary>
    /// This script simply allows forwarding collision events for the objects that collide with something. This
    /// allows you to have a generic collision handler and attach a collision forwarder to your child objects.
    /// In addition, you also get access to the game object that is colliding, along with the object being
    /// collided into, which is helpful.
    /// </summary>
    public class FireCollisionForwardScript : MonoBehaviour
    {
        public ICollisionHandler CollisionHandler;
        public EnemyStatus es;
        public int damage = 20;
        GameObject player;
        PlayerAttribute playerAttribute;
        protected void Start()
        {

        
        }
        public void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Debug.Log("forward");
                player = GameManager.player;
                playerAttribute = player.GetComponent<PlayerAttribute>();
                es = SceneManager.GetSceneByName("UI").GetRootGameObjects()[0].transform.GetChild(11).GetComponent<EnemyStatus>();
                EnemyAttribute temp = col.gameObject.GetComponent<EnemyAttribute>();
                temp.TakeDamage(damage + playerAttribute.atk);
                es.UpdateUI(temp);
            }
            //CollisionHandler.HandleCollision(gameObject, col);
        }
    }
}
