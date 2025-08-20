using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmxModx.Bridge.TestModule.Tests
{
    public static class ComparisonAnalyzer
    {
        private class TestResult
        {
            public string TestName { get; set; } = string.Empty;
            public string Result { get; set; } = string.Empty;
            public string Details { get; set; } = string.Empty;
            public string Source { get; set; } = string.Empty;
        }

        public static void AnalyzeResults()
        {
            string pawnResultsFile = "addons/amxmodx/logs/bridge_test_pawn_results.txt";
            string csharpResultsFile = "addons/amxmodx/logs/bridge_test_csharp_results.txt";
            string analysisFile = "addons/amxmodx/logs/bridge_test_analysis.txt";

            Console.WriteLine("[ComparisonAnalyzer] 开始分析测试结果...");

            try
            {
                var pawnResults = ParseResults(pawnResultsFile, "Pawn");
                var csharpResults = ParseResults(csharpResultsFile, "C#");

                GenerateAnalysis(pawnResults, csharpResults, analysisFile);
                
                Console.WriteLine("[ComparisonAnalyzer] 分析完成！结果已保存到 " + analysisFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[ComparisonAnalyzer] 分析失败: " + ex.Message);
            }
        }

        private static List<TestResult> ParseResults(string filePath, string source)
        {
            var results = new List<TestResult>();
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);

            if (!System.IO.File.Exists(fullPath))
            {
                Console.WriteLine($"[ComparisonAnalyzer] 文件不存在: {fullPath}");
                return results;
            }

            var lines = System.IO.File.ReadAllLines(fullPath);
            foreach (var line in lines)
            {
                var trimmedLine = line.Trim();
                if (string.IsNullOrEmpty(trimmedLine) || 
                    trimmedLine.StartsWith("=") || 
                    trimmedLine.StartsWith("测试总结:") ||
                    trimmedLine.StartsWith("Start Time:") ||
                    trimmedLine.StartsWith("总测试数:"))
                    continue;

                var match = ParseTestLine(trimmedLine);
                if (match != null)
                {
                    match.Source = source;
                    results.Add(match);
                }
            }

            return results;
        }

        private static TestResult ParseTestLine(string line)
        {
            try
            {
                // 格式: [测试名称] 结果: 详细信息
                int startBracket = line.IndexOf('[');
                int endBracket = line.IndexOf(']');
                
                if (startBracket == -1 || endBracket == -1 || endBracket <= startBracket)
                    return null;

                string testName = line.Substring(startBracket + 1, endBracket - startBracket - 1).Trim();
                
                string rest = line.Substring(endBracket + 1).Trim();
                
                string result = "UNKNOWN";
                string details = "";
                
                if (rest.StartsWith("PASS:", StringComparison.OrdinalIgnoreCase))
                {
                    result = "PASS";
                    details = rest.Substring(5).Trim();
                }
                else if (rest.StartsWith("FAIL:", StringComparison.OrdinalIgnoreCase))
                {
                    result = "FAIL";
                    details = rest.Substring(5).Trim();
                }
                else
                {
                    // 尝试其他格式
                    int colonIndex = rest.IndexOf(':');
                    if (colonIndex > 0)
                    {
                        result = rest.Substring(0, colonIndex).Trim();
                        details = rest.Substring(colonIndex + 1).Trim();
                    }
                }

                return new TestResult
                {
                    TestName = testName,
                    Result = result,
                    Details = details
                };
            }
            catch
            {
                return null;
            }
        }

        private static void GenerateAnalysis(List<TestResult> pawnResults, List<TestResult> csharpResults, string analysisFile)
        {
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), analysisFile);
            string logDir = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(logDir))
                Directory.CreateDirectory(logDir);

            using (var writer = new StreamWriter(fullPath, false))
            {
                writer.WriteLine("========================================");
                writer.WriteLine("桥接接口测试结果对比分析");
                writer.WriteLine("========================================");
                writer.WriteLine($"分析时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine();

                // 统计信息
                writer.WriteLine("=== 统计信息 ===");
                writer.WriteLine($"Pawn测试结果数: {pawnResults.Count}");
                writer.WriteLine($"C#测试结果数: {csharpResults.Count}");
                writer.WriteLine();

                // 匹配分析
                var matchedTests = new List<(TestResult Pawn, TestResult CSharp)>();
                var unmatchedPawn = new List<TestResult>();
                var unmatchedCSharp = new List<TestResult>();

                foreach (var pawn in pawnResults)
                {
                    var csharp = csharpResults.FirstOrDefault(c => c.TestName == pawn.TestName);
                    if (csharp != null)
                        matchedTests.Add((pawn, csharp));
                    else
                        unmatchedPawn.Add(pawn);
                }

                unmatchedCSharp = csharpResults.Where(c => !pawnResults.Any(p => p.TestName == c.TestName)).ToList();

                writer.WriteLine("=== 匹配测试结果 ===");
                writer.WriteLine($"匹配测试数: {matchedTests.Count}");
                writer.WriteLine();

                int consistentResults = 0;
                int inconsistentResults = 0;

                foreach (var match in matchedTests)
                {
                    bool consistent = match.Pawn.Result == match.CSharp.Result && 
                                    match.Pawn.Details == match.CSharp.Details;
                    
                    writer.WriteLine($"测试: {match.Pawn.TestName}");
                    writer.WriteLine($"  Pawn结果: {match.Pawn.Result} - {match.Pawn.Details}");
                    writer.WriteLine($"  C#结果: {match.CSharp.Result} - {match.CSharp.Details}");
                    writer.WriteLine($"  一致性: {(consistent ? "一致" : "不一致")}");
                    writer.WriteLine();

                    if (consistent)
                        consistentResults++;
                    else
                        inconsistentResults++;
                }

                // 未匹配测试
                if (unmatchedPawn.Count > 0)
                {
                    writer.WriteLine("=== 仅在Pawn中的测试 ===");
                    foreach (var test in unmatchedPawn)
                    {
                        writer.WriteLine($"  {test.TestName}: {test.Result} - {test.Details}");
                    }
                    writer.WriteLine();
                }

                if (unmatchedCSharp.Count > 0)
                {
                    writer.WriteLine("=== 仅在C#中的测试 ===");
                    foreach (var test in unmatchedCSharp)
                    {
                        writer.WriteLine($"  {test.TestName}: {test.Result} - {test.Details}");
                    }
                    writer.WriteLine();
                }

                // 总结
                writer.WriteLine("=== 对比总结 ===");
                writer.WriteLine($"总匹配测试: {matchedTests.Count}");
                writer.WriteLine($"一致结果: {consistentResults}");
                writer.WriteLine($"不一致结果: {inconsistentResults}");
                writer.WriteLine($"一致性率: {(matchedTests.Count > 0 ? (float)consistentResults / matchedTests.Count * 100 : 0):F1}%");
                writer.WriteLine();

                // 建议
                writer.WriteLine("=== 建议 ===");
                if (inconsistentResults > 0)
                {
                    writer.WriteLine("发现不一致的测试结果，建议：");
                    writer.WriteLine("1. 检查cpp桥接接口实现");
                    writer.WriteLine("2. 验证P/Invoke定义是否正确");
                    writer.WriteLine("3. 检查参数传递和返回值处理");
                }
                else
                {
                    writer.WriteLine("所有匹配测试结果一致，cpp桥接接口验证通过！");
                }

                if (unmatchedPawn.Count > 0 || unmatchedCSharp.Count > 0)
                {
                    writer.WriteLine("4. 同步测试用例，确保两边测试覆盖相同接口");
                }

                writer.WriteLine("========================================");
            }

            Console.WriteLine("[ComparisonAnalyzer] 分析文件已生成: " + fullPath);
        }

        public static void PrintResults()
        {
            string analysisFile = "addons/amxmodx/logs/bridge_test_analysis.txt";
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), analysisFile);

            if (System.IO.File.Exists(fullPath))
            {
                Console.WriteLine("[ComparisonAnalyzer] 分析结果:");
                Console.WriteLine(System.IO.File.ReadAllText(fullPath));
            }
            else
            {
                Console.WriteLine("[ComparisonAnalyzer] 分析文件不存在，请先运行分析");
            }
        }
    }
}