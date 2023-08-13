using UnityEngine;
using UnityEngine.UI;

public class OnOffSound : MonoBehaviour
{
    private const string PREFS_KEY = "IsSoundOn";

    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private AudioSource[] _audioArray;

    private bool _isPlayingSound = true;

    private void Start()
    {

        if (PlayerPrefs.HasKey(PREFS_KEY))
        {
            _isPlayingSound = PlayerPrefs.GetInt(PREFS_KEY) == 1;
            UpdateSoundState();
        }
    }

    private void Update()
    {
        SpriteChangeOver();
    }

    public void ToggleSound()
    {
        _isPlayingSound = !_isPlayingSound;
        UpdateSoundState();

        PlayerPrefs.SetInt(PREFS_KEY, _isPlayingSound ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateSoundState()
    {
        foreach (var audios in _audioArray)
        {
            audios.enabled = _isPlayingSound;
        }
    }

    private void SpriteChangeOver()
    {
        if (_isPlayingSound)
        {
            _buttonImage.sprite = _soundOn;
        }
        else
        {
            _buttonImage.sprite = _soundOff;
        }
    }
}