﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW_Equation
{
    public class LinearEquation
    {
        List<float> coefficients;
        public int Size => coefficients.Count;

        /// <summary>
        /// Конструирует уравнение вида aN*x + coefficients[0]y + ... + coefficients[N-2]z + coefficients[N-1] = 0
        /// </summary>
        /// 
        /// Примеры:
        /// <example>
        /// LinearEquation(1,2,3,4) => 1x + 2y + 3z + 4 = 0
        /// LinearEquation(1,2) => 1x + 2 = 0
        /// LinearEquation(1) => 1 = 0 (не имеет решений)
        /// </example>
        /// 
        /// <param name="aN">Последний коэффициент</param>
        /// <param name="coefficients">Остальные коэффициенты</param>

        public LinearEquation(float aN, params float[] coefficients)
        {
            this.coefficients = new List<float>(coefficients); // Исправление: Инициализируем список coefficients
            this.coefficients.Insert(0, aN); // Исправление: Вставляем aN в начало списка с помощью метода
        }

        public LinearEquation(params float[] coefficients)
        {
            this.coefficients = new List<float>(coefficients); // Исправление: Инициализируем список coefficients
        }

        public LinearEquation(List<float> coefficients)
        {
            this.coefficients = new List<float>(coefficients); // Исправлено
        }

        /// <summary>
        /// Суммирует свободный член first с second
        /// </summary>
        static public LinearEquation operator +(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.coefficients.Count - 1] += second; // Исправлено
            return equation;
        }

        /// <summary>
        /// Вычитает second из свободного члена first
        /// </summary>
        static public LinearEquation operator -(LinearEquation first, float second)
        {
            LinearEquation equation = first;
            equation.coefficients[equation.coefficients.Count - 1] -= second; // Исправлено
            return equation;
        }

        public override bool Equals(object obj)
        {
            if (obj is LinearEquation equation)
            {
                if (Size != equation.Size)
                    return false; // Исправление: Возвращаем false, если размеры уравнений не совпадают.
                for (int i = 0; i < Size; i++)
                {
                    if (coefficients[i] != equation.coefficients[i])
                        return false; // Исправление: Возвращаем false, если найдено несовпадение коэффициентов.
                }
                return true; // Исправление: Возвращаем true, если все коэффициенты совпадают.
            }
            return false; // Исправление: Возвращаем false, если объект не является экземпляром LinearEquation.
        }

        static public bool operator ==(LinearEquation first, LinearEquation second)
        {
            return first.Equals(second);
        }

        static public bool operator !=(LinearEquation first, LinearEquation second)
        {
            return !first.Equals(second);
        }

        public float this[int i]
        {
            get { return coefficients[i]; }
        }

        public static LinearEquation operator +(LinearEquation equation1, LinearEquation equation2)
        {
            if (equation1.coefficients.Count != equation2.coefficients.Count)
                throw new ArgumentException("Equations should have the same number of coefficients.");

            int n = equation1.coefficients.Count;
            float[] resultCoefficients = new float[n];

            for (int i = 0; i < n; i++)
            {
                resultCoefficients[i] = equation1.coefficients[i] + equation2.coefficients[i];
            }

            return new LinearEquation(resultCoefficients);
        }

        public static LinearEquation operator -(LinearEquation equation1, LinearEquation equation2)
        {
            if (equation1.coefficients.Count != equation2.coefficients.Count)
                throw new ArgumentException("Equations should have the same number of coefficients.");

            int n = equation1.coefficients.Count;
            float[] resultCoefficients = new float[n];

            for (int i = 0; i < n; i++)
            {
                resultCoefficients[i] = equation1.coefficients[i] - equation2.coefficients[i];
            }

            return new LinearEquation(resultCoefficients);
        }

        public bool HasSolution()
        {
            if (coefficients.Count == 2)
            {
                float a = coefficients[0];
                float b = coefficients[1];

                return a != 0;
            }

            // Если количество неизвестных больше или меньше 1, считаем, что решения нет
            return false;
        }

        public float Solve()
        {
            if (coefficients.Count == 2)
            {
                if (coefficients[0] == 0)
                    throw new InvalidOperationException("The equation has no solution.");

                return -coefficients[1] / coefficients[0];
            }

            throw new InvalidOperationException("Solving the equation is not supported for equations with more than one unknown.");
        }

        public override string ToString()
        {
            int n = coefficients.Count - 1;
            string equationString = "";

            for (int i = 0; i < n; i++)
            {
                equationString += $"{coefficients[i]}x{i + 1} + ";
            }

            equationString += $"{coefficients[n]} = 0";

            return equationString;
        }

        public void InitializeRandom()
        {
            Random random = new Random();

            for (int i = 0; i < coefficients.Count - 1; i++)
            {
                coefficients[i] = random.Next(-10, 10);
            }

            coefficients[coefficients.Count - 1] = random.Next(-10, 10);
        }

        public void InitializeWithSameValue(float value)
        {
            for (int i = 0; i < coefficients.Count; i++)
            {
                coefficients[i] = value;
            }
        }

        public static LinearEquation operator *(float scalar, LinearEquation equation)
        {
            int n = equation.coefficients.Count;
            float[] resultCoefficients = new float[n];

            for (int i = 0; i < n; i++)
            {
                resultCoefficients[i] = scalar * equation.coefficients[i];
            }

            return new LinearEquation(resultCoefficients);
        }
    }
}
