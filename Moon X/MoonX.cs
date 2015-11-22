using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX.DirectInput;

namespace Moon_X
{
    public class MoonX
    {
        public static MoonX instance = null;
        public MoonX()
        {
            instance = this;
        }
    }

    public class Controller
    {
        public static List<Controller> GetControllers()
        {
            List<Controller> controllers = new List<Controller>();
            foreach (DeviceInstance di in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                controllers.Add(new Controller(new Device(di.InstanceGuid)));
                break;
            }
            return controllers;
        }

        public Device device;
        public Controller(Device device)
        {
            this.device = device;
        }
    }
}
