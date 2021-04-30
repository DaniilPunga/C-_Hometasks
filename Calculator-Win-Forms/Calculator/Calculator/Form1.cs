using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        float a, b;
        int count;
        bool znak = true;

        private void button17_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 0;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ",";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 2;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 4;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 5;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 6;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 7;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 8;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + 9;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(znak==true)
            {
                textBox1.Text = "-" + textBox1.Text;
                znak = false;
            }
            else if (znak==false)
            {
                textBox1.Text=textBox1.Text.Replace("-", "");
                znak = true;
            }
            
        }
        /*
        private void calculate(string equation) {
            int begin_brake=-1;
            int end_brake=-1;
         for ( int i =0;i< equation.Length; i++)
            {
                if (equation[i] == '(')
                {
                    long first_brake = 1;
                    long second_brake = 0;
                    int j = i;
                    while (second_brake!= first_brake)
                    {
                        if (equation[j] == '(')
                        {
                            first_brake ++;
                        }
                        if (equation[j] == ')')
                        {
                            second_brake++;
                        }

                        j++;
                    }
                }
            }

        }*/

        public static double Result(string expr)
        {
            var postfix = ToPostfixForm(ParseStr(expr));
            return FromPostfixForm(postfix);
        }

        private static List<string> ParseStr(string expr)
        {
            var list = new List<string>();
            string buffer = string.Empty;
            foreach (var element in expr)
            {
                if (element >= '0' && element <= '9' || element == ',')
                {
                    buffer += element;
                }
                else if (IsOperator(element.ToString()) || element.ToString() == ")" || element.ToString() == "(")
                {
                    if (buffer != string.Empty)
                    {
                        list.Add(buffer);
                        buffer = string.Empty;
                    }

                    list.Add(element.ToString());
                }
            }
            if (buffer != string.Empty)
            {
                list.Add(buffer);
            }
            return list;
        }

        // private static bool IsOperator(string ch) => ch == "+" || ch == "*" || ch == "/" || ch == "-";
        private static bool IsOperator(string ch)
        {
            if(ch == "+" || ch == "*" || ch == "/" || ch == "-")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static string ToPostfixForm(List<string> expr)
        {
            var stack = new Stack<string>();
            expr.Insert(0, "(");
            expr.Add(")");
            string postfix = string.Empty;

            for (var i = 0; i < expr.Count;)
            {
                if (expr[i] == "(")
                {
                    stack.Push("(");
                    ++i;
                }
                else if (expr[i] == "*" || expr[i] == "/")
                {
                    if (stack.Peek() == "*" || stack.Peek() == "/")
                    {
                        postfix += stack.Pop() + " ";
                    }
                    else
                    {
                        stack.Push(expr[i]);
                        ++i;
                    }
                }
                else if (expr[i] == "+" || expr[i] == "-")
                {
                    if (stack.Count == 0 || stack.Peek() == "(")
                    {
                        stack.Push(expr[i]);
                        ++i;
                    }
                    else
                    {
                        postfix += stack.Pop() + " ";
                    }
                }
                else if (expr[i] == ")")
                {
                    if (stack.Peek() == "(")
                    {
                        stack.Pop();
                        ++i;
                    }
                    else
                    {
                        postfix += stack.Pop() + " ";
                    }
                }
                else if (double.TryParse(expr[i], out double _))
                {
                    postfix += expr[i] + " ";
                    ++i;
                }
            }

            if (stack.Count != 0)
            {
                throw new ArgumentException("Не удалось перевести выражение в постфиксную форму");
            }

            return postfix;
        }

        private static double FromPostfixForm(string expression)
        {
            var stack = new Stack<double>();
            string[] lits = expression.Split(' ');
            foreach (var lit in lits)
            {
                if (double.TryParse(lit, out double result))
                {
                    stack.Push(result);
                }
                else if (IsOperator(lit))
                {
                    double secondNumber = stack.Pop();
                    double firstNumber = stack.Pop();
                    stack.Push(OperationsFinal(firstNumber, secondNumber, lit));
                }
            }

            var buffer = stack.Pop();
            if (stack.Count != 0)
            {
                throw new ArgumentException("Невозможно вычислить, проверьте правильность записи");
            }

            return buffer;
        }

        private static double OperationsFinal(double firstNum, double secondNum, string operation)
        {
            switch (operation)
            {
                case "+":
                    return (firstNum + secondNum);
                case "-":
                    return firstNum - secondNum;
                case "*":
                    return firstNum * secondNum;
                case "/":
                    if (secondNum == 0)
                    {
                        throw new DivideByZeroException();
                    }
                    return firstNum / secondNum;
            }
            return 0;
        }
        private void calculate()
        {
            //string[] subs=textBox1.Text
            switch (count)
            {
                case 1:
                    b = a + float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;

                case 2:
                    b = a - float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case 3:
                    b = a * float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;
                case 4:
                    b = a / float.Parse(textBox1.Text);
                    textBox1.Text = b.ToString();
                    break;

                default:
                    break;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "+";
             znak = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "-";
            znak = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "*";
            znak = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "/";
            znak = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string expr = textBox1.Text;
            string expr2=expr ;
            for (int i = 0; i < expr.Length; i++)
            {
                if (expr[i] == '-')
                {
                    if (i == 0)
                        {
                        expr = "0" + expr;
                        }
                    else
                    {
                        if (expr[i - 1] == '(' )
                        { 
                            expr = expr.Insert(i , "0");
                            i++;
                        }

                    }
                }
            }
            Console.WriteLine(expr);
            label1.Text =  Result(expr).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text+"(";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + ")";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int lenght = textBox1.Text.Length - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
            {
                textBox1.Text = textBox1.Text + text[i];
            }
        }


        
    }
}
