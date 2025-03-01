using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] private Transform _player; 

    private TMP_Text _scoreText;
    private float _score;
    private float _maxScore;

    private void Start()
    {
        _scoreText = GetComponent<TMP_Text>();
        _score = _player.position.y;
        _maxScore = 0;
    }

    private void Update()
    {
        if (_score > _maxScore)
        {
            _maxScore = Mathf.Round(_score);
            UpdateText();
        }
            
        _score = _player.position.y;
    }

    private void UpdateText() => _scoreText.text = "Max Height: " + _maxScore.ToString();
}
