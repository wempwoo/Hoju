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

            Func<int, float> calcDepth = phase => this.generator.CurrentPhase.phase - phase;
            Func<int, float> calcScale = phase => calcDepth(phase) == 0 ? 0.3f : 0.3f;
            Func<int, int, float> calcX = (phase, line) => (-1.2f + (line - 1) * 1.3f) * calcScale(phase);
            Func<int, float> calcY = phase => (-0.9f - (calcDepth(phase) == 0 ? 0 : 0)) - calcDepth(phase) * 0.5f;

            for (
                ExplorePhase renderPhase = this.generator.CurrentPhase;
                renderPhase.next != null;
                renderPhase = renderPhase.next)
            {
                float scale = calcScale(renderPhase.phase);
                float y = calcY(renderPhase.phase);

                var nextRooms = renderPhase.next.rooms;
                for (int i = 0; i < nextRooms.Count; i++)
                {
                    var room = nextRooms[i];
                    var tile = Prefabs.Instantiate<ExploreTile>("ExploreScene/ExploreTilePrefab");

                    float x = calcX(room.phase, room.line);
                    tile.Position = new Vector2(x, y);
                    tile.transform.localScale = new Vector2(scale, scale);
                    tile.Initialize(room, RoomSelected);

                    this.gameObjects.Add(tile.gameObject);

                    foreach(int toLine in room.nextLines)
                    {
                        var connect = Prefabs.Instantiate<LineRenderer>("ExploreScene/RoomConnectionPrefab");

                        var from = new Vector2(x, y);
                        var to = new Vector2(
                            calcX(renderPhase.phase, toLine),
                            calcY(renderPhase.phase + 1));

                        connect.SetPositions(new Vector3[] { from, to });

                        this.gameObjects.Add(connect.gameObject);
                    }
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