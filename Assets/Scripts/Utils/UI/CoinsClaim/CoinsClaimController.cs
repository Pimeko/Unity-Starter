using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CoinsClaimController : MonoBehaviour
{
    [SerializeField, MinMaxSlider(0, 2, true)]
    Vector2 durationRangeFirstStep = new Vector2(.8f, 1f);
    [SerializeField]
    float durationSecondStep = 1.2f;
    [SerializeField]
    RectTransform to;

    bool isInitialized;
    List<RectTransform> coins;
    List<Sequence> sequences;
    List<Vector3> initialPositions;

    void Init()
    {
        coins = GetComponentsInChildren<RectTransform>().ToList();
        initialPositions = coins.Select(coin => coin.position).ToList();
        sequences = new List<Sequence>();
        isInitialized = true;
    }

    void OnEnable()
    {
        if (!isInitialized)
            Init();

        DoAnimation();
    }

    void DoAnimation()
    {
        coins.ForEach((coin, i) =>
        {
            coin.position = initialPositions[i];
            coin.gameObject.SetActive(true);
        });
        foreach (RectTransform coin in coins)
        {
            sequences.Add(DOTween.Sequence()
            .Append(coin.DOMove(
                coin.position + new Vector3(Random.Range(-80, 80), Random.Range(-80, 80), 0),
                Random.Range(durationRangeFirstStep.x, durationRangeFirstStep.y))
                .SetEase(Ease.OutQuart))
            .Append(coin.DOMove(to.position, durationSecondStep).SetEase(Ease.OutExpo))
            .OnComplete(() => coin.gameObject.SetActive(false)));
        }
    }

    void OnDestroy()
    {
        foreach (Sequence sequence in sequences)
            sequence?.Kill();
    }
}