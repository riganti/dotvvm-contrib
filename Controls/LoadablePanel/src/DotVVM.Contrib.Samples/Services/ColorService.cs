using System;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Contrib.Samples.Services
{
    public class ColorService
    {
        [AllowStaticCommand]
        public async Task<string> LoadData1()
        {
            await Task.Delay(GetRandomDelay());
            return "black";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData2()
        {
            await Task.Delay(GetRandomDelay());
            return "red";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData3()
        {
            await Task.Delay(GetRandomDelay());
            return "blue";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData4()
        {
            await Task.Delay(GetRandomDelay());
            return "green";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData5()
        {
            await Task.Delay(GetRandomDelay());
            return "yellow";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData6()
        {
            await Task.Delay(GetRandomDelay());
            return "gray";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData7()
        {
            await Task.Delay(GetRandomDelay());
            return "darkblue";
        }

        [AllowStaticCommand]
        public async Task<string> LoadData8()
        {
            await Task.Delay(GetRandomDelay());
            return "purple";
        }

        private int GetRandomDelay()
        {
            return new Random((int)DateTime.Now.Ticks).Next(200, 5000);
        }
    }
}

