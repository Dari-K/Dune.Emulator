﻿using System;
using System.IO;
using System.Text;

namespace Dune.Utility
{
    public class BigEndianBinaryReader : BinaryReader
    {
        public BigEndianBinaryReader(Stream input)
            : base(input)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding)
            : base(input, encoding)
        {
        }

        public BigEndianBinaryReader(Stream input, Encoding encoding, bool leaveOpen)
            : base(input, encoding, leaveOpen)
        {
        }

        private byte[] Reverse(byte[] b)
        {
            Array.Reverse(b);
            return b;
        }

        private byte[] ReadBytesRequired(int byteCount)
        {
            var result = ReadBytes(byteCount);

            if (result.Length != byteCount)
                throw new EndOfStreamException(string.Format("{0} bytes required from stream, but only {1} returned.",
                    byteCount, result.Length));

            return result;
        }

        public override decimal ReadDecimal()
        {
            byte[] buffer = Reverse(ReadBytesRequired(sizeof(decimal)));
            var i1 = BitConverter.ToInt32(buffer, 0);
            var i2 = BitConverter.ToInt32(buffer, 4);
            var i3 = BitConverter.ToInt32(buffer, 8);
            var i4 = BitConverter.ToInt32(buffer, 12);
            return new decimal(new[] { i1, i2, i3, i4 });
        }

        public override double ReadDouble()
        {
            return BitConverter.ToDouble(Reverse(ReadBytesRequired(sizeof(double))), 0);
        }

        public override short ReadInt16()
        {
            return BitConverter.ToInt16(Reverse(ReadBytesRequired(sizeof(Int16))), 0);
        }

        public override int ReadInt32()
        {
            return BitConverter.ToInt32(Reverse(ReadBytesRequired(sizeof(Int32))), 0);
        }

        public override long ReadInt64()
        {
            return BitConverter.ToInt64(Reverse(ReadBytesRequired(sizeof(Int64))), 0);
        }

        public override float ReadSingle()
        {
            return BitConverter.ToSingle(Reverse(ReadBytesRequired(sizeof(float))), 0);
        }

        public override ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(Reverse(ReadBytesRequired(sizeof(UInt16))), 0);
        }

        public override uint ReadUInt32()
        {
            return BitConverter.ToUInt32(Reverse(ReadBytesRequired(sizeof(UInt32))), 0);
        }

        public override ulong ReadUInt64()
        {
            return BitConverter.ToUInt64(Reverse(ReadBytesRequired(sizeof(UInt64))), 0);
        }
    }
}