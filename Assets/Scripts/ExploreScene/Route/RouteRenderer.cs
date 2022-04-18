using System;
using System.Collections.Generic;
using UnityEngine;

namespace ExploreScene
{

    public class RouteRenderer
    {
        private readonly Prefab tilePrefab = new Prefab("ExploreScene/ExploreTilePrefab");
        private readonly Prefab connectPrefab = new Prefab("ExploreScene/RoomConnectionPrefab");

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

            this.RenderSelectableRooms();

            Func<int, int> calcDepth = phase => phase - this.generator.CurrentPhase.phase;

            for (
                var currentPhase = this.generator.CurrentPhase.next;
                currentPhase.next != null;
                currentPhase = currentPhase.next)
            {
                int depth = calcDepth(currentPhase.phase);
                int nextDepth = depth + 1;

                // 次の部屋への接続
                foreach (var room in currentPhase.rooms)
                {
                    var from = new Vector2(Calc.X(depth, room.line), Calc.Y(depth));

                    foreach (var nextLine in room.nextLines)
                    {
                        var to = new Vector2(Calc.X(nextDepth, nextLine), Calc.Y(nextDepth));
                        this.CreateConnect(from, to);
                    }
                }

                // 次のフェーズの部屋
                foreach (var room in currentPhase.next.rooms)
                {
                    var tile = this.CreateTile();
                    tile.Position = new Vector2(Calc.X(nextDepth, room.line), Calc.Y(nextDepth));
                    tile.transform.localScale = new Vector2(0.3f, 0.3f);
                    tile.Initialize(room, _ => { });
                }
            }
        }

        private void RenderSelectableRooms()
        {
            // 現在の部屋から移動可能な部屋
            foreach (int nextLine in this.generator.CurrentRoom.nextLines)
            {
                var tile = this.CreateTile();
                tile.Position = new Vector2(Calc.X(1, nextLine), Calc.Y(1));

                var nextRoom = this.generator.CurrentPhase.next.RoomAt(nextLine);
                tile.Initialize(nextRoom, RoomSelected);
            }
        }

        private static class Calc
        {
            public static float X(int depth, int line)
            {
                return (line - 2) * 1.3f;
            }

            public static float Y(int depth)
            {
                if (depth == 1) return -1.4f;
                else return -1.45f + (0.6f * depth);
            }
        }

        private void RoomSelected(ExploreRoom selectedRoom)
        {
            this.generator.GoTo(selectedRoom.line);
            this.Render();

            Debug.Log($"room: {selectedRoom.phase} - {selectedRoom.line}");
        }

        private RoomTile CreateTile()
        {
            var tile = this.tilePrefab.Instantiate<RoomTile>();
            this.gameObjects.Add(tile.gameObject);
            return tile;
        }

        private LineRenderer CreateConnect(Vector2 from, Vector2 to)
        {
            var connect = this.connectPrefab.Instantiate<LineRenderer>();
            this.gameObjects.Add(connect.gameObject);
            connect.SetPositions(new Vector3[] { from, to });
            return connect;
        }
    }

}