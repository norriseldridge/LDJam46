using System.Linq;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    private float _timePlayed;
    private int _banditCount;
    private float _banditTimeout = 3.0f;
    private float _currentBanditTimeout;
    private SpawnLocation[] _spawnLocations;
    [SerializeField]
    private Bandit _sourceBandit;

    // Start is called before the first frame update
    void Start()
    {
        _timePlayed = 0;
        _banditCount = 0;
        _currentBanditTimeout = 0.0f;
        _spawnLocations = FindObjectsOfType<SpawnLocation>();
    }

    // Update is called once per frame
    void Update()
    {
        // while this is a player
        if (FindObjectOfType<Player>() != null)
        {
            // Increase how long we've been playing
            _timePlayed += Time.deltaTime;
            UpdateDifficulty();

            // Spawn bandits?
            _currentBanditTimeout += Time.deltaTime;
            if (_currentBanditTimeout >= _banditTimeout)
            {
                SpawnBandits();
                _currentBanditTimeout = 0;
            }
        }
    }

    private void UpdateDifficulty()
    {
        // first 30 seconds
        if (_timePlayed < 30)
        {
            _banditCount = 1;
            _banditTimeout = 3;
        }
        else if (_timePlayed < 90)
        {
            _banditCount = 2;
            _banditTimeout = 3;
        }
        else if (_timePlayed < 120)
        {
            _banditCount = 3;
            _banditTimeout = 3;
        }
        else if (_timePlayed < 130)
        {
            _banditCount = 1;
            _banditTimeout = 0.5f;
        }
        else if (_timePlayed < 140)
        {
            _banditCount = 1;
            _banditTimeout = 0.75f;
        }
        else if (_timePlayed < 150)
        {
            _banditCount = 3;
            _banditTimeout = 5;
        }
        else if (_timePlayed < 160)
        {
            _banditCount = 3;
            _banditTimeout = 10;
        }
        else if (_timePlayed < 185)
        {
            _banditCount = 5;
            _banditTimeout = 10;
        }
    }

    private void SpawnBandits()
    {
        // pick random locations based on the number of bandits to spawn in
        int[] indexes = new int[_banditCount];
        for (int i = 0; i < _banditCount; ++i)
        {
            int randIndex = Random.Range(0, _spawnLocations.Length);
            while (indexes.Contains(randIndex) && indexes.Length < _spawnLocations.Length) // keep picking a new random until we have all unique locations or we have picked every location
                randIndex = Random.Range(0, _spawnLocations.Length);
            indexes[i] = randIndex;

            // Spawn the bandit
            Bandit temp = Instantiate(_sourceBandit);
            temp.transform.position = _spawnLocations[randIndex].transform.position;
        }
    }
}
