using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace cSharpPassAttack
{
    public class Permutation
    {
        public static string _saltFound { get; set; } = string.Empty;
        public static List<List<char>> PermutationWithRepetition(int n, int r, char[] elems, int maxThreads, string encryptedPass, string expectedPassword)
        {
            if (r == 0)
            {
                return new List<List<char>> { new List<char>() };
            }

            var permutations = new ConcurrentBag<List<char>>();
            var options = new ParallelOptions { MaxDegreeOfParallelism = maxThreads };
            var cts = new CancellationTokenSource();

            options.CancellationToken = cts.Token;


            try
            {
                Parallel.For(0, n, options, i =>
                {
                    var subPermutations = PermutationWithRepetition(n, r - 1, elems, maxThreads, encryptedPass, expectedPassword);
                    foreach (var subPermutation in subPermutations)
                    {

                        var newPermutation = new List<char>(subPermutation);
                        newPermutation.Insert(0, elems[i]);
                        permutations.Add(newPermutation);

                        string checker = string.Join("", newPermutation);

                        if (checker.Length == 3)
                        {
                            string decrypted = AESEncryption.Decrypt(encryptedPass, checker);
                            if (decrypted == expectedPassword)
                            {
                                _saltFound = checker;
                                cts.Cancel();
                                return;
                            }
                        }

                    }
                });
            }
            catch (OperationCanceledException)
            {
                // Console.WriteLine(cancellationReason);
            }
            return new List<List<char>>(permutations);
        }
    }

}
