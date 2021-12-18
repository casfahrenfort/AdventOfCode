using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021
{
    public class Day16 : Day
    {
        public Day16()
            : base(16, true)
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
        public BasePacket Parse(string binary)
        {
            int version = Convert.ToInt32(binary.Substring(0, 3), 2);
            int type = Convert.ToInt32(binary.Substring(3, 3), 2);

            if (type == 4)
            {
                string literalPrefixedBinary = binary.Substring(6, binary.Length - 6);
                return new LiteralPacket(version, type, LiteralValue(literalPrefixedBinary));
            }
            else
            {
                char lengthType = binary[6];
                int length = lengthType == '0' ? 15 : 11;
                if (length == 15)
                {
                    int subPacketLength = Convert.ToInt32(binary.Substring(7, length), 2);
                    string subPacketBinary = binary.Substring(7 + length, subPacketLength);
                    return new OperatorPacket(1, 1, new List<BasePacket>());
                }
                else
                {
                    int subPacketCount = Convert.ToInt32(binary.Substring(7, length), 2);
                    string subPacketsBinary = binary.Substring(7 + length, +subPacketCount * 11);
                    List<BasePacket> subPackets = new List<BasePacket>();
                    for (int i = 0; i < subPacketsBinary.Length; i += 11)
                    {
                        subPackets.Add(Parse(subPacketsBinary.Substring(i, 11)));
                    }
                    return new OperatorPacket(version, type, subPackets);
                }
            }
        }

        private int LiteralValue(string binary)
        {
            var chunks = WholeChunks(binary, 5).ToList();
            string literalBinary = "";
            for (int i = 0; i < chunks.Count; i++)
            {
                if (chunks[i].Length < 5)
                    continue;
                literalBinary += chunks[i].Substring(1, 4);
            }
            return Convert.ToInt32(literalBinary, 2);
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
    }

    public class LiteralPacket : BasePacket
    {
        public int value;

        public LiteralPacket(int version, int type, int value)
            : base(version, type)
        {
            this.value = value;
        }
    }

    public class OperatorPacket : BasePacket
    {
        public List<BasePacket> subPackets;

        public OperatorPacket(int version, int type, List<BasePacket> subPackets)
            : base(version, type)
        {
            this.subPackets = subPackets;
            this.version = version + subPackets.Sum(x => x.version);
        }
    }
}
