using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ScreensaverService
{
    public partial class ScreensaverService : ServiceBase
    {

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSRegisterSessionNotification(IntPtr hWnd, int dwFlags);

        [DllImport("wtsapi32.dll")]
        private static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);
        private const int NotifyForThisSession = 0; // This session only
        private const int SessionChangeMessage = 0x02B1;
        private const int SessionLockParam = 0x7;
        private const int SessionUnlockParam = 0x8;
        public static System.Timers.Timer timer;
        bool alreadyAskedPermission = false;
        bool isBusy = false;


        public ScreensaverService()
        {
           
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WTSRegisterSessionNotification(this.ServiceHandle, NotifyForThisSession);
                askForWritePermission();
                Class.WriteErrorLog("After Permission granted");
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("2" + ex.Message);
            }
        }

        protected override void OnStop()
        {
        }
        protected void WndProc(int m, int i)
        {
            try
            {
                // check for session change notifications
                m = 689;
                if (m == SessionChangeMessage)
                {
                    Class.WriteErrorLog("before Scrren Lock" + SessionChangeMessage);
                    i = 689;
                    if (i == SessionLockParam)
                    {
                        OnSessionLock(); // Do something when locked
                        Class.WriteErrorLog("After Scrren Lock" + SessionChangeMessage);
                    }
                    else if (i == SessionUnlockParam)
                        OnSessionUnlock(); // Do something when unlocked
                }
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("4" + ex.Message);
            }

            WndProc(m, i);
            return;
        }

        public void OnSessionLock()
        {
            try
            {
                timer = new System.Timers.Timer(1000);
                // Hook up the Elapsed event for the timer.
                timer.Elapsed += new ElapsedEventHandler(ShowPopUp);
                // Set the Interval to 5 seconds (2000 milliseconds).
                timer.Interval = 1000;
                timer.Enabled = true;

                Debug.WriteLine("Locked...");
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("5" + ex.Message);
            }
            //label1.Text = label1.Text.ToString() + "Locked";
        }
        public void OnSessionUnlock()
        {
            Debug.WriteLine("Unlocked...");
            //label1.Text = label1.Text.ToString() + "Unlocked";
        }
        public void ShowPopUp(object source, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                applyWallpaper();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("6" + ex.Message);
            }
        }
        #region Screen Saver Changer
        void launchEnableCmd()
        {
            try
            {
                launchCmd(true);
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("7" + ex.Message);
            }
        }
        /// <summary>
        /// Execute as administrator the cmd file that enable the login screen change if enable == true.
        /// Otherwise, execute the cmd file that disable the login screen change.
        /// Shows an UAC prompt.
        /// </summary>
        bool launchCmd(bool enable)
        {

            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                if (enable)
                    p.StartInfo.Arguments = "/C \"" + path + "\\enable background change\"";
                else
                    p.StartInfo.Arguments = "/C \"" + path + "\\disable background change\"";
                p.StartInfo.Verb = "runas"; //shows the uac prompt
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                p.Start();
                alreadyAskedPermission = true;
                return true;
            }
            catch (Exception)
            {
                //  showMessage(Util.getText("error"), Util.getText("operationCanceled"), "");
            }
            return false;
        }
        // public delegate void copyHandler();
        /// <summary>
        /// Callback for the end of the cmd file execution. Tries again to apply the background.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void p_Exited(object sender, EventArgs e)
        {
            try
            {
                applyWallpaper();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("8" + ex.Message);
            }
            // Dispatcher.Invoke(new copyHandler(applyWallpaper));
        }
        private void askForWritePermission()
        {
            isBusy = false;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                ResourcesCreation.createCmdFile();
                launchEnableCmd();
            }
            catch (Exception ex)
            {
            }


        }
        void applyWallpaper()
        {
            isBusy = true;

            try
            {
                string systemPath = Environment.GetFolderPath(Environment.SpecialFolder.System);
                DirectoryInfo di = new DirectoryInfo(systemPath + @"\oobe\info\backgrounds\");
                if (di.Exists)
                {
                    Class.WriteErrorLog("inside  Di.exist");
                    ImagesCreation imgCreation = new ImagesCreation(@"D:\folder\Test.jpg");
                    BackgroundWorker bgThread = new BackgroundWorker();
                    bgThread.ProgressChanged += new ProgressChangedEventHandler(bgThread_ProgressChanged);//as for permissi
                    bgThread.WorkerReportsProgress = true;
                    bgThread.DoWork += new DoWorkEventHandler(imgCreation.run);
                    bgThread.RunWorkerAsync();
                    Class.WriteErrorLog("end ofinsdie  Di.exist");
                }
                else
                {// askForWritePermission(); } //if the backgrounds directory does not exist
                    //  
                }
            }
            catch (Exception ex)
            {
                //showMessage(Util.getText("error"), ex.Message, "");
                isBusy = false;
            }

        }
        void bgThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        #endregion
    }
    class ImagesCreation
    {

        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        const int WTS_CURRENT_SESSION = -1;
        static readonly IntPtr WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;

        public void SwithEvent()
        {

            if (!WTSDisconnectSession(WTS_CURRENT_SERVER_HANDLE,
                 WTS_CURRENT_SESSION, false))

                SwithEvents();
        }

        public void SwithEvents()
        { }

        /// <summary>
        /// Resolutions of the pictures that the Logon UI is going to look for. 
        /// Note : the vertical wallpapers are useful for portrait screens (i.e Tablet PCs, ...)
        /// </summary>
        public static int[,] imgResolutions = new int[,] { 
                                                    //{ 1280, 1024 },
                                                    { 1280, 960 },
                                                    { 1024, 768 },
                                                    { 1600, 1200 },
                                                    { 1440, 900 },
                                                    { 1920, 1200 },
                                                    { 1280, 768 },
                                                    { 1360, 768 },
                                                    { 1024, 1280 },
                                                    { 960, 1280 },
                                                    { 900, 1440 },
                                                    { 768, 1280 },
                                                    { 768, 1360 }
                                                };

        /// <summary>
        /// Path of the original picture
        /// </summary>
        string imgPath;

        public ImagesCreation(string imgPath)
        {
            try
            {
                this.imgPath = imgPath;
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("9" + ex.Message);
            }
        }

        /// <summary>
        /// Creates the background at each supported resolution
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void run(object sender, DoWorkEventArgs args)
        {
            Thread.Sleep(200);
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            try
            {
                CachedBitmap bmpSrc = new CachedBitmap(new BitmapImage(new Uri(imgPath)), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);

                for (int i = 0; i < imgResolutions.GetLength(0); i++)
                {
                    int w = imgResolutions[i, 0];
                    int h = imgResolutions[i, 1];
                    createImage(bmpSrc, w, h, "background" + w + "x" + h + ".jpg");
                    bgWorker.ReportProgress(i + 1);
                }

                int screenWidth = (int)SystemParameters.PrimaryScreenWidth;
                int screenHeight = (int)SystemParameters.PrimaryScreenHeight;
                createImage(bmpSrc, screenWidth, screenHeight, "backgroundDefault.jpg");

                bgWorker.ReportProgress(100); // 100 means we have finished, no userState parameter means the operation has been successful
                Class.WriteErrorLog("end of bgWorker.ReportProgress");
                SwithEvent();
            }
            catch (Exception ex)
            {

                bgWorker.ReportProgress(100, ex); // userState parameter has been defined -> operation failed
            }
        }


        /// <summary>
        /// Creates a jpg file which is as close as sub 256kb as possible. (don't know if the limit is 256kb or 256 000 bytes)
        /// Truncates it if the ratio is not the same as the resolution needed
        /// </summary>
        /// <param name="bmpSrc">original bitmap</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <param name="filename">destination file name</param>
        private void createImage(BitmapSource bmpSrc, int w, int h, string filename)
        {
            try
            {
                int srcW = bmpSrc.PixelWidth;
                int srcH = bmpSrc.PixelHeight;

                double ratioSrc = (double)srcW / srcH;
                double ratioDest = (double)w / h;

                int newW;
                int newH;
                if (ratioDest < ratioSrc)
                {
                    newH = srcH;
                    newW = (int)(srcW / ratioSrc * ratioDest);
                }
                else
                {
                    newW = srcW;
                    newH = (int)(srcH * ratioSrc / ratioDest);
                }

                CroppedBitmap bmp = new CroppedBitmap(bmpSrc,
                    new Int32Rect((srcW - newW) / 2, (srcH - newH) / 2, newW, newH));

                DrawingVisual drawingVisual = new DrawingVisual();
                DrawingContext drawingContext = drawingVisual.RenderOpen();
                drawingContext.DrawImage(bmp, new Rect(0, 0, w, h));
                drawingContext.Close();

                //Allows us to do a bitmap rendering of a WPF Visual
                RenderTargetBitmap tg = new RenderTargetBitmap(w, h, 96, 96, PixelFormats.Pbgra32);
                tg.Render(drawingVisual);

                bool found = false; //has the best quality possible been found?
                MemoryStream ms = null;

                int quality = 94;
                char step = 'd'; //d = decrease quality, i = increase, e = end
                int delta = -8;
                int topInterval = 101;
                while (!found)
                {
                    if (ms != null)
                        ms.Close();
                    ms = new MemoryStream();

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    quality += delta;
                    encoder.QualityLevel = quality;
                    encoder.Frames.Add(BitmapFrame.Create(tg));
                    encoder.Save(ms);
                    long pos = ms.Position;
                    if (pos < 256 * 1000) //the image file size is small enough
                    {
                        if (quality == 100)
                            found = true;
                        else if (step == 'd') // maybe we decreased the quality too much
                        {
                            delta = Math.Max(1, Math.Abs(delta / 2));
                            step = 'i'; // increasing quality
                        }
                        else if (step == 'i')
                        {
                            delta = Math.Max(1, Math.Abs(delta / 2));
                            if (delta == 1 && topInterval == quality + delta)
                                found = true;
                        }
                        else if (step == 'e') //finished
                            found = true;
                    }
                    else //the image file size is too big
                    {
                        if (step == 'i' && delta == 1) // quality is just 1 unit above the best quality 
                        // we can afford to keep a sub 256kb image
                        {
                            step = 'e'; // the search of the best quality is finished
                            delta = -1;
                        }
                        else if (delta != -8)
                        {
                            delta = -Math.Max(1, Math.Abs(delta / 2));
                            step = 'd';
                        }
                        topInterval = quality;
                        // if delta == initial delta (-8) -> we haven't increased yet, we can continue descreasing with a large delta
                    }
                }

                //writes the file : may fail if the user has not the right to create a file. In this case an exception
                //will be thrown and catched by the application. The user will then be prompted by an UAC window, and the operation
                //will be launched again.
                ms.Position = 0;
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\oobe\\info\\backgrounds\\";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                FileStream fs = new FileStream(folder + filename, FileMode.OpenOrCreate, FileAccess.Write);
                fs.SetLength(0); //truncates the file if it already exists
                int octet;
                while ((octet = ms.ReadByte()) != -1)
                    fs.WriteByte((byte)octet);
                fs.Close();
                ms.Close();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("10" + ex.Message);
            }
        }

    }
    class ResourcesCreation
    {
         
        /// <summary>
        /// Creates the "enable background change.cmd" file.
        /// It contains instructions to create empty "background*.jpg" files with the write right for all authenticated users.
        /// This allows limited users to modifiy the content of the "background*.jpg" files.
        /// </summary>
        public static void createCmdFile()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream strm = assembly.GetManifestResourceStream("ScreensaverService.setup.cmd");
                StreamReader sr = new StreamReader(strm);
                string sCmd = sr.ReadToEnd();
                strm.Close();

                string sFiles = "backgroundDefault.jpg";
                for (int i = 0; i < ImagesCreation.imgResolutions.GetLength(0); i++)
                    sFiles += " background" + ImagesCreation.imgResolutions[i, 0] + "x" + ImagesCreation.imgResolutions[i, 1] + ".jpg";
                sCmd = sCmd.Replace("@files@", sFiles);
                Uri path = new Uri(assembly.CodeBase);
                string sPath = System.IO.Path.GetDirectoryName(path.LocalPath);
                FileStream fs = File.Open(sPath + "\\enable background change.cmd", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sCmd);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("12" + ex.Message + "--------" + ex.InnerException + "--------" + ex.Data);
            }
        }
        /// <summary>
        /// Creates the "disable background change.cmd" file.
        /// It contains instructions to revoke the write right for all authenticated users from the "background*.jpg" files.
        /// This prevents users from altering the background*.jpg files without administrator privileges.
        /// </summary>
        public static void createCmdDisableFile()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream strm = assembly.GetManifestResourceStream("ScreensaverService.disable.cmd");
                StreamReader sr = new StreamReader(strm);
                string sCmd = sr.ReadToEnd();
                strm.Close();

                string sFiles = "backgroundDefault.jpg";
                for (int i = 0; i < ImagesCreation.imgResolutions.GetLength(0); i++)
                    sFiles += " background" + ImagesCreation.imgResolutions[i, 0] + "x" + ImagesCreation.imgResolutions[i, 1] + ".jpg";
                sCmd = sCmd.Replace("@files@", sFiles);
                Uri path = new Uri(assembly.CodeBase);
                string sPath = System.IO.Path.GetDirectoryName(path.LocalPath);
                FileStream fs = File.Open(sPath + "\\disable background change.cmd", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(sCmd);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("13" + ex.Message);
            }
        }
        /// <summary>
        /// Executes the "enable background change.cmd" file. Called from the installer, with the "setup" command line argument.
        /// </summary>
        public static void execCmdFile()
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                p.StartInfo.Arguments = "/C \"" + path + "\\enable background change\"";
                //p.StartInfo.Verb = "runas"; //shows the uac prompt
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //p.EnableRaisingEvents = true;
                //p.Exited += new EventHandler(p_Exited);

                p.Start();
            }
            catch (Exception ex)
            {
                Class.WriteErrorLog("14" + ex.Message);
            }
        }
    }
}
