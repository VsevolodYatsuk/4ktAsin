using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Matrix a = new Matrix(2, 3);
        Matrix b = new Matrix(3, 2);

        a.Data[0, 0] = 1; a.Data[0, 1] = 2; a.Data[0, 2] = 3;
        a.Data[1, 0] = 4; a.Data[1, 1] = 5; a.Data[1, 2] = 6;

        b.Data[0, 0] = 7; b.Data[0, 1] = 8;
        b.Data[1, 0] = 9; b.Data[1, 1] = 10;
        b.Data[2, 0] = 11; b.Data[2, 1] = 12;

        Matrix result = await Matrix.MultiplyPipelineAsync(a, b);

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
