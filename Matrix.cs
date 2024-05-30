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
            return (a, b);
        });

        var computeTask = prepareTask.ContinueWith(prepTask =>
        {
            var (matrixA, matrixB) = prepTask.Result;
            return Multiply(matrixA, matrixB);
        });

        return await computeTask;
    }
}