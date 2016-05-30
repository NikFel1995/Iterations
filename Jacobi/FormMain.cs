using System;
using System.Windows.Forms;

namespace Jacobi
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        double[,] matrix = // double[size, size + 1]
        {
            {8, 4,  2,  10},
            {3, 5,  1,  5},
            {3, -2, 10, 4}
        
        };


        double[,] matrix1 = // double[size, size + 1]
        {
            {10, -2, -2},
            {-1, 10,-2 },
            {-1,-2,10}
        
        };


        private void btnResult_Click(object sender, EventArgs e)
        {
            ShowMatrix();

            txtResult.Clear();
            SolutionJacobi();
            SolutionZeidel();
            SolutionHighRelax();
        }


        private void ShowMatrix()
        {
            txtInput.Text += "Метод Якоби и Зейделя\r\n";
            PrintArray(matrix, txtInput);

            txtInput.Text += "Метод Релаксации\r\n";
            PrintArray(matrix1, txtInput);

        }


        private void SolutionJacobi()
        {

            txtResult.Text += "Методом Якобиана (простых итераций): " + "\r\n";

            int k = 0; //количество итераций


            // Считываем размер вводимой матрицы
            int size;
            // size = 3; // size = scanner.nextInt();


            // Будем хранить матрицу в векторе, состоящем из
            // векторов вещественных чисел
            double[,] matrix = // double[size, size + 1]
        {
            {8, 4,  2,  10},
            {3, 5,  1,  5},
            {3, -2, 10, 4}
        
        };
            size = matrix.GetLength(0);


            // Матрица будет иметь размер (size) x (size + 1),
            // c учетом столбца свободных членов        
            /*for (int i = 0; i < size; i++) {
                for (int j = 0; j < size + 1; j++) {
                   matrix[i,j] = scanner.nextDouble();
                }
            }
             * */






            // Считываем необходимую точность решения
            double eps;
            eps = 0.01d; //scanner.nextDouble();

            // Введем вектор значений неизвестных на предыдущей итерации,
            // размер которого равен числу строк в матрице, т.е. size,
            // причем согласно методу изначально заполняем его нулями
            double[] previousVariableValues = new double[size];
            for (int i = 0; i < size; i++)
            {
                previousVariableValues[i] = 0.0;
            }

            // Будем выполнять итерационный процесс до тех пор,
            // пока не будет достигнута необходимая точность
            while (true)
            {
                k++;

                // Введем вектор значений неизвестных на текущем шаге
                double[] currentVariableValues = new double[size];

                // Посчитаем значения неизвестных на текущей итерации
                // в соответствии с теоретическими формулами
                for (int i = 0; i < size; i++)
                {
                    // Инициализируем i-ую неизвестную значением
                    // свободного члена i-ой строки матрицы
                    currentVariableValues[i] = matrix[i, size];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < size; j++)
                    {
                        if (i != j)
                        {
                            currentVariableValues[i] -= matrix[i, j] * previousVariableValues[j];
                        }
                    }

                    // Делим на коэффициент при i-ой неизвестной
                    currentVariableValues[i] /= matrix[i, i];
                }

                // Посчитаем текущую погрешность относительно предыдущей итерации
                double error = 0.0;

                for (int i = 0; i < size; i++)
                {
                    error += Math.Abs(currentVariableValues[i] - previousVariableValues[i]);
                }

                // Если необходимая точность достигнута, то завершаем процесс
                if (error < eps)
                {
                    break;
                }

                // Переходим к следующей итерации, так
                // что текущие значения неизвестных
                // становятся значениями на предыдущей итерации
                previousVariableValues = currentVariableValues;

            }

            txtResult.Text += "Количество итераций: " + k + "\r\n";


            // Выводим найденные значения неизвестных
            for (int i = 0; i < size; i++)
            {
                txtResult.Text += previousVariableValues[i] + "\t";
            }



        }


        private void SolutionZeidel()
        {

            txtResult.Text += "\r\r\r\nМетодом Зейделя: " + "\r\n";

            int k = 0; //количество итераций


            // Считываем размер вводимой матрицы
            int size;
            // size = scanner.nextInt();

            // Будем хранить матрицу в векторе, состоящем из
            // векторов вещественных чисел
            double[,] matrix = // double[size, size + 1]
        {
            {8, 4,  2,  10},
            {3, 5,  1,  5},
            {3, -2, 10, 4}
        
        };
            size = matrix.GetLength(0);


            /*
        // Матрица будет иметь размер (size) x (size + 1),
        // c учетом столбца свободных членов        
        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size + 1; j++) {
                matrix[i][j] = scanner.nextDouble();
            }
        }
            */



            // Считываем необходимую точность решения
            double eps;
            eps = 0.01d;//scanner.nextDouble();

            // Введем вектор значений неизвестных на предыдущей итерации,
            // размер которого равен числу строк в матрице, т.е. size,
            // причем согласно методу изначально заполняем его нулями
            double[] previousVariableValues = new double[size];
            for (int i = 0; i < size; i++)
            {
                previousVariableValues[i] = 0.0;
            }

            // Будем выполнять итерационный процесс до тех пор,
            // пока не будет достигнута необходимая точность
            while (true)
            {

                k++;

                // Введем вектор значений неизвестных на текущем шаге
                double[] currentVariableValues = new double[size];

                // Посчитаем значения неизвестных на текущей итерации
                // в соответствии с теоретическими формулами
                for (int i = 0; i < size; i++)
                {
                    // Инициализируем i-ую неизвестную значением
                    // свободного члена i-ой строки матрицы
                    currentVariableValues[i] = matrix[i, size];

                    // Вычитаем сумму по всем отличным от i-ой неизвестным
                    for (int j = 0; j < size; j++)
                    {
                        // При j < i можем использовать уже посчитанные
                        // на этой итерации значения неизвестных
                        if (j < i)
                        {
                            currentVariableValues[i] -= matrix[i, j] * currentVariableValues[j];
                        }

                        // При j > i используем значения с прошлой итерации
                        if (j > i)
                        {
                            currentVariableValues[i] -= matrix[i, j] * previousVariableValues[j];
                        }
                    }

                    // Делим на коэффициент при i-ой неизвестной
                    currentVariableValues[i] /= matrix[i, i];
                }

                // Посчитаем текущую погрешность относительно предыдущей итерации
                double error = 0.0;

                for (int i = 0; i < size; i++)
                {
                    error += Math.Abs(currentVariableValues[i] - previousVariableValues[i]);
                }

                // Если необходимая точность достигнута, то завершаем процесс
                if (error < eps)
                {
                    break;
                }

                // Переходим к следующей итерации, так
                // что текущие значения неизвестных
                // становятся значениями на предыдущей итерации
                previousVariableValues = currentVariableValues;
            }

            txtResult.Text += "Количество итераций: " + k + "\r\n";


            // Выводим найденные значения неизвестных
            for (int i = 0; i < size; i++)
            {
                txtResult.Text += previousVariableValues[i] + "\t";
            }


        }






        private void SolutionHighRelax()
        {


            txtResult.Text += "\r\r\r\nМетодом релаксаций: " + "\r\n";

            int k = 0; //количество итераций
            double norma;


            double[,] matrix = // double[size, size + 1]
        {
            {10, -2, -2},
            {-1, 10,-2 },
            {-1,-2,10}
        
        };
            var size = matrix.GetLength(0);
            //matrix = A
            //size = n
            double[] x = new double[size];
            double[] xn = new double[size];


            double eps = 0.000001d; // точность

            double[] B = { 6, 7, 8 };

            double w = 1.1d; // параметр релаксации



            do
            {
                k++;
                norma = 0;

                for (int i = 0; i < size; i++)
                {
                    x[i] = B[i];
                    for (int j = 0; j < size; j++)
                    {
                        if (i != j)
                            x[i] = x[i] - matrix[i, j] * x[j];
                    }
                    x[i] /= matrix[i, i];

                    x[i] = w * x[i] + (1 - w) * xn[i];

                    if (Math.Abs(x[i] - xn[i]) > norma)
                        norma = Math.Abs(x[i] - xn[i]);
                    xn[i] = x[i];
                }
            }
            while (norma >= eps);

            txtResult.Text += "Количество итераций: " + k + "\r\n";


            for (int i = 0; i < size; i++)

                txtResult.Text += x[i] + "\t";





        }


        private void PrintArray(double[,] array, TextBox textBox)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                    textBox.Text += array[i, j] + "\t";
                textBox.Text += Environment.NewLine;
            }
        }
    }
}
