using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace AoC2022
{
    public class Day13 : Day
    {
        public Day13()
            : base(13, true)
        { }

        public override void Solve()
        {
            /*input.Sublists(3)
                .Take(2)
                .Select(line =>
                {
                    var packet = new Packet();
                    JsonConvert.DeserializeObject(line)
                })*/
        }
    }

    public class Packet
    {
        PacketItem Item { get; set; }
    }

    public class PacketItem
    {

    }

    public class PacketList : PacketItem
    {
        public List<PacketItem> PacketItems { get; set; }
    }

    public class PacketInt : PacketItem
    {
        public int Value { get; set; }
    }
}
