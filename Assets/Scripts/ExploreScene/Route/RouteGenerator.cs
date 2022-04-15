using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExploreScene
{
    public static class RouteConsts
    {
        /// <summary>
        /// 一度に生成しておくフェーズ数
        /// </summary>
        public const int MaxKeepPhases = 5;

        /// <summary>
        /// 休息部屋の配置間隔
        /// この数字の倍数フェーズで休息部屋が１つだけ配置されたフェーズを生成する
        /// </summary>
        public const int IntervalRestPhase = 4;

        /// <summary>
        /// 部屋が配置される位置、縦のラインの数
        /// </summary>
        public const int Lines = 3;
    }

    public class RouteGenerator
    {
        private ExplorePhase currentPhase;
        private int currentLine;

        public void Start()
        {
            var firstPhase = new ExplorePhase(1);
            this.currentPhase = firstPhase;
            this.currentLine = firstPhase.rooms.First().line;

            var addingPhase = this.currentPhase;
            for (int i = addingPhase.phase + 1; i <= RouteConsts.MaxKeepPhases; i++)
            {
                addingPhase = addingPhase.AddNext();
            }
        }

        public ExplorePhase CurrentPhase { get => this.currentPhase; }

        public ExploreRoom CurrentRoom { get => this.CurrentPhase.RoomAt(currentLine); }

        public void GoTo(int nextLine)
        {
            this.currentPhase = this.currentPhase.next;
            this.currentLine = nextLine;
            this.currentPhase.GetLastPhase().AddNext();
        }
    }

    /// <summary>
    /// フェーズ
    /// </summary>
    public class ExplorePhase
    {
        public readonly int phase;

        public readonly List<ExploreRoom> rooms;

        public ExplorePhase next;

        public ExplorePhase(int phase)
        {
            this.phase = phase;
            this.rooms = CreateRooms(phase).ToList();
        }

        public bool IsSingleRoom { get => this.rooms.Count() == 1; }

        

        /// <summary>
        /// 部屋を作成する
        /// </summary>
        /// <param name="phase"></param>
        /// <returns></returns>
        private static IEnumerable<ExploreRoom> CreateRooms(int phase)
        {
            if ((phase - 1) % RouteConsts.IntervalRestPhase == 0)
            {
                return new[] { new ExploreRoom(phase, 2) };
            }

            int roomsCount = DecideRoomsCount();

            switch (roomsCount)
            {
                case 1:
                    return new[] { new ExploreRoom(phase, 2) };
                case 2:
                    float rand = UnityEngine.Random.value;
                    if (rand < 0.4f) return new[] { new ExploreRoom(phase, 1), new ExploreRoom(phase, 2) };
                    if (rand < 0.8f) return new[] { new ExploreRoom(phase, 2), new ExploreRoom(phase, 3) };
                    return new[] { new ExploreRoom(phase, 1), new ExploreRoom(phase, 3) };
                case 3:
                    return new[] { new ExploreRoom(phase, 1), new ExploreRoom(phase, 2), new ExploreRoom(phase, 3) };
                default:
                    throw new NotImplementedException();
            }
        }

        private static int DecideRoomsCount()
        {
            float rand = UnityEngine.Random.value;

            if (rand < 0.2f) return 1;
            if (rand < 0.7f) return 2;
            return 3;
        }

        public ExplorePhase AddNext()
        {
            var nextPhase = new ExplorePhase(this.phase + 1);
            this.ConnectToNext(nextPhase);

            return nextPhase;
        }

        private void ConnectToNext(ExplorePhase next)
        {
            this.next = next;

            if (this.IsSingleRoom)
            {
                this.rooms.First().ConnectTo(next.rooms);
                return;
            }

            if (next.IsSingleRoom)
            {
                foreach (var r in this.rooms) r.ConnectTo(next.rooms.First());
                return;
            }

            // 両端は確定
            this.rooms.First().ConnectTo(next.rooms.First());
            this.rooms.Last().ConnectTo(next.rooms.Last());

            // 1-2, 2-3
            foreach (var pair in new [] { new [] { 1, 2 }, new[] { 2, 3 } })
            {
                int left = pair[0];
                int right = pair[1];
                if (this.HasLines(left, right) && next.HasLines(left, right))
                {
                    Probable.AtMostOne()
                        .Case(0.4f, () => this.RoomAt(left).ConnectTo(right))
                        .Case(0.4f, () => this.RoomAt(right).ConnectTo(left));
                }
            }

            // 中央の部屋の接続チェック
            // 両フェーズに中央あり
            if (this.HasLine(2) && next.HasLine(2))
            {
                if (this.RoomAt(2).IsNoConnect || !this.HasConnectTo(2))
                {
                    this.RoomAt(2).ConnectTo(2);
                }
                else
                {
                    Probable.Single(0.5f, () => this.RoomAt(2).ConnectTo(2));
                }
            }

            // 現在フェーズに中央あるが次フェーズは両端のみ
            else if (this.HasLine(2) && this.RoomAt(2).IsNoConnect)
            {
                Probable.AtMostOne()
                    .Case(0.5f, () => this.RoomAt(2).ConnectTo(1))
                    .Case(1.0f, () => this.RoomAt(2).ConnectTo(3));
            }

            // 次フェーズに中央あるが現在フェーズは両端のみ
            else if (next.HasLine(2) && !this.HasConnectTo(2))
            {
                Probable.AtMostOne()
                    .Case(0.5f, () => this.RoomAt(1).ConnectTo(2))
                    .Case(1.0f, () => this.RoomAt(3).ConnectTo(2));
            }
        }

        private bool HasLine(int line)
        {
            return this.rooms.Any(r => r.line == line);
        }

        /// <summary>
        /// line1とline2の両方に部屋があればtrue
        /// </summary>
        /// <param name="line1"></param>
        /// <param name="line2"></param>
        /// <returns></returns>
        private bool HasLines(int line1, int line2)
        {
            return this.HasLine(line1) && this.HasLine(line2);
        }

        private bool HasConnectTo(int line)
        {
            return this.rooms.Where(r => r.nextLines.Any(n => n == line)).Any();
        }

        public ExploreRoom RoomAt(int line)
        {
            return this.rooms.Where(r => r.line == line).First();
        }

        public ExplorePhase GetLastPhase()
        {
            if (this.next == null) return this;
            return this.next.GetLastPhase();
        }
    }

    /// <summary>
    /// 部屋
    /// </summary>
    public class ExploreRoom
    {
        public readonly int phase;
        public readonly int line;
        public readonly List<int> nextLines;

        public ExploreRoom(int phase, int line)
        {
            this.phase = phase;
            this.line = line;
            this.nextLines = new List<int>();
        }

        public bool IsNoConnect { get => this.nextLines.Count() == 0; }

        public bool IsConnectable(int fromLine)
        {
            return Math.Abs(this.line - fromLine) <= 1;
        }

        public void ConnectTo(IEnumerable<ExploreRoom> nextRooms)
        {
            foreach (var nr in nextRooms) this.ConnectTo(nr);
        }

        public void ConnectTo(ExploreRoom nextRoom)
        {
            this.ConnectTo(nextRoom.line);
        }

        public void ConnectTo(int line)
        {
            this.nextLines.Add(line);
        }
    }
}