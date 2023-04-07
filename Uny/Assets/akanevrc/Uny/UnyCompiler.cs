using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace akanevrc.Uny
{
    public static class UnyCompiler
    {
        public static void Compile(string code)
        {
            File.WriteAllText("./program.rs", code, Encoding.UTF8);

            var proc = Process.Start(new ProcessStartInfo("rustc", "--target wasm32-unknown-unknown --crate-type cdylib -o ./runner.wasm ./runner.rs"));
            proc.WaitForExit();
            
            if (proc.ExitCode != 0) throw new InvalidOperationException("Failed to compile");
        }
    }
}
