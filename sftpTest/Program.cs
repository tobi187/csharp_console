using System;
using System.Linq;
using Renci.SshNet;
using Renci.SshNet.Sftp;
using System.IO;
using PowerArgs;

namespace sftpTest
{
    [TabCompletion]
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
    public class ParseArgs
    {
        [ArgShortcut(ArgShortcutPolicy.NoShortcut),
            ArgRequired,
            ArgDescription("Adresse des Server"),
            ArgPosition(0)]
        public string? Server { get; set; }

        [ArgRequired(PromptIfMissing =true),
            ArgShortcut("-x")]
        public string? Protokoll { get; set; }
        [ArgRequired]
        public string path { get; set; }

        [ArgRequired]
        [ArgShortcut("-s")]
        public string Pattern { get; set; }
        [ArgShortcut("-a")]
        public string BasicAuth { get; set; }
        [ArgShortcut("-k")]
        public string KeyFile { get; set; }
        public int Port { get; set; }
        public bool Test { get; set; } 

    }

    public static class SftpTests
    {
        static void Main(string[] args)
        {
            /*ParseArgs command;
            try
            {
                command = Args.Parse<ParseArgs>(args);
            }
            catch (ArgException e) { throw new ArgException(e.Message); }*/

            (string user, string pass) = decodePass("tobi:nikelef123");

            ftpConnection connection = new ftpConnection(user, pass, "127.0.0.1");

            connection.DeleteFiles("txt", "/test", 3, false);

        }

        static Tuple<string, string> decodePass(string userInfo)
        {
            var i = userInfo.Split(":");
            return Tuple.Create(i[0], i[1]);
        }
    }
}