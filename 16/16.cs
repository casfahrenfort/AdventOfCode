using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day16 : Day
    {
        public Day16()
            : base(16, false)
        { }

        public override void Solve()
        {
            var parser = new PacketParser();

            var packets = input
                .Select(i => string.Join("", i.Select(HexToBin)))
                .Select(parser.Parse)
                .ToList();
        }

        private string HexToBin(char c)
        {
            switch (c)
            {
                case '0': return "0000";
                case '1': return "0001";
                case '2': return "0010";
                case '3': return "0011";
                case '4': return "0100";
                case '5': return "0101";
                case '6': return "0110";
                case '7': return "0111";
                case '8': return "1000";
                case '9': return "1001";
                case 'A': return "1010";
                case 'B': return "1011";
                case 'C': return "1100";
                case 'D': return "1101";
                case 'E': return "1110";
                case 'F': return "1111";
                default: throw new Exception("Invalid hex char");
            }
        }
    }

    public class PacketParser
    {
        public (List<BasePacket>, int) Parse(string binary, int nrOfPackets = -1)
        {
            var packets = new List<BasePacket>();
            int p = 0;
            while (p < binary.Length - 6)
            {
                if (binary.Substring(p, binary.Length - p).All(x => x == '0'))
                    break;

                int version = Convert.ToInt32(binary.Substring(p, 3), 2);
                int type = Convert.ToInt32(binary.Substring(p + 3, 3), 2);

                if (type == 4)
                {
                    (long value, int index) = LiteralValue(binary.Substring(p + 6, binary.Length - (p + 6)));
                    packets.Add(new LiteralPacket(version, type, value));
                    p = p + 6 + (index + 1) * 5;
                }
                else
                {
                    char lengthType = binary[p + 6];
                    int length = lengthType == '0' ? 15 : 11;
                    List<BasePacket> subPackets = new List<BasePacket>();
                    if (length == 15)
                    {
                        int subPacketLength = Convert.ToInt32(binary.Substring(p + 7, length), 2);
                        string subPacketBinary = binary.Substring(p + 7 + length, subPacketLength);
                        subPackets.AddRange(Parse(subPacketBinary).Item1);
                        p = p + 7 + length + subPacketBinary.Length;
                    }
                    else
                    {
                        int subPacketCount = Convert.ToInt32(binary.Substring(p + 7, length), 2);
                        string subPacketBinary = binary.Substring(p + 7 + length, binary.Length - (p + 7 + length));
                        (var subs, int subBinaryTermination) = Parse(subPacketBinary, subPacketCount);
                        subPackets.AddRange(subs);
                        p = p + 7 + length + subBinaryTermination;
                    }
                    packets.Add(new OperatorPacket(version, type, subPackets));
                }
                if (nrOfPackets != -1 && packets.Count == nrOfPackets)
                    break;
            }


            return (packets, p);
        }

        private (long, int) LiteralValue(string binary)
        {
            var chunks = WholeChunks(binary, 5).ToList();
            string literalBinary = "";
            int finalPacketIndex = -1;
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i][0] == '0')
                {
                    finalPacketIndex = i;
                    literalBinary += chunks[i].Substring(1, 4);
                    break;
                }
                literalBinary += chunks[i].Substring(1, 4);
            }
            return (Convert.ToInt64(literalBinary, 2), finalPacketIndex);
        }

        private IEnumerable<string> WholeChunks(string str, int chunkSize)
        {
            for (int i = 0; i < str.Length - 1; i += chunkSize)
            {
                if (i + chunkSize > str.Length)
                    yield return str.Substring(i, (str.Length - 1) - i);
                else
                    yield return str.Substring(i, chunkSize);
            }
        }
    }

    public abstract class BasePacket
    {
        public int version;
        public int type;

        public BasePacket(int version, int type)
        {
            this.version = version;
            this.type = type;
        }

        public abstract long Value { get; }

        public abstract int Version { get; }
    }

    public class LiteralPacket : BasePacket
    {
        public long value;

        public LiteralPacket(int version, int type, long value)
            : base(version, type)
        {
            this.value = value;
        }

        public override long Value
        {
            get { return value; }
        }

        public override int Version
        {
            get { return version; }
        }
    }

    public class OperatorPacket : BasePacket
    {
        public List<BasePacket> subPackets;

        public OperatorPacket(int version, int type, List<BasePacket> subPackets)
            : base(version, type)
        {
            this.subPackets = subPackets;
            this.version = version;
        }

        public override long Value
        {
            get
            {
                switch (type)
                {
                    case 0: return subPackets.Sum(x => x.Value);
                    case 1: return subPackets.Aggregate((long)1, (total, next) => total * next.Value);
                    case 2: return subPackets.Min(x => x.Value);
                    case 3: return subPackets.Max(x => x.Value);
                    case 5: return subPackets[0].Value > subPackets[1].Value ? 1 : 0;
                    case 6: return subPackets[0].Value < subPackets[1].Value ? 1 : 0;
                    case 7: return subPackets[0].Value == subPackets[1].Value ? 1 : 0;
                    default: throw new Exception("Invalid type");
                }
            }
        }

        public override int Version
        {
            get { return version + subPackets.Sum(x => x.version); }
        }
    }
}
