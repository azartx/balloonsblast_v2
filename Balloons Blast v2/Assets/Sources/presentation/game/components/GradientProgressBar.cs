using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

[RequireComponent(typeof(Image))]
public class GradientProgressBar : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float _initialFill = 0f;
    [SerializeField] private Color _fillColor = Color.green;
    [SerializeField] private bool _smoothFill = true;
    [SerializeField] private float _fillSpeed = 3f;

    private Image _progressImage;
    private float _targetFill;

    public bool canUpdateProgress = true;

    public event EventHandler<Unit> OnProgressFilled;

    private void Awake()
    {
        _progressImage = GetComponent<Image>();
        InitializeProgressBar();
    }

    private void InitializeProgressBar()
    {
        _progressImage.type = Image.Type.Filled;
        _progressImage.fillMethod = Image.FillMethod.Vertical;
        _progressImage.fillOrigin = (int)Image.OriginVertical.Bottom;

        _targetFill = _initialFill;
        _progressImage.fillAmount = _initialFill;
        _progressImage.color = _fillColor;
    }

    private void Update()
    {
        if (_smoothFill && !Mathf.Approximately(_progressImage.fillAmount, _targetFill))
        {
            _progressImage.fillAmount = Mathf.Lerp(
                _progressImage.fillAmount,
                _targetFill,
                _fillSpeed * Time.deltaTime
            );
        }
    }

    /// <summary>
    /// Устанавливает значение прогресс-бара
    /// </summary>
    /// <param name="value">Значение от 0 до 1 (0 - пусто, 1 - заполнен)</param>
    public void UpdateFill(float value)
    {
        if (!canUpdateProgress) return;

        var newFill = _targetFill + value;

        if (newFill >= 1f)
        {
            OnProgressFilled.Invoke(this, Unit.Default);
            newFill = 0f;
        }

        _targetFill = Mathf.Clamp01(newFill);

        if (!_smoothFill)
        {
            _progressImage.fillAmount = _targetFill;
        }
    }

    private void OnValidate()
    {
        if (_progressImage == null)
            _progressImage = GetComponent<Image>();

        if (_progressImage != null)
        {
            _progressImage.type = Image.Type.Filled;
            _progressImage.fillMethod = Image.FillMethod.Vertical;
            _progressImage.fillOrigin = (int)Image.OriginVertical.Bottom;
            _progressImage.color = _fillColor;
            _progressImage.fillAmount = _initialFill;
        }
    }
}