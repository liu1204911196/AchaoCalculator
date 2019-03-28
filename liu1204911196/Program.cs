using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp3
{

    class Operation
    {
        public int getNum(int[] arrNum, int tmp, int minNum, int maxNum, Random ra)
        {
            int n = 0;
            while (n <= arrNum.Length - 1)
            {
                if (arrNum[n] == tmp) //利用循环判断是否有重复
                {
                    tmp = ra.Next(minNum, maxNum); //重新随机获取。
                    getNum(arrNum, tmp, minNum, maxNum, ra);//递归:如果取出来的数字和已取得的数字有重复就重新随机获取。
                }
                n++;
            }
            return tmp;
        }
        public int[] getRandomNum(int num, int minNum, int maxNum)
        {

            Random ra = new Random(unchecked((int)DateTime.Now.Ticks));
            int[] arrNum = new int[num];
            int tmp = 0;
            for (int i = 0; i <= num - 1; i++)
            {
                tmp = ra.Next(minNum, maxNum); //随机取数
                arrNum[i] = getNum(arrNum, tmp, minNum, maxNum, ra); //取出值赋到数组中
            }
            return arrNum;
        }
        public void Two_op()
        {
            int p, t;
            int[] arr = getRandomNum(5, 1, 100); //从1至100中取出5个互不相同的随机数,其中4，5决定操作符
            int[] b = new int[1];//存储每次操作符后的结果
            char[] c = new char[2];//存储确定的操作符
            string d1;  //存储算式及结果，后续输入到文件中
            p = arr[3] % 3;
            switch (p)
            {
                case 0:
                    b[0] = arr[0] + arr[1];
                    c[0] = '+';
                    break;
                case 1:
                    if (arr[0] < arr[1])//排除负数结果，用大数减去小数
                    {
                        t = arr[0];
                        arr[0] = arr[1];
                        arr[1] = arr[0];
                    }
                    b[0] = arr[0] - arr[1];
                    c[0] = '-';
                    break;
                case 2:
                    if (arr[0] % arr[1] == 0)//判断m是否可以整除n,可以就打印除法，否则做乘法
                    {
                        b[0] = arr[0] / arr[1];
                        c[0] = '/';
                    }
                    else
                    {
                        b[0] = arr[0] * arr[1];
                        c[0] = '*';
                    }
                    break;
                default:
                    break;
            }
            p = arr[4] % 2;//第二个操作符
            switch (p)
            {
                case 0:
                    c[1] = '+';
                    b[0] += arr[2];
                    break;
                case 1:
                    if (b[0] < arr[2])//排除负数结果
                    {
                        c[1] = '+';
                        b[0] += arr[2];
                    }
                    else
                    {
                        c[1] = '-';
                        b[0] -= arr[2];
                    }
                    break;
                default:
                    break;
            }
            Console.WriteLine("{0}{1}{2}{3}{4}=", arr[0], c[0], arr[1], c[1], arr[2]);//打印算式
            d1 = Convert.ToString(arr[0]) + Convert.ToString(c[0]) + Convert.ToString(arr[1]) + Convert.ToString(c[1]) + Convert.ToString(arr[2]) + "=" + Convert.ToString(b[0]);
            PutFile(d1);

        }
        public void PutFile(string x)//将算式打印到文件中
        {
            string path = @"F:\zy.txt";
            FileInfo fileInfo = new FileInfo(path);
            StreamWriter sw = fileInfo.AppendText();
            sw.WriteLine(x);
            sw.Close();
        }
        public void Third_op()
        {
            string d1;
            int n, i, a, b;
            float e;
            string[] c = new string[] { "+", "-", "*", "/" };
            string d;
            n = Convert.ToInt16(Console.ReadLine());
            for (i = 0; i < n; i++)
            {
                while (true)
                {
                    Random r = new Random(int.Parse(DateTime.Now.ToString("HHmmssfff")) + i);
                    b = r.Next(0, 100);
                    a = r.Next(0, 100);
                    d = c[r.Next(1, 4)];
                    if (a >= b && b != 0 && a % b == 0)
                    {
                        switch (d)
                        {
                            case "+":
                                e = a + b;
                                Console.WriteLine(a + "+" + b + "=" + e);
                                break;
                            case "-":
                                e = a - b;
                                Console.WriteLine(a + "-" + b + "=" + e);
                                break;
                            case "*":
                                e = a * b;
                                Console.WriteLine(a + "*" + b + "=" + e);
                                break;
                            case "/":
                                e = a / b; ;
                                Console.WriteLine(a + "/" + b + "=" + e);
                                break;
                        }
                    }
                    


                }
            }
        }
    }

    class Program
    {
       
        static void Main(string[] args)
        {
            Operation A = new Operation();//新建生成算式对象
            int n, x, y, i, j;
            Console.WriteLine("请输入您想生成的四则运算题目个数：");
            n = Convert.ToInt32(Console.ReadLine());//题目个数
            x = n / 2;//生成的含有2个运算符的题目个数
            y = n - x;//生成的含有3个运算符的题目个数
            for (i = 0; i < x; i++)
            {
                A.Two_op();//生成x个含有2个运算符的算式
            }
            for (j = 0; j < y; j++)
            {
                A.Third_op();//生成n-x个含有2个运算符的算式
            }
        }

    }
}
