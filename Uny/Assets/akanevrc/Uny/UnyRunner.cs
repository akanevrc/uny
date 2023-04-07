using System.Text;
using UnityEngine;
using Wasmtime;

namespace akanevrc.Uny
{
    public class UnyRunner
    {
        private Memory Memory { get; set; }

        public void Run()
        {
            using var engine = new Engine();
            using var module = Module.FromFile(engine, "./runner.wasm");
            using var linker = new Linker(engine);
            using var store = new Store(engine);

            linker.Define(
                "env",
                "__debug_log",
                Function.FromCallback<int, int>(store, (ptr, len) => Debug.Log(ReadMemoryAsString(ptr, len)))
            );

            var instance = linker.Instantiate(store, module);
            Memory = instance.GetMemory("memory")!;
            var run = instance.GetAction("run")!;
            run();
            
            Memory = null;
        }

        private string ReadMemoryAsString(int ptr, int len)
        {
            var address = (long)unchecked((uint)ptr);
            return Memory.ReadString(address, len, Encoding.UTF8);
        }
    }
}
