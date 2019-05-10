using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using utcAltkomDevices.FakeServices;
using utcAltkomDevices.IServices;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.ConsoleApp
{
    class Program
    {
        //To use async entrypoints, you need to use newer than 7.0 c# (7.1 for example)
        static void Main(string[] args)
        {
            //DoServiceThingy();

            Console.WriteLine("Main started...");
            var prg = new Program();
            prg.Tasks();

            Console.ReadKey();
        }

        private void Tasks()
        {
            Console.WriteLine("Starting tasks...");

            //You can do it like this in c# 7.1/newer
            //CancellationToken token = default
            //instead of:
            //  default(type)
            CancellationTokenSource sourcetoken = new CancellationTokenSource();
            //sourcetoken.CancelAfter(TimeSpan.FromSeconds(1));

            CancellationToken token = sourcetoken.Token;

            IProgress<int> progress =  new Progress<int>(x=>Console.Write(x+"%"));

            ConcurrentStack<double> stack = new ConcurrentStack<double>();
            for (int i = 0; i < 100; i++)
            {
                //To make sure reference to "i" is not sent, instead a new value;
                var temp = i;
                Console.WriteLine($"Task {i} start");

                //Task task = new Task(()=> { stack.Push(Calc(i)); });
                //task.Start();
                //Or easier just do:
                //Task returned = Task.Run(() => { stack.Push(Calc(i)); });
                //Or even better:
                //Task<double> task = Task.Run(() => Calc(i));

                //This will force wait for thread 
                //Console.WriteLine(task.Result);
                //This one will be executed after task has finished
                //task.ContinueWith(x => { Console.WriteLine($"Im done with {i}"); stack.Push(task.Result); });

                //The prettier
                //CalcAsync(i).ContinueWith(x => { Console.WriteLine($"Im done with {temp}"); stack.Push(x.Result); });


                //The prietiest ^-^
                AsyncAwaitKeepCalc(temp, token, progress);
            }
        }
        private double Calc(double amt)
        {
            var result = amt * 0.1;

            Console.WriteLine($"Calculated {result}");
            return result;
        }

        private Task<double> CalcAsync(double amt)
        {
            return Task.Run(() => Calc(amt));
        }
        //If you want to catch exceptions you need to return tasks, void wont do
        private async void AsyncAwaitCalc(double amt, CancellationToken token = default(CancellationToken))
        {
            //token.ThrowIfCancellationRequested();
            // Or do :
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"I stopped while doing {amt}");
                return;
            }
            double result = await CalcAsync(amt);
            Console.WriteLine($"Im done with {amt}");
        }

        private async void AsyncAwaitKeepCalc(double amt, CancellationToken token = default(CancellationToken), IProgress<int> progress = null)
        {
            //token.ThrowIfCancellationRequested();
            // Or do :
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"I stopped while doing {amt}");
                    return;
                }
                progress.Report(i);
            }

            double result = await CalcAsync(amt);
            Console.WriteLine($"Im done with {amt}");
        }

        private static void DoServiceThingy()
        {
            IEntityServices<Device> service = new FakeDeviceService(null,new DeviceFaker());

            var list = service.Get();

            foreach (var device in list)
            {
                Console.WriteLine(device.ToString());
            }
        }


    }
}
