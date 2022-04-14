using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExploreScene
{

    public class RouteRenderer
    {
        private readonly RouteGenerator generator;
        private readonly List<GameObject> gameObjects;

        public RouteRenderer()
        {
            this.generator = new RouteGenerator();
            this.gameObjects = new List<GameObject>();
        }

        public void Start()
        {
            this.generator.Start();
            this.Render();
        }

        public void Render()
        {
            this.gameObjects.ForEach(g => MonoBehaviour.Destroy(g));
            this.gameObjects.Clear();

            for (
                ExplorePhase renderPhase = this.generator.CurrentPhase;
                renderPhase.next != null;
                renderPhase = renderPhase.next)
            {
                int depth = this.generator.CurrentPhase.phase - renderPhase.phase;
                float scale = depth == 0 ? 1.0f : 0.3f;

                var nextRooms = renderPhase.next.rooms;
                for (int i = 0; i < nextRooms.Count; i++)
                {
                    var room = nextRooms[i];
                    var tile = Prefabs.Instantiate<ExploreTile>("ExploreScene/ExploreTilePrefab");

                    float x = -1.2f + (room.line - 1) * 1.3f;
                    float y = -0.9f - (depth == 0 ? 0.5f : 0);
                    tile.Position = new Vector2(x * scale, y - depth * 0.5f);
                    tile.transform.localScale = new Vector2(scale, scale);
                    tile.Initialize(room, RoomSelected);

                    this.gameObjects.Add(tile.gameObject);
                }

            }

        }

        private void RoomSelected(ExploreRoom selectedRoom)
        {
            this.generator.GoTo(selectedRoom.line);
            this.Render();

            Debug.Log($"room: {selectedRoom.phase} - {selectedRoom.line}");
        }
    }
}