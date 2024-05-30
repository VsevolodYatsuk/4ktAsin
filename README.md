# Matrix Multiplication Pipeline

## Структура проекта

Проект состоит из следующих файлов:
- `Matrix.cs`: Класс для представления матриц и методов их умножения.
- `Program.cs`: Основной файл программы, содержащий метод `Main`, который инициализирует матрицы, выполняет их умножение и выводит результат.

## Пример использования

В `Program.cs` инициализируются две матрицы `A` и `B`, и выполняется их умножение с использованием конвейерного алгоритма:

```csharp
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Инициализация матриц A и B
        Matrix a = new Matrix(2, 3);
        Matrix b = new Matrix(3, 2);

        // Заполнение матриц данными для примера
        a.Data[0, 0] = 1; a.Data[0, 1] = 2; a.Data[0, 2] = 3;
        a.Data[1, 0] = 4; a.Data[1, 1] = 5; a.Data[1, 2] = 6;

        b.Data[0, 0] = 7; b.Data[0, 1] = 8;
        b.Data[1, 0] = 9; b.Data[1, 1] = 10;
        b.Data[2, 0] = 11; b.Data[2, 1] = 12;

        // Асинхронное умножение с использованием конвейера
        Matrix result = await Matrix.MultiplyPipelineAsync(a, b);

        // Вывод результата
        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Cols; j++)
            {
                Console.Write(result.Data[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
```
## Matrix

```csharp
public class Matrix
{
    public int[,] Data { get; }
    public int Rows => Data.GetLength(0);
    public int Cols => Data.GetLength(1);

    public Matrix(int rows, int cols)
    {
        Data = new int[rows, cols];
    }

    public static Matrix Multiply(Matrix a, Matrix b)
    {
        if (a.Cols != b.Rows)
            throw new ArgumentException("Matrix dimensions are not suitable for multiplication.");

        var result = new Matrix(a.Rows, b.Cols);
        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Cols; j++)
            {
                for (int k = 0; k < a.Cols; k++)
                {
                    result.Data[i, j] += a.Data[i, k] * b.Data[k, j];
                }
            }
        }
        return result;
    }

    public static async Task<Matrix> MultiplyAsync(Matrix a, Matrix b)
    {
        return await Task.Run(() => Multiply(a, b));
    }

    public static async Task<Matrix> MultiplyPipelineAsync(Matrix a, Matrix b)
    {
        var prepareTask = Task.Run(() =>
        {
            // Подготовка данных для умножения (если необходимо)
            return (a, b);
        });

        var computeTask = prepareTask.ContinueWith(prepTask =>
        {
            // Умножение матриц
            var (matrixA, matrixB) = prepTask.Result;
            return Multiply(matrixA, matrixB);
        });

        return await computeTask;
    }
}
```

##Вывод:

![image](https://github.com/VsevolodYatsuk/4ktAsin/assets/130091517/cb4bd64e-a6a7-4c27-b2ea-37b164b2f94e)
