using System;
using System.Diagnostics;
using System.IO;

namespace Netch.Controllers
{
    public class MainController
    {
        public static Process GetProcess()
        {
            var process = new Process();
            process.StartInfo.WorkingDirectory = String.Format("{0}\\bin", Directory.GetCurrentDirectory());
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.EnableRaisingEvents = true;

            return process;
        }
        /// <summary>
        ///     SSR 控制器
        /// </summary>
        public SSRController pSSRController = new SSRController();


        /// <summary>
        ///     TUN/TAP 控制器
        /// </summary>
        public TUNTAPController pTUNTAPController = new TUNTAPController();

        /// <summary>
        ///		启动
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="mode">模式</param>
        /// <returns>是否启动成功</returns>
        public bool Start(Objects.Server server, Objects.Mode mode)
        {
            var result = false;
            switch (server.Type)
            {
                //case "Socks5":
                //    result = true;
                //    System.Windows.Forms.MessageBox.Show("Socks5");
                //    break;
                //case "Shadowsocks":
                //    result = pSSController.Start(server, mode);
                //    System.Windows.Forms.MessageBox.Show("Shadowsocks");
                //    break;
                case "ShadowsocksR":
                    result = pSSRController.Start(server, mode);
                    //System.Windows.Forms.MessageBox.Show("ShadowsocksR");
                    break;
                //case "VMess":
                //    result = pVMessController.Start(server, mode);
                //    System.Windows.Forms.MessageBox.Show("VMess");
                //    break;
                default:
                    break;
            }

            if (result)
            {
                if (mode.Type == 1)
                {
                    // TUN/TAP 全局代理模式，启动 TUN/TAP 控制器
                    result = pTUNTAPController.Start(server, mode);
                }
                else if (mode.Type == 2)
                {
                    // TUN/TAP 全局代理模式，启动 TUN/TAP 控制器
                    result = pTUNTAPController.Start(server, mode);
                }
                else
                {
                    result = false;
                }
            }

            if (!result)
            {
                Stop();
            }

            return result;
        }

        /// <summary>
        ///		停止
        /// </summary>
        public void Stop()
        {

            pSSRController.Stop();
            pTUNTAPController.Stop();
        }
    }
}
