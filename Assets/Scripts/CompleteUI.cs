using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompleteUI : MonoBehaviour
{
    public Image background;
    public Button next, again;
    public Image starsBackground;
    public Image levelComplete;
    public Image gameOver;

    public ParticleSystem[] confetti;

    private Sequence seq;

    static public CompleteUI instance;
    private void Start()
    {
        if (CompleteUI.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        CompleteUI.instance = this;
    }
    private void PlayConfetti()
    {
        int lenght = confetti.Length;
        for (int i = 0; i < lenght; i++)
        {
            confetti[i].Play();
        }
    }

    public void ButtonFunction()
    {
        seq.Kill();
        next.DOKill();
        again.DOKill();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Win()
    {
        seq = DOTween.Sequence();
        seq.Append(background.gameObject.transform.DOScale(1f, 0.25f));
        seq.Append(levelComplete.gameObject.transform.DOScale(1.5f, 0.25f));
        seq.Append(levelComplete.gameObject.transform.DOScale(1f, 0.25f).SetEase(Ease.InSine));
        seq.Append(next.gameObject.transform.DOScale(1.5f, 0.25f));
        seq.Append(next.gameObject.transform.DOScale(1f, 0.25f).SetEase(Ease.InSine));
    }

    private void Lose()
    {
        seq = DOTween.Sequence();
        seq.Append(background.gameObject.transform.DOScale(1f, 0.25f));
        seq.Append(gameOver.gameObject.transform.DOScale(1.5f, 0.25f));
        seq.Append(gameOver.gameObject.transform.DOScale(1f, 0.25f).SetEase(Ease.InSine));

        seq.Append(again.gameObject.transform.DOScale(1.5f, 0.25f));
        seq.Append(again.gameObject.transform.DOScale(1f, 0.25f).SetEase(Ease.InSine));
    }

    public IEnumerator DelayBeforeComplete(bool isItWin, float delay)
    {
        if (isItWin)
            PlayConfetti();
        yield return new WaitForSeconds(delay);
        if (isItWin)
            Win();
        else
            Lose();
    }
}
