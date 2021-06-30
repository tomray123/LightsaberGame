using DG.Tweening;
using System.Collections;
using UnityEngine;

public class DoTweenController : MonoBehaviour
{
    // End point of animation.
    [SerializeField]
    private Vector3 _targetLocation = Vector3.zero;

    // Animation duration.
    [Range(0.5f, 10.0f), SerializeField]
    private float _moveDuration = 1.0f;

    // Rotation animation duration.
    [SerializeField]
    private float _rotate360Duration = 1.0f;

    // Simple animation type.
    [SerializeField]
    private Ease _moveEase = Ease.Linear;

    // Forward animation type.
    [SerializeField]
    private Ease _moveForwardEase = Ease.Linear;

    // Backward animation type.
    [SerializeField]
    private Ease _moveBackwardEase = Ease.Linear;

    // End color of the object.
    [SerializeField]
    private Color _targetColor;

    [Range(1.0f, 500.0f), SerializeField]
    private float _scaleMultiplier = 3.0f;

    [Range(1.0f, 10.0f), SerializeField]
    private float _colorChangeDuration = 1.0f;

    [SerializeField]
    private DoTweenType _doTweenType = DoTweenType.MovementOneWay;

    // Bool for checking whether the saber throw finished.
    public bool isThrowTweenCompleted = true;

    private enum DoTweenType
    {
        MovementOneWay,
        MovementTwoWay,
        MovementTwoWayWithSequence,
        MovementOneWayColorChange,
        MovementOneWayColorChangeAndScale
    }

    void Start()
    {
        isThrowTweenCompleted = true;

        // Uncomment this to test and debug tween

        /*
        if (_doTweenType == DoTweenType.MovementOneWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        }
        else if (_doTweenType == DoTweenType.MovementTwoWay)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            StartCoroutine(MoveWithBothWays());
        }
        else if (_doTweenType == DoTweenType.MovementTwoWayWithSequence)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            Vector3 originalLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase));
        }
        else if (_doTweenType == DoTweenType.MovementOneWayColorChange)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.GetComponent<Renderer>().material
                .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
        }
        else if (_doTweenType == DoTweenType.MovementOneWayColorChangeAndScale)
        {
            if (_targetLocation == Vector3.zero)
                _targetLocation = transform.position;
            DOTween.Sequence()
                .Append(transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase))
                .Append(transform.DOScale(_scaleMultiplier, _moveDuration / 2.0f).SetEase(_moveEase))
                .Append(transform.GetComponent<Renderer>().material
                .DOColor(_targetColor, _colorChangeDuration).SetEase(_moveEase));
        }
        */
    }

    public void Rotate360()
    {
        transform.DORotate(new Vector3(0, 0, 360), 0.1f, RotateMode.Fast).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }

    // Checks the target position and starts the animation in both ways.
    public void MoveTwoWays(Vector3 targetPosition)
    {
        if (targetPosition == Vector3.zero)
            targetPosition = transform.position;
        StartCoroutine(MoveWithBothWays(targetPosition));
    }

    private IEnumerator MoveWithBothWays()
    {
        Vector3 originalLocation = transform.position;
        transform.DOMove(_targetLocation, _moveDuration).SetEase(_moveEase);
        // Wait until first animation is finished.
        yield return new WaitForSeconds(_moveDuration);
        transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);
    }

    private IEnumerator MoveWithBothWays(Vector3 targetPosition)
    {
        targetPosition.z = 0f;
        Vector3 originalLocation = transform.position;
        transform.DOMove(targetPosition, _moveDuration).SetEase(_moveEase);
        // Wait until first animation is finished.
        yield return new WaitForSeconds(_moveDuration);
        transform.DOMove(originalLocation, _moveDuration).SetEase(_moveEase);
    }

    // Creates the animation for throw with rotation of the saber.
    public IEnumerator DoThrowAndRotate(Vector3 targetPosition)
    {
        Transform player = transform.parent;
        Quaternion originalRotation = transform.localRotation;
        isThrowTweenCompleted = false;
        Vector3 originalLocation = player.position;
        Vector3 localOriginalLocation = transform.localPosition;
        // Detaching the saber from the player.
        transform.parent = null;
        // Start rotating the saber.
        Tween Rotation = transform.DORotate(new Vector3(0, 0, -360), _rotate360Duration, RotateMode.LocalAxisAdd).SetEase(Ease.Linear).SetLoops(-1);
        if (targetPosition == Vector3.zero)
            targetPosition = transform.position;
        targetPosition.z = 0f;
        // Forward throw of the saber.
        Tween forwardTween = transform.DOMove(targetPosition, _moveDuration).SetEase(_moveForwardEase);

        //yield return forwardTween.WaitForCompletion();

        // Wait until forward throw animation is finished.
        yield return new WaitForSeconds(_moveDuration);
        // Starting backward animation.
        Tween backTween = transform.DOMove(originalLocation, _moveDuration).SetEase(_moveBackwardEase);
        // Waiting for its finishing.
        yield return backTween.WaitForCompletion();
        // Killing the rotation.
        Rotation.Kill();
        isThrowTweenCompleted = true;
        // Attach saber back to the player.
        transform.parent = player;
        transform.localRotation = originalRotation;
        transform.localPosition = localOriginalLocation;
    }
}