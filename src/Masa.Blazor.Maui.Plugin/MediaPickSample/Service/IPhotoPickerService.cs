using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPickSample.Service
{
    public interface IPhotoPickerService
    {
        Task<Dictionary<string, string>> GetImageAsync1();
        Task<Dictionary<string, string>> GetImageAsync2();
        Task<Dictionary<string, string>> GetImageAsync3();
    }
}
