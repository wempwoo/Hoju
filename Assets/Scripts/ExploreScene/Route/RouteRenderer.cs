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

            var nextRooms = this.generator.CurrentPhase.next.rooms;
            for (int i = 0; i < nextRooms.Count; i++)
            {
                var room = nextRooms[i];
                var tile = Prefabs.Instantiate<ExploreTile>("ExploreScene/ExploreTilePrefab");
                tile.Position = new Vector2(-1.5f + (room.line - 1) * 1.3f, -0.5f);
                tile.Initialize(room, RoomSelected);

                this.gameObjects.Add(tile.gameObject);
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