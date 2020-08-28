using System;
using System.Buffers;
using System.Buffers.Text;
using System.IO;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;
using FastBFF.HTTP.Server.Models;

namespace FastBFF.HTTP.Server.Controllers
{

    public static class ResponseExtensions
    {
        public static ReadOnlyMemory<byte> StartObject = new byte[] { (byte)'{' };
        public static ReadOnlyMemory<byte> EndObject = new byte[] { (byte)'}' };
        public static ReadOnlyMemory<byte> DoubleQuote = new byte[] { (byte)'"' };
        public static ReadOnlyMemory<byte> Comma = new byte[] { (byte)',' };
        public static ReadOnlyMemory<byte> Column = new byte[] { (byte)':' };
        public static byte[] NullValue;

        public static readonly ReadOnlyMemory<byte> AccountName;
        public static readonly ReadOnlyMemory<byte> BalanceName;
        public static readonly ReadOnlyMemory<byte> OrdersName;


        static ResponseExtensions()
        {
            var encoder = Encoding.UTF8.GetEncoder();


            GetPropertyName(encoder, nameof(AccountSummaryClass.Account), ref AccountName);
            GetPropertyName(encoder, nameof(AccountSummaryClass.Balance), ref BalanceName);
            GetPropertyName(encoder, nameof(AccountSummaryClass.Orders), ref OrdersName);

        }


        public static void WriteProperty(this PipeWriter pipe, ReadOnlyMemory<byte> property, int value)
        {
            pipe.Write(property.Span);

            Utf8Formatter.TryFormat(value, pipe.GetSpan(8), out int bytesWritten);

            pipe.Advance(bytesWritten);
        }

        public static void WriteProperty(this PipeWriter pipe, ReadOnlyMemory<byte> property, ReadOnlyMemory<byte> value)
        {
            pipe.Write(property.Span);

            pipe.Write(value.Span);
        }

        public static async ValueTask WriteProperty(this Stream stream, ReadOnlyMemory<byte> property, int value)
        {
            await stream.WriteAsync(property);

            var buffer = new byte[8];

            Utf8Formatter.TryFormat(value, buffer, out int bytesWritten);

            await stream.WriteAsync(buffer, 0, bytesWritten);
        }

        public static async ValueTask WriteProperty(this Stream stream, ReadOnlyMemory<byte> property, ReadOnlyMemory<byte> value)
        {
            await stream.WriteAsync(property);

            await stream.WriteAsync(value);
        }


        private static void GetPropertyName(Encoder encoder, string value, ref ReadOnlyMemory<byte> memory)
        {
            var chars = value.ToCharArray();
            var buffer = new byte[encoder.GetByteCount(chars, true) + 3];

            var quoteSpan = DoubleQuote.Span;


            buffer[0] = quoteSpan[0];

            var count = encoder.GetBytes(chars, 0, chars.Length, buffer, 1, true);

            count++;

            buffer[count++] = quoteSpan[0];
            buffer[count++] = Column.Span[0];

            memory = buffer;
        }

    }
}
