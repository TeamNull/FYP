using System.Collections;
using UnityEngine.TestTools;
using NUnit.Framework;
using UnityEngine;
using UnityEditor;

namespace Tests
{
    public class PlayerSpawnTests
    {
        [UnityTest]
        public IEnumerator SpawnPlayer() {
            var playerPrefab = Resources.Load("Warrior");
            var playerSpawner = new GameObject().AddComponent<SpawnPlayer>();
            playerSpawner.Construct(playerPrefab);
            yield return null;

            var playerInGame = GameObject.FindWithTag("CleanPlayer");
            var prefabOfTheSpawnedPlayer = PrefabUtility.GetPrefabParent(playerInGame);

            Assert.AreEqual(playerPrefab, prefabOfTheSpawnedPlayer);
        }
    }
}