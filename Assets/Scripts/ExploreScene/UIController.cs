using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ExploreScene
{
    public class UIController : MonoBehaviour
    {
        private ExploreDirector director;

        void Start()
        {
            this.director = FindObjectOfType<ExploreDirector>();

            SetUpdate("ExplorerMP", this.director.player.explorer.mp, v => $"MP {v}");

            this.director.player.manaroids.Subscribe((newList, change, item) =>
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    var manaroid = this.director.player.manaroids[i];
                    SetUpdate($"Manaroid{i + 1}", manaroid);
                }
            });

        }

        private static void SetUpdate(string manaroidPrefix, ManaroidState state)
        {
            state.name.UnsubcribeAll();
            SetUpdate($"{manaroidPrefix}Name", state.name, v => v);

            state.hp.UnsubcribeAll();
            SetUpdate($"{manaroidPrefix}HP", state.hp, v => $"HP {v}");

            state.manaCharge.UnsubcribeAll();
            SetUpdate($"{manaroidPrefix}Charge", state.manaCharge, v => $"Chg {v}");
        }

        private static void SetUpdate<T>(string textComponentName, Reactive<T> subject, Func<T, string> format)
        {
            var component = GameObject.Find(textComponentName).GetComponent<Text>();
            subject.Subscribe(value => component.text = format(value));
        }

        void Update()
        {

        }
    }
}