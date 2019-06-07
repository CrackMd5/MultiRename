某种情况下，例如我在做[https://www.cmd5.com](https://www.cmd5.com)的支付系统的时候，需要处理大量的二维码图片，按照金额给文件命名，于是我 写了这个程序。

主要用到Tesseract-OCR这个控件。有时候识别不出，或者出现其它错误，程序都会提示。这些个别文件可以人工处理了。

关键代码如下：
~~~csharp
				Bitmap bmp = new Bitmap(f);
                TesseractProcessor process = new TesseractProcessor();
                process.SetPageSegMode(ePageSegMode.PSM_SINGLE_LINE);
                process.Init(System.Environment.CurrentDirectory + "\\", "chi_sim", (int)eOcrEngineMode.OEM_DEFAULT);
                try
                {
                    string result = process.Recognize(bmp);
                    Match m = reg.Match(result);
                    if (m.Success)
                    {
                        string amount = m.ToString();
                        File.Copy(f, d1 + amount + Path.GetExtension(f), true);
                    }
                    else
                    {
                        File.Copy(f, d1 + Path.GetFileName(f), true);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("识别文件出错:" + f);
                    File.Copy(f, d1 + Path.GetFileName(f), true);
                }
                bmp.Dispose();
~~~

完整源代码地址：https://github.com/CrackMd5/MultiRename
转载请注明来源官网:https://www.cmd5.org

稍后我将发布批量改名+批量裁剪可执行程序，并且是免费的，敬请关注。
