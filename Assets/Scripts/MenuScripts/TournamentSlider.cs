using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TournamentSlider : MonoBehaviour
{
    public TextMeshProUGUI textBox;

    public Slider slider;

    int amountOfGames;

    public int[] games;


    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;
        slider.minValue = 0;
        slider.maxValue = games.Length - 1;
        slider.wholeNumbers = true;
        UpdateTournament();
    }

    public void UpdateTournament()
    {
        amountOfGames = games[(int)slider.value];
        textBox.text = amountOfGames.ToString();
    }
}
