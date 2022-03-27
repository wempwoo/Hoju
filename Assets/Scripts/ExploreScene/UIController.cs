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

            var go = GameObject.Find("ExplorerMP");
            var text = go.GetComponent<Text>();

            this.director.explorerMP.Subscribe(mp =>
            {
                text.text = $"MP {mp.Current}/{mp.Max}";
            });
        }

        void Update()
        {

        }
    }
}